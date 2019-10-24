using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GensureAPIv2.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public decimal CustomerId { get; set; }
        public string UserID { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string NationalIdentificationNumber { get; set; }

        public string Zipcode { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }
        public bool? IsWelcomeNoteSent { get; set; }
        public bool? IsPolicyDocSent { get; set; }
        public bool? IsLicenseDiskNeeded { get; set; }
        public bool? IsOTPConfirmed { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }

        public string CountryCode { get; set; }
        public string role { get; set; }

        public bool IsCustomEmail { get; set; }

        public string CustomEmail { get; set; }

        public string UserRoleName { get; set; }


        // Business details

        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyBusinessId { get; set; }
        public bool IsCorporate { get; set; }

        public int BranchId { get; set; }


    }


    public class PolicyDetail
    {
        public int BusinessSourceId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CurrencyId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? EndDate { get; set; }
        public int Id { get; set; }
        public int? InsurerId { get; set; }
        public bool? IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string PolicyName { get; set; }
        public string PolicyNumber { get; set; }
        public int PolicyStatusId { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? TransactionDate { get; set; }

     



    }

    public class Product
    {
        public string ProductName { get; set; }
        public string ProductMake { get; set; }
        public string ProductModel { get; set; }

        public override bool Equals(object obj)
        {
            return ((Product)obj).ProductMake == ProductMake && ((Product)obj).ProductModel == ProductModel && ((Product)obj).ProductName == ProductName;
        }
        public override int GetHashCode()
        {
            return ProductName.GetHashCode();
        }
    }


    public enum eCoverType
    {

        Comprehensive = 4,
        ThirdParty = 1,
        FullThirdParty = 2


        //Comprehensive = 1,
        //ThirdParty = 2,
        //FullThirdParty = 3
    }



    public enum ePaymentMethod
    {

        Swipe = 4,
        Mobile = 5,
       
    }


}
