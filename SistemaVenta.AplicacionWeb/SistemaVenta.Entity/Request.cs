using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Request
    {
        public object requestNumber;

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public Request()
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        {
            RequestDetail = new HashSet<RequestDetail>();
        }

        public int idRequest { get; set; }
        public string? numberRequest { get; set; }
        public int? idAssetRequestType{ get; set; }
        public int? idUsuario { get; set; }
        public string? description { get; set; }
        public string? odometer { get; set; }
        public int? idPriority { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual AssetRequestType? IdAssetRequestTypeNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual Priority? IdPriorityNavigation { get; set; }
        public virtual ICollection<RequestDetail> RequestDetail { get; set; }
    }
}
