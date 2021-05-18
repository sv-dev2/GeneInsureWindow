using GensureAPIv2.Models;
using Insurance.Service;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using RestSharp;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;


namespace Gene
{
    public partial class CertificateSerialForm : Form
    {
        public RiskDetailModel RiskDetailModel;
        public string ParternToken { get; set; }

        ICEcashTokenResponse ObjToken;
        ICEcashService IcServiceobj;

        public string _base64Data = "";
        // static String ApiURL = "http://windowsapi.gene.co.zw/api/Account/";
        static String ApiURL = WebConfigurationManager.AppSettings["urlPath"] + "/api/Account/";
        static String IceCashRequestUrl = WebConfigurationManager.AppSettings["urlPath"] + "/api/ICEcash/";

        [System.ComponentModel.Browsable(false)]
        public event EventHandler GotFocus;

        public CertificateSerialForm(RiskDetailModel objRiskDetail, string Partnertoken, string base64Data)
        {
            InitializeComponent();
            this.ActiveControl = txtCertificateSerialNumber;
            txtCertificateSerialNumber.Focus();
            RiskDetailModel = objRiskDetail;
            ParternToken = Partnertoken;
            IcServiceobj = new ICEcashService();
            _base64Data = base64Data;
        }


        private void txtCertificateSerialNumber_GotFocus(Object sender, EventArgs e)
        {
            //  MessageBox.Show("You are in the Control.GotFocus event.");
        }


        private void txtCertificateSerialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (valatedSerialNumber(txtCertificateSerialNumber.Text))
                    {


                        //else
                        //{
                        //    MessageBox.Show("Please Eneter the correct Serial Number", "Error");
                        //}

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

                        CertSerialNoDetailModel model = new CertSerialNoDetailModel();
                        model.VehicleId = RiskDetailModel.Id;
                        model.CertSerialNo = txtCertificateSerialNumber.Text;

                        SaveCertSerialNum(model);

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

        private void CertificateSerialForm_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Confirm printing.");
            // pictureBox2.Visible = true;
            //pictureBox2.WaitOnLoad = true;

            //   btnScan.Click += new System.EventHandler(this.btnScan_Click);

            btnScan_Click(sender, e); // commented for now

            txtCertificateSerialNumber.Focus();


        }


        public void printPDFWithAcrobat(string Filepath)
        {
            // string Filepath = @"D:\Certificate120190724174642.pdf";
            try
            {

                //  string raderPath = ConfigurationManager.AppSettings["adobeReaderPath"];

                //  Thread.Sleep(1000);

                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "print";

                //Define location of adobe reader/command line
                //switches to launch adobe in "print" mode
                proc.StartInfo.FileName =
                  @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";

                //proc.StartInfo.FileName = raderPath;


                proc.StartInfo.Arguments = String.Format(@"/p /h {0}", Filepath);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;


                if (proc.HasExited == false)
                {
                    proc.WaitForExit(6000);
                }

                proc.EnableRaisingEvents = true;

                proc.Close();
                KillAdobe("AcroRd32");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static bool KillAdobe(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses().Where(
                         clsProcess => clsProcess.ProcessName.StartsWith(name)))
            {
                clsProcess.Kill();
                return true;
            }
            return false;
        }

        public void CreateLicenseFile(string base64data)
        {
            try
            {

                byte[] bytes = Encoding.ASCII.GetBytes(base64data);

                PdfModel objPlanModel = new PdfModel();
                objPlanModel.Base64String = base64data;
                var client = new RestClient(ApiURL + "SaveCertificate");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("password", "Geninsure@123");
                request.AddHeader("username", "ameyoApi@geneinsure.com");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(objPlanModel);
                IRestResponse response = client.Execute(request);
                var pdfPath = JsonConvert.DeserializeObject<string>(response.Content);


                //using (WebClient webClient = new WebClient())
                //{
                //    byte[] data = webClient.DownloadData(pdfPath);


                //    //   using (MemoryStream stream = new MemoryStream(data))
                //    using (MemoryStream stream = new MemoryStream(bytes))
                //    {
                //        PdfDocument doc = new PdfDocument(stream);
                //        doc.Pages.Insert(0);
                //        doc.Pages.Add();
                //        doc.Pages.RemoveAt(0);//Since First page have always Red Text if use Free Version.
                //        doc.PrintDocument.Print();
                //    }
                //}
            }
            catch (Exception ex)
            {
            }
        }


        private string SavePdf(string base64data)
        {
            string destinationFileName = "";
            try
            {
                List<string> pdfFiles = new List<string>();
                byte[] pdfbytes = Convert.FromBase64String(base64data);

                // string installedPath = @"C:\";
                string installedPath = @"C:\Users\Public\";
                string fileName = "Certificatebk" + ".pdf";

                destinationFileName = System.IO.Path.Combine(installedPath, System.IO.Path.GetFileName(fileName));
                // byte[] readFile = File.ReadAllBytes("http://geneinsureclaim.kindlebit.com//Documents/29062/GMCC200002648-1/20200403155646,Invoice.pdf");

                File.WriteAllBytes(destinationFileName, pdfbytes);

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Modal error message");
            }
            return destinationFileName;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Visible = true;
                pictureBox2.WaitOnLoad = true;
                var pdfPath = SavePdf(_base64Data);

                //PdfDocument doc = new PdfDocument();
                //doc.LoadFromFile(pdfPath);
                //doc.Pages.Insert(0);
                //doc.Pages.Add();
                //doc.Pages.RemoveAt(0);//Since First page have always Red Text if use Free Version.
                //doc.SaveToFile(pdfPath);

                //string installedPath = @"C:\Users\Public\";
                //string fileName = "Certificate" + ".pdf";
                string installedPath = @"C:\Users\Public\";
                string fileName = "Certificate" + ".pdf";

                var destinationFileName = System.IO.Path.Combine(installedPath, System.IO.Path.GetFileName(fileName));

                PdfReader reader = new PdfReader(pdfPath);
                PdfStamper stamper = new PdfStamper(reader, new FileStream(destinationFileName, FileMode.Create));
                int total = reader.NumberOfPages;
                for (int pageNumber = total; pageNumber > 0; pageNumber--)
                {
                    stamper.InsertPage(pageNumber, PageSize.A4);
                }
                stamper.Close();
                reader.Close();


                //MessageBox.Show("Please Print Licence Disk.                                                                       ", "Print License Disk");

                MyMessageBox.ShowBox("Please Print Licence Disk. ", "Print License Disk");

                printPDFWithAcrobat(destinationFileName);

                CreateLicenseFile(_base64Data);

                CertSerialNoDetailModel model = new CertSerialNoDetailModel();
                model.VehicleId = RiskDetailModel.Id;
                model.CertSerialNo = txtCertificateSerialNumber.Text;

                txtCertificateSerialNumber.ForeColor = Color.Gray;
                txtCertificateSerialNumber.Focus();

                // SaveCertSerialNum(model);

                pictureBox2.WaitOnLoad = false;
                pictureBox2.Visible = false;


            }
            catch (Exception ex)
            {

                pictureBox2.WaitOnLoad = false;
                pictureBox2.Visible = false;
                // MessageBox.Show(ex.Message);
                MyMessageBox.ShowBox(ex.Message, "Modal error message");
            }
        }


        public bool valatedSerialNumber(string serialNumber)
        {
            string pattern = @"^[a-zA-Z]{1}[0-9]{8}";
            // Create a Regex  
            Regex rg = new Regex(pattern, RegexOptions.IgnoreCase);
            return rg.IsMatch(serialNumber);
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


    }
}
