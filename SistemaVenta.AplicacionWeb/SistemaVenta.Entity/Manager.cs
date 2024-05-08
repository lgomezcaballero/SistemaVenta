using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{

    public partial class Manager
    {
        public Manager()
        {
            Assets = new HashSet<Asset>();
        }

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
        public string? pictureName { get; set; }
        public bool? active { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }

    }
}
