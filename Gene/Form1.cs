using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net.Http;
using RestSharp;
using System.Configuration;
using System.Reflection;
using Insurance.Service;
using GensureAPIv2.Models;
using System.Globalization;
using System.IO;
using Spire.Pdf;
using System.Drawing.Printing;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Xml.Serialization;
using InsuranceClaim.Models;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace Gene
{
    public partial class Form1 : Form
    {
        //ResultRootObjects _quoteresponse;

        static string ApiURL = WebConfigurationManager.AppSettings["urlPath"] + "/api/account/";
        static string IceCashRequestUrl = WebConfigurationManager.AppSettings["urlPath"] + "/api/icecash/";
      
        static String username = "ameyoApi@geneinsure.com";
        static String Pwd = "Geninsure@123";

        int selectedBranchId = 0;
        List<Branch> branchList = new List<Branch>();
        ICEcashTokenResponse _ObjToken;
     
        public Form1(ICEcashTokenResponse ObjToken = null)
        {

            //MyMessageBox.ShowBox("Please select branch.", "Modal error message");

            InitializeComponent();
            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            if (!CheckForInternetConnection())
            {
                MyMessageBox.ShowBox("Internet connection is not available, please connect to Internet.");
            }

            _ObjToken = ObjToken;

            // CreateLicenseFile("this is test file");   
            // ConverToPdf();
            BindBranch();
            GetBannerImage();

            //  var date = Convert.ToDateTime("2019/01/31");
            //  CreateLicenseFile("");
            //LincenceFileTest();

            //SaveCertificatePdf();
            //  MyMessageBox.ShowBox("Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.", "Modal error message");

            // MyMessageBox.ShowBox("Do you want to exit?", "Exit");

        }


        public void testPrintConfirmationMehtod()
        {

        }

        private void CreateLicenseFile(string base64Data)
        {
            try
            {
                string path = @"C:\Users\Public\Certificate.txt";

                // Example #2: Write one string to a text file.
                string text = "A class is the most powerful data type in C#. Like a structure, " +
                               "a class defines the data and behavior of the data type. ";
                // WriteAllText creates a file, writes the specified string to the file,
                // and then closes the file.    You do NOT need to call Flush() or Close().
                System.IO.File.WriteAllText(path, text);

            }
            catch (Exception ex)
            {

            }
        }


        public bool CheckForInternetConnection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void LincenceFileTest()
        {
            try
            {
                string textFile = @"../../Pdf/base_64_pdf.txt";

                string installedPath = Application.StartupPath + "/pdf";


                if (File.Exists(textFile))
                {
                    byte[] pdfbytes = Convert.FromBase64String(File.ReadAllText(textFile));
                    // File.WriteAllBytes(fileFullPath, pdfbytes);
                }
            }
            catch (Exception ex) { }
        }
        public void WriteLog(string error)
        {
            string message = string.Format("Error Time: {0}", DateTime.Now);
            message += error;
            message += "-----------------------------------------------------------";

            message += Environment.NewLine;

            //string path = System.Web.HttpContext.Current.Server.MapPath("~/LogFile.txt");

            string path = @"../../LogFile.txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frmClaimRegister objFrm = new frmClaimRegister();
            objFrm.Show();
            this.Hide();
        }

        private void btnNewQuote_Click(object sender, EventArgs e)
        {
            //frmNewQuote obj = new frmNewQuote();
            //obj.Show();

            //if (cmbBranch.SelectedValue != null && cmbBranch.SelectedValue.ToString() == "0")
            //{
            //    MyMessageBox.ShowBox("Please select branch.", "Modal error message");

            //    cmbBranch.Focus();
            //    return;
            //}


            if (cmbBranch.SelectedValue == null)
            {
                MyMessageBox.ShowBox("Please select branch.", "Modal error message");
                cmbBranch.Focus();
                cmbBranch.Visible = true;
                lblBranch.Visible = true;
                return;
            }


            var branch = cmbBranch.SelectedValue == null ? "" : cmbBranch.SelectedValue.ToString();
            btnNewQuote.Text = "Processing..";

            frmQuote obj = new frmQuote(branch, _ObjToken, true);
            obj.Show();
            this.Hide();
            btnNewQuote.Text = "New Quote";

        }



        private  void Form1_Load(object sender, EventArgs e)
        {           
            this.FormClosing += Form1_FormClosing_1;
        }

        private void btnQuickPrint_Click(object sender, EventArgs e)
        {        
            frmLicence objLic = new frmLicence();
            objLic.Show();
            this.Hide();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedValue == null)
            {
                MyMessageBox.ShowBox("Please select branch.", "Modal error message");
                cmbBranch.Focus();
            }
            var branch = cmbBranch.SelectedValue == null ? "" : cmbBranch.SelectedValue.ToString();

            frmRenewPolicy objRE = new frmRenewPolicy(_ObjToken, branch);
            objRE.Show();
            this.Hide();
        }

        private void SetSelectedValue(List<Branch> branchList)
        {     
            string branchId = ReadBranchFromLogFile();

            if(branchId==null || branchId =="")
            {
                MyMessageBox.ShowBox("Branch is not set, please contact to admistrator.");
                lblSelectedBranch.Text = "Select Branch";
            }

            cmbBranch.SelectedValue = branchId == "" ? 0 : Convert.ToInt32(branchId);

            if (branchId != "" && Convert.ToInt32(branchId) > 0)
            {
                cmbBranch.Visible = false;
                lblBranch.Visible = false;

                var branchDetial = branchList.FirstOrDefault(c => c.Id == Convert.ToInt32(branchId));

                if(branchDetial!=null)
                    lblSelectedBranch.Text = branchDetial.BranchName;
                else
                    lblSelectedBranch.Text = "Select Branch";

            }
        }

        public string ReadBranchFromLogFile()
        {
            string installedPath = @"C:\Users\Public\";
            string fileName = "Branch" + ".txt";
            var destinationFileName = System.IO.Path.Combine(installedPath, System.IO.Path.GetFileName(fileName));

            var res = System.IO.File.ReadAllLines(destinationFileName);

            return res[0];

        }



        public void InsertMachineBranch(string branchName1)
        {

            var ipAddress = GetAddresses();

            if (branchName1 != "GensureAPIv2.Models.Branch")
            {

                var client = new RestClient(ApiURL + "InsertMachineBranch?brachId=" + branchName1 + "&IpAddress=" + ipAddress);
                var request = new RestRequest(Method.POST);
                request.AddHeader("password", Pwd);
                request.AddHeader("username", username);
                request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                // var result = (new JavaScriptSerializer()).Deserialize<List<int>>(response.Content);
            }
        }




        //public void printPDFWithAcrobat()
        //{
        //    string Filepath = @"E:\Test\file.pdf";

        //    PdfDocument pdfdocument = new PdfDocument();
        //    pdfdocument.LoadFromFile(Filepath);
        //    pdfdocument.PrinterName = "My Printer";
        //    pdfdocument.PrintDocument.PrinterSettings.Copies = 2;
        //    pdfdocument.PrintDocument.Print();
        //    pdfdocument.Dispose();
        //}


        public string GetAddresses()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            // Get the IP  
            return Dns.GetHostByName(hostName).AddressList[0].ToString();
        }


        public void BindBranch()
        {
            try
            {
                var client = new RestClient(ApiURL + "AllBranch");
                var request = new RestRequest(Method.GET);
                request.AddHeader("password", Pwd);
                request.AddHeader("username", username);
                request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                branchList = (new JavaScriptSerializer()).Deserialize<List<Branch>>(response.Content);

                branchList.Insert(0, new Branch { Id = 0, BranchName = "-Select-" });

                cmbBranch.DataSource = branchList;
                cmbBranch.DisplayMember = "BranchName";
                cmbBranch.ValueMember = "Id";

                cmbBranch.SelectedIndex = 0;

                SetSelectedValue(branchList);
            }
            catch (Exception ex)
            {

            }
        }


        public void GetBannerImage()
        {
            try
            {
                var client = new RestClient(ApiURL + "GetBannerImage");
                var request = new RestRequest(Method.GET);
                request.AddHeader("password", Pwd);
                request.AddHeader("username", username);
                request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                var bannerImage = JsonConvert.DeserializeObject<BannerImage>(response.Content);

                if (bannerImage != null)
                {
                    pictureBox2.Image = byteArrayToImage(bannerImage.Data);
                }
            }
            catch (Exception ex)
            {

            }
        }


        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public void WriteBranch(string brachId)
        {
            //List<string> pdfFiles = new List<string>();
            //byte[] pdfbytes = Convert.FromBase64String(base64data);
            //string installedPath = @"C:\Users\Public\";
            //string fileName = "Branch" + ".pdf";
            //var destinationFileName = System.IO.Path.Combine(installedPath, System.IO.Path.GetFileName(fileName));
            //File.WriteAllBytes(destinationFileName, pdfbytes);


            string installedPath = @"C:\Users\Public\";
            string fileName = "Branch" + ".txt";
            var destinationFileName = System.IO.Path.Combine(installedPath, System.IO.Path.GetFileName(fileName));
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(destinationFileName))
            {
                file.WriteLine(brachId);
            }
        }



        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblErrMsg.Text = "";

            //if (cmbBranch.SelectedValue != null && cmbBranch.SelectedIndex != 0)
            //{
            //   InsertMachineBranch(cmbBranch.SelectedValue.ToString());
            //}

            if (cmbBranch.SelectedValue != null && cmbBranch.SelectedValue.ToString() != "GensureAPIv2.Models.Branch")
            {
                WriteBranch(cmbBranch.SelectedValue.ToString());
            }
        }

        //public void SaveCertificatePdf()
        //{
        //    try
        //    {

        //string textFile = @"../../Pdf/base_64_pdf.txt";
        //byte[] pdfbytes = null;
        //        if (File.Exists(textFile))
        //        {
        //            var client = new RestClient(ApiURL + "SaveCertificatePdf?base64String=" + File.ReadAllText(textFile));
        //            var request = new RestRequest(Method.POST);
        //            request.AddHeader("password", Pwd);
        //            request.AddHeader("username", username);
        //            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
        //            IRestResponse response = client.Execute(request);
        //            var test = response.Content;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        public string SaveCertificatePdf(string base64data = "")
        {
            var pdfPath = "";
            try
            {
                PdfModel objPlanModel = new PdfModel();
                objPlanModel.Base64String = base64data;

                string textFile = @"../../Pdf/base_64_pdf.txt";
                if (File.Exists(textFile))
                {
                    objPlanModel.Base64String = File.ReadAllText(textFile);
                }

                var client = new RestClient(ApiURL + "SaveCertificate");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("password", "Geninsure@123");
                request.AddHeader("username", "ameyoApi@geneinsure.com");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(objPlanModel);
                IRestResponse response = client.Execute(request);
                pdfPath = JsonConvert.DeserializeObject<string>(response.Content);

            }
            catch (Exception ex)
            {
            }
            return pdfPath;
        }

        private void btnLicenseQuote_Click(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedValue == null)
            {
                MyMessageBox.ShowBox("Please select branch.", "Modal error message");
                cmbBranch.Focus();
                return;
            }

            var branch = cmbBranch.SelectedValue == null ? "" : cmbBranch.SelectedValue.ToString();
            btnLicenseQuote.Text = "Processing..";
  
            frmLicenseQuote obj = new frmLicenseQuote(branch, _ObjToken, false);
            obj.Show();
            this.Hide();
            btnLicenseQuote.Text = "License";
        }

        private void btnInsurance_Click(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedValue == null)
            {
                MyMessageBox.ShowBox("Please select branch.", "Modal error message");
                cmbBranch.Focus();
                cmbBranch.Visible = true;
                lblBranch.Visible = true;
                return;
            }

            var branch = cmbBranch.SelectedValue == null ? "" : cmbBranch.SelectedValue.ToString();
            btnInsurance.Text = "Processing..";

            frmQuote obj = new frmQuote(branch, _ObjToken, false);
            obj.Show();
            this.Hide();
            btnInsurance.Text = "Insurance Only";
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
           // e.Cancel = true;
        }
    }
}
