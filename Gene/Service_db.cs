using Insurance.Service;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace Gene
{
    public class Service_db
    {

        //ResultRootObjects _quoteresponse;
        //static String ApiURL = "http://windowsapi.gene.co.zw/api/Account/";
        //static String IceCashRequestUrl = "http://windowsapi.gene.co.zw/api/ICEcash/";

        static String ApiURL = "http://geneinsureclaim2.kindlebit.com/api/Account/";
        static String IceCashRequestUrl = "http://geneinsureclaim2.kindlebit.com/api/ICEcash/";

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





    }
}
