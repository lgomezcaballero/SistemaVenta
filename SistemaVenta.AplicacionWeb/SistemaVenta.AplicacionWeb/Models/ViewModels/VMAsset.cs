namespace SistemaVenta.AplicacionWeb.Models.ViewModels
{
    public class VMAsset
    {
        public int idAsset { get; set; }
        public string? inter { get; set; }
        public string? make { get; set; }
        public string? model { get; set; }
        public int? idAssetType { get; set; }
        public string? nombreAssetType { get; set; }
        public int? year { get; set; }
        public string? licencePlate { get; set; }
        public string? vin { get; set; }
        public string? engine { get; set; }
        public int? idFuel { get; set; }
        public string? nombreFuel { get; set; }
        public decimal? purchasedPrice { get; set; }
        public DateTime? purchasedDate { get; set; }
        public int? idInsurance { get; set; }
        public string? nombreInsurance { get; set; }
        public int? idInsurancePT { get; set; }
        public string? nombreInsurancePT { get; set; }
        public DateTime? insuranceExpiration { get; set; }
        public int? idSectionalRegistration { get; set; }
        public string? nombreSectionalRegistration { get; set; }
        public int? idLocation { get; set; }
        public string? nombreLocation { get; set; }
        public int? idUserAsset { get; set; }
        public string? nombreUserAsset { get; set; }
        public int? idBusiness { get; set; }
        public string? nombreBusiness { get; set; }
        public string? businessType { get; set; }
        public int? idManager { get; set; }
        public string? nombreManager { get; set; }
        public int? idStatus { get; set; }
        public string? nombreStatus { get; set; }
        public int? board { get; set; }
        public string? pictureUrl { get; set; }
        public string? pictureName { get; set; }
        public DateTime? registerDate { get; set; }
        

    }
}
