using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using GensureAPIv2.Models;
using InsuranceClaim.Models;

namespace Gene
{


    public static class GeneApiClass
    {




    }
    public class MainGenic
    {
        Int32 Id;
        String Name;
    }
    public class RootObject
    {
        public int Id { get; set; }
        public string CityName { get; set; }
    }

    public class RequestToke
    {
        public string Token { get; set; }
    }

    public class Country
    {
        public string code { get; set; }
        public string name { get; set; }
        public string DisplayName { get; set; }
    }

    public class CountryObject
    {
        public List<Country> countries { get; set; }
    }


    public class VehUsageObject
    {
        public int Id { get; set; }
        public string VehUsage { get; set; }
    }
    public class MakeObject
    {
        public int Id { get; set; }
        public string MakeDescription { get; set; }
        public string MakeCode { get; set; }
    }

    public class CurrencyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ProductsModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public int VehicleTypeId { get; set; }
    }
    public class Messages
    {
        public bool Suceess { get; set; }

        //public bool Error { get; set; }
    }
    public class VehicalMakeModel
    {
        public string make { get; set; }
        public string model { get; set; }
    }
    public class ModelObject
    {
        public string ModelDescription { get; set; }

        public string ModelCode { get; set; }
        public string id { get; set; }
    }

    public class VehicalModel
    {
        public int VehicalId { get; set; }
        public string VRN { get; set; }
    }

    public class CoverObject
    {
        public int Id { get; set; }
        public string name { get; set; }
    }
    public class RadioLicenceAmount
    {
        public int RadioLicenceAmounts { get; set; }
    }

    public class GetAllCities
    {
        public int Id { get; set; }
        public string CityName { get; set; }
    }

    public class VehicleTaxClassModel
    {
        public string Description { get; set; }
        public int TaxClassId { get; set; }
        public int VehicleType { get; set; }
    }

    public class VehicleDetails
    {
        public int Id { get; set; }
        public int vehicleUsageId { get; set; }
        public decimal sumInsured { get; set; }
        public int coverType { get; set; }
        public int excessType { get; set; } = 0;
        public decimal excess { get; set; } = 0.00m;
        public decimal? AddThirdPartyAmount { get; set; }
        public int NumberofPersons { get; set; }
        public Boolean Addthirdparty { get; set; }
        public Boolean PassengerAccidentCover { get; set; }
        public Boolean ExcessBuyBack { get; set; }
        public Boolean RoadsideAssistance { get; set; }
        public Boolean MedicalExpenses { get; set; }
        public decimal? RadioLicenseCost { get; set; }
        public Boolean IncludeRadioLicenseCost { get; set; }
        public int PaymentTermid { get; set; }
        public Boolean isVehicleRegisteredonICEcash { get; set; }
        public string BasicPremiumICEcash { get; set; }
        public string StampDutyICEcash { get; set; }
        public string ZTSCLevyICEcash { get; set; }
        public int ProductId { get; set; } = 0;

        public int VehicelId { get; set; }

        public string LicenseId { get; set; }

        public string CombinedID { get; set; }

        public string RegistrationNo { get; set; }

        public string LicenseExpiryDate { get; set; }
    }

    public class QuoteLogic
    {
        public decimal Premium { get; set; }
        public decimal StamDuty { get; set; }
        public decimal ZtscLevy { get; set; }
        public bool Status { get; set; } = true;
        public string Message { get; set; }
        public decimal ExcessBuyBackAmount { get; set; }
        public decimal RoadsideAssistanceAmount { get; set; }
        public decimal MedicalExpensesAmount { get; set; }
        public decimal PassengerAccidentCoverAmount { get; set; }
        public decimal PassengerAccidentCoverAmountPerPerson { get; set; }
        public decimal ExcessBuyBackPercentage { get; set; }
        public decimal RoadsideAssistancePercentage { get; set; }
        public decimal MedicalExpensesPercentage { get; set; }
        public decimal ExcessAmount { get; set; }
        public decimal AnnualRiskPremium { get; set; }
        public decimal TermlyRiskPremium { get; set; }
        public decimal QuaterlyRiskPremium { get; set; }
        public decimal Discount { get; set; }
    }

    public class ProductIdModel
    {
        public int ProductId { get; set; }
    }
    public class EmailModel
    {
        public string EmailAddress { get; set; }
        public string PhonuNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string IDNumber { get; set; }
        public string ZipCode { get; set; }
    }

    public class CompanyEmailModel
    {
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyBusinessId { get; set; }
        public bool IsCorporate { get; set; }

    }

    public class PaymentInfoModel
    {
        public long TransactionId { get; set; }
    }


    public class PdfModel
    {
        public string Base64String { get; set; }
    }




    public class CustomerVehicalModel
    {
        public CustomerVehicalModel()
        {
            CustomerModel = new CustomerModel();
            PolicyDetail = new PolicyDetail();
            RiskDetailModel = new List<RiskDetailModel>();
            SummaryDetailModel = new SummaryDetailModel();
        }
        public CustomerModel CustomerModel { get; set; }   // from model
        public PolicyDetail PolicyDetail { get; set; }    // from Entity  
        public List<RiskDetailModel> RiskDetailModel { get; set; }
        public SummaryDetailModel SummaryDetailModel { get; set; }

    }
    //6 JAn Ds
    public class CustomerREVehicalModel
    {
        public CustomerREVehicalModel()
        {
            CustomerModel = new CustomerModel();
            PolicyDetail = new PolicyDetail();
            RiskDetailModel = new RiskDetailModel();
            SummaryDetailModel = new SummaryDetailModel();
        }
        public CustomerModel CustomerModel { get; set; }   // from model
        public PolicyDetail PolicyDetail { get; set; }    // from Entity  
        public RiskDetailModel RiskDetailModel { get; set; }
        public SummaryDetailModel SummaryDetailModel { get; set; }

    }
    public class PaymentResult
    {
        public string TransactionId { get; set; }
        public string ActionCode { get; set; }
    }

    public class VehicleUpdateModel
    {
        public string SummaryId { get; set; }
        public string InsuranceStatus { get; set; }
        public string CoverNote { get; set; }

        public int LicenseId { get; set; }

        public string VRN { get; set; }

        public int VehicleId { get; set; }

    }

    public class vehicledetailModel
    {
        public int ProductId { get; set; }

        public int PaymentTermId { get; set; }
    }

    public class DropdownTables
    {
        public List<MakeObject> MakeModel { get; set; }
        public List<CoverObject> CoverTypeModel { get; set; }
        public List<CoverObject> PaymentTermModel { get; set; }
        public List<GetAllCities> CitiesModel { get; set; }
        public List<VehicleTaxClassModel> TaxClassModel { get; set; }
        public List<ProductsModel> ProductsModel { get; set; }
        public List<CurrencyModel> CurrencyModel { get; set; }
    }
}
