namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMRequest
    {
        public int idRequest { get; set; }
        public string? numberRequest { get; set; }
        public int? idAssetRequestType { get; set; }
        public string? AssetRequestType { get; set; }
        public int? idUsuario { get; set; }
        public string? Usuario { get; set; }
        public string? description { get; set; }
        public string? odometer { get; set; }
        public int? idPriority { get; set; }
        public string? Priority { get; set; }
        public string? registerDate { get; set; }


#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public virtual ICollection<VMRequestDetail> RequestDetail { get; set; }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    }
}
