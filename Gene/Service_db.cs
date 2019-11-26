using Insurance.Service;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Script.Serialization;


namespace Gene
{
    public class Service_db
    {

        //ResultRootObjects _quoteresponse;
        //static string ApiURL = "http://windowsapi.gene.co.zw/api/account/";
        //static string IceCashRequestUrl = "http://windowsapi.gene.co.zw/api/icecash/";

        static String ApiURL = WebConfigurationManager.AppSettings["urlPath"] + "/api/Account/";
        static String IceCashRequestUrl = WebConfigurationManager.AppSettings["urlPath"] + "/api/ICEcash/";

        //static String ApiURL = "http://localhost:6220/api/Account/";
        //static String IceCashRequestUrl = "http://localhost:6220/api/ICEcash/";

        static String username = "ameyoApi@geneinsure.com";
        static String Pwd = "Geninsure@123";


        public static RequestToke GetLatestToken()
        {

            var client = new RestClient(IceCashRequestUrl + "GetLatestToken");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            IRestResponse response = client.Execute(request);
            RequestToke result = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestToke>(response.Content);
            return result;
        }


        public static void UpdateToken(ICEcashTokenResponse tokenObject)
        {
            var client = new RestClient(IceCashRequestUrl + "UpdateToken");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(tokenObject);

            //request.Timeout = 5000;
            //request.ReadWriteTimeout = 5000;
            IRestResponse response = client.Execute(request);
        }

        public static void WriteIceCashLog(string requestData, string responseData, string method, string vrn="", string BranchId="")
        { 
            var client = new RestClient(IceCashRequestUrl + "WriteIceCashLog?request='" + requestData + "'&response='" + responseData + "'&method='" + method +"'" + "'&vrn='" + vrn + "'" + "'&BranchId=" + BranchId );
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }



    }
}
