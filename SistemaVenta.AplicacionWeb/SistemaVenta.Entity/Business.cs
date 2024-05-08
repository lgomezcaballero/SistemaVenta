using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class Business
    {
        public Business()
        {
            Assets = new HashSet<Asset>();
        }

        public int idBusiness { get; set; }
        public string? description { get; set; }
        public string? ein { get; set; }
        public string? type { get; set; }
        public string? country { get; set; }
        public string? state { get; set; }
        public string? address { get; set; }
        public bool? active { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
