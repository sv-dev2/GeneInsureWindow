using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using InsuranceClaim.Models;
using RestSharp;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Insurance.Service;
using GensureAPIv2.Models;

namespace Gene
{
    public partial class License_PrintPreview : Form
    {

        Bitmap bitmap;


        static String ApiURL = "http://windowsapi.gene.co.zw/api/Account/";

        // static String ApiURL = "http://geneinsureclaim2.kindlebit.com/api/Account/";

        //  static String ApiURL = "http://localhost:6220/api/Account/";


        static String username = "ameyoApi@geneinsure.com";
        static String Pwd = "Geninsure@123";

        string _registrationNumber = "";

        Int32 counter = 0;
        ICEcashTokenResponse ObjToken;
        ICEcashService IcServiceobj = new ICEcashService();


        string parternToken = "";
        List<ResultLicenceIDResponse> licenseDiskList = new List<ResultLicenceIDResponse>();


        public License_PrintPreview(string registrationNumber)
        {
            _registrationNumber = registrationNumber;
            InitializeComponent();
        }


        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            bitmap = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(bitmap);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }



        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void PrintPreview_Load(object sender, EventArgs e)
        {
              loadPolicyPanel();
            //loadLicenseAndInsuranceByVrn();

           // this.Activated += btnPrint_Click;
        }

        private List<PrintDetail> GetPolicyDetials(string policyNumber)
        {
            var client = new RestClient(ApiURL + "GetPolicyDetials?policyNumber=" + policyNumber);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<PrintDetail> result = (new JavaScriptSerializer()).Deserialize<List<PrintDetail>>(response.Content);
            return result;
        }



        private VehicleDetails GetVehicelDetials(string vrn)
        {
            var client = new RestClient(ApiURL + "GetVehicelDetails?vrn=" + vrn);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<VehicleDetails>(response.Content);
            return result;
        }




      



        public void loadPolicyPanel()
        {

            var objlistRisk = GetPolicyDetials(_registrationNumber);

            pnlSummery.Controls.Clear(); //to remove all controls

            counter = objlistRisk.Count();

            for (int i = 0; i < counter; i++)
            {

                if (counter == 1)
                {

                    Panel Bottompnl = new Panel();

                    Label lblCustomer = new System.Windows.Forms.Label();
                    lblCustomer.Name = lblCustomer + i.ToString();
                    lblCustomer.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblCustomer.Text = "Name:                            ";
                    lblCustomer.Text += objlistRisk[i].CustomerName == null ? "0" : Convert.ToString(objlistRisk[i].CustomerName);
                    lblCustomer.AutoSize = true;
                    lblCustomer.BackColor = Color.Transparent;
                    lblCustomer.Location = new Point(i, 50);
                    lblCustomer.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblCustomer);


                    Label lblVehicleRegNum = new System.Windows.Forms.Label();
                    lblVehicleRegNum.Name = lblVehicleRegNum + i.ToString();
                    lblVehicleRegNum.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblVehicleRegNum.Text = "Vehicle Reg Num:           ";
                    lblVehicleRegNum.Text += objlistRisk[i].VRN;
                    lblVehicleRegNum.AutoSize = true;
                    lblVehicleRegNum.BackColor = Color.Transparent;
                    lblVehicleRegNum.Location = new Point(i, 100);
                    lblVehicleRegNum.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblVehicleRegNum);


                    Label lblMakeModel = new System.Windows.Forms.Label();
                    lblMakeModel.Name = lblMakeModel + i.ToString();
                    lblMakeModel.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblMakeModel.Text = "Vehicle:                         ";
                    lblMakeModel.Text += objlistRisk[i].Make + " " + objlistRisk[i].Model;
                    lblMakeModel.AutoSize = true;
                    lblMakeModel.BackColor = Color.Transparent;
                    lblMakeModel.Location = new Point(i, 150);
                    lblMakeModel.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblMakeModel);


