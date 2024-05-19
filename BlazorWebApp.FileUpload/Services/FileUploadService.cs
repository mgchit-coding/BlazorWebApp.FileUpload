using BlazorWebApp.FileUpload.Model;
using ClosedXML.Excel;
using DotNet8.DbService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace BlazorWebApp.FileUpload.Services;

public class FileUploadService
{
    private readonly CustomSettingModel _setting;
    private readonly ILogger<FileUploadService> _logger;
    private readonly AppDbContext _db;

    public FileUploadService(
        IOptionsMonitor<CustomSettingModel> setting,
        ILogger<FileUploadService> logger,
        AppDbContext db)
    {
        _setting = setting.CurrentValue;
        _logger = logger;
        _db = db;
    }

    public async Task<(string, string)> WriteFileToStorageAsync(string fileName, string base64Str)
    {
        try
        {
            var path = Path.Combine(_setting.wwwrootFolder, _setting.ExcelFolder);
            Directory.CreateDirectory(path);
            var completePath = $"{path}\\{DateTime.Now.ToString("yyyyMMddTHHmmss") + fileName}";
            await File.WriteAllBytesAsync(completePath, Convert.FromBase64String(base64Str));
            var excelJsonData = ConvertExcelToDataTable(completePath);
            return (excelJsonData, completePath);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            throw;
        }
    }

    public void DeleteFile(string filePath)
    {
        //var path = GetFolderPath();
        File.Delete(filePath);
    }

    public string GetFolderPath()
    {
        var path = Path.Combine(_setting.wwwrootFolder, _setting.ExcelFolder);
        return path;
    }

    public async Task SaveToDb(MeterBillViewModel requestModel)
    {
        try
        {
            var meterBills = await _db.TblMeterBills.AsNoTracking().ToListAsync();
            if (meterBills.Count > 0)
            {
                await MoveToBackupTable(meterBills);
                foreach (var entry in meterBills)
                {
                    _db.Entry(entry).State = EntityState.Deleted;
                    _db.TblMeterBills.Remove(entry);
                }
                await _db.SaveChangesAsync();
            }
            var model = new TblMeterBill
            {
                MeterBillCode = Guid.NewGuid().ToString(),
                MeterBillFilePath = requestModel.MeterBillFilePath,
                MeterBillFileData = requestModel.MeterBillFileData,
                CreatedUserId = requestModel.CreatedUserId,
                CreatedDateTime = DateTime.Now,
            };
            await _db.TblMeterBills.AddAsync(model);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
        }
    }
    private async Task MoveToBackupTable(List<TblMeterBill> meterBills)
    {
        var dataLst = meterBills.Select(x=> new TblBackupMeterBill
        {
            MeterBillCode = x.MeterBillCode,
            MeterBillFileData=x.MeterBillFileData,
            MeterBillFilePath=x.MeterBillFilePath,
            CreatedUserId=x.CreatedUserId,
            CreatedDateTime=x.CreatedDateTime,
            ModifiedDateTime=x.ModifiedDateTime,
            ModifiedUserId =x.ModifiedUserId,
        }).ToList();
        await _db.TblBackupMeterBills.AddRangeAsync(dataLst);
        await _db.SaveChangesAsync();
    }
    public string ConvertExcelToDataTable(string filePath)
    {
        var stopwatch = Stopwatch.StartNew();
        DataTable dataTable = new DataTable();
        try
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                // Access the first worksheet by name or index
                var worksheet = workbook.Worksheet(1); // or workbook.Worksheet("Sheet1");

                // Assuming the first row contains column names
                bool firstRow = true;

                foreach (var row in worksheet.RowsUsed())
                {
                    // If it's the first row, add columns to the DataTable
                    if (firstRow)
                    {
                        foreach (var cell in row.CellsUsed())
                        {
                            dataTable.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        // Add rows to the DataTable
                        DataRow dataRow = dataTable.NewRow();
                        int cellIndex = 0;
                        foreach (var cell in row.CellsUsed())
                        {
                            dataRow[cellIndex] = cell.Value;
                            cellIndex++;
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            dataTable = new DataTable();
        }
        var processTime =
            $"{stopwatch.ElapsedMilliseconds} ms ({TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalSeconds} seconds)";
        _logger.LogInformation(processTime);
        return JsonConvert.SerializeObject(dataTable);
    }
}
