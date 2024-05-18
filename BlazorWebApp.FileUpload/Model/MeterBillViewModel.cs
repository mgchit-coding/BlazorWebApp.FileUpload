namespace BlazorWebApp.FileUpload.Model
{
    public class MeterBillViewModel
    {
        public string MeterBillCode { get; set; }
        public string MeterBillFilePath { get; set; }

        public string MeterBillFileData { get; set; } 

        public string CreatedUserId { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
