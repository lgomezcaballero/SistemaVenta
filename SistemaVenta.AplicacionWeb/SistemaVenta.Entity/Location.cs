using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class Location
    {
        public Location()
        {
            Assets = new HashSet<Asset>();
        }

        public int idLocation { get; set; }
        public string? description { get; set; }
        public string? name { get; set; }
        public bool? active { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
