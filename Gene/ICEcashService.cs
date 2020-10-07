using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;
//using Insurance.Domain;
using GensureAPIv2.Models;
using RestSharp;
using Gene;
using System.Management;
using System.IO;

namespace Insurance.Service
{
    public class ICEcashService
    {
        //Test SANDBOX urL 
        public static string PSK = "127782435202916376850511";
        public static string SandboxIceCashApi = "http://api-test.icecash.com/request/20523588";

        // Live url
        //public static string PSK = "565205790573235453203546";
        //public static string SandboxIceCashApi = "https://api.icecash.co.zw/request/20350763";


        private static string GetSHA512(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);
            SHA512Managed hashString = new SHA512Managed();
            string encodedData = Convert.ToBase64String(message);
            string hex = "";
            hashValue = hashString.ComputeHash(UE.GetBytes(encodedData));
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

        public ICEcashTokenResponse getToken()
        {

            ICEcashTokenResponse json = null;
            try
            {
                string _json = "";
                Arguments objArg = new Arguments();
                objArg.PartnerReference = Guid.NewGuid().ToString();
                objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
                objArg.Version = "2.0";
                objArg.Request = new FunctionObject { Function = "PartnerToken" };

                _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

                //string  = json.Reverse()
                string reversejsonString = new string(_json.Reverse().ToArray());
                string reversepartneridString = new string(PSK.Reverse().ToArray());

                string concatinatedString = reversejsonString + reversepartneridString;

                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

                string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

                string GetSHA512encrypted = SHA512(returnValue);

                string MAC = "";

                for (int i = 0; i < 16; i++)
                {
                    MAC += GetSHA512encrypted.Substring((i * 8), 1);
                }

                MAC = MAC.ToUpper();


                ICERootObject objroot = new ICERootObject();
                objroot.Arguments = objArg;
                objroot.MAC = MAC;
                objroot.Mode = "SH";

                var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);


                JObject jsonobject = JObject.Parse(data);

                var client = new RestClient(SandboxIceCashApi);
                //  var client = new RestClient(LiveIceCashApi);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                json = JsonConvert.DeserializeObject<ICEcashTokenResponse>(response.Content);

                Service_db.WriteIceCashLog(data, response.Content, "PartnerToken");




            }
            catch (Exception ex)
            {
                json = new ICEcashTokenResponse() { Date = "", PartnerReference = "", Version = "", Response = new TokenReposone() { Result = "0", Message = "A Connection Error Occured ! Please add manually", ExpireDate = "", Function = "", PartnerToken = "" } };
            }

            return json;
        }

        //public ResultRootObject checkVehicleTBAVRN(RiskDetailModel item, CustomerModel CustomerInfo, string PartnerToken)
        //{

        //    string _json = "";


        //    //List<VehicleObjectVRN> obj = new List<VehicleObjectVRN>();
        //    // obj.Add(new VehicleObjectVRN { VRN = RegistrationNo });


        //    List<VehicleObject> obj = new List<VehicleObject>();


        //    if(item.RegistrationNo=="TBA")
        //    {
        //        int durationMonth = 0;
        //        if (item.PaymentTermId == 1)
        //            durationMonth = 12;
        //        else
        //            durationMonth = item.PaymentTermId;

        //        string MSISDN = "+263" + "775308520";

        //        obj.Add(new VehicleObject { VRN = item.RegistrationNo, IDNumber = CustomerInfo.NationalIdentificationNumber, FirstName = CustomerInfo.FirstName, LastName = CustomerInfo.LastName, MSISDN = MSISDN, Address1 = CustomerInfo.AddressLine1, Town = CustomerInfo.AddressLine2, EntityType = "Personal", DurationMonths = durationMonth.ToString(), InsuranceType = item.CoverTypeId.ToString(), VehicleType = item.ProductId.ToString(), Make = item.MakeId, Model = item.ModelId, TaxClass = item.TaxClassId.ToString(), YearManufacture = item.VehicleYear.ToString() });

        //    }



        //    QuoteArgumentsVRN objArg = new QuoteArgumentsVRN();
        //    objArg.PartnerReference = Guid.NewGuid().ToString();
        //    objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
        //    objArg.Version = "2.0";
        //    objArg.PartnerToken = PartnerToken;
        //    objArg.Request = new QuoteFunctionObjectVRN { Function = "TPIQuote", Vehicles = obj };

        //    _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

        //    //string  = json.Reverse()
        //    string reversejsonString = new string(_json.Reverse().ToArray());
        //    string reversepartneridString = new string(PSK.Reverse().ToArray());

        //    string concatinatedString = reversejsonString + reversepartneridString;

        //    byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

        //    string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

        //    string GetSHA512encrypted = SHA512(returnValue);

        //    string MAC = "";

        //    for (int i = 0; i < 16; i++)
        //    {
        //        MAC += GetSHA512encrypted.Substring((i * 8), 1);
        //    }

        //    MAC = MAC.ToUpper();


        //    ICEQuoteRequestVRN objroot = new ICEQuoteRequestVRN();
        //    objroot.Arguments = objArg;
        //    objroot.MAC = MAC;
        //    objroot.Mode = "SH";

        //    var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

        //    JObject jsonobject = JObject.Parse(data);

        //    //var client = new RestClient("http://api-test.icecash.com/request/20523588");


        //    var client = new RestClient(SandboxIceCashApi);
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("content-type", "application/x-www-form-urlencoded");
        //    request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);

        //    ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

        //    return json;
        //}


