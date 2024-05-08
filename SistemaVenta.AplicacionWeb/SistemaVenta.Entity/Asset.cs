using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity
{
    public partial class Asset
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public Asset()
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        {
            DailyReport = new HashSet<DailyReport>();
        
        }

        public int idAsset { get; set; }
        public string? inter { get; set; }
        public string? make { get; set; }
        public string? model { get; set; }
        public int? idAssetType { get; set; }
        public int? year { get; set; }
        public string? licencePlate { get; set; }
        public string? vin { get; set; }
        public string? engine { get; set; }
        public int? idFuel { get; set; }
        public decimal? purchasedPrice { get; set; }
        public DateTime? purchasedDate { get; set; }
        public int? idInsurance { get; set; }
        public int? idInsurancePT { get; set; }
        public DateTime? insuranceExpiration { get; set; }
        public int? idSectionalRegistration { get; set; }
        public int? idLocation { get; set; }
        public int? idUserAsset { get; set; }
        public int? idBusiness { get; set; }
        public string? businessType { get; set; }
        public int? idManager { get; set; }
        public int? idStatus { get; set; }
        public int? board { get; set; }
        public string? pictureUrl { get; set; }
        public string? pictureName { get; set; }       
        public DateTime? registerDate { get; set; }
        
        


        //llaves de donde tenes que solicitar 
        public virtual AssetType? IdAssetTypeNavigation { get; set; }
        public virtual Fuel? IdFuelNavigation { get; set; }
        public virtual Insurance? IdInsuranceNavigation { get; set; }
        public virtual InsurancePT? IdInsurancePTNavigation { get; set; }
        public virtual SectionalRegistration? IdSectionalRegistrationNavigation { get; set; }
        public virtual Location? IdLocationNavigation { get; set; }
        public virtual UserAsset? IdUserAssetNavigation { get; set; }
        public virtual Business? IdBusinessNavigation { get; set; }
        public virtual Manager? IdManagerNavigation { get; set; }
        public virtual Status? IdStatusNavigation { get; set; }



        // llaves de donde te consultan
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public virtual ICollection<DailyReport> DailyReport { get; set; }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public virtual ICollection<FuelLoad> FuelLoad { get; set; }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public virtual ICollection<RequestDetail> RequestDetail { get; set; }

        public virtual ICollection<Files> Files { get; set; }

    }
}
