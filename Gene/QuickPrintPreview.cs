using GensureAPIv2.Models;
using Insurance.Service;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gene
{
    public partial class QuickPrintPreview : Form
    {
        static String IceCashRequestUrl = "http://windowsapi.gene.co.zw/api/ICEcash/";
        // static String IceCashRequestUrl = "http://geneinsureclaim2.kindlebit.com/api/ICEcash/";

        //  static String IceCashRequestUrl = "http://localhost:6220/api/ICEcash/";

        static String username = "ameyoApi@geneinsure.com";
        static String Pwd = "Geninsure@123";

        ICEcashService IcServiceobj;
        ICEcashTokenResponse ObjToken;


        string parternToken = "";
        string registrationNumber = "";

        Bitmap bitmap;

        public QuickPrintPreview(string regNum)
        {

            registrationNumber = regNum;

            InitializeComponent();
        }

        private void QuickPrintPreview_Load(object sender, EventArgs e)
        {
            GetVrnLicenseAndInsurace(registrationNumber);
        }

        private VehicleDetails GetVehicelDetials(string vrn)
        {
            var client = new RestClient(IceCashRequestUrl + "GetVehicelDetails?vrn=" + vrn);
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<VehicleDetails>(response.Content);
            return result;

        }


        private void GetVrnLicenseAndInsurace(string vrn)
        {
            vrn = "AAZ1001";
            VehicleDetails vehicleDetails = GetVehicelDetials(vrn);

            if (vehicleDetails != null)
            {
                IcServiceobj = new ICEcashService();
                ObjToken = IcServiceobj.getToken();
                parternToken = ObjToken.Response.PartnerToken;
            }

            if (vehicleDetails != null && vehicleDetails.LicenseId != null)
            {

                ObjToken = IcServiceobj.getToken();
                if (ObjToken != null)
                {
                    parternToken = ObjToken.Response.PartnerToken;
                }

                ResultLicenceIDRootObject quoteresponseResult = IcServiceobj.LICResult(vehicleDetails.LicenseId, parternToken);
                if (quoteresponseResult.Response != null)
                {
                    lblRegNum.Text = quoteresponseResult.Response.VRN;
                }
            }


        }

        private void GetInsuranceDetials(VehicleDetails vehicelDetails)
        {

            //RiskDetailModel model = new RiskDetailModel {InsuranceId= vehicelDetails.in }

            //var res = ICEcashService.TPIPolicy(vehicleDetails, parternToken);

        }




    }
}
