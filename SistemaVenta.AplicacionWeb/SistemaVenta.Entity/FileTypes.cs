using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class FileTypes
    {
        public FileTypes()
        {
            Files = new HashSet<Files>();
        }

        public int idFileTypes { get; set; }
        public string? description { get; set; }
        public bool? active { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual ICollection<Files> Files { get; set; }

    }
}
