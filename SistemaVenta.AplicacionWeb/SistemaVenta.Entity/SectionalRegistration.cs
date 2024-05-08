using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class SectionalRegistration
    {
        public SectionalRegistration()
        {
            Assets = new HashSet<Asset>();
        }

        public int idSectionalRegistration { get; set; }
        public string? description { get; set; }
        public string? state { get; set; }
        public string? location { get; set; }
        public string? address { get; set; }
        public bool? active { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
