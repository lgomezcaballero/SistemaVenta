using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class RequestDetail
    {
        public int idRequestDetail { get; set; }
        public int? idRequest { get; set; }
        public int? idAsset { get; set; }
        public string? makerAsset { get; set; }
        public string? modelAsset { get; set; }
        public string? assetType { get; set; }
        public string? assetInternal { get; set; }
        public string? fuelAsset { get; set; }
        public string? locationAsset { get; set; }
        public string? userAsset { get; set; }
        public string? managerAsset { get; set; }
        public int? yearAsset { get; set; }
        public string? licencePlateAsset { get; set; }





        public virtual Request? IdRequestNavigation { get; set; }
        public virtual Asset? IdAssetNavigation { get; set; }

    }
}