                    Label lblCoverType = new System.Windows.Forms.Label();
                    lblCoverType.Name = lblCoverType + i.ToString();
                    lblCoverType.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblCoverType.Text = "Cover Type:                   ";
                    lblCoverType.Text += objlistRisk[i].CoverType;
                    lblCoverType.AutoSize = true;
                    lblCoverType.BackColor = Color.Transparent;
                    lblCoverType.Location = new Point(i, 200);
                    lblCoverType.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblCoverType);


                    Label lblPaymentTerm = new System.Windows.Forms.Label();
                    lblPaymentTerm.Name = lblCoverType + i.ToString();
                    lblPaymentTerm.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblPaymentTerm.Text = "Payment Term:                ";
                    lblPaymentTerm.Text += objlistRisk[i].PaymentTerm;
                    lblPaymentTerm.AutoSize = true;
                    lblPaymentTerm.BackColor = Color.Transparent;
                    lblPaymentTerm.Location = new Point(i, 250);
                    lblPaymentTerm.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblPaymentTerm);


                    Label lblAmountPaid = new System.Windows.Forms.Label();
                    lblAmountPaid.Name = lblCoverType + i.ToString();
                    lblAmountPaid.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblAmountPaid.Text = "Amount Paid:                   ";
                    lblAmountPaid.Text += objlistRisk[i].AmountPaid;
                    lblAmountPaid.AutoSize = true;
                    lblAmountPaid.BackColor = Color.Transparent;
                    lblAmountPaid.Location = new Point(i, 300);
                    lblAmountPaid.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblAmountPaid);

                    Bottompnl.Size = new System.Drawing.Size(697, 1000);
                    Bottompnl.Location = new Point(i, (i * 1020));


                    pnlSummery.Controls.Add(Bottompnl);

                }
                else if (counter == 2)
                {
                    Panel Bottompnl = new Panel();


                    Label lblCustomer = new System.Windows.Forms.Label();
                    lblCustomer.Name = lblCustomer + i.ToString();
                    lblCustomer.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblCustomer.Text = "Name:                            ";
                    lblCustomer.Text += objlistRisk[i].CustomerName == null ? "0" : Convert.ToString(objlistRisk[i].CustomerName);
                    lblCustomer.AutoSize = true;
                    lblCustomer.BackColor = Color.Transparent;
                    lblCustomer.Location = new Point(i, 50);
                    lblCustomer.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblCustomer);



                    Label lblVehicleRegNum = new System.Windows.Forms.Label();
                    lblVehicleRegNum.Name = lblVehicleRegNum + i.ToString();
                    lblVehicleRegNum.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblVehicleRegNum.Text = "Vehicle Reg Num:           ";
                    lblVehicleRegNum.Text += objlistRisk[i].VRN;
                    lblVehicleRegNum.AutoSize = true;
                    lblVehicleRegNum.BackColor = Color.Transparent;
                    lblVehicleRegNum.Location = new Point(i, 90);
                    lblVehicleRegNum.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblVehicleRegNum);


                    Label lblMakeModel = new System.Windows.Forms.Label();
                    lblMakeModel.Name = lblMakeModel + i.ToString();
                    lblMakeModel.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblMakeModel.Text = "Vehicle:                         ";
                    lblMakeModel.Text += objlistRisk[i].Make + " " + objlistRisk[i].Model;
                    lblMakeModel.AutoSize = true;
                    lblMakeModel.BackColor = Color.Transparent;
                    lblMakeModel.Location = new Point(i, 130);
                    lblMakeModel.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblMakeModel);


                    Label lblCoverType = new System.Windows.Forms.Label();
                    lblCoverType.Name = lblCoverType + i.ToString();
                    lblCoverType.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblCoverType.Text = "Cover Type:                   ";
                    lblCoverType.Text += objlistRisk[i].CoverType;
                    lblCoverType.AutoSize = true;
                    lblCoverType.BackColor = Color.Transparent;
                    lblCoverType.Location = new Point(i, 170);
                    lblCoverType.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblCoverType);


                    Label lblPaymentTerm = new System.Windows.Forms.Label();
                    lblPaymentTerm.Name = lblCoverType + i.ToString();
                    lblPaymentTerm.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblPaymentTerm.Text = "Payment Term:                ";
                    lblPaymentTerm.Text += objlistRisk[i].PaymentTerm;
                    lblPaymentTerm.AutoSize = true;
                    lblPaymentTerm.BackColor = Color.Transparent;
                    lblPaymentTerm.Location = new Point(i, 210);
                    lblPaymentTerm.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblPaymentTerm);


                    Label lblAmountPaid = new System.Windows.Forms.Label();
                    lblAmountPaid.Name = lblCoverType + i.ToString();
                    lblAmountPaid.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblAmountPaid.Text = "Amount Paid:                   ";
                    lblAmountPaid.Text += objlistRisk[i].AmountPaid;
                    lblAmountPaid.AutoSize = true;
                    lblAmountPaid.BackColor = Color.Transparent;
                    lblAmountPaid.Location = new Point(i, 250);
                    lblAmountPaid.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblAmountPaid);

                    Bottompnl.Size = new System.Drawing.Size(697, 280);
                    Bottompnl.Location = new Point(i, (i * 290));


                    pnlSummery.Controls.Add(Bottompnl);
                }
                else
                {
                    Panel Bottompnl = new Panel();
                    Bottompnl.BackColor = Color.White;


                    Label lblCustomer = new System.Windows.Forms.Label();
                    lblCustomer.Name = lblCustomer + i.ToString();
                    lblCustomer.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblCustomer.Text = "Name :";
                    lblCustomer.Text += objlistRisk[i].CustomerName == null ? "0" : Convert.ToString(objlistRisk[i].CustomerName);
                    lblCustomer.AutoSize = true;
                    lblCustomer.BackColor = Color.Transparent;
                    lblCustomer.Location = new Point(i, 10);
                    lblCustomer.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblCustomer);



                    Label lblVehicleRegNum = new System.Windows.Forms.Label();
                    lblVehicleRegNum.Name = lblVehicleRegNum + i.ToString();
                    lblVehicleRegNum.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblVehicleRegNum.Text = "Vehicle Reg Num :";
                    lblVehicleRegNum.Text += objlistRisk[i].VRN;
                    lblVehicleRegNum.AutoSize = true;
                    lblVehicleRegNum.BackColor = Color.Transparent;
                    lblVehicleRegNum.Location = new Point(i, 40);
                    lblVehicleRegNum.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblVehicleRegNum);


                    Label lblMakeModel = new System.Windows.Forms.Label();
                    lblMakeModel.Name = lblMakeModel + i.ToString();
                    lblMakeModel.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblMakeModel.Text = "Vehicle :";
                    lblMakeModel.Text += objlistRisk[i].Make + " " + objlistRisk[i].Model;
                    lblMakeModel.AutoSize = true;
                    lblMakeModel.BackColor = Color.Transparent;
                    lblMakeModel.Location = new Point(i, 70);
                    lblMakeModel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblMakeModel);


                    Label lblCoverType = new System.Windows.Forms.Label();
                    lblCoverType.Name = lblCoverType + i.ToString();
                    lblCoverType.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblCoverType.Text = "Cover Type :";
                    lblCoverType.Text += objlistRisk[i].CoverType;
                    lblCoverType.AutoSize = true;
                    lblCoverType.BackColor = Color.Transparent;
                    lblCoverType.Location = new Point(i, 100);
                    lblCoverType.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblCoverType);


                    Label lblPaymentTerm = new System.Windows.Forms.Label();
                    lblPaymentTerm.Name = lblCoverType + i.ToString();
                    lblPaymentTerm.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblPaymentTerm.Text = "Payment Term :";
                    lblPaymentTerm.Text += objlistRisk[i].PaymentTerm;
                    lblPaymentTerm.AutoSize = true;
                    lblPaymentTerm.BackColor = Color.Transparent;
                    lblPaymentTerm.Location = new Point(i, 130);
                    lblPaymentTerm.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblPaymentTerm);


                    Label lblAmountPaid = new System.Windows.Forms.Label();
                    lblAmountPaid.Name = lblCoverType + i.ToString();
                    lblAmountPaid.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblAmountPaid.Text = "Amount Paid :";
                    lblAmountPaid.Text += objlistRisk[i].AmountPaid;
                    lblAmountPaid.AutoSize = true;
                    lblAmountPaid.BackColor = Color.Transparent;
                    lblAmountPaid.Location = new Point(i, 160);
                    lblAmountPaid.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblAmountPaid);

                    Bottompnl.Size = new System.Drawing.Size(750, 200);
                    Bottompnl.Location = new Point(i, (i * 220));


                    pnlSummery.Controls.Add(Bottompnl);
                }

            }
        }





        private List<ResultLicenceIDResponse> DisplayLicenseDisc(RiskDetailModel riskDetailModel, string parterToken)
        {
            // List<ResultLicenceIDResponse> list = new List<ResultLicenceIDResponse>();

            ObjToken = IcServiceobj.getToken();
            if(ObjToken!=null)
            {
                parternToken = ObjToken.Response.PartnerToken;
            }
          

            ResultLicenceIDRootObject quoteresponseResult = IcServiceobj.LICResult(riskDetailModel.LicenseId, parternToken);
            if (quoteresponseResult != null && quoteresponseResult.Response.Message.Contains("Partner Token has expired"))
            {
                ObjToken = IcServiceobj.getToken();
                if (ObjToken != null)
                {
                    parternToken = ObjToken.Response.PartnerToken;
                    //  quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 
                    quoteresponseResult = IcServiceobj.LICResult(riskDetailModel.LicenseId, parternToken);

                }
            }

            if (quoteresponseResult.Response != null)
            {
                licenseDiskList.Add(quoteresponseResult.Response);
            }

            // 
            return licenseDiskList;
        }








        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.Activated -= btnPrint_Click;

            if (counter==0)
            {
                MessageBox.Show("No records found.");
                return;
            }

            Panel panel = new Panel();
            this.Controls.Add(panel);
            Graphics grp = panel.CreateGraphics();
            Size formSize = this.ClientSize;
            bitmap = new Bitmap(formSize.Width, formSize.Height, grp);
            grp = Graphics.FromImage(bitmap);
            Point panelLocation = PointToScreen(panel.Location);
            grp.CopyFromScreen(panelLocation.X, panelLocation.Y, 0, 0, formSize);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();


        }




    }
}
