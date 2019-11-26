using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gene
{
    public class VehicleLicQuote
    {
        public string VRN { get; set; }
        public string ClientIDType { get; set; }
        public string IDNumber { get; set; }
        public string DurationMonths { get; set; }
        public string LicFrequency { get; set; }
        public string RadioTVUsage { get; set; }
        public string RadioTVFrequency { get; set; }


          



    }


    public class VehicleRatioTvQuote
    {
        public string VRN { get; set; }
        public string ClientIDType { get; set; }
        public string IDNumber { get; set; }
        public string DurationMonths { get; set; }
        public string LicFrequency { get; set; }

    }











    public class VehicleLicQuoteUpdate
    {
        public int LicenceID { get; set; }
        public int PaymentMethod { get; set; }
        public string Status { get; set; }
        public int DeliveryMethod { get; set; }    
        public int LicenceCert { get; set; }
        public string MachineName { get; set; }
    }


    public class ResponseCodeObj
    {
        public string Name { get; set; }
        public string TransactionAmount { get; set;}
    }


    public class CountryModel
    {
        public int Id { get; set; }
        public string Country { get; set; }
    }



}
