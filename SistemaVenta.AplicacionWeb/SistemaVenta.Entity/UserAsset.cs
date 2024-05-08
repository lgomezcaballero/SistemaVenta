using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class UserAsset
    {
        public UserAsset()
        {
            Assets = new HashSet<Asset>();
        }

        public int idUserAsset { get; set; }
        public string? name { get; set; }
        public string? workFile { get; set; }
        public string? licence { get; set; }
        public DateTime? licenceExpiration { get; set; }
        public DateTime? birthday { get; set; }
        public int? phone { get; set; }
        public int? emergencyContact { get; set; }
        public string? email { get; set; }
        public string? pictureUrl { get; set; }
        public string? pictureName { get; set; }
        public string? fileLicenceUrl { get; set; }
        public string? fileLicenceName { get; set; }
        public bool? active { get; set; }
        public DateTime? registerDate { get; set; }
        


        public virtual ICollection<Asset> Assets { get; set; }

    }

}
