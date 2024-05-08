namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMDailyReport
    {
        public int idDailyReport { get; set; }
        public int? idAsset { get; set; }
        public string? nombreAsset { get; set; }
        public int? numberReport { get; set; }
        public int? final { get; set; }
        public string? closingOfDay { get; set; }
        public DateTime? registerDate { get; set; }
    }
}

