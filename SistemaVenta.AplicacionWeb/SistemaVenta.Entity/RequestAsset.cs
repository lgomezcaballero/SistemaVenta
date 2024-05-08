using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class RequestAsset
    {

        public int IdRequestAsset { get; set; }
        public int IdAsset { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public string? PictureName { get; set; }
        public int? IdPriority { get; set; }
        public int? CurrentBoard { get; set; }
        public DateTime? RegisterDate { get; set; }

        public virtual Asset? IdAssetNavigation { get; set; }
        public virtual Priority? IdPriorityNavigation { get; set; }

    }
}
