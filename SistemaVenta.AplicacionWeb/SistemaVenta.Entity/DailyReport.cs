using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class DailyReport
    {

        public int idDailyReport { get; set; }
        public int? idAsset { get; set; }
        public int? numberReport{ get; set; }
        public int? final { get; set; }
        public string? closingOfDay { get; set; }
        public DateTime? registerDate { get; set; }

        public virtual Asset? IdAssetNavigation { get; set; }

    }
}
