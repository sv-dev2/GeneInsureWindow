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
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;

namespace Gene
{
    public partial class frmLicence : Form
    {
        ICEcashService IcServiceobj;
        ICEcashTokenResponse ObjToken;
        frmQuote objfrmQuote;
        string parternToken = "";

        Bitmap bitmap;
        //static String IceCashRequestUrl = "http://windowsapi.gene.co.zw/api/ICEcash/";
        //static String ApiURL = "http://windowsapi.gene.co.zw/api/Account/";

        static String IceCashRequestUrl = WebConfigurationManager.AppSettings["urlPath"] + "/api/ICEcash/";
        static String ApiURL = WebConfigurationManager.AppSettings["urlPath"] + "/api/Account/";

        static String username = "ameyoApi@geneinsure.com";
        static String Pwd = "Geninsure@123";

        string _registrationNumber = "";
        public string CertificateNumber { get; set; }

        string _branchId = "0";
        bool IsSearchByVrn = false;
        RiskDetailModel riskDetail;

        List<ResultLicenceIDResponse> licenseDiskList = new List<ResultLicenceIDResponse>();

        public frmLicence()
        {
            ObjToken = new ICEcashTokenResponse();
            IcServiceobj = new ICEcashService();
            objfrmQuote = new frmQuote("");

            InitializeComponent();

            //

            //_branchId = branchId;
            //this.Size = new System.Drawing.Size(1300, 720);
            //PnlLicenceVrn.Visible = true;

            //  1572, 818


            // PnlLicenceVrn.Location = new Point(335, 100);
            //    PnlLicenceVrn.Size = new System.Drawing.Size(1300, 720);
            // PnlLicenceVrn.Size = new System.Drawing.Size(1300, 1200);
            //pnlPrintPreview.Visible = false;
            //pnlPrintPreview.Location = new Point(335, 50);
            //pnlPrintPreview.Size = new System.Drawing.Size(739, 800);

        }

        private void btnLicPrint_Click(object sender, EventArgs e)
        {

            pictureBox2.Visible = true;
            pictureBox2.WaitOnLoad = true;

            //txtLicVrn.Text = "KJVV456456";
            try
            {
                if (txtLicVrn.Text == "" || txtLicVrn.Text == "Enter Registration Number")
                {
                    txtLicVrn.Focus();
                    errorProvider1.SetError(txtLicVrn, "Please enter Registration Number");
                    return;
                }
                else
                    errorProvider1.Clear();


                pictureBox2.Visible = true;
                pictureBox2.WaitOnLoad = true;
                //PrintPreview1 dlg1 = new PrintPreview1(txtLicVrn.Text);
                //dlg1.ShowDialog();
                var vehicelDetails = GetVehicelDetials(txtLicVrn.Text);

                //    vehicelDetails.LicenseId = 2743;
                if (vehicelDetails != null && vehicelDetails.CombinedID != null)
                {
                    //ObjToken = IcServiceobj.getToken();
                    //if (ObjToken != null)
                    //{
                    //    parternToken = ObjToken.Response.PartnerToken;
                    //}

                    RequestToke token = Service_db.GetLatestToken();
                    if (ObjToken != null)
                        parternToken = token.Token;

                    // riskDetail = new RiskDetailModel { LicenseId = vehicelDetails.LicenseId.ToString(), RegistrationNo = vehicelDetails.RegistrationNo };

                    // _branchId = _branchId==null? "0" : _branchId;

                    riskDetail = new RiskDetailModel { CombinedID = vehicelDetails.CombinedID, LicenseId = vehicelDetails.LicenseId, RegistrationNo = vehicelDetails.RegistrationNo };

                    if (!string.IsNullOrEmpty(vehicelDetails.CombinedID) && (vehicelDetails.CombinedID != "0"))
                    {
                        DisplayLicenseDisc(riskDetail, parternToken);
                    }
                }
                else if (vehicelDetails != null && vehicelDetails.LicenseId != null)
                {
                    RequestToke token = Service_db.GetLatestToken();
                    if (ObjToken != null)
                        parternToken = token.Token;

                    // riskDetail = new RiskDetailModel { LicenseId = vehicelDetails.LicenseId.ToString(), RegistrationNo = vehicelDetails.RegistrationNo };

                    riskDetail = new RiskDetailModel { LicenseId = vehicelDetails.LicenseId, RegistrationNo = vehicelDetails.RegistrationNo };

                    if (!string.IsNullOrEmpty(vehicelDetails.LicenseId) && (vehicelDetails.LicenseId != "0"))
                    {
                        DisplayLicenseOnly(riskDetail, parternToken);
                    }
                }
                else
                {
                    pictureBox2.Visible = false;
                }
                //PrintPreview1 dlg1 = new PrintPreview1(licenseDiskList);
                //dlg1.ShowDialog();
            }
            catch (Exception ex)
            {
                pictureBox2.Visible = false;
            }
        }

