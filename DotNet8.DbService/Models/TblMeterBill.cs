using System;
using System.Collections.Generic;

namespace DotNet8.DbService.Models;

public partial class TblMeterBill
{
    public int MeterBillId { get; set; }

    public string MeterBillCode { get; set; } = null!;

    public string MeterBillFilePath { get; set; } = null!;

    public string? MeterBillFileData { get; set; } 

    public string CreatedUserId { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string? ModifiedUserId { get; set; }

    public DateTime? ModifiedDateTime { get; set; }
}
