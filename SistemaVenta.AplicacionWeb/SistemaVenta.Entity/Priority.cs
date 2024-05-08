using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class Priority
    {
        public Priority()
        {
            Request = new HashSet<Request>();
        }

        public int idPriority { get; set; }
        public string? description { get; set; }
        public bool? active { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual ICollection<Request> Request { get; set; }

    }
}
