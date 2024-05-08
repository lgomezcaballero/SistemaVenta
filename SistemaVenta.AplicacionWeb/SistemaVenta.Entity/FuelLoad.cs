using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class FuelLoad
    {
        public int IdFuelLoad { get; set; }
        public int IdAsset { get; set; }
        public int IdFuel { get; set; }
        public int? Amount {  get; set; }
        public int? CurrentBoard { get; set; }
        public DateTime? RegisterDate { get; set; }

        public virtual Asset? IdAssetNavigation { get; set; }
        public virtual Fuel? IdFuelNavigation { get; set; }


    }
}
