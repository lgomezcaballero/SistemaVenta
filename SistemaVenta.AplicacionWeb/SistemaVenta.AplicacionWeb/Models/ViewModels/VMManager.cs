
namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMManager
    {
        public int idManager { get; set; }
        public string? name { get; set; }
        public string? workFile { get; set; }
        public string? description { get; set; }
        public string? licence { get; set; }
        public DateTime? licenceExpiration { get; set; }
        public DateTime? birthday { get; set; }
        public int? phone { get; set; }
        public int? emergencyContact { get; set; }
        public string? email { get; set; }
        public string? pictureUrl { get; set; }
        public int? active { get; set; }

    }
}