        private void PrintOutByVRN(string vrn)
        {
            pictureBox2.Visible = true;
            pictureBox2.WaitOnLoad = true;

            try
            {

                pictureBox2.Visible = true;
                pictureBox2.WaitOnLoad = true;
                //PrintPreview1 dlg1 = new PrintPreview1(txtLicVrn.Text);
                //dlg1.ShowDialog();
                var vehicelDetails = GetVehicelDetials(vrn);

                //    vehicelDetails.LicenseId = 2743;
                if (vehicelDetails != null && vehicelDetails.CombinedID != null)
                {
                    //ObjToken = IcServiceobj.getToken();
                    //if (ObjToken != null)
                    //{
                    //    parternToken = ObjToken.Response.PartnerToken;
                    //}

                    RequestToke token = Service_db.GetLatestToken();
                    if (ObjToken != null)
                        parternToken = token.Token;

                    // riskDetail = new RiskDetailModel { LicenseId = vehicelDetails.LicenseId.ToString(), RegistrationNo = vehicelDetails.RegistrationNo };

                    // _branchId = _branchId==null? "0" : _branchId;

                    riskDetail = new RiskDetailModel { CombinedID = vehicelDetails.CombinedID, LicenseId = vehicelDetails.LicenseId, RegistrationNo = vehicelDetails.RegistrationNo };

                    if (!string.IsNullOrEmpty(vehicelDetails.CombinedID) && (vehicelDetails.CombinedID != "0"))
                    {
                        DisplayLicenseDisc(riskDetail, parternToken);
                    }
                }
                else if (vehicelDetails != null && vehicelDetails.LicenseId != null)
                {
                    RequestToke token = Service_db.GetLatestToken();
                    if (ObjToken != null)
                        parternToken = token.Token;

                    // riskDetail = new RiskDetailModel { LicenseId = vehicelDetails.LicenseId.ToString(), RegistrationNo = vehicelDetails.RegistrationNo };

                    riskDetail = new RiskDetailModel { LicenseId = vehicelDetails.LicenseId, RegistrationNo = vehicelDetails.RegistrationNo };

                    if (!string.IsNullOrEmpty(vehicelDetails.LicenseId) && (vehicelDetails.LicenseId != "0"))
                    {
                        DisplayLicenseOnly(riskDetail, parternToken);
                    }
                }
                else
                {
                    pictureBox2.Visible = false;
                }
                //PrintPreview1 dlg1 = new PrintPreview1(licenseDiskList);
                //dlg1.ShowDialog();
            }
            catch (Exception ex)
            {
                pictureBox2.Visible = false;
            }
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


        private VehicleDetails GetVehicelDetialsByLicPdfCode(string code)
        {
            var client = new RestClient(IceCashRequestUrl + "GetVehicelDetailsByLicPdfCode?pdfCode=" + code);
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<VehicleDetails>(response.Content);
            return result;
        }


        private ALMRePrintModel GetAlmPrintDetialByCode(string otp)
        {
            var client = new RestClient(IceCashRequestUrl + "GetAlmPrintDetialByCode?otp=" + otp);
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<ALMRePrintModel>(response.Content);
            return result;
        }

        public void SaveAlmPrintDetial(ALMRePrint objPlanModel)
        {

            if (objPlanModel != null)
            {
                var client = new RestClient(IceCashRequestUrl + "SaveAlmPrintDetial");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("password", "Geninsure@123");
                request.AddHeader("username", "ameyoApi@geneinsure.com");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(objPlanModel);

                //request.Timeout = 5000;
                //request.ReadWriteTimeout = 5000;
                IRestResponse response = client.Execute(request);

            }
        }


        //uncomment after getting response from icecash
        private List<ResultLicenceIDResponse> DisplayLicenseDisc(RiskDetailModel riskDetailModel, string parterToken)
        {
            // List<ResultLicenceIDResponse> list = new List<ResultLicenceIDResponse>();

            //  ResultLicenceIDRootObject quoteresponseResult = IcServiceobj.LICResult(riskDetailModel.LicenseId, parternToken);

            ResultLicenceIDRootObject quoteresponseResult = ICEcashService.TPILICResult(riskDetailModel, parternToken);


            //TPILICResult

            if (quoteresponseResult != null && (quoteresponseResult.Response.Message.Contains("Partner Token has expired") || quoteresponseResult.Response.Message.Contains("Invalid Partner Token")))
            {
                ObjToken = IcServiceobj.getToken();
                if (ObjToken != null)
                {
                    parternToken = ObjToken.Response.PartnerToken;
                    Service_db.UpdateToken(ObjToken);
                    //  quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 
                    quoteresponseResult = ICEcashService.TPILICResult(riskDetailModel, parternToken);

                }
            }

            if (quoteresponseResult != null && quoteresponseResult.Response != null)
            {
                licenseDiskList.Add(quoteresponseResult.Response);

                if (quoteresponseResult.Response.LicenceCert == null)
                {
                    //MessageBox.Show("Pdf not found for this  certificate.");
                    MyMessageBox.ShowBox("Pdf not found for this  certificate.", "Message");
                    pictureBox2.Visible = false;
                    return licenseDiskList;
                }

                this.Close();
                CertificateSerialForm obj = new CertificateSerialForm(riskDetailModel, parternToken, quoteresponseResult.Response.LicenceCert);
                obj.Show();
            }
            // 
            return licenseDiskList;
        }

        private List<ResultLicenceIDResponse> DisplayLicenseOnly(RiskDetailModel riskDetailModel, string parterToken) // only for license
        {
            // List<ResultLicenceIDResponse> list = new List<ResultLicenceIDResponse>();

            ResultLicenceIDRootObject quoteresponseResult = IcServiceobj.LICResult(riskDetailModel.LicenseId, parternToken);

            //TPILICResult

            if (quoteresponseResult != null && (quoteresponseResult.Response.Message.Contains("Partner Token has expired") || quoteresponseResult.Response.Message.Contains("Invalid Partner Token")))
            {
                ObjToken = IcServiceobj.getToken();
                if (ObjToken != null)
                {
                    parternToken = ObjToken.Response.PartnerToken;
                    Service_db.UpdateToken(ObjToken);
                    //  quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 
                    quoteresponseResult = IcServiceobj.LICResult(riskDetailModel.LicenseId, parternToken);
                }
            }

            if (quoteresponseResult != null && quoteresponseResult.Response != null)
            {
                licenseDiskList.Add(quoteresponseResult.Response);

                if (quoteresponseResult.Response.LicenceCert == null)
                {
                    //MessageBox.Show("Pdf not found for this  certificate.");
                    MyMessageBox.ShowBox("Pdf not found for this  certificate.", "Message");
                    pictureBox2.Visible = false;
                    return licenseDiskList;
                }

                this.Close();
                CertificateSerialForm obj = new CertificateSerialForm(riskDetailModel, parternToken, quoteresponseResult.Response.LicenceCert);
                obj.Show();

            }
            // 
            return licenseDiskList;
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
                    proc.WaitForExit(10000);
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

            /// https://svgvijay.blogspot.com/2013/02/how-to-save-image-into-folder-in-c.html
            //string imagepath = pictureBox1.ImageLocation.ToString();
            //string picname = imagepath.Substring(imagepath.LastIndexOf('\\'));
            //string path = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin"));
            //Bitmap imgImage = new Bitmap(pictureBox1.Image);    //Create an object of Bitmap class/
            //imgImage.Save(path + "\\Image\\" + picname + ".pdf");
            //MessageBox.Show("image svaed in :" + path + "'\'Image'\'" + picname);


            //   string certificatePath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin")) + "\\Certificate" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            //string certificatePath = HttpContext.Current.Server.MapPath("~/CertificatePDF");
            //string fileFullPath = certificatePath + "\\Certificate" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            //string FinalCertificatePath = ConfigurationManager.AppSettings["CerificatePathBase"] + "/CertificatePDF/Certificate" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf"; ;


            List<string> pdfFiles = new List<string>();
            //   byte[] bytes = Encoding.ASCII.GetBytes(base64data);

            byte[] pdfbytes = Convert.FromBase64String(base64data);


            //string installedPath = Application.StartupPath + "/pdf";
            //string fileName = "Certificate1" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";



            string installedPath = @"C:\";

            string fileName = "Certificate" + ".pdf";




            //Check whether folder path is exist
            //if (!System.IO.Directory.Exists(installedPath))
            //{
            //    // If not create new folder
            //    System.IO.Directory.CreateDirectory(installedPath);
            //}
            //Save pdf files in installedPath

            string destinationFileName = System.IO.Path.Combine(installedPath, System.IO.Path.GetFileName(fileName));
            File.WriteAllBytes(destinationFileName, pdfbytes);



            return destinationFileName;


        }

        private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void txtLicVrn_MouseEnter(object sender, EventArgs e)
        {
            txtLicVrn.Text = "";
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            //PnlLicenceVrn.Visible = false;
            //PnlVrn.Visible = true;
            obj.Show();
            this.Hide();
        }

        private void GetVrnLicenseAndInsurace(int vehicelId)
        {
            // ResultRootObject quoteresponseResult = IcServiceobj.LICResult(LicenceID, ObjToken.Response.PartnerToken);
        }


        private void LicenseApproved()
        {

            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            ObjToken = objfrmQuote.CheckParterTokenExpire();
            if (ObjToken != null)
            {
                parternToken = ObjToken.Response.PartnerToken;
            }

            List<VehicleLicQuote> obj = new List<VehicleLicQuote>();
            obj.Add(new VehicleLicQuote
            {
                VRN = txtLicVrn.Text,
                IDNumber = "ABCDEFGHIJ1",
                ClientIDType = "1",
                LicFrequency = "3"
            });


            ResultRootObject quoteresponse = IcServiceobj.LICQuote(obj, ObjToken.Response.PartnerToken);
            response.result = quoteresponse.Response.Result;
            if (response.result == 0)
            {
                response.message = quoteresponse.Response.Quotes[0].Message;
            }
            else
            {
                if (quoteresponse.Response.Quotes != null)
                {
                    List<VehicleLicQuoteUpdate> objLicQuoteUpdate = new List<VehicleLicQuoteUpdate>();
                    foreach (var item in quoteresponse.Response.Quotes.ToList())
                    {
                        objLicQuoteUpdate.Add(new VehicleLicQuoteUpdate
                        {
                            PaymentMethod = Convert.ToInt32("1"),
                            Status = "1",
                            DeliveryMethod = Convert.ToInt32("1"),
                            LicenceID = Convert.ToInt32(item.LicenceID)
                        });
                    }
                    ResultRootObject quoteresponseNew = IcServiceobj.LICQuoteUpdate(objLicQuoteUpdate, ObjToken.Response.PartnerToken);
                    response.result = quoteresponseNew.Response.Result;
                    if (response.result == 0)
                    {
                        response.message = quoteresponse.Response.Quotes[0].Message;
                    }

                    else
                    {
                        if (quoteresponse.Response.Quotes != null)
                        {
                            var LicenceID = quoteresponse.Response.Quotes[0].LicenceID;
                            //  ResultRootObject quoteresponseResult = IcServiceobj.LICResult(LicenceID, ObjToken.Response.PartnerToken);
                        }
                    }
                }
            }
        }

        private void txtLicVrn_Enter(object sender, EventArgs e)
        {
            txtLicVrn.Text = "";
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (txtLicPdfCode.Text == "" || txtLicPdfCode.Text == "Registration/Pdf Verification Code")
            {
                txtLicPdfCode.Focus();
                errorProvider1.SetError(txtLicPdfCode, "Registration/Pdf Verification Code");
                return;
            }
            else
                errorProvider1.Clear();

            if (IsSearchByVrn == true && (txtOtp.Text == "" || txtOtp.Text == "OTP"))
            {
                txtOtp.Focus();
                errorProvider1.SetError(txtOtp, "Please enter otp");
                return;
            }

           

            var vehicelDetails = new VehicleDetails();

            if (txtLicPdfCode.Text != "" && (txtOtp.Text != "" && txtOtp.Text != "OTP"))
            {             
                var reprintDetial = GetAlmPrintDetialByCode(txtOtp.Text);
                if (reprintDetial!=null && reprintDetial.Id != 0)
                {
                    ALMRePrint reprint = new ALMRePrint();
                    reprint.VRN = txtLicPdfCode.Text;
                    reprint.IsActive = true;
                    reprint.Id = reprintDetial.Id;
                    reprint.CreatedOn = DateTime.Now;
                    SaveAlmPrintDetial(reprint);
                    PrintOutByVRN(txtLicPdfCode.Text);
                }
                else
                {
                    MyMessageBox.ShowBox("Certificate is not found for this code", "Message");

                }

            }
            else
            {
                vehicelDetails = GetVehicelDetialsByLicPdfCode(txtLicPdfCode.Text);
                if (vehicelDetails != null && vehicelDetails.VehicelId != 0)
                {

                    RequestToke token = Service_db.GetLatestToken();
                    if (ObjToken != null)
                        parternToken = token.Token;

                    pictureBox2.Visible = true;
                    pictureBox2.WaitOnLoad = true;

                    String WebUrlPath = WebConfigurationManager.AppSettings["WebUrlPath"];
                    string filePath = WebUrlPath + "/" + "Documents/License/" + vehicelDetails.VehicelId + ".pdf";
                    string optionalFilePath = WebUrlPath + "/" + "Documents/License/" + vehicelDetails.RegistrationNo + ".pdf";
                    //urlPath
                    //var pdfPath = SavePdfFromUrl(filePath, optionalFilePath);
                    //// var pdfPath = @"F:\sample.pdf";
                    //PdfDocument doc = new PdfDocument();
                    //doc.LoadFromFile(pdfPath);
                    //doc.Pages.Insert(0);
                    //doc.Pages.Add();
                    //doc.Pages.RemoveAt(0);//Since First page have always Red Text if use Free Version.
                    //doc.SaveToFile(pdfPath);


                    string installedPath = @"C:\Users\Public\";
                    string fileName = "Certificate" + ".pdf";

                    var destinationFileName = System.IO.Path.Combine(installedPath, System.IO.Path.GetFileName(fileName));


                    PdfReader reader = new PdfReader(filePath);
                    PdfStamper stamper = new PdfStamper(reader, new FileStream(destinationFileName, FileMode.Create));
                    int total = reader.NumberOfPages;
                    for (int pageNumber = total; pageNumber > 0; pageNumber--)
                    {
                        stamper.InsertPage(pageNumber, PageSize.A4);
                    }
                    stamper.Close();
                    reader.Close();


                    MyMessageBox.ShowBox("Please Print Licence Disk. ", "Print License Disk");

                    printPDFWithAcrobat(destinationFileName);
                    pictureBox2.Visible = false;

                    riskDetail = new RiskDetailModel { CombinedID = vehicelDetails.CombinedID, LicenseId = vehicelDetails.LicenseId, RegistrationNo = vehicelDetails.RegistrationNo };


                    this.Close();
                    WebCertificateSerial obj = new WebCertificateSerial(riskDetail, parternToken);
                    obj.Show();

                }
                else
                {
                    pictureBox2.Visible = false;
                    MyMessageBox.ShowBox("Certificate is not found for this code", "Message");

                }

            }

            pictureBox2.Visible = false;
        }



        private string SavePdfFromUrl(string filePath, string optionalFilePath)
        {

            string destinationFileName = "";
            try
            {
                List<string> pdfFiles = new List<string>();
                byte[] pdfbytes = new Byte[64];

                try
                {
                    using (var webClient = new WebClient())
                    {
                        pdfbytes = webClient.DownloadData(filePath);
                    }
                }
                catch (Exception ex)
                {
                    // Handlng exception for geting pdf by vrn
                    using (var webClient = new WebClient())
                    {
                        pdfbytes = webClient.DownloadData(optionalFilePath);
                    }
                }



                // string installedPath = @"C:\";
                string installedPath = @"C:\Users\Public\";
                string fileName = "Certificate" + ".pdf";

                destinationFileName = System.IO.Path.Combine(installedPath, System.IO.Path.GetFileName(fileName));
                File.WriteAllBytes(destinationFileName, pdfbytes);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Modal error message");
            }
            return destinationFileName;
        }

        private void txtLicPdfCode_Enter(object sender, EventArgs e)
        {
            txtLicPdfCode.Text = "";
        }

        private void txtLicVrn_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void txtLicPdfCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

      
        private void txtOtp_Enter(object sender, EventArgs e)
        {
            txtOtp.Text = "";
        }

        private void txtLicPdfCode_Leave(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox2.WaitOnLoad = true;

            var vehicelDetails = GetVehicelDetials(txtLicPdfCode.Text);
            if (vehicelDetails != null && vehicelDetails.CombinedID != null)
            {
                txtOtp.Visible = true;
                IsSearchByVrn = true;
            }
            else
            {
                txtOtp.Visible = false;
                IsSearchByVrn = false;
            }

            pictureBox2.Visible = false;
        }
    }
}