        public ResultRootObject checkVehicleExistsWithVRN(RiskDetailModel item, CustomerModel CustomerInfo, string PartnerToken)
        {

            string _json = "";


            List<VehicleObjectVRN> obj = new List<VehicleObjectVRN>();
            obj.Add(new VehicleObjectVRN { VRN = item.RegistrationNo, CompanyName = "GeneInsure" });



            QuoteArgumentsVRN objArg = new QuoteArgumentsVRN();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new QuoteFunctionObjectVRN { Function = "TPIQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();


            ICEQuoteRequestVRN objroot = new ICEQuoteRequestVRN();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //var client = new RestClient("http://api-test.icecash.com/request/20523588");


            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            string branchId = CustomerInfo == null ? "" : Convert.ToString(CustomerInfo.BranchId);

            Service_db.WriteIceCashLog(data, response.Content, "TPIQuote", item.RegistrationNo, branchId);

            return json;
        }


        public ResultRootObject RequestQuote(RiskDetailModel riskDetail, CustomerModel CustomerInfo, string PartnerToken)
        {
            //string PSK = "127782435202916376850511";
            string _json = "";
            //make = RemoveSpecialChars(make);
            //model = RemoveSpecialChars(model);


            if (!string.IsNullOrEmpty(riskDetail.MakeId))
            {
                riskDetail.MakeId = RemoveSpecialChars(riskDetail.MakeId);
            }

            if (!string.IsNullOrEmpty(riskDetail.ModelId))
            {
                riskDetail.ModelId = RemoveSpecialChars(riskDetail.ModelId);
            }




            List<VehicleObject> obj = new List<VehicleObject>();

            //foreach (var item in listofvehicles)
            //{

            //obj.Add(new VehicleObject { VRN = RegistrationNo, DurationMonths = (PaymentTermId == 1 ? 12 : PaymentTermId), VehicleValue = Convert.ToInt32(suminsured), YearManufacture = Convert.ToInt32(VehicleYear), InsuranceType = Convert.ToInt32(CoverTypeId), VehicleType = Convert.ToInt32(VehicleUsage), TaxClass = 1, Make = make, Model = model, EntityType = "", Town = CustomerInfo.City, Address1 = CustomerInfo.AddressLine1, Address2 = CustomerInfo.AddressLine2, CompanyName = "", FirstName = CustomerInfo.FirstName, LastName = CustomerInfo.LastName, IDNumber = CustomerInfo.NationalIdentificationNumber, MSISDN = CustomerInfo.CountryCode + CustomerInfo.PhoneNumber });


            //obj.Add(new VehicleObject { VRN = RegistrationNo, DurationMonths = (PaymentTermId == 1 ? 12 : PaymentTermId), VehicleValue = Convert.ToInt32(suminsured), YearManufacture = Convert.ToInt32(VehicleYear), InsuranceType = Convert.ToInt32(CoverTypeId), VehicleType = Convert.ToInt32(VehicleUsage), TaxClass = 1, Make = make, Model = model, EntityType = ""  });


            if (riskDetail.RegistrationNo == "TBA")
            {
                string MSISDN = "+263" + CustomerInfo.PhoneNumber;
                obj.Add(new VehicleObject { VRN = riskDetail.RegistrationNo, IDNumber = CustomerInfo.NationalIdentificationNumber, FirstName = CustomerInfo.FirstName, LastName = CustomerInfo.LastName, MSISDN = MSISDN, Address1 = CustomerInfo.AddressLine1, Town = CustomerInfo.AddressLine2, EntityType = "Corporate", DurationMonths = Convert.ToString(riskDetail.PaymentTermId == 1 ? 12 : riskDetail.PaymentTermId), InsuranceType = riskDetail.CoverTypeId.ToString(), VehicleType = riskDetail.ProductId.ToString(), Make = riskDetail.MakeId, Model = riskDetail.ModelId, YearManufacture = DateTime.Now.Year.ToString(), TaxClass = riskDetail.TaxClassId.ToString() });
                //TaxClass = riskDetail.TaxClassId.ToString()
            }
            else
            {
                obj.Add(new VehicleObject { VRN = riskDetail.RegistrationNo, FirstName = CustomerInfo.FirstName, LastName = CustomerInfo.LastName, MSISDN = CustomerInfo.CountryCode + CustomerInfo.PhoneNumber, Address1 = CustomerInfo.AddressLine1, Town = CustomerInfo.AddressLine2, IDNumber = CustomerInfo.NationalIdentificationNumber, DurationMonths = Convert.ToString(riskDetail.PaymentTermId == 1 ? 12 : riskDetail.PaymentTermId), VehicleValue = Convert.ToString(riskDetail.SumInsured), YearManufacture = Convert.ToString(riskDetail.VehicleYear), InsuranceType = Convert.ToString(riskDetail.CoverTypeId), VehicleType = Convert.ToString(riskDetail.ProductId), Make = riskDetail.MakeId, Model = riskDetail.MakeId, EntityType = "" });     
            }

            //}

            QuoteArguments objArg = new QuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString(); ;
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new QuoteFunctionObject { Function = "TPIQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            ICEQuoteRequest objroot = new ICEQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            // var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            string branchId = CustomerInfo == null ? "" : Convert.ToString(CustomerInfo.BranchId);

            Service_db.WriteIceCashLog(data, response.Content, "TPIQuote", riskDetail.RegistrationNo, branchId);


            //if (json.Response.Quotes != null && json.Response.Quotes.Count > 0)
            //{
            //    if (json.Response.Quotes[0].Policy != null)
            //    {
            //        var Setting = InsuranceContext.Settings.All();
            //        var DiscountOnRenewalSettings = Setting.Where(x => x.keyname == "Discount On Renewal").FirstOrDefault();
            //        var premium = Convert.ToDecimal(json.Response.Quotes[0].Policy.CoverAmount);
            //    }
            //}


            return json;
        }

        //LicInsuraceObject


        //public ResultRootObject RequestQuoteInsurance(RiskDetailModel riskDetail, CustomerModel CustomerInfo, string PartnerToken)
        //{
        //    //string PSK = "127782435202916376850511";
        //    string _json = "";
        //    //make = RemoveSpecialChars(make);
        //    //model = RemoveSpecialChars(model);


        //    if (!string.IsNullOrEmpty(riskDetail.MakeId))
        //    {
        //        riskDetail.MakeId = RemoveSpecialChars(riskDetail.MakeId);
        //    }

        //    if (!string.IsNullOrEmpty(riskDetail.ModelId))
        //    {
        //        riskDetail.ModelId = RemoveSpecialChars(riskDetail.ModelId);
        //    }




        //    List<LicInsuraceObject> obj = new List<LicInsuraceObject>();

        //    //foreach (var item in listofvehicles)
        //    //{

        //    //obj.Add(new VehicleObject { VRN = RegistrationNo, DurationMonths = (PaymentTermId == 1 ? 12 : PaymentTermId), VehicleValue = Convert.ToInt32(suminsured), YearManufacture = Convert.ToInt32(VehicleYear), InsuranceType = Convert.ToInt32(CoverTypeId), VehicleType = Convert.ToInt32(VehicleUsage), TaxClass = 1, Make = make, Model = model, EntityType = "", Town = CustomerInfo.City, Address1 = CustomerInfo.AddressLine1, Address2 = CustomerInfo.AddressLine2, CompanyName = "", FirstName = CustomerInfo.FirstName, LastName = CustomerInfo.LastName, IDNumber = CustomerInfo.NationalIdentificationNumber, MSISDN = CustomerInfo.CountryCode + CustomerInfo.PhoneNumber });


        //    //obj.Add(new VehicleObject { VRN = RegistrationNo, DurationMonths = (PaymentTermId == 1 ? 12 : PaymentTermId), VehicleValue = Convert.ToInt32(suminsured), YearManufacture = Convert.ToInt32(VehicleYear), InsuranceType = Convert.ToInt32(CoverTypeId), VehicleType = Convert.ToInt32(VehicleUsage), TaxClass = 1, Make = make, Model = model, EntityType = ""  });


        //    obj.Add(new LicInsuraceObject { VRN = riskDetail.RegistrationNo, IDNumber = CustomerInfo.NationalIdentificationNumber, FirstName = CustomerInfo.FirstName, LastName = CustomerInfo.LastName, MSISDN = MSISDN, Address1 = CustomerInfo.AddressLine1, DurationMonths = Convert.ToString(riskDetail.PaymentTermId == 1 ? 12 : riskDetail.PaymentTermId), InsuranceType = riskDetail.CoverTypeId.ToString(), TaxClass = riskDetail.TaxClassId.ToString() });

        //    //}

        //    QuoteArguments objArg = new QuoteArguments();
        //    objArg.PartnerReference = Guid.NewGuid().ToString(); ;
        //    objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
        //    objArg.Version = "2.0";
        //    objArg.PartnerToken = PartnerToken;
        //    objArg.Request = new QuoteFunctionObject { Function = "TPIQuote", Vehicles = obj };

        //    _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

        //    //string  = json.Reverse()
        //    string reversejsonString = new string(_json.Reverse().ToArray());
        //    string reversepartneridString = new string(PSK.Reverse().ToArray());

        //    string concatinatedString = reversejsonString + reversepartneridString;

        //    byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

        //    string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

        //    string GetSHA512encrypted = SHA512(returnValue);

        //    string MAC = "";

        //    for (int i = 0; i < 16; i++)
        //    {
        //        MAC += GetSHA512encrypted.Substring((i * 8), 1);
        //    }

        //    MAC = MAC.ToUpper();

        //    ICEQuoteRequest objroot = new ICEQuoteRequest();
        //    objroot.Arguments = objArg;
        //    objroot.MAC = MAC;
        //    objroot.Mode = "SH";

        //    var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

        //    JObject jsonobject = JObject.Parse(data);

        //    // var client = new RestClient("http://api-test.icecash.com/request/20523588");
        //    var client = new RestClient(SandboxIceCashApi);
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("content-type", "application/x-www-form-urlencoded");
        //    request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);

        //    ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

        //    string branchId = CustomerInfo == null ? "" : Convert.ToString(CustomerInfo.BranchId);

        //    Service_db.WriteIceCashLog(data, response.Content, "TPIQuote", riskDetail.RegistrationNo, branchId);


        //    //if (json.Response.Quotes != null && json.Response.Quotes.Count > 0)
        //    //{
        //    //    if (json.Response.Quotes[0].Policy != null)
        //    //    {
        //    //        var Setting = InsuranceContext.Settings.All();
        //    //        var DiscountOnRenewalSettings = Setting.Where(x => x.keyname == "Discount On Renewal").FirstOrDefault();
        //    //        var premium = Convert.ToDecimal(json.Response.Quotes[0].Policy.CoverAmount);
        //    //    }
        //    //}


        //    return json;
        //}



        //List<VehicleLicQuote> listVehicleLic
        public ResultRootObject TPILICQuote(RiskDetailModel riskDetail, CustomerModel customerInfo, string PartnerToken)
        {
            //string PSK = "127782435202916376850511";
            string _json = "";

            string clientIdType = "1";
            if (customerInfo.IsCorporate)
                clientIdType = "2";


            List<LicInsuraceObject> obj = new List<LicInsuraceObject>();
           //  CustomerInfo = new CustomerModel();


            if(riskDetail.PaymentTermId==1)
                riskDetail.PaymentTermId = 12;

            string taxClass = null;
            if(riskDetail.TaxClassId!=0)
                taxClass = Convert.ToString(riskDetail.TaxClassId);
            

            obj.Add(new LicInsuraceObject
            {
                VRN = riskDetail.RegistrationNo,
                IDNumber = customerInfo.NationalIdentificationNumber,
                FirstName = customerInfo.FirstName,
                LastName = customerInfo.LastName,
                MSISDN = customerInfo.CountryCode + "" + customerInfo.PhoneNumber,
                Address1 = customerInfo.AddressLine1,
                Address2 = customerInfo.AddressLine2,
                SuburbID = "1", 
                ClientIDType = clientIdType,
                InsuranceType = riskDetail.CoverTypeId.ToString(),
                TaxClass = riskDetail.TaxClassId.ToString(),
                VehicleType = riskDetail.ProductId.ToString(),
                DurationMonths = Convert.ToString(riskDetail.PaymentTermId),
                LicFrequency = riskDetail.licenseFreequency,
                RadioTVUsage = riskDetail.radioTvUsage,
                RadioTVFrequency = riskDetail.RadioFreequency,
                VehicleValue = riskDetail.SumInsured.ToString()
            });


            LICInsuranceNewQuoteArguments objArg = new LICInsuranceNewQuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICInsuranceNewQuoteObject { Function = "TPILICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICInsuranceNewQuoteRequest objroot = new LICInsuranceNewQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);
            string branchId = customerInfo == null ? "" : Convert.ToString(customerInfo.BranchId);
            Service_db.WriteIceCashLog(data, response.Content, "TPILICQuote", riskDetail.RegistrationNo, branchId);

            return json;

        }



        public ResultRootObject TPILICQuoteZinraOnly(RiskDetailModel riskDetail, CustomerModel customerInfo, string PartnerToken)
        {
            //string PSK = "127782435202916376850511";
            string _json = "";

            string clientIdType = "1";
            if (customerInfo.IsCorporate)
                clientIdType = "2";


            List<LicInsuraceZinraObject> obj = new List<LicInsuraceZinraObject>();
            //  CustomerInfo = new CustomerModel();


            if (riskDetail.PaymentTermId == 1)
                riskDetail.PaymentTermId = 12;

            string taxClass = null;
            if (riskDetail.TaxClassId != 0)
                taxClass = Convert.ToString(riskDetail.TaxClassId);


            obj.Add(new LicInsuraceZinraObject
            {
                VRN = riskDetail.RegistrationNo,
                IDNumber = customerInfo.NationalIdentificationNumber,
                FirstName = customerInfo.FirstName,
                LastName = customerInfo.LastName,
                MSISDN = customerInfo.CountryCode + "" + customerInfo.PhoneNumber,
                Address1 = customerInfo.AddressLine1,
                Address2 = customerInfo.AddressLine2,
                SuburbID = "1",
                ClientIDType = clientIdType,
                InsuranceType = riskDetail.CoverTypeId.ToString(),
                TaxClass = riskDetail.TaxClassId.ToString(),
                VehicleType = riskDetail.ProductId.ToString(),
                DurationMonths = Convert.ToString(riskDetail.PaymentTermId),
                LicFrequency = riskDetail.licenseFreequency,
                VehicleValue = riskDetail.SumInsured.ToString()
            });


            LicInsuraceZinraObjectArguments objArg = new LicInsuraceZinraObjectArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICInsuranceZinaraNewQuoteObject { Function = "TPILICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICInsuranceZinaraRequest objroot = new LICInsuranceZinaraRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);
            string branchId = customerInfo == null ? "" : Convert.ToString(customerInfo.BranchId);
            Service_db.WriteIceCashLog(data, response.Content, "TPILICQuoteZinraOnly", riskDetail.RegistrationNo, branchId);

            return json;

        }




        public ResultRootObject LICQuote(List<VehicleLicQuote> listVehicleLic, string PartnerToken)
        {

            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleLicInsuraceObject> obj = new List<VehicleLicInsuraceObject>();

            var CustomerInfo = new CustomerModel();

            foreach (var item in listVehicleLic)
            {
                obj.Add(new VehicleLicInsuraceObject
                {
                    VRN = item.VRN,
                    //EntityType= "",
                    ClientIDType = item.ClientIDType,
                    IDNumber = item.IDNumber,
                    DurationMonths = item.DurationMonths,
                    LicFrequency = item.LicFrequency,
                    RadioTVUsage = item.RadioTVUsage,
                    RadioTVFrequency = item.RadioTVFrequency
                });
            }

            LICInsuranceQuoteArguments objArg = new LICInsuranceQuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICInsuranceQuoteFunctionObject { Function = "LICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICInsuranceQuoteRequest objroot = new LICInsuranceQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //  var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            Service_db.WriteIceCashLog(data, response.Content, "LICQuote", listVehicleLic[0].VRN);

            return json;

        }




        public ResultRootObject RadioQuote(List<VehicleLicQuote> listVehicleLic, string PartnerToken)
        {

            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleRadioLiceObject> obj = new List<VehicleRadioLiceObject>();

            var CustomerInfo = new CustomerModel();

            foreach (var item in listVehicleLic)
            {
                obj.Add(new VehicleRadioLiceObject
                {
                    VRN = item.VRN,
                    //EntityType= "",
                    ClientIDType = "1",
                    IDNumber = item.IDNumber,
                    DurationMonths = item.DurationMonths,
                    LicFrequency = item.LicFrequency
                    //RadioTVUsage = item.RadioTVUsage,
                    //RadioTVFrequency = item.RadioTVFrequency
                });
            }

            RadioQuoteArguments objArg = new RadioQuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new RadioLICIQuoteFunctionObject { Function = "LICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            RadioQuoteRequest objroot = new RadioQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //  var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            Service_db.WriteIceCashLog(data, response.Content, "LICQuote", listVehicleLic[0].VRN);


            return json;
        }


        public ResultRootObject LICQuoteUpdate(List<VehicleLicQuoteUpdate> listVehicleLic, string PartnerToken)
        {
            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleLicUpdateObject> obj = new List<VehicleLicUpdateObject>();

            foreach (var item in listVehicleLic)
            {
                obj.Add(new VehicleLicUpdateObject
                {
                    LicenceID = Convert.ToString(item.LicenceID),
                    DeliveryMethod = Convert.ToString(item.DeliveryMethod),
                    Status = item.Status,
                    LicenceCert = item.LicenceCert,
                    MachineName = Environment.MachineName
                });
            }

            LICQuoteUpdateArguments objArg = new LICQuoteUpdateArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICQuoteUpdateFunctionObject { Function = "LicQuoteUpdate", PaymentMethod = "1", Identifier = "", MSISDN = "", Quotes = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICQuoteUpdateRequest objroot = new LICQuoteUpdateRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //  var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);


            Service_db.WriteIceCashLog(data, response.Content, "LicQuoteUpdate");

            return json;
        }


        public ResultRootObject TPILICUpdate(List<VehicleLicQuoteUpdate> listVehicleLic, string PartnerToken)
        {
            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleLicUpdateObject> obj = new List<VehicleLicUpdateObject>();

            foreach (var item in listVehicleLic)
            {
                obj.Add(new VehicleLicUpdateObject
                {
                    LicenceID = Convert.ToString(item.LicenceID),
                    DeliveryMethod = Convert.ToString(item.DeliveryMethod),
                    Status = item.Status,
                    LicenceCert = item.LicenceCert,
                    MachineName = Environment.MachineName
                });
            }

            LICQuoteUpdateArguments objArg = new LICQuoteUpdateArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICQuoteUpdateFunctionObject { Function = "LicQuoteUpdate", PaymentMethod = "1", Identifier = "", MSISDN = "", Quotes = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICQuoteUpdateRequest objroot = new LICQuoteUpdateRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //  var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            Service_db.WriteIceCashLog(data, response.Content, "LicQuoteUpdate");

            return json;
        }



        public ResultLicenceIDRootObject LICResult(string LicenceID, string PartnerToken)
        {
            string _json = "";
            //List<VehicleLicUpdateObject> obj = new List<VehicleLicUpdateObject>();

            LICResultArguments objArg = new LICResultArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = new LICResultFunctionObject { Function = "LICResult", LicenceID = LicenceID };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICResultRequest objroot = new LICResultRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //  var client = new RestClient("http://api-test.icecash.com/request/20523588");
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultLicenceIDRootObject json = JsonConvert.DeserializeObject<ResultLicenceIDRootObject>(response.Content);

            Service_db.WriteIceCashLog(data, json.Response.Message, "LICResult");


            return json;
        }

        public static ResultRootObject TPIQuoteUpdate(string Phonenumber, RiskDetailModel vehicleDetails, string PartnerToken, int? paymentMethod)
        {
            string _json = "";

            List<VehicleObject> obj = new List<VehicleObject>();
            var item = vehicleDetails;
            List<QuoteDetial> qut = new List<QuoteDetial>();
            qut.Add(new QuoteDetial { InsuranceID = item.InsuranceId, Status = "1" });


            var quotesDetial = new RequestTPIQuoteUpdate { Function = "TPIQuoteUpdate", PaymentMethod = Convert.ToString("1"), Identifier = "1", MSISDN = "01" + Phonenumber, Quotes = qut };
            QuoteArgumentsTPIQuote objArg = new QuoteArgumentsTPIQuote();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = quotesDetial;



            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            ICEQuoteRequestTPIQuote objroot = new ICEQuoteRequestTPIQuote();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //var client = new RestClient(LiveIceCashApi);
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            Service_db.WriteIceCashLog(data, response.Content, "TPIQuoteUpdate", item.RegistrationNo, item.ALMBranchId.ToString());

            return json;
        }


        public static ResultRootObject TPILICUpdate(string Phonenumber, RiskDetailModel vehicleDetails, string PartnerToken, int? paymentMethod)
        {
            string _json = "";

            List<VehicleObject> obj = new List<VehicleObject>();
            var item = vehicleDetails;
            List<QuoteCombineDetial> qut = new List<QuoteCombineDetial>();
            qut.Add(new QuoteCombineDetial { CombinedID = item.CombinedID, Status = "1", DeliveryMethod = "3", LicenceCert="1" });


            var quotesDetial = new RequestTPILICUpdate { Function = "TPILICUpdate", PaymentMethod = Convert.ToString("1"), Identifier = "1", MSISDN = "01" + Phonenumber, Quotes = qut };
            QuoteArgumentsTPILICUpdate objArg = new QuoteArgumentsTPILICUpdate();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = quotesDetial;



            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            ICEQuoteRequestTPILICUpdate objroot = new ICEQuoteRequestTPILICUpdate();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            //var client = new RestClient(LiveIceCashApi);
            var client = new RestClient(SandboxIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);


            Service_db.WriteIceCashLog(data, response.Content, "TPILICUpdate", item.RegistrationNo, item.ALMBranchId.ToString());

            return json;
        }


        public static ResultRootObject TPIPolicy(RiskDetailModel vehicleDetails, string PartnerToken)
        {

            string _json = "";

            List<VehicleObject> obj = new List<VehicleObject>();
            var item = vehicleDetails;
            TPIPolicyDetial qut = new TPIPolicyDetial { InsuranceID = item.InsuranceId, Function = "TPIPolicy" };

            QuoteArgumentsTPIPolicy objArg = new QuoteArgumentsTPIPolicy();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = qut;

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            ICEQuoteRequestTPIPolicy objroot = new ICEQuoteRequestTPIPolicy();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //  var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            Service_db.WriteIceCashLog(data, response.Content, "TPIPolicy", vehicleDetails.RegistrationNo, vehicleDetails.ALMBranchId.ToString());

            return json;
        }


        public static ResultLicenceIDRootObject TPILICResult(RiskDetailModel vehicleDetails, string PartnerToken)
        {

            string _json = "";

            List<VehicleObject> obj = new List<VehicleObject>();
            var item = vehicleDetails;
            TPILICResult qut = new TPILICResult { CombinedID = item.CombinedID, Function = "TPILICResult" };

            QuoteArgumentsTPILICResult objArg = new QuoteArgumentsTPILICResult();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = qut;

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            ICEQuoteRequestTPILICResult objroot = new ICEQuoteRequestTPILICResult();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //  var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultLicenceIDRootObject json = JsonConvert.DeserializeObject<ResultLicenceIDRootObject>(response.Content);
    
            Service_db.WriteIceCashLog(data, json.Response.Message, "TPILICResult", vehicleDetails.RegistrationNo, vehicleDetails.ALMBranchId.ToString());

            return json;
        }




        public string RemoveSpecialChars(string str)
        {
            // Create  a string array and add the special characters you want to remove
            // You can include / exclude more special characters based on your needs
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };
            //Iterate the number of times based on the String array length.
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }

        public static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        public static ResultRootObject LICCertConf(RiskDetailModel vehicleDetails, string PartnerToken, string ReceiptID)
        {
            string _json = "";
            //ReceiptID = RandomDigits(9);// Generate 9 digit Random Number.
            List<VehicleObject> obj = new List<VehicleObject>();
            var item = vehicleDetails;

            //Removing The First Char from ReceiptID.
            //if (!string.IsNullOrEmpty(ReceiptID))
            //{
            //    ReceiptID = ReceiptID.Substring(1);
            //}


            LicenseCertificateDetial qut = new LicenseCertificateDetial { LicenceID = item.LicenseId, Function = "LICCertConf", MachineName = GetMachineId(), CertSerialNo = ReceiptID, PrintResult = "1", VRN = item.RegistrationNo };

            PdfArgumentsTPIPolicy objArg = new PdfArgumentsTPIPolicy();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = PartnerToken;
            objArg.Request = qut;

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            PdfQuoteRequestTPIPolicy objroot = new PdfQuoteRequestTPIPolicy();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //  var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            Service_db.WriteIceCashLog(data, response.Content, "LICCertConf", vehicleDetails.RegistrationNo);
            return json;
        }

        public static string GetMachineId()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            return Convert.ToString(cpuInfo);
        }

        //14-Feb-2019

        public ResultRootObject ZineraLICQuote(string RegistrationNo, string parternToken, string _clientIdType, string PaymentTermId, string ProductId, string IDsNumber, CustomerModel CustomerInfo)
        {
            var requestToken = Service_db.GetLatestToken();

            if (requestToken != null)
            {
                parternToken = requestToken.Token;
            }

            int RadioTVUsage = 1; // for private car

            if (ProductId == null)
            {
                RadioTVUsage = 1;
            }
            else if (ProductId == "3" || ProductId == "11") // fr 
            {
                RadioTVUsage = 2;
            }


            int licenseFreequency = GetMonthKey(Convert.ToInt32(PaymentTermId));

            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleLicObject> obj = new List<VehicleLicObject>();
            //var CustomerInfo = (CustomerModel)HttpContext.Current.Session["CustomerDataModal"];

            //foreach (var item in listofvehicles)
            //{
            obj.Add(new VehicleLicObject
            {
                VRN = RegistrationNo,
                IDNumber = IDsNumber,
                ClientIDType = _clientIdType,
                FirstName = CustomerInfo.FirstName,
                LastName = CustomerInfo.LastName,
                Address1 = CustomerInfo.AddressLine1,
                Address2 = CustomerInfo.AddressLine2,
                SuburbID = "2",
                LicFrequency = licenseFreequency,
                RadioTVUsage = RadioTVUsage,
                RadioTVFrequency = licenseFreequency
            });
            //}

            LICQuoteArguments objArg = new LICQuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = parternToken;
            objArg.Request = new LICQuoteFunctionObject { Function = "LICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICQuoteRequest objroot = new LICQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            string branchId = CustomerInfo == null ? "" : Convert.ToString(CustomerInfo.BranchId);

            Service_db.WriteIceCashLog(data, response.Content, "LICQuote", RegistrationNo, branchId);


            return json;
        }



        public ResultRootObject ZineraAndRadioLICQuote(string RegistrationNo, string parternToken, string _clientIdType,  string ProductId, string IDsNumber, CustomerModel CustomerInfo, string zinaraPaymentTermId, string radioPaymentTermId)
        {
            var requestToken = Service_db.GetLatestToken();

            if (requestToken != null)
            {
                parternToken = requestToken.Token;
            }

            int RadioTVUsage = 1; // for private car

            if (ProductId == null)
            {
                RadioTVUsage = 1;
            }
            else if (ProductId == "3" || ProductId == "11") // fr 
            {
                RadioTVUsage = 2;
            }


            int licenseFreequency = GetMonthKey(Convert.ToInt32(zinaraPaymentTermId));

            int radioFreequency = GetMonthKey(Convert.ToInt32(radioPaymentTermId));

            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleLicObject> obj = new List<VehicleLicObject>();
            //var CustomerInfo = (CustomerModel)HttpContext.Current.Session["CustomerDataModal"];

            //foreach (var item in listofvehicles)
            //{
            obj.Add(new VehicleLicObject
            {
                VRN = RegistrationNo,
                IDNumber = IDsNumber,
                ClientIDType = _clientIdType,
                FirstName = CustomerInfo.FirstName,
                LastName = CustomerInfo.LastName,
                Address1 = CustomerInfo.AddressLine1,
                Address2 = CustomerInfo.AddressLine2,
                SuburbID = "2",
                LicFrequency = licenseFreequency,
                RadioTVUsage = RadioTVUsage,
                RadioTVFrequency = radioFreequency
            });
            //}

            LICQuoteArguments objArg = new LICQuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = parternToken;
            objArg.Request = new LICQuoteFunctionObject { Function = "LICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICQuoteRequest objroot = new LICQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            string branchId = CustomerInfo == null ? "" : Convert.ToString(CustomerInfo.BranchId);

            Service_db.WriteIceCashLog(data, response.Content, "LICQuote", RegistrationNo, branchId);


            return json;
        }



        public ResultRootObject ZineraLICQuoteOnly(string RegistrationNo, string parternToken, string _clientIdType, string PaymentTermId, string ProductId, string IDsNumber, CustomerModel CustomerInfo)
        {
            var requestToken = Service_db.GetLatestToken();

            if (requestToken != null)
            {
                parternToken = requestToken.Token;
            }

            int RadioTVUsage = 1; // for private car

            if (ProductId == null)
            {
                RadioTVUsage = 1;
            }
            else if (ProductId == "3" || ProductId == "11") // fr 
            {
                RadioTVUsage = 2;
            }


            int licenseFreequency = GetMonthKey(Convert.ToInt32(PaymentTermId));

            //string PSK = "127782435202916376850511";
            string _json = "";

            List<VehicleLicOnlyObject> obj = new List<VehicleLicOnlyObject>();
            //var CustomerInfo = (CustomerModel)HttpContext.Current.Session["CustomerDataModal"];

            //foreach (var item in listofvehicles)
            //{
            obj.Add(new VehicleLicOnlyObject
            {
                VRN = RegistrationNo,
                IDNumber = IDsNumber,
                ClientIDType = _clientIdType,
                FirstName = CustomerInfo.FirstName,
                LastName = CustomerInfo.LastName,
                Address1 = CustomerInfo.AddressLine1,
                Address2 = CustomerInfo.AddressLine2,
                SuburbID = "2",
                LicFrequency = licenseFreequency
                //RadioTVUsage = RadioTVUsage,
                //RadioTVFrequency = licenseFreequency
            });
            //}

            LICOnlyQuoteArguments objArg = new LICOnlyQuoteArguments();
            objArg.PartnerReference = Guid.NewGuid().ToString();
            objArg.Date = DateTime.Now.ToString("yyyyMMddhhmmss");
            objArg.Version = "2.0";
            objArg.PartnerToken = parternToken;
            objArg.Request = new LICOnlyQuoteFunctionObject { Function = "LICQuote", Vehicles = obj };

            _json = Newtonsoft.Json.JsonConvert.SerializeObject(objArg);

            //string  = json.Reverse()
            string reversejsonString = new string(_json.Reverse().ToArray());
            string reversepartneridString = new string(PSK.Reverse().ToArray());

            string concatinatedString = reversejsonString + reversepartneridString;

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concatinatedString);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            string GetSHA512encrypted = SHA512(returnValue);

            string MAC = "";

            for (int i = 0; i < 16; i++)
            {
                MAC += GetSHA512encrypted.Substring((i * 8), 1);
            }

            MAC = MAC.ToUpper();

            LICOnlyQuoteRequest objroot = new LICOnlyQuoteRequest();
            objroot.Arguments = objArg;
            objroot.MAC = MAC;
            objroot.Mode = "SH";

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(objroot);

            JObject jsonobject = JObject.Parse(data);

            var client = new RestClient(SandboxIceCashApi);
            //var client = new RestClient(LiveIceCashApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResultRootObject json = JsonConvert.DeserializeObject<ResultRootObject>(response.Content);

            string branchId = CustomerInfo == null ? "" : Convert.ToString(CustomerInfo.BranchId);

            Service_db.WriteIceCashLog(data, response.Content, "LICQuote", RegistrationNo, branchId);


            return json;
        }






        public int GetRadioTvUsage(string ProductId)
        {
            int RadioTVUsage = 1; // for private car

            if (ProductId == null)
            {
                RadioTVUsage = 1;
            }
            else if (ProductId == "3" || ProductId == "11") // fr 
            {
                RadioTVUsage = 2;
            }
            return RadioTVUsage;
        }
        public int GetMonthKey(int monthId)
        {

            int licFreequency = 0;

            switch (monthId)
            {
                case 1: // represent to 12 month
                    licFreequency = 3;
                    break;
                case 2:
                    Console.WriteLine("Case 2");
                    break;
                case 3:
                    Console.WriteLine("Case 1");
                    break;
                case 4:
                    licFreequency = 1;
                    break;
                case 5:
                    licFreequency = 4;
                    break;
                case 6:
                    licFreequency = 2;
                    break;
                case 7:
                    licFreequency = 5;
                    break;
                case 8:
                    licFreequency = 6;
                    break;
                case 9:
                    licFreequency = 7;
                    break;
                case 10:
                    licFreequency = 8;
                    break;
                case 11:
                    licFreequency = 9;
                    break;
                default:
                    licFreequency = 3;
                    break;
            }

            return licFreequency;
        }


    }

    public class Arguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public FunctionObject Request { get; set; }
    }
    public class FunctionObject
    {
        public string Function { get; set; }
    }
    public class ICERootObject
    {
        public Arguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class VehicleObjectVRN
    {
        public string VRN { get; set; }

        public String CompanyName { get; set; }

        //public string PartnerToken { get; set; }
        //public string PaymentTerm { get; set; }
        //public string CoverTypeId { get; set; }
        //public string ProductId { get; set; }
        //public string MakeId { get; set; }
        //public string ModelId { get; set; }
        //public string TaxClassId { get; set; }
        //public string VehicleYear { get; set; }


        //string RegistrationNo, string PartnerToken, string PartnerReference

        //  , string PaymentTerm, string CoverTypeId, string ProductId, string MakeId, string ModelId, string TaxClassId, string VehicleYear

    }

    public class LICInsuranceQuoteRequest
    {
        public LICInsuranceQuoteArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class LICInsuranceNewQuoteRequest
    {
        public LICInsuranceNewQuoteArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }


    public class LICInsuranceZinaraRequest
    {
        public LicInsuraceZinraObjectArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }


    public class RadioQuoteRequest
    {
        public RadioQuoteArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }



    public class LICQuoteUpdateRequest
    {
        public LICQuoteUpdateArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }


    public class LICResultRequest
    {
        public LICResultArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class LICInsuranceQuoteArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICInsuranceQuoteFunctionObject Request { get; set; }
    }


    public class LICInsuranceNewQuoteArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICInsuranceNewQuoteObject Request { get; set; }
    }

    public class LicInsuraceZinraObjectArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICInsuranceZinaraNewQuoteObject Request { get; set; }
    }


    //LicInsuraceZinraObject

    public class RadioQuoteArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public RadioLICIQuoteFunctionObject Request { get; set; }
    }





    public class LICQuoteUpdateArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICQuoteUpdateFunctionObject Request { get; set; }
    }


    public class LICResultArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICResultFunctionObject Request { get; set; }
    }


    public class LICResultFunctionObject
    {
        public string Function { get; set; }
        //public string Identifier { get; set; }
        //public string MSISDN { get; set; }
        public string LicenceID { get; set; }
    }


    public class LICQuoteUpdateFunctionObject
    {
        public string Function { get; set; }
        public string PaymentMethod { get; set; }
        public string Identifier { get; set; }
        public string MSISDN { get; set; }
        public List<VehicleLicUpdateObject> Quotes { get; set; }
    }



    public class LICInsuranceQuoteFunctionObject
    {
        public string Function { get; set; }
        public List<VehicleLicInsuraceObject> Vehicles { get; set; }
    }

    public class LICInsuranceNewQuoteObject
    {
        public string Function { get; set; }
        public List<LicInsuraceObject> Vehicles { get; set; }
    }

    public class LICInsuranceZinaraNewQuoteObject
    {
        public string Function { get; set; }
        public List<LicInsuraceZinraObject> Vehicles { get; set; }
    }

    /// <summary>
    /// LicInsuraceObject
    /// </summary>


    public class RadioLICIQuoteFunctionObject
    {
        public string Function { get; set; }
        public List<VehicleRadioLiceObject> Vehicles { get; set; }
    }


    public class VehicleLicUpdateObject
    {
        public string LicenceID { get; set; }
        public string Status { get; set; }
        public string DeliveryMethod { get; set; }

        public int LicenceCert { get; set; }

        public string MachineName { get; set; }
        //  public string PaymentMethod { get; set; }
    }

    public class InsuranceLicObject
    {
        public string CombinedID { get; set; }
        public string Status { get; set; }
        public string DeliveryMethod { get; set; }

        public int LicenceCert { get; set; }
    }


    public class VehicleLicInsuraceObject
    {
        public string VRN { get; set; }
        public string EntityType { get; set; }
        public string ClientIDType { get; set; }
        public string IDNumber { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }


        public string SuburbID { get; set; }
        public string InsuranceType { get; set; }
        public string VehicleType { get; set; }
        public string VehicleValue { get; set; }

        public string DurationMonths { get; set; }
        public string LicFrequency { get; set; }
        public string RadioTVUsage { get; set; }
        public string RadioTVFrequency { get; set; }
        public string NettMass { get; set; }
        public string LicExpiryDate { get; set; }
        public string TransactionAmt { get; set; }
        public string ArrearsAmt { get; set; }
        public string PenaltiesAmt { get; set; }
        public string AdministrationAmt { get; set; }
        public string TotalLicAmt { get; set; }
        public string RadioTVAmt { get; set; }
        public string RadioTVArrearsAmt { get; set; }
        public string TotalRadioTVAmt { get; set; }

        public string TotalAmount { get; set; }



    }

    //            RadioTVUsage = riskDetail.radioTvUsage,
    //            RadioTVFrequency = riskDetail.RadioFreequency

    public class LicInsuraceObject
    {
        public string VRN { get; set; }
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string SuburbID { get; set; }
        public string ClientIDType { get; set; }
        public string InsuranceType { get; set; }
        public string TaxClass { get; set; }
        public string VehicleType { get; set; }
        public string DurationMonths { get; set; }
        public string LicFrequency { get; set; }
        public string RadioTVUsage { get; set; }
        public string RadioTVFrequency { get; set; }
        public string VehicleValue { get; set; }


    }


    public class LicInsuraceZinraObject
    {
        public string VRN { get; set; }
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string SuburbID { get; set; }
        public string ClientIDType { get; set; }
        public string InsuranceType { get; set; }

        public string TaxClass { get; set; }

        public string VehicleType { get; set; }

        public string DurationMonths { get; set; }
        public string LicFrequency { get; set; }

        public string VehicleValue { get; set; }

    }




    public class VehicleRadioLiceObject
    {
        public string VRN { get; set; }
        public string EntityType { get; set; }
        public string ClientIDType { get; set; }
        public string IDNumber { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }


        public string SuburbID { get; set; }
        public string InsuranceType { get; set; }
        public string VehicleType { get; set; }
        public string VehicleValue { get; set; }

        public string DurationMonths { get; set; }
        public string LicFrequency { get; set; }
        public string RadioTVUsage { get; set; }
        public string RadioTVFrequency { get; set; }
        public string NettMass { get; set; }
        public string LicExpiryDate { get; set; }
        public string TransactionAmt { get; set; }
        public string ArrearsAmt { get; set; }
        public string PenaltiesAmt { get; set; }
        public string AdministrationAmt { get; set; }
        public string TotalLicAmt { get; set; }
        public string RadioTVAmt { get; set; }
        public string RadioTVArrearsAmt { get; set; }
        public string TotalRadioTVAmt { get; set; }

        public string TotalAmount { get; set; }
    }

    public class VehicleObjectWithNullable
    {
        public string VRN { get; set; }
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string EntityType { get; set; }
        public string CompanyName { get; set; }
        public string DurationMonths { get; set; }
        public string VehicleValue { get; set; }
        public string InsuranceType { get; set; }
        public string VehicleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string TaxClass { get; set; }
        public string YearManufacture { get; set; }
    }

    public class QuoteArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public QuoteFunctionObject Request { get; set; }
    }

    public class QuoteArgumentsVRN
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public QuoteFunctionObjectVRN Request { get; set; }
    }



