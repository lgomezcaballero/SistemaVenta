namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMUser
    {
        public int idUser { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public int? phone { get; set; }
        public int? IdRol { get; set; }
        public string? NombreRol { get; set; }
        public string? pictureUrl { get; set; }
        public string? pictureName { get; set; }
        public string? password { get; set; }
        public int? active { get; set; }

    }
}
