using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class AssetRequestType
    {
        public AssetRequestType()
        {
            Request = new HashSet<Request>();
        }

        public int idAssetRequestType { get; set; }
        public string? description { get; set; }
        public bool? active{ get; set; }
        public DateTime? registerDate { get; set; }

        public virtual ICollection<Request> Request { get; set; }

    }
}