    public class QuoteFunctionObjectVRN
    {
        public string Function { get; set; }
        public List<VehicleObjectVRN> Vehicles { get; set; }
        //  public List<VehicleObject> Vehicles { get; set; }
    }
    public class QuoteFunctionObject
    {
        public string Function { get; set; }
        public List<VehicleObject> Vehicles { get; set; }
    }
    public class ICEQuoteRequest
    {
        public QuoteArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class ICEQuoteRequestVRN
    {
        public QuoteArgumentsVRN Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }


    public class TokenReposone
    {
        public string Function { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public string PartnerToken { get; set; }
        public string ExpireDate { get; set; }
    }
    public class ICEcashTokenResponse
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public TokenReposone Response { get; set; }

        public Quote Quotes { get; set; }
    }
    public class Quote
    {
        public string VRN { get; set; }
        public string InsuranceID { get; set; }
        public int Result { get; set; }
        public string Message { get; set; }
    }
    public class QuoteResponse
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public List<Quote> Quotes { get; set; }
    }
    public class ICEcashQuoteResponse
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public QuoteResponse Response { get; set; }
    }

    public class ResultClient
    {
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string EntityType { get; set; }
        public string CompanyName { get; set; }
    }

    public class ResultPolicy
    {
        public string InsuranceType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DurationMonths { get; set; }
        public string Amount { get; set; }
        public string StampDuty { get; set; }
        public string GovernmentLevy { get; set; }
        public string CoverAmount { get; set; }
        public string PremiumAmount { get; set; }
    }
    public class ResultVehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string TaxClass { get; set; }
        public string YearManufacture { get; set; }
        public int VehicleType { get; set; }
        public string VehicleValue { get; set; }
    }
    public class ResultQuote
    {
        public string VRN { get; set; }
        public string InsuranceID { get; set; }
        public int Result { get; set; }
        public string LicenceID { get; set; }
        public string CombinedID { get; set; }
        public string Message { get; set; }
        public decimal TotalLicAmt { get; set; }
        public decimal TransactionAmt { get; set; }

        public decimal AdministrationAmt { get; set; }
        public decimal PenaltiesAmt { get; set; }
        public decimal ArrearsAmt { get; set; }
        public decimal RadioTVAmt { get; set; }
        public string LicExpiryDate { get; set; }

        public string RadioTVExpiryDate { get; set; }

        public string LicenceCert { get; set; }

        public ResultPolicy Policy { get; set; }
        public ResultClient Client { get; set; }
        public ResultVehicle Vehicle { get; set; }
        public VehicleLicInsuraceObject Licence { get; set; }
    }
    public class ResultResponse
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public string PolicyNo { get; set; }

        public string Status { get; set; }
        public string VRN { get; set; }
        public List<ResultQuote> Quotes { get; set; }
    }


    public class ResultLicenceIDResponse
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public string LicenceID { get; set; }
        //  public string PolicyNo { get; set; }
        public string VRN { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TaxClass { get; set; }
        public string LicExpiryDate { get; set; }
        public string ArrearsAmt { get; set; }
        public string PenaltiesAmt { get; set; }
        public string AdministrationAmt { get; set; }
        public string TransactionAmt { get; set; }
        public string TotalAmount { get; set; }
        public string ApprovedBy { get; set; }

        public string ApprovedDate { get; set; }
        public string ReceiptID { get; set; }
        public string TotalLicAmt { get; set; }
        public string RadioTVAmt { get; set; }

        public string make { get; set; }
        public string model { get; set; }
        public string vehicleUsage { get; set; }

        public string LicenceCert { get; set; }

        public string PolicyNo { get; set; }

    }


    public class ResultRootObject
    {
        public decimal LoyaltyDiscount { get; set; }
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public ResultResponse Response { get; set; }
    }

    public class ResultLicenceIDRootObject
    {
        public decimal LoyaltyDiscount { get; set; }
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public ResultLicenceIDResponse Response { get; set; }
    }

    public class ResultRootObjects
    {
        public decimal LoyaltyDiscount { get; set; }
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public ResultResponse Response { get; set; }
    }


    public class RequestTPIQuoteUpdate
    {
        public string Function { get; set; }
        public string PaymentMethod { get; set; }
        public string Identifier { get; set; }
        public string MSISDN { get; set; }
        public List<QuoteDetial> Quotes { get; set; }
    }

    public class RequestTPILICUpdate
    {
        public string Function { get; set; }
        public string PaymentMethod { get; set; }
        public string Identifier { get; set; }
        public string MSISDN { get; set; }
        public List<QuoteCombineDetial> Quotes { get; set; }
    }


    public class QuoteDetial
    {
        public string InsuranceID { get; set; }

        public string Status { get; set; }
    }


    public class QuoteCombineDetial
    {
        public string CombinedID { get; set; }

        public string Status { get; set; }

        public string DeliveryMethod { get; set; }

        public string LicenceCert {get; set;}
    }

    public class QuoteArgumentsTPIQuote
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public RequestTPIQuoteUpdate Request { get; set; }
    }


    public class QuoteArgumentsTPILICUpdate
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public RequestTPILICUpdate Request { get; set; }
    }



    

    public class ICEQuoteRequestTPIQuote
    {
        public QuoteArgumentsTPIQuote Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class ICEQuoteRequestTPILICUpdate
    {
        public QuoteArgumentsTPILICUpdate Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class checkVRNwithICEcashResponse
    {
        public int result { get; set; }
        public string message { get; set; }
        public ResultRootObject Data { get; set; }
    }

    public class VehicleObject
    {
        public string VRN { get; set; }
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MSISDN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string EntityType { get; set; }
        public string CompanyName { get; set; }
        //public int DurationMonths { get; set; }
        //public int VehicleValue { get; set; }
        //public int InsuranceType { get; set; }
        //public int VehicleType { get; set; }

        public string DurationMonths { get; set; }


        public string VehicleValue { get; set; }
        public string InsuranceType { get; set; }
        public string VehicleType { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }

        public string TaxClass { get; set; }
        public string YearManufacture { get; set; }
        //public int TaxClass { get; set; }
        //public int YearManufacture { get; set; }
    }

    public class ICEQuoteRequestTPIPolicy
    {
        public QuoteArgumentsTPIPolicy Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class ICEQuoteRequestTPILICResult
    {
        public QuoteArgumentsTPILICResult Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    //QuoteArgumentsTPILICResult

    public class PdfQuoteRequestTPIPolicy
    {
        public PdfArgumentsTPIPolicy Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class TPIPolicyDetial
    {
        public string InsuranceID { get; set; }
        public string Function { get; set; }
    }


    public class TPILICResult
    {
        public string CombinedID { get; set; }
        public string Function { get; set; }
    }


    public class LicenseCertificateDetial
    {
        public string LicenceID { get; set; }
        public string MachineName { get; set; }

        public string CertSerialNo { get; set; }
        public string PrintResult { get; set; }

        public string Function { get; set; }
        public string VRN { get; set; }
    }



    public class QuoteArgumentsTPIPolicy
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public TPIPolicyDetial Request { get; set; }
    }

    public class QuoteArgumentsTPILICResult
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public TPILICResult Request { get; set; }
    }

   


    public class PdfArgumentsTPIPolicy
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LicenseCertificateDetial Request { get; set; }
    }




    public class VehicleLicObject
    {
        public string VRN { get; set; }
        public string IDNumber { get; set; }
        public string ClientIDType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string SuburbID { get; set; }
        public int LicFrequency { get; set; }
        public int RadioTVUsage { get; set; }
        public int RadioTVFrequency { get; set; }

    }



    public class VehicleLicOnlyObject
    {
        public string VRN { get; set; }
        public string IDNumber { get; set; }
        public string ClientIDType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string SuburbID { get; set; }
        public int LicFrequency { get; set; }
     

    }


    public class LICQuoteArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICQuoteFunctionObject Request { get; set; }
    }
    public class LICOnlyQuoteArguments
    {
        public string PartnerReference { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string PartnerToken { get; set; }
        public LICOnlyQuoteFunctionObject Request { get; set; }
    }


    public class LICQuoteFunctionObject
    {
        public string Function { get; set; }
        public List<VehicleLicObject> Vehicles { get; set; }
    }


    public class LICOnlyQuoteFunctionObject
    {
        public string Function { get; set; }
        public List<VehicleLicOnlyObject> Vehicles { get; set; }
    }


    


    public class LICQuoteRequest
    {
        public LICQuoteArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }

    public class LICOnlyQuoteRequest
    {
        public LICOnlyQuoteArguments Arguments { get; set; }
        public string MAC { get; set; }
        public string Mode { get; set; }
    }




}
