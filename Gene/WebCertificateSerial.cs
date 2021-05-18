using GensureAPIv2.Models;
using Insurance.Service;
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


    }
}
