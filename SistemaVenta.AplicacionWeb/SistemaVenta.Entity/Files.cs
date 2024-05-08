using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity
{
    public partial class Files
    {

        public int idFiles { get; set; }
        public int? idAsset { get; set; }
        public int? idFileTypes { get; set; }
        public byte[]? filex { get; set; }
        public string? extension { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual Asset? IdAssetNavigation { get; set; }
        public virtual FileTypes? IdFileTypesNavigation { get; set; }
    }
}
