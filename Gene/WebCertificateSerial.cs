using GensureAPIv2.Models;
using Insurance.Service;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;

namespace Gene
{
    public partial class WebCertificateSerial : Form
    {
        public RiskDetailModel RiskDetailModel;
        public string ParternToken { get; set; }

        ICEcashTokenResponse ObjToken;
        ICEcashService IcServiceobj;

        public string _base64Data = "";
        // static String ApiURL = "http://windowsapi.gene.co.zw/api/Account/";
        static String ApiURL = WebConfigurationManager.AppSettings["urlPath"] + "/api/Account/";
        static String IceCashRequestUrl = WebConfigurationManager.AppSettings["urlPath"] + "/api/ICEcash/";

        public WebCertificateSerial(RiskDetailModel objRiskDetail, string Partnertoken )
        {
            InitializeComponent();

            RiskDetailModel = objRiskDetail;
            ParternToken = Partnertoken;
            IcServiceobj = new ICEcashService();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            GotoHome();
        }


        public void GotoHome()
        {
            Form1 objFrm = new Form1(ObjToken);
            objFrm.Show();
            this.Close();
        }

        private void txtCertificateSerialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (valatedSerialNumber(txtCertificateSerialNumber.Text))
                    {

                        CertSerialNoDetailModel model = new CertSerialNoDetailModel();
                        model.VehicleId = RiskDetailModel.Id;
                        model.CertSerialNo = txtCertificateSerialNumber.Text;
                        SaveCertSerialNum(model);



                        frmLicence quotObj = new frmLicence();
                        quotObj.CertificateNumber = txtCertificateSerialNumber.Text;
                        var response = ICEcashService.LICCertConf(RiskDetailModel, ParternToken, txtCertificateSerialNumber.Text);

                        if (response != null && response.Response.Message.Contains("Partner Token has expired"))
                        {
                            ObjToken = IcServiceobj.getToken();
                            ParternToken = ObjToken.Response.PartnerToken;
                            Service_db.UpdateToken(ObjToken);
                            response = ICEcashService.LICCertConf(RiskDetailModel, ParternToken, txtCertificateSerialNumber.Text);
                        }

                        MessageBox.Show(response.Response.Message);
                        this.Close();
                        Form1 obj = new Form1();
                        obj.Show();
                    }
                    else
                    {
                        MessageBox.Show("Please Eneter the correct Serial Number", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void SaveCertSerialNum(CertSerialNoDetailModel model)
        {

            if (model.VehicleId != 0)
            {
                var client = new RestClient(IceCashRequestUrl + "SaveCertSerialNum");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("password", "Geninsure@123");
                request.AddHeader("username", "ameyoApi@geneinsure.com");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(model);

                //request.Timeout = 5000;
                //request.ReadWriteTimeout = 5000;
                IRestResponse response = client.Execute(request);


                try
                {

                    Service_db service = new Service_db();
                    string branchId = service.ReadBranchFromLogFile();
                    var apiStock = new RestClient("http://api.gene.co.zw/inventory/api/paper/usage/" + branchId + "/" + model.CertSerialNo + "");
                    var stockRequest = new RestRequest(Method.GET);
                    IRestResponse responseAPI = apiStock.Execute(stockRequest);

                }
                catch (Exception e)
                {

                }

            }
        }




        private void WebCertificateSerial_Load(object sender, EventArgs e)
        {
            txtCertificateSerialNumber.Focus();
        }

        public bool valatedSerialNumber(string serialNumber)
        {
            string pattern = @"^[a-zA-Z]{1}[0-9]{8}";
            // Create a Regex  
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);
            return rg.IsMatch(serialNumber);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (valatedSerialNumber(txtCertificateSerialNumber.Text))
            {
                //else
                //{
                //    MessageBox.Show("Please Eneter the correct Serial Number", "Error");
                //}

                CertSerialNoDetailModel model = new CertSerialNoDetailModel();
                model.VehicleId = RiskDetailModel.Id;
                model.CertSerialNo = txtCertificateSerialNumber.Text;
                SaveCertSerialNum(model);


                this.Close();
                Form1 obj = new Form1();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Please Eneter the correct Serial Number", "Error");
            }
        }
    }
}
