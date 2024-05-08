using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class Insurance
    {
        public Insurance()
        {
            Assets = new HashSet<Asset>();
        }

        public int idInsurance { get; set; }
        public string? description { get; set; }
        public bool? active { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
