using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GensureAPIv2.Models
{
    public class RiskDetailModel
    {
        public bool IncludeRadioLicenseCost { get; set; }
        public int Id { get; set; }
        public int PolicyId { get; set; }
        //[Required(ErrorMessage = "Please Enter No Of Cars Covered")]
        //[DefaultValue(1)]
        public int? NoOfCarsCovered { get; set; }
       
        public string RegistrationNo { get; set; }
        public int? CustomerId { get; set; }
       
        public string MakeId { get; set; }
       
        public string ModelId { get; set; }
       
        public decimal? CubicCapacity { get; set; }
        
        public int? VehicleYear { get; set; }
        
        public string EngineNumber { get; set; }
       
        public string ChasisNumber { get; set; }
        public string VehicleColor { get; set; }
        public int? VehicleUsage { get; set; }
       
        public int? CoverTypeId { get; set; }
        
        public DateTime? CoverStartDate { get; set; }
       

        public DateTime? CoverEndDate { get; set; }


        public decimal? SumInsured { get; set; }
        
        public decimal? Premium { get; set; }
        public int? AgentCommissionId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? StampDuty { get; set; }
        public decimal? ZTSCLevy { get; set; }
      
        public string OptionalCovers { get; set; }
        public int ExcessType { get; set; }
        public decimal Excess { get; set; }
        public string CoverNoteNo { get; set; }
        public bool IsLicenseDiskNeeded { get; set; }
        public Boolean Addthirdparty { get; set; }
        public decimal? AddThirdPartyAmount { get; set; }

        public Boolean PassengerAccidentCover { get; set; }
        public Boolean ExcessBuyBack { get; set; }
        public Boolean RoadsideAssistance { get; set; }
        public Boolean MedicalExpenses { get; set; }
        public int? NumberofPersons { get; set; }
        public bool chkAddVehicles { get; set; }
        public bool isUpdate { get; set; }
        public int vehicleindex { get; set; }
        public decimal? PassengerAccidentCoverAmount { get; set; }
        public decimal? ExcessBuyBackAmount { get; set; }
        public decimal? RoadsideAssistanceAmount { get; set; }
        public decimal? MedicalExpensesAmount { get; set; }
        public decimal? PassengerAccidentCoverAmountPerPerson { get; set; }
        public decimal? ExcessBuyBackPercentage { get; set; }
        public decimal? RoadsideAssistancePercentage { get; set; }
        public decimal? MedicalExpensesPercentage { get; set; }
        public decimal? ExcessAmount { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime TransactionDate { get; set; }
       
        public int PaymentTermId { get; set; }
       
        public int ProductId { get; set; }
        public string InsuranceId { get; set; }
        public decimal? AnnualRiskPremium { get; set; }
        public decimal? TermlyRiskPremium { get; set; }
        public decimal? QuaterlyRiskPremium { get; set; }
        public decimal? Discount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string LicenseAddress1 { get; set; }
        public string LicenseAddress2 { get; set; }
        public string LicenseCity { get; set; }
        public bool isWebUser { get; set; }
        public decimal SuggestedValue { get; set; }
        public decimal VehicleLicenceFee { get; set; }

        public int? SummaryId { get; set; }

        public CustomerModel CustomerDetails { get; set; }
        public string VechicalMake { get; set; }

        public string VechicalModel { get; set; }

        public string CoverTypeName { get; set; }

        public string PolicyExpireDate { get; set; }
        public int BusinessSourceDetailId { get; set; }

        public int CurrencyId { get; set; }
        public bool isVehicleRegisteredonICEcash { get; set; }
        public decimal BasicPremiumICEcash { get; set; }
        public decimal? RadioLicenseCost { get; set; }
        public decimal? TotalLicAmount { get; set; }
        public decimal? PenaltiesAmount { get; set; }
        //public bool? IncludeZineraCost { get; set; }
        public string LicenseId { get; set; }

        public int TaxClassId { get; set; }
        public bool IsCorporateField { get; set; }      
        public string IceCashRequest { get; set; }


        public int ZinaraPaymentTermId { get; set; }
        public int ZinaraRadioPaymentTermId { get; set; }

        public string LicExpiryDate { get; set; }
        public string RadioTVExpiryDate { get; set; }


    }


    public  class Branch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
    }


    public class BannerImage
    {
        public byte[] Data { get; set; }
    }

    public class PartialPaymentModel
    {       
        public int Id { get; set; }
        public decimal PartialAmount { get; set; }
        public int SummaryDetailId { get; set; }
        public string RegistratonNumber { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime? CreatedOn { get; set; }
        public decimal CalulatedPremium { get; set; }

    }


}