using GensureAPIv2.Models;
using Insurance.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
//using MetroFramework;
//using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Drawing2D;
using System.Globalization;
using InsuranceClaim.Models;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Xml;
using Spire.Pdf;
using System.Net;
using System.Diagnostics;
using System.Web.Configuration;

namespace Gene
{
    public partial class frmQuote : Form
    {
        ResultRootObject quoteresponse;
        static String ApiURL = WebConfigurationManager.AppSettings["urlPath"] + "/api/Account/";
        static String IceCashRequestUrl = WebConfigurationManager.AppSettings["urlPath"] + "/api/ICEcash/";

        public string CertificateNumber { get; set; }
        static String username = "ameyoApi@geneinsure.com";
        static String Pwd = "Geninsure@123";


        ICEcashTokenResponse ObjToken;
        List<UserInput> objListUserInput;
        List<RiskDetailModel> objlistRisk;

        RiskDetailModel objRiskModel;
        ICEcashService IcServiceobj;
        CustomerModel customerInfo;
        ResultResponse resObject;
        SummaryDetailModel summaryModel;

        ResultRootObject quoteresponseQuote;

        List<VehicleTaxClassModel> TaxClassList;

        List<VehicleLicenseModel> objVehicleLicense;

        List<VehicalModel> objlistVehicalModel;

        List<ProductsModel> ProductsList;

        string parternToken = "";
        bool isVehicalDeleted = false;
        bool isbackclicked = false;

        int VehicalIndex = -1;
        long TransactionId = 0;
        string responseMessage = "";
        string CardDetail = "";
        string TransactionAmount = "";
        string VRNnumForBack = "";
        string branchName = "0";

        public string _clientIdType;
        public int _ProductId;
        public int _TaxClass;

        Int32 counter = 0;
        string _iceCashErrorMsg = "";
        const string _tba = "TBA";
        int prior = 0;

        string _licenseId = "0";
        bool _insuranceAndLicense = true;

        List<ResultLicenceIDResponse> licenseDiskList = new List<ResultLicenceIDResponse>();
        List<Branch> branchList = new List<Branch>();

        //private static frmQuote _mf;
        public frmQuote(string branch, ICEcashTokenResponse _ObjToken = null, bool insuranceAndLicense = true)
        {

            // Keep the current form active by calling the Activate
            // method.
            this.Activate();



            branchName = branch;
            // this for testing
            //Load += new EventHandler(frmQuote_Load);
            objListUserInput = new List<UserInput>();
            customerInfo = new CustomerModel();

            if (_ObjToken == null)
                ObjToken = new ICEcashTokenResponse();
            else
                ObjToken = _ObjToken;

            _insuranceAndLicense = insuranceAndLicense;


            objlistRisk = new List<RiskDetailModel>();
            IcServiceobj = new ICEcashService();
            //  objRiskModel = new RiskDetailModel();

            resObject = new ResultResponse();

            //objlistRisk.Add(objRiskModel);

            InitializeComponent();


            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;


            this.Size = new System.Drawing.Size(1300, 720);
            pnlRiskDetails.Visible = false;
            pnlPersonalDetails.Visible = false;
            pnlInsurance.Visible = false;
            pnlTBAPersonalDetails.Visible = false;
            pnlsumary.Visible = false;
            pnlSum.Visible = false;
            pnlAddMoreVehicle.Visible = false;
            PnlVrn.Visible = true;
            pnlconfimpaymeny.Visible = false;
            pnlErrormessage.Visible = false;
            //cmbMake.Visible = false;
            //cmbModel.Visible = false;

            //vehicleMakeTxt.Visible = false;
            //vehicleModeltxt.Visible = false;

            cmbCurrency.Enabled = false;
            // pnlSummery.
            //pnlSum.Location = new Point(350, 100);
            //pnlSum.Size = new System.Drawing.Size(1390, 638);

            //Old code
            pnlSum.Location = new Point(335, 100);
            pnlSum.Size = new System.Drawing.Size(1300, 1200);
            //new Code
            pnlSum.Location = new Point(335, 20);
            pnlSum.Size = new System.Drawing.Size(1300, 1200);

            pnlConfirm.Visible = false;
            pnlOptionalCover.Visible = false;

            //PnlVrn.Location = new Point(335, 100);
            //PnlVrn.Size = new System.Drawing.Size(739, 238);

            //Old
            //PnlVrn.Location = new Point(350, 120);
            //PnlVrn.Size = new System.Drawing.Size(2600, 638);

            //new Changes 24/05/2019
            PnlVrn.Location = new Point(215, 20);
            PnlVrn.Size = new System.Drawing.Size(1300, 700);
            //1367, 763

            //pnlInsurance.Location = new Point(195, 20);
            //// pnlInsurance.Size = new System.Drawing.Size(1550, 750);
            //pnlInsurance.Size = new System.Drawing.Size(1300, 1200);

            pnlInsurance.Location = new Point(210, 20);
            pnlInsurance.Size = new System.Drawing.Size(1000, 600);

            // pnlLogo.Location = new Point(this.Width - 320, this.Height - 220);
            pnlLogo.Location = new Point(this.Width - 300, this.Height - 300);
            pnlLogo.Size = new System.Drawing.Size(300, 220);

            //pnlRiskDetails.Location = new Point(120, 33);
            //pnlRiskDetails.Size = new System.Drawing.Size(900, 700);

            //Old Code
            //pnlRiskDetails.Location = new Point(335, 100);
            //pnlRiskDetails.Size = new System.Drawing.Size(1550, 750);
            //New Code 

            pnlRiskDetails.Location = new Point(205, 20);
            pnlRiskDetails.Size = new System.Drawing.Size(1550, 600);

            //pnlOptionalCover.Location = new Point(200, 33);
            //pnlOptionalCover.Size = new System.Drawing.Size(800, 1040);

            //Old Code
            //pnlOptionalCover.Location = new Point(335, 100);
            //pnlOptionalCover.Size = new System.Drawing.Size(1550, 750);
            //new code
            pnlOptionalCover.Location = new Point(335, 20);
            pnlOptionalCover.Size = new System.Drawing.Size(1550, 750);

            //pnlAddMoreVehicle.Location = new Point(994, 398);
            //pnlAddMoreVehicle.Size = new System.Drawing.Size(263, 99);

            pnlAddMoreVehicle.Location = new Point(1300, 208);
            pnlAddMoreVehicle.Size = new System.Drawing.Size(690, 200);
            //testdd

            //12Feb 

            pnlRadioZinara.Visible = false;
            pnlRadio.Visible = false;
            pnlZinara.Visible = false;
            pnlCorporate.Visible = false;

            pnlRadioZinara.Location = new Point(215, 20);
            //pnlRadioZinara.Size = new System.Drawing.Size(1390, 750);
            pnlRadioZinara.Size = new System.Drawing.Size(1590, 750);


            //pnlRadioZinaraIns.Location = new Point(215, 20);
            ////pnlRadioZinara.Size = new System.Drawing.Size(1390, 750);
            //pnlRadioZinaraIns.Size = new System.Drawing.Size(1590, 750);



            // pnlRadio.Location = new Point(14, 152);
            //pnlRadio.Location = new Point(14, 270); // 20 mar 2019
            //pnlRadio.Location = new Point(7, 350); // 20 mar 2019


            //pnlRadio.Location = new Point(107, 150); // 20 mar 2019  // 12-june _2019
            //pnlRadio.Size = new System.Drawing.Size(625, 335);


            //  pnlZinara.Location = new Point(656, 149);
            //pnlZinara.Location = new Point(656, 249);
            //pnlZinara.Location = new Point(800, 349);
            //pnlZinara.Location = new Point(750, 349);

            //pnlZinara.Location = new Point(780, 150);  // 12 june 2019
            //pnlZinara.Size = new System.Drawing.Size(562, 340);


            //Old Code

            // pnlPersonalDetails.Location = new Point(355, 100);
            //  pnlPersonalDetails.Size = new System.Drawing.Size(1550, 750);

            //New Code
            pnlPersonalDetails.Location = new Point(210, 20);
            pnlPersonalDetails.Size = new System.Drawing.Size(1550, 750);

            pnlTBAPersonalDetails.Location = new Point(355, 100);
            //pnlTBAPersonalDetails.Size = new System.Drawing.Size(1450, 750);
            pnlTBAPersonalDetails.Size = new System.Drawing.Size(1550, 750);

            pnlCorporate.Location = new Point(355, 25);
            //pnlCorporate.Size = new System.Drawing.Size(1450, 750);
            pnlCorporate.Size = new System.Drawing.Size(1550, 750);

            //Old Code
            //pnlPersonalDetails2.Location = new Point(355, 100);
            //pnlPersonalDetails2.Size = new System.Drawing.Size(1550, 750);

            //New  Code
            pnlPersonalDetails2.Location = new Point(215, 20);
            pnlPersonalDetails2.Size = new System.Drawing.Size(1550, 750);


            //Old Code
            //pnlsumary.Location = new Point(355, 100);
            //pnlsumary.Size = new System.Drawing.Size(1390, 1040);

            //new code
            pnlsumary.Location = new Point(230, 20);
            pnlsumary.Size = new System.Drawing.Size(1390, 1040);

            //pnlConfirm.Location = new Point(200, 33);
            //pnlConfirm.Size = new System.Drawing.Size(800, 1040);


            //pnlConfirm.Location = new Point(335, 100);
            //pnlConfirm.Size = new System.Drawing.Size(1350, 750);

            //Old Code
            //pnlConfirm.Location = new Point(335, 100);
            //pnlConfirm.Size = new System.Drawing.Size(1550, 750);
            //New Code 24/05/19
            pnlConfirm.Location = new Point(215, 20);
            pnlConfirm.Size = new System.Drawing.Size(1550, 750);

            pnlThankyou.Location = new Point(300, 33);
            pnlThankyou.Size = new System.Drawing.Size(1180, 1040);


            pnlErrormessage.Location = new Point(300, 33);
            pnlErrormessage.Size = new System.Drawing.Size(1180, 1040);

            //pnlPaymentStatus.Location = new Point(200, 33);
            //pnlPaymentStatus.Size = new System.Drawing.Size(800, 1040);
            //Old Code
            //pnlconfimpaymeny.Location = new Point(300, 33);
            //pnlconfimpaymeny.Size = new System.Drawing.Size(1380, 1040);
            //New Code 
            pnlconfimpaymeny.Location = new Point(300, 20);
            pnlconfimpaymeny.Size = new System.Drawing.Size(1380, 600);


            txtVrn.Text = "Vehicle Registration Number";
            textSearchVrn.Text = "ID Number";

            //txtVrn.Text = "AAD333";
            txtVrn.ForeColor = SystemColors.GrayText;
            textSearchVrn.ForeColor = SystemColors.GrayText;

            //txtZipCode.Text = "00263";
            //txtZipCode.ForeColor = SystemColors.GrayText;


            // txtDOB.Size = new Size(292, 50);

            SetLocationButton();

            btnBacktoList.Hide();
            bindAllCodes();
            GetTablesList();


            lblChas.Visible = false;
            lblEngine.Visible = false;
            txtChasis.Visible = false;
            txtEngine.Visible = false;

            //txtPartialAmount.Enabled = false;// default


            // bindMake();
            //bindCoverType();
            //bindPaymentType();
            //bindAllCities();
            //bindAllClasses();   
            //bindCurrency();
            bindProduct();
            GetBranch();
            // KeyDown event.

            // this.KeyPress += new KeyEventHandler(txtYear_KeyPress);

            //  txtYear.KeyPress += new KeyPressEventHandler(txtYear_KeyPress);

            // cmbCmpCity.Height = 150;

        }

        public void GetBranch()
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


            }
            catch (Exception ex)
            {

            }
        }
        private void SetLoadingPnlInsurance(bool displayLoader)
        {
            if (displayLoader)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    LoadingInsurance.Visible = true;
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    LoadingInsurance.Visible = false;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
        }

        private void SetLocationButton()
        {
            //  btnCancel.Location = new Point(68, 24);

            //  btnCancel.Location = new Point(140, 6);

            //btnSave.Location = new Point(684, 24);

            //btnConfBack.Location = new Point(68, 24);
            //btnConfContinue.Location = new Point(684, 24);


            //btnRiskBack.Location = new Point(68, 24);
            //btnRiskCont.Location = new Point(684, 24);


        }

        public void loadVRNPanel()
        {
            pnlSummery.Controls.Clear(); //to remove all controls
            // Int32 counter = 2;
            Int32 counter = objlistRisk.Count();

            for (int i = 0; i < counter; i++)
            {
                if (counter == 1)
                {
                    Panel pnl = new Panel();
                    // pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
                    pnl.BackgroundImage = Gene.Properties.Resources.top_bar;
                    pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    Panel Bottompnl = new Panel();
                    //  Bottompnl.BackColor = Color.White;
                    Bottompnl.BackgroundImage = Gene.Properties.Resources.bottom_bar;
                    Bottompnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    Label lblvehicle = new System.Windows.Forms.Label();
                    lblvehicle.Name = lblvehicle + i.ToString();
                    lblvehicle.ForeColor = Color.White;
                    lblvehicle.Text = "Vehicle Name :";
                    lblvehicle.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);

                    lblvehicle.AutoSize = true;
                    lblvehicle.Location = new Point(i + 5, 3);
                    lblvehicle.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblvehicle.BackColor = Color.Transparent;

                    pnl.Controls.Add(lblvehicle);


                    Label lblTermly = new System.Windows.Forms.Label();
                    lblTermly.Name = lblTermly + i.ToString();
                    lblTermly.ForeColor = Color.White;

                    var paymentTerm = objlistRisk[i].PaymentTermId == 0 ? "" : Convert.ToString(objlistRisk[i].PaymentTermId);
                    var paymentTermName = "";
                    paymentTermName = GetPaymentTerm(paymentTerm);


                    lblTermly.Text = paymentTermName.ToString();
                    lblTermly.AutoSize = true;
                    lblTermly.Location = new Point(i + 5, 50);
                    lblTermly.BackColor = Color.Transparent;
                    lblTermly.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblTermly);


                    Label lblSumInsured = new System.Windows.Forms.Label();
                    lblSumInsured.Name = lblSumInsured + i.ToString();
                    lblSumInsured.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblSumInsured.Text = "Sum Insured :       ";
                    lblSumInsured.Text += objlistRisk[i].SumInsured == null ? "0" : Convert.ToString(objlistRisk[i].SumInsured);

                    lblSumInsured.AutoSize = true;
                    lblSumInsured.BackColor = Color.Transparent;
                    lblSumInsured.Location = new Point(10, 140);
                    lblSumInsured.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblSumInsured);


                    //Label lblSumInsuredAmount = new System.Windows.Forms.Label();
                    //lblSumInsuredAmount.Name = lblSumInsuredAmount + i.ToString();
                    //lblSumInsuredAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblSumInsuredAmount.Text = objlistRisk[i].SumInsured == null ? "0" : Convert.ToString(objlistRisk[i].SumInsured); // sum insured amount
                    //lblSumInsuredAmount.AutoSize = true;
                    //lblSumInsuredAmount.BackColor = Color.Transparent;
                    //lblSumInsuredAmount.Location = new Point(160, 140);
                    //lblSumInsuredAmount.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblSumInsuredAmount);



                    Label lblTotalpremium = new System.Windows.Forms.Label();
                    lblTotalpremium.Name = lblTotalpremium + i.ToString();
                    lblTotalpremium.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblTotalpremium.Text = "Basic premium :     ";
                    lblTotalpremium.Text += objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    lblTotalpremium.BackColor = Color.Transparent;
                    lblTotalpremium.AutoSize = true;
                    lblTotalpremium.Location = new Point(10, 190);
                    lblTotalpremium.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblTotalpremium);


                    //Label lblTotalpremiumAmount = new System.Windows.Forms.Label();
                    //lblTotalpremiumAmount.Name = lblTotalpremiumAmount + i.ToString();
                    //lblTotalpremiumAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    //lblTotalpremiumAmount.Text = objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    //lblTotalpremiumAmount.BackColor = Color.Transparent;
                    //lblTotalpremiumAmount.AutoSize = true;
                    //lblTotalpremiumAmount.Location = new Point(170, 190);
                    //lblTotalpremiumAmount.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //Bottompnl.Controls.Add(lblTotalpremiumAmount);




                    Label lblZTSC = new System.Windows.Forms.Label();
                    lblZTSC.Name = lblZTSC + i.ToString();
                    lblZTSC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblZTSC.Text = "ZTSC :                  ";
                    lblZTSC.Text += objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    //lblZTSC.Width = 100;

                    lblZTSC.BackColor = Color.Transparent;
                    lblZTSC.Location = new Point(10, 240);
                    lblZTSC.AutoSize = true;
                    lblZTSC.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblZTSC);




                    Label lblStampDuty = new System.Windows.Forms.Label();
                    lblStampDuty.Name = lblStampDuty + i.ToString();
                    lblStampDuty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblStampDuty.Text = "Stamp Duty :         ";
                    lblStampDuty.Text += objlistRisk[i].StampDuty == null ? "0" : Convert.ToString(objlistRisk[i].StampDuty);
                    lblStampDuty.Width = 100;
                    lblStampDuty.AutoSize = true;
                    lblStampDuty.BackColor = Color.Transparent;
                    lblStampDuty.Location = new Point(10, 290);
                    lblStampDuty.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblStampDuty);



                    if (objRiskModel.CoverTypeId != 1 && objRiskModel.CoverTypeId != 2 && objRiskModel.CoverTypeId != 4)
                    {
                        Label lblDiscount = new System.Windows.Forms.Label();
                        lblDiscount.Name = lblDiscount + i.ToString();
                        lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                        lblDiscount.Text = "Discount :             ";
                        lblDiscount.Text += objlistRisk[i].Discount == null ? "0" : Convert.ToString(objlistRisk[i].Discount);
                        lblDiscount.BackColor = Color.Transparent;
                        lblDiscount.AutoSize = true;
                        lblDiscount.Location = new Point(10, 340);
                        lblDiscount.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Bottompnl.Controls.Add(lblDiscount);
                    }


                    string res = "";
                    string paddedParam = res.PadRight(50);

                    Button btnEdit = new System.Windows.Forms.Button();
                    btnEdit.Click += BtnEdit_Click;
                    btnEdit.Text = paddedParam;
                    btnEdit.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    btnEdit.Width = 100;
                    btnEdit.Height = 40;

                    btnEdit.Location = new Point(60, 395);
                    btnEdit.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnEdit.BackgroundImage = Gene.Properties.Resources.edit;

                    btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnEdit.BackColor = Color.Transparent;
                    btnEdit.FlatAppearance.BorderSize = 0;
                    Bottompnl.Controls.Add(btnEdit);




                    Button btnDelete = new System.Windows.Forms.Button();
                    btnDelete.Click += BtnDelete_Click;
                    btnDelete.Text = paddedParam;
                    btnDelete.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);

                    btnDelete.Width = 100;
                    btnDelete.Height = 40;
                    btnDelete.BackColor = Color.Transparent;
                    btnDelete.Location = new Point(480, 395);

                    btnDelete.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnDelete.FlatAppearance.BorderSize = 0;
                    btnDelete.BackgroundImage = Gene.Properties.Resources.delete;
                    btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;


                    Bottompnl.Controls.Add(btnDelete);
                    pnl.Size = new System.Drawing.Size(650, 100);
                    pnl.Location = new Point(140, (i * 140));

                    //pnl.Size = new System.Drawing.Size(650, 100);
                    //pnl.Location = new Point(140, (i * 140));

                    Bottompnl.Size = new System.Drawing.Size(650, 460);
                    Bottompnl.Location = new Point(140, (i * 140));


                    //  pnlSummery.Location = new Point(140, 300);

                    //pnlSummery.Location = new Point(140, 100);
                    pnlSummery.Location = new Point(180, 100);
                    pnlSummery.Size = new System.Drawing.Size(790, 470);
                    pnlSummery.Controls.Add(pnl);
                    pnlSummery.Controls.Add(Bottompnl);

                }

                if (counter == 2)
                {
                    Panel pnl = new Panel();
                    // pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
                    pnl.BackgroundImage = Gene.Properties.Resources.top_bar;
                    pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;


                    Panel Bottompnl = new Panel();
                    //Bottompnl.BackColor = Color.White;
                    Bottompnl.BackgroundImage = Gene.Properties.Resources.bottom_bar;
                    Bottompnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    Label lblvehicle = new System.Windows.Forms.Label();
                    lblvehicle.Name = lblvehicle + i.ToString();
                    lblvehicle.ForeColor = Color.White;
                    lblvehicle.BackColor = Color.Transparent;
                    lblvehicle.Text = "Vehicle Name :";
                    lblvehicle.Text += objlistRisk[i].RegistrationNo == null ? "0" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    lblvehicle.AutoSize = true;
                    lblvehicle.Location = new Point(5, 3);
                    lblvehicle.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblvehicle);


                    var paymentTerm = objlistRisk[i].PaymentTermId == 0 ? "" : Convert.ToString(objlistRisk[i].PaymentTermId);
                    var paymentTermName = "";
                    paymentTermName = GetPaymentTerm(paymentTerm);


                    Label lblTermly = new System.Windows.Forms.Label();
                    lblTermly.Name = lblTermly + i.ToString();
                    lblTermly.ForeColor = Color.White;
                    lblTermly.BackColor = Color.Transparent;
                    lblTermly.Text = paymentTermName;
                    lblTermly.AutoSize = true;
                    lblTermly.Location = new Point(5, 50);
                    lblTermly.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblTermly);


                    Label lblSumInsured = new System.Windows.Forms.Label();
                    lblSumInsured.Name = lblSumInsured + i.ToString();
                    lblSumInsured.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblSumInsured.Text = "Sum Insured :"; // sum insured amount
                    lblSumInsured.Text += objlistRisk[i].SumInsured == null ? "" : Convert.ToString(objlistRisk[i].SumInsured);
                    lblSumInsured.BackColor = Color.Transparent;
                    lblSumInsured.AutoSize = true;
                    lblSumInsured.Location = new Point(5, 110);
                    lblSumInsured.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblSumInsured);


                    Label lblTotalpremium = new System.Windows.Forms.Label();
                    lblTotalpremium.Name = lblTotalpremium + i.ToString();
                    lblTotalpremium.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblTotalpremium.Text = "Basic premium :";
                    lblTotalpremium.Text += objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    lblTotalpremium.BackColor = Color.Transparent;
                    lblTotalpremium.AutoSize = true;
                    lblTotalpremium.Location = new Point(5, 155);
                    lblTotalpremium.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblTotalpremium);




                    Label lblZTSC = new System.Windows.Forms.Label();
                    lblZTSC.Name = lblZTSC + i.ToString();
                    lblZTSC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblZTSC.Text = "ZTSC :";
                    lblZTSC.Text += objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    //lblZTSC.Width = 100;
                    lblZTSC.AutoSize = true;
                    lblZTSC.BackColor = Color.Transparent;
                    lblZTSC.Location = new Point(5, 210);
                    lblZTSC.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblZTSC);


                    Label lblStampDuty = new System.Windows.Forms.Label();
                    lblStampDuty.Name = lblStampDuty + i.ToString();
                    lblStampDuty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblStampDuty.Text = "Stamp Duty :";
                    lblStampDuty.Text += objlistRisk[i].StampDuty == null ? "0" : Convert.ToString(objlistRisk[i].StampDuty);
                    lblStampDuty.BackColor = Color.Transparent;
                    lblStampDuty.AutoSize = true;
                    lblStampDuty.Width = 100;
                    lblStampDuty.Location = new Point(5, 260);
                    lblStampDuty.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblStampDuty);


                    //objRiskModel.CoverTypeId

                    if (objRiskModel.CoverTypeId != 1 && objRiskModel.CoverTypeId != 2 && objRiskModel.CoverTypeId != 4)
                    {
                        Label lblDiscount = new System.Windows.Forms.Label();
                        lblDiscount.Name = lblDiscount + i.ToString();
                        lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                        lblDiscount.Text = "Discount :";
                        lblDiscount.Text += objlistRisk[i].Discount == null ? "0" : Convert.ToString(objlistRisk[i].Discount);
                        lblDiscount.BackColor = Color.Transparent;
                        lblDiscount.AutoSize = true;
                        lblDiscount.Location = new Point(5, 310);
                        lblDiscount.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        Bottompnl.Controls.Add(lblDiscount);
                    }

                    //lblStampDuty.Location = new Point(5, 310);


                    string res = "";
                    string paddedParam = res.PadRight(50);

                    Button btnEdit = new System.Windows.Forms.Button();
                    btnEdit.Click += BtnEdit_Click;
                    btnEdit.Text = paddedParam;
                    btnEdit.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    btnEdit.Width = 90;
                    btnEdit.Height = 40;
                    btnEdit.BackColor = Color.Transparent;
                    btnEdit.Location = new Point(25, 340);
                    btnEdit.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnEdit.BackgroundImage = Gene.Properties.Resources.edit;
                    btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnEdit.FlatAppearance.BorderSize = 0;

                    Bottompnl.Controls.Add(btnEdit);


                    Button btnDelete = new System.Windows.Forms.Button();
                    btnDelete.Click += BtnDelete_Click;
                    btnDelete.Text = paddedParam;
                    btnDelete.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    //btnDelete.Text = "";
                    btnDelete.Width = 90;
                    btnDelete.Height = 40;
                    //btnDelete.Location = new Point(270, 340);
                    btnDelete.Location = new Point(230, 340);
                    btnDelete.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnDelete.FlatAppearance.BorderSize = 0;
                    btnDelete.BackColor = Color.Transparent;
                    btnDelete.BackgroundImage = Gene.Properties.Resources.delete;
                    btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;


                    Bottompnl.Controls.Add(btnDelete);
                    //pnl.Size = new System.Drawing.Size(370, 100);
                    //pnl.Location = new Point((i * 420), 50);

                    pnl.Size = new System.Drawing.Size(330, 100);
                    pnl.Location = new Point((i * 420), 50);


                    //Bottompnl.Size = new System.Drawing.Size(370, 400);
                    //Bottompnl.Location = new Point((i * 420), 50);
                    Bottompnl.Size = new System.Drawing.Size(330, 400);
                    Bottompnl.Location = new Point((i * 420), 50);

                    //pnlSummery.Location = new Point(140, 240);
                    //pnlSummery.Size = new System.Drawing.Size(900, 470);
                    //pnlSummery.Location = new Point(150, 300);


                    //pnlSummery.Location = new Point(200, 100);
                    pnlSummery.Location = new Point(275, 100);
                    pnlSummery.Size = new System.Drawing.Size(790, 470);
                    pnlSummery.Controls.Add(pnl);
                    pnlSummery.Controls.Add(Bottompnl);

                }
                else if (counter > 2)
                {

                    Panel pnl = new Panel();
                    pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));

                    Panel Bottompnl = new Panel();

                    Bottompnl.BackColor = Color.White;

                    Label lblvehicle = new System.Windows.Forms.Label();
                    lblvehicle.Name = lblvehicle + i.ToString();
                    lblvehicle.ForeColor = Color.White;

                    lblvehicle.Text = "Vehicle Name :";
                    lblvehicle.Text += objlistRisk[i].RegistrationNo == null ? "0" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    lblvehicle.AutoSize = true;
                    lblvehicle.Location = new Point(i, 3);
                    lblvehicle.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblvehicle);


                    var paymentTerm = objlistRisk[i].PaymentTermId == 0 ? "" : Convert.ToString(objlistRisk[i].PaymentTermId);
                    var paymentTermName = "";
                    paymentTermName = GetPaymentTerm(paymentTerm);

                    Label lblTermly = new System.Windows.Forms.Label();
                    lblTermly.Name = lblTermly + i.ToString();
                    lblTermly.ForeColor = Color.White;
                    lblTermly.Text = paymentTermName;
                    lblTermly.AutoSize = true;
                    lblTermly.Location = new Point(i + 400, 3);
                    lblTermly.Font = new System.Drawing.Font("Arial Narrow", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnl.Controls.Add(lblTermly);


                    Label lblSumInsured = new System.Windows.Forms.Label();
                    lblSumInsured.Name = lblSumInsured + i.ToString();
                    lblSumInsured.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblSumInsured.Text = "Sum Insured :";
                    lblSumInsured.Text += objlistRisk[i].SumInsured == null ? "0" : Convert.ToString(objlistRisk[i].SumInsured);
                    lblSumInsured.AutoSize = true;
                    lblSumInsured.BackColor = Color.Transparent;
                    lblSumInsured.Location = new Point(i, 55);
                    lblSumInsured.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblSumInsured);


                    Label lblTotalpremium = new System.Windows.Forms.Label();
                    lblTotalpremium.Name = lblTotalpremium + i.ToString();
                    lblTotalpremium.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblTotalpremium.Text = "Basic premium :";
                    lblTotalpremium.Text += objlistRisk[i].Premium == null ? "0" : Convert.ToString(objlistRisk[i].Premium);
                    lblTotalpremium.AutoSize = true;
                    lblTotalpremium.BackColor = Color.Transparent;
                    //lblTotalpremium.Location = new Point(i + 150, 55);
                    lblTotalpremium.Location = new Point(i + 170, 55);
                    lblTotalpremium.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblTotalpremium);


                    Label lblZTSC = new System.Windows.Forms.Label();
                    lblZTSC.Name = lblZTSC + i.ToString();
                    lblZTSC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblZTSC.Text = "ZTSC :";
                    lblZTSC.Text += objlistRisk[i].ZTSCLevy == null ? "0" : Convert.ToString(objlistRisk[i].ZTSCLevy);
                    // lblZTSC.Width = 70;
                    lblZTSC.AutoSize = true;
                    lblZTSC.BackColor = Color.Transparent;
                    //lblZTSC.Location = new Point(i + 300, 55);
                    lblZTSC.Location = new Point(i + 340, 55);
                    lblZTSC.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblZTSC);

                    Label lblStampDuty = new System.Windows.Forms.Label();
                    lblStampDuty.Name = lblStampDuty + i.ToString();
                    lblStampDuty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
                    lblStampDuty.Text = "Stamp Duty :";
                    lblStampDuty.Text += objlistRisk[i].StampDuty == null ? "0" : Convert.ToString(objlistRisk[i].StampDuty);
                    //lblStampDuty.Width = 100;
                    lblStampDuty.AutoSize = true;
                    lblStampDuty.BackColor = Color.Transparent;
                    //lblStampDuty.Location = new Point(i + 420, 55);
                    lblStampDuty.Location = new Point(i + 450, 55);
                    lblStampDuty.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblStampDuty);


                    string res = "";
                    string paddedParam = res.PadRight(50);
                    //string paddedParam = res.PadRight(80);

                    Button btnEdit = new System.Windows.Forms.Button();
                    btnEdit.Click += BtnEdit_Click;
                    btnEdit.Text = paddedParam;
                    btnEdit.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    btnEdit.Width = 70;
                    //btnEdit.Location = new Point(i + 540, 55);

                    btnEdit.Location = new Point(i + 600, 55);

                    btnEdit.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    btnEdit.BackgroundImage = Gene.Properties.Resources.edit;
                    btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnEdit.FlatAppearance.BorderSize = 0;




                    Bottompnl.Controls.Add(btnEdit);


                    Button btnDelete = new System.Windows.Forms.Button();
                    //btnDelete.Text = "Delete";
                    btnDelete.Click += BtnDelete_Click;
                    btnDelete.Text = paddedParam;
                    btnDelete.Text += objlistRisk[i].RegistrationNo == null ? "" : Convert.ToString(objlistRisk[i].RegistrationNo);
                    btnDelete.Width = 70;

                    //btnDelete.Location = new Point(i + 630, 55);
                    btnDelete.Location = new Point(i + 680, 55);
                    btnDelete.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnDelete.FlatAppearance.BorderSize = 0;
                    btnDelete.BackgroundImage = Gene.Properties.Resources.delete;
                    btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                    Bottompnl.Controls.Add(btnDelete);

                    pnl.Size = new System.Drawing.Size(760, 40);
                    pnl.Location = new Point(i, (i * 110));


                    Bottompnl.Size = new System.Drawing.Size(760, 90);
                    Bottompnl.Location = new Point(i, (i * 110));

                    //pnlSummery.Location = new Point(140, 240);
                    //pnlSummery.Size = new System.Drawing.Size(900, 470);
                    pnlSummery.Location = new Point(270, 120);
                    pnlSummery.Size = new System.Drawing.Size(790, 470);
                    pnlSummery.Controls.Add(pnl);
                    pnlSummery.Controls.Add(Bottompnl);

                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string s = (sender as Button).Text;
            var vehicalDetails = objlistRisk.Find(c => c.RegistrationNo == s.Trim());
            objlistRisk.Remove(vehicalDetails);

            objRiskModel = new RiskDetailModel();

            loadVRNPanel();

            MessageBox.Show(s.Trim() + " Vehicle Successfully Deleted.");

            isVehicalDeleted = true;

            VehicalIndex = objlistRisk.Count - 1;

            if (objlistRisk.Count == 0)
            {
                PnlVrn.Visible = true;
                pnlSum.Visible = false;
                NewVRN();
            }

            //throw new NotImplementedException();
        }

        public string GetPaymentTerm(string paymentTerm)
        {
            var paymentTermName = "";
            if (paymentTerm == "1")
                paymentTermName = "Yearly";
            else
                paymentTermName = paymentTerm + " Months";

            return paymentTermName;
        }

        //private void ClearControl()
        //{
        //    txtVrn.Text = string.Empty;
        //    txtSumInsured.Text = string.Empty;
        //    cmbVehicleUsage.SelectedIndex = 0;
        //    cmbPaymentTerm.SelectedIndex = 0;
        //    cmbCoverType.SelectedIndex = 0;
        //    cmbMake.SelectedIndex = 0;
        //    cmbModel.SelectedIndex = 0;
        //    txtYear.Text = string.Empty;
        //    txtChasis.Text = string.Empty;
        //    txtEngine.Text = string.Empty;
        //    pnlAddMoreVehicle.Visible = false;
        //}

        private void RequestQuote()
        {
            throw new NotImplementedException();
        }

        public void CheckToken()
        {
            if (ObjToken == null)
            {
                ObjToken = IcServiceobj.getToken();
                ProcessICECashrequest(txtVrn.Text, txtSumInsured.Text, cmbMake.SelectedText, cmbModel.SelectedText, Convert.ToString(cmbPaymentTerm.SelectedValue), "", Convert.ToString(cmbCoverType.SelectedValue), Convert.ToString(cmbVehicleUsage.SelectedValue), "1");

            }

        }
        public void ProcessICECashrequest(String VRN, String SumINsured, String Make, String Model, String Paymentterm, String VehYear, String CoverType, String VehicleUsage, String TaxClass)
        {
            //ResultRootObject quoteresponse = IcServiceobj.RequestQuote(ObjToken.Response.PartnerToken, VRN, SumINsured, Make, Model, Convert.ToInt32(Paymentterm), Convert.ToInt32(VehYear), Convert.ToInt32(CoverType), Convert.ToInt32(VehicleUsage), ObjToken.PartnerReference, customerInfo);


            ResultRootObject quoteresponse = IcServiceobj.RequestQuote(objRiskModel, customerInfo, parternToken);
            if (quoteresponse != null)
            {
                ResultResponse resObject = quoteresponse.Response;
                if (resObject.Message.Contains("Partner Token has expired"))
                {
                    //ObjToken = null;
                    //CheckToken();
                    //  ObjToken = CheckParterTokenExpire();
                    ObjToken = IcServiceobj.getToken();
                    if (ObjToken != null)
                        parternToken = ObjToken.Response.PartnerToken;

                    Service_db.UpdateToken(ObjToken);

                }
                else if (resObject.Message == "Insufficient Fund to process financial transaction")
                {
                    MessageBox.Show(resObject.Message.ToString());
                }
                else
                {
                    MessageBox.Show(resObject.Message.ToString());
                }
            }
        }
        public void bindCoverType()
        {
            var client = new RestClient(ApiURL + "CoverTypes");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<CoverObject>>(response.Content);

            result.Insert(0, new CoverObject { Id = 0, name = "-Select-" });

            cmbCoverType.DataSource = result;
            cmbCoverType.DisplayMember = "name";
            cmbCoverType.ValueMember = "ID";
            //cmbCoverType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        public void bindMake()
        {
            var client = new RestClient(ApiURL + "Makes");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<MakeObject>>(response.Content);


            result.Insert(0, new MakeObject { MakeCode = "0", MakeDescription = "-Select-" });

            cmbMake.DataSource = result;
            cmbMake.DisplayMember = "MakeDescription";
            cmbMake.ValueMember = "MakeCode";


            cmbMake.SelectedValue = "0";

            bindModel(Convert.ToString(cmbMake.SelectedValue));

            //if (!this.IsHandleCreated)
            //{
            //    this.CreateHandle();
            //    bindModel(Convert.ToString(cmbMake.SelectedValue));
            //}
            //else
            //{
            //    this.Invoke(new Action(() => bindModel(Convert.ToString(cmbMake.SelectedValue))));
            //}
        }

        public void bindModel(String MaKECode)
        {
            var client = new RestClient(ApiURL + "Models?makeCode=" + MaKECode);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<ModelObject>>(response.Content);

            result.Insert(0, new ModelObject { ModelCode = "0", ModelDescription = "-Select-" });

            cmbModel.DataSource = result;
            cmbModel.DisplayMember = "ModelDescription";
            cmbModel.ValueMember = "ModelCode";
            cmbPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        }

        public void bindCurrency()
        {

            var client = new RestClient(ApiURL + "Currencies");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<CurrencyModel>>(response.Content);
            cmbCurrency.DataSource = result;

            result.Insert(0, new CurrencyModel { Id = 0, Name = "-Select-" });
            cmbCurrency.DisplayMember = "Name";
            cmbCurrency.ValueMember = "Id";


        }
        public void bindProduct()
        {

            var client = new RestClient(ApiURL + "Products");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<ProductsModel>>(response.Content);

            if (result != null)
            {
                result.Insert(0, new ProductsModel { Id = 0, ProductName = "-Select-" });
                cmbProducts.DataSource = result;
                cmbProducts.DisplayMember = "ProductName";
                cmbProducts.ValueMember = "Id";
                bindVehicleUsage(Convert.ToInt32(cmbProducts.SelectedValue));
            }


        }


        public decimal GetDiscount(decimal premiumAmount, int paymentTermId)
        {

            decimal discount = 0;
            var client = new RestClient(IceCashRequestUrl + "CalculateDiscount?premiumAmount=" + premiumAmount + "&PaymentTermId=" + paymentTermId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<RiskDetailModel>(response.Content);

            return result.Discount == null ? 0 : Math.Round(result.Discount.Value, 2);
        }

        public void bindPaymentType()
        {

            var client = new RestClient(ApiURL + "AllPayment");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<CoverObject>>(response.Content);
            var resultZinra = (new JavaScriptSerializer()).Deserialize<List<CoverObject>>(response.Content);
            var resultRadio = (new JavaScriptSerializer()).Deserialize<List<CoverObject>>(response.Content);
            if (result != null)
            {

                result.Insert(0, new CoverObject { Id = 0, name = "-Select-" });
                cmbPaymentTerm.DataSource = result;
                cmbPaymentTerm.DisplayMember = "name";
                cmbPaymentTerm.ValueMember = "ID";

                //Ds 13 Feb

                resultZinra.Insert(0, new CoverObject { Id = 0, name = "-Select-" });
                ZinPaymentDetail.DataSource = resultZinra;
                ZinPaymentDetail.DisplayMember = "name";
                ZinPaymentDetail.ValueMember = "ID";



                resultRadio.Insert(0, new CoverObject { Id = 0, name = "-Select-" });
                RadioPaymnetTerm.DataSource = resultRadio;
                RadioPaymnetTerm.DisplayMember = "name";
                RadioPaymnetTerm.ValueMember = "ID";

            }
        }

        public void bindAllCities()
        {
            var client = new RestClient(ApiURL + "AllCities");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<GetAllCities>>(response.Content);
            var resultTbA = (new JavaScriptSerializer()).Deserialize<List<GetAllCities>>(response.Content);

            var resultCorporate = (new JavaScriptSerializer()).Deserialize<List<GetAllCities>>(response.Content);

            if (result != null)
            {
                result.Insert(0, new GetAllCities { Id = 0, CityName = "-Select-" });
                cmdCity.DataSource = result;
                cmdCity.DisplayMember = "name";
                cmdCity.ValueMember = "CityName";

                // TBA combobox
                resultTbA.Insert(0, new GetAllCities { Id = 0, CityName = "-Select-" });
                cmbTBACity.DataSource = resultTbA;
                cmbTBACity.DisplayMember = "name";
                cmbTBACity.ValueMember = "CityName";

                // TBA combobox
                resultCorporate.Insert(0, new GetAllCities { Id = 0, CityName = "-Select-" });
                cmbCmpCity.DataSource = resultCorporate;
                cmbCmpCity.DisplayMember = "name";
                cmbCmpCity.ValueMember = "CityName";

            }
        }


        public void bindAllClasses()
        {
            var client = new RestClient(ApiURL + "AllTaxClasses");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<VehicleTaxClassModel>>(response.Content);

            if (result != null)
            {
                result.Insert(0, new VehicleTaxClassModel { TaxClassId = 0, Description = "-Select-" });
                cmbTaxClasses.DataSource = result;
                cmbTaxClasses.DisplayMember = "Description";
                cmbTaxClasses.ValueMember = "TaxClassId";
            }

        }

        public void bindVehicleUsage(int ProductId)
        {

            var client = new RestClient(ApiURL + "VehicleUsage?ProductId=" + ProductId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<VehUsageObject>>(response.Content);
            if (result != null && result.Count() > 0)
            {

                result.Insert(0, new VehUsageObject { Id = 0, VehUsage = "-Select-" });

                cmbVehicleUsage.DataSource = result;
                cmbVehicleUsage.DisplayMember = "vehUsage";
                cmbVehicleUsage.ValueMember = "id";
                //cmbVehicleUsage.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }

        }

        public int bindradioamout(int ProductId, int PaymentTermId)
        {

            vehicledetailModel obj = new vehicledetailModel { ProductId = ProductId, PaymentTermId = PaymentTermId };



            var client = new RestClient(ApiURL + "VehicleUsage");
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            //request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(obj);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<RadioLicenceAmount>(response.Content);


            return result.RadioLicenceAmounts == null ? 0 : result.RadioLicenceAmounts;

            //return result.RadioLicenceAmounts == null ? 0 : result.RadioLicenceAmounts;

            //objRiskModel.RadioLicenseCost = result.RadioLicenceAmounts;



        }

        private void frmQuote_Load(object sender, EventArgs e)
        {
            //txtZipCode.Text = "00263";
            //SmsService service = new SmsService();
            //service.SendSMS("263777670323", "test message from window");
        }

        private void txtVrn_Enter(object sender, EventArgs e)
        {

        }

        private void txtVrn_Leave(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            NewObjectDuringEditOrBack();
            //btnSave.Text = "Processing.";
            btnSave.Text = "Process...";

            Service_db.WriteIceCashLog("vrn req", "search vrn", "btnSave_Click", txtVrn.Text, branchName);
            //pictureBox1.Visible = true;

            //Service_db _service = new Service_db();
            //if (!_service.CheckVehicleExistOrNot(txtVrn.Text))
            //{
            //    MyMessageBox.ShowBox("This vrn alrady exist.", "Message");
            //    btnSave.Text = "Submit";
            //    return;
            //}


            // CheckToken();

            Worker_DoWork();
            btnSave.Text = "Submit";

            var TBA = ConfigurationManager.AppSettings["tba"];
            if (txtVrn.Text.ToUpper() == TBA)
            {

                lblChas.Visible = true;
                lblEngine.Visible = true;
                txtChasis.Visible = true;
                txtEngine.Visible = true;
            }
            else
            {
                lblChas.Visible = false;
                lblEngine.Visible = false;
                txtChasis.Visible = false;
                txtEngine.Visible = false;
            }


            //  pictureBox1.Visible = false;

        }




        private void Worker_DoWork()
        {
            isbackclicked = false;
            // this.Invoke(new Action(() => pictureBox1.Visible = true));
            // first screen where enter vrn number
            if (txtVrn.Text == string.Empty || txtVrn.Text == "Vehicle Registration Number" || txtVrn.Text.Length == 0 || (string.IsNullOrWhiteSpace(txtVrn.Text)))
            {
                //MessageBox.Show("Please Enter Registration Number");
                NewerrorProvider.SetError(txtVrn, "Please Enter Registration Number.");
                txtVrn.Focus();
                //  txtVrn.ForeColor = Color.Red;
                return;
            }
            else
            {
                txtIDNumber.Text = textSearchVrn.Text == "ID Number" ? "" : textSearchVrn.Text;
                if (VehicalIndex == -1)
                {
                    var vehicalDetails = objlistRisk.FirstOrDefault(c => c.RegistrationNo == txtVrn.Text.Trim());
                    if (vehicalDetails != null)
                    {
                        MessageBox.Show("You have already added this registration number.");
                        VrnAlredyExist();
                        return;
                    }
                }
                else
                {
                    objlistRisk[VehicalIndex].RegistrationNo = txtVrn.Text;
                    var vehicalList = objlistRisk.Where(c => c.RegistrationNo == txtVrn.Text.Trim()).ToList();
                    if (vehicalList != null)
                    {
                        if (vehicalList.Count > 1)
                        {
                            MessageBox.Show("You have already added this registration number.");
                            VrnAlredyExist();
                            return;
                        }
                    }
                }

                objRiskModel = new RiskDetailModel();
                objRiskModel.RegistrationNo = txtVrn.Text;
                objRiskModel.ALMBranchId = branchName == "" ? 0 : Convert.ToInt32(branchName);
                if (objRiskModel != null)
                {
                    if (rdCorporate.Checked)
                    {
                        if (VehicalIndex == -1)
                        {
                            objRiskModel.IsCorporateField = true;
                        }
                        else
                            objlistRisk[VehicalIndex].IsCorporateField = true;
                    }
                    if (rdPresonal.Checked)
                    {
                        if (VehicalIndex == -1)
                            objRiskModel.IsCorporateField = false;

                        else
                            objlistRisk[VehicalIndex].IsCorporateField = false;

                    }
                }

                //if (txtVrn.Text == "TBA")
                //{
                //    ObjToken = CheckParterTokenExpire();
                //    if (ObjToken != null)
                //    {
                //        parternToken = ObjToken.Response.PartnerToken;
                //    }

                //    pnlTBAPersonalDetails.Visible = true;
                //    PnlVrn.Visible = false;
                //    pnlAddMoreVehicle.Visible = false;
                //    txtTBAIDNumber.Text = textSearchVrn.Text == "Id Number" ? "" : textSearchVrn.Text;
                //    return;
                //}


                var success = 0;
                // success = ProcessQuote();
                success = 2;
                // objRiskModel.RegistrationNo = txtVrn.Text;

                if (success == 1)
                {
                    //pictureBox1.Visible = false;
                    pictureBox1.Visible = false;
                    pnlPersonalDetails.Visible = true;
                    // pnlConfirm.Visible = true;
                    //this.Invoke(new Action(() => pnlRiskDetails.Visible = true));
                    PnlVrn.Visible = false;
                    pnlAddMoreVehicle.Visible = false;
                }
                if (success == 2)
                {
                    //pictureBox1.Visible = false;
                    //this.Invoke(new Action(() => pictureBox1.Visible = false));


                    pictureBox1.Visible = false;

                    if (rdCorporate.Checked)
                    {
                        pnlCorporate.Visible = true;
                        txtCmpBusinessId.Text = textSearchVrn.Text == "ID Number" ? "" : textSearchVrn.Text;
                    }
                    else
                        pnlPersonalDetails.Visible = true;

                    // pnlRiskDetails.Visible = true;
                    // pnlConfirm.Visible = true;
                    //this.Invoke(new Action(() => pnlRiskDetails.Visible = true));
                    PnlVrn.Visible = false;
                    pnlAddMoreVehicle.Visible = false;

                    //MessageBox.Show("Unable to retrieve vehicle info from Zimlic.");
                    //  lblmessageConf.Text = "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.";
                    //MessageBox.Show("Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.");
                }
                if (success == 3)
                {
                    pictureBox1.Visible = false;
                    pnlPersonalDetails.Visible = true;
                    //  pnlConfirm.Visible = true;
                    //this.Invoke(new Action(() => pnlRiskDetails.Visible = true));
                    PnlVrn.Visible = false;
                    pnlAddMoreVehicle.Visible = false;
                }


                if (success == 4)
                {
                    lblVrnErrMsg.Text = "Vehicle Registration Number and ID Number are not correct.";
                    lblVrnErrMsg.ForeColor = Color.Red;
                }
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;
            // throw new NotImplementedException();
        }

        private void VrnAlredyExist()
        {
            pnlConfirm.Visible = false;
            //this.Invoke(new Action(() => pnlRiskDetails.Visible = false));
            PnlVrn.Visible = true;
            pnlAddMoreVehicle.Visible = false;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            PnlVrn.Visible = true;
            pnlSum.Visible = false;
            pnlAddMoreVehicle.Visible = false;
            pnlPersonalDetails.Visible = false;
            pnlConfirm.Visible = false;
            pnlRiskDetails.Visible = false;
            btnBacktoList.Visible = false;

            string s = (sender as Button).Text;
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == s.Trim());

            if (VehicalIndex != -1)
            {
                txtVrn.Text = s.Trim();
            }

            NewObjectDuringEditOrBack();
            //bindVehicleUsage();
            //bindPaymentType();
            //bindCoverType();
        }

        //public void ProcessQuote()
        public int ProcessQuote()
        {
            lblmessageConf.Text = "";
            int result = 0;
            // objRiskModel = new RiskDetailModel();
            ICEcashService IcServiceobj = new ICEcashService();

            //9Jan2019
            ObjToken = CheckParterTokenExpire();
            if (ObjToken != null)
                parternToken = ObjToken.Response.PartnerToken;



            quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(objRiskModel, customerInfo, parternToken); // comment for testing

            // parternToken = ObjToken.Response.PartnerToken;


            //quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(txtVrn.Text, parternToken, "d3238e6e-65fd-4b55-b7aa-5206ad79"); // comment for testing

            NewObjectDuringEditOrBack();


            if (quoteresponse != null)
            {
                resObject = quoteresponse.Response;
                //if token expire
                if (resObject != null && resObject.Message.Contains("Partner Token has expired"))
                {

                    ObjToken = IcServiceobj.getToken();
                    if (ObjToken != null)
                        parternToken = ObjToken.Response.PartnerToken;

                    Service_db.UpdateToken(ObjToken);

                    quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(objRiskModel, customerInfo, parternToken);
                    resObject = quoteresponse.Response;



                    //ObjToken = IcServiceobj.getToken();
                    //if (ObjToken != null)
                    //{
                    //    parternToken = ObjToken.Response.PartnerToken;
                    //    quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(objRiskModel, customerInfo, parternToken);
                    //    resObject = quoteresponse.Response;
                    //}
                    //if (ObjToken != null)
                    //{
                    //    parternToken = ObjToken.Response.PartnerToken;
                    //    quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber);
                    //    var _resObject = quoteresponse.Response;
                    //}
                }


                if (resObject != null && resObject.Quotes != null && resObject.Message.Contains("1 failed"))
                {
                    // lblConfirmMessage.Text = resObject.Quotes[0].Message;
                    MyMessageBox.ShowBox(resObject.Quotes[0].Message, "Message");
                }


                // if (resObject != null && resObject.Quotes != null && resObject.Quotes[0].Message != "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                if (resObject != null && resObject.Quotes != null && !resObject.Message.Contains("1 failed"))
                {
                    if (resObject != null && resObject.Message != "ICEcash System Error [O]")
                    {
                        result = 1;


                        // to check security reason
                        //if (resObject.Quotes[0].Client.IDNumber != textSearchVrn.Text.Trim())
                        //{
                        //    result = 4;
                        //}


                        //17-jan-19

                        if (resObject.Quotes != null && resObject.Quotes[0].Vehicle != null)
                        {


                            objRiskModel.isVehicleRegisteredonICEcash = true;

                            string make = resObject.Quotes[0].Vehicle.Make;

                            _ProductId = resObject.Quotes[0].Vehicle.VehicleType;

                            _TaxClass = resObject.Quotes[0].Vehicle.TaxClass == null ? 0 : Convert.ToInt32(resObject.Quotes[0].Vehicle.TaxClass);


                            _clientIdType = resObject.Quotes[0].Client == null ? "" : resObject.Quotes[0].Client.IDNumber;


                            string model = resObject.Quotes[0].Vehicle.Model;
                            if (!string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(model))
                            {
                                SaveVehicalMakeAndModel(make, model);
                                // 16-april
                                bindMake();
                            }
                            else
                            {
                                // set make and model if IceCash does not retrun
                                resObject.Quotes[0].Vehicle.Make = "0";
                                resObject.Quotes[0].Vehicle.Model = "0";
                            }
                        }
                        //End

                        //15 feb

                        //var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber);
                        //var _resObjects = _quoteresponse.Response;
                        //if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                        //{
                        //    //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
                        //    //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
                        //    this.Invoke(new Action(() => txtAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt)));
                        //    this.Invoke(new Action(() => txtpenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt)));
                        //    this.Invoke(new Action(() => txtradioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt)));
                        //}


                        //end

                        //Policy Details 
                        if (VehicalIndex != -1)
                        {
                            if (resObject.Quotes[0].InsuranceID != null)
                            {
                                objlistRisk[VehicalIndex].InsuranceId = resObject.Quotes[0].InsuranceID;
                            }
                            if (resObject.Quotes[0].Policy != null)
                            {
                                cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType);
                                if (resObject.Quotes[0].Policy.DurationMonths != null)
                                {
                                    cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                    if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                    {
                                        cmbPaymentTerm.SelectedValue = 1;
                                    }
                                    else
                                    {
                                        cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                    }


                                    // Ds 13 Feb

                                    ZinPaymentDetail.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                    if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                    {
                                        ZinPaymentDetail.SelectedValue = 1;
                                    }
                                    else
                                    {
                                        ZinPaymentDetail.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                    }


                                    RadioPaymnetTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                    if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                    {
                                        RadioPaymnetTerm.SelectedValue = 1;

                                    }
                                    else
                                    {
                                        RadioPaymnetTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                    }


                                }


                                //Bind premium amount
                                objlistRisk[VehicalIndex].Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                objlistRisk[VehicalIndex].ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
                                objlistRisk[VehicalIndex].StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);

                                var discount = 0.00m;
                                discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
                                objlistRisk[VehicalIndex].Discount = discount;
                            }

                            if (resObject.Quotes[0].Vehicle != null)
                            {
                                cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType;

                                _ProductId = resObject.Quotes[0].Vehicle.VehicleType;

                                // txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture;
                                Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);
                                cmbMake.SelectedIndex = index;

                                //Populating The readOnly TextFields for Model And Make !!!
                                vehicleMakeTxt.Text = resObject.Quotes[0].Vehicle.Make;
                                vehicleModeltxt.Text = resObject.Quotes[0].Vehicle.Model;


                                bindModel(cmbMake.SelectedValue.ToString());
                                Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);
                                cmbModel.SelectedIndex = indexModel;



                                //   SetValueDuringEdit(); 21_feb
                            }
                            int cmVehicleValue = 0;

                            if (!this.IsHandleCreated)
                            {
                                this.CreateHandle();
                                if (cmbVehicleUsage.SelectedValue != null)
                                {
                                    cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                                }
                            }
                            else
                            {
                                cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                            }



                            //if (cmbVehicleUsage.SelectedValue != null)
                            //{
                            //    this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                            //}
                            if (cmVehicleValue != 0)
                            {
                                //this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));

                                //29-Jan-2019

                                //var ProductId = bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
                                var ProductId = Convert.ToInt32(cmbProducts.SelectedValue);
                                if (ProductId != 0)
                                {
                                    objlistRisk[VehicalIndex].ProductId = Convert.ToInt32(ProductId);
                                }
                                //End
                            }
                            if (resObject.Quotes[0].Client != null)
                            {
                                txtFirstName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName;
                                txtPhone.Text = "";
                                txtAdd1.Text = resObject.Quotes[0].Client.Address1;
                                txtAdd2.Text = resObject.Quotes[0].Client.Address2;
                                //this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
                                cmdCity.Text = resObject.Quotes[0].Client.Town;
                                // txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber; // 09_may_2019
                                txtIDNumber.Text = textSearchVrn.Text == "ID Number" ? resObject.Quotes[0].Client.IDNumber : textSearchVrn.Text;
                            }


                            //this.Invoke(new Action(() => cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType)));
                            //this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType));

                            //int cmVehicleValue = 0;
                            //this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                            //if (cmVehicleValue != 0)
                            //{
                            //    this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));
                            //}
                            //this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));


                            //if (resObject.Quotes[0].Policy.DurationMonths == "12")
                            //{
                            //    cmbPaymentTerm.SelectedValue = 1;
                            //}
                            //else
                            //{
                            //    this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths)));
                            //}

                            //txtName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName;
                            //txtPhone.Text = "";
                            //this.Invoke(new Action(() => txtAdd1.Text = resObject.Quotes[0].Client.Address1));
                            //this.Invoke(new Action(() => txtAdd2.Text = resObject.Quotes[0].Client.Address2));
                            //this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
                            //this.Invoke(new Action(() => txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber));
                            // this.Invoke(new Action(() => txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture));

                            // Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);

                            //this.Invoke(new Action(() => cmbMake.SelectedIndex = index));

                            //this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));


                            //Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);

                            //this.Invoke(new Action(() => cmbModel.SelectedIndex = indexModel));

                            //Bind premium amount
                            //objlistRisk[VehicalIndex].Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount);
                            //objlistRisk[VehicalIndex].ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy);
                            //objlistRisk[VehicalIndex].StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty);
                            //objlistRisk[VehicalIndex].InsuranceId = resObject.Quotes[0].InsuranceID;

                            //var discount = 0.00m;
                            //this.Invoke(new Action(() => discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue))));

                            //objlistRisk[VehicalIndex].Discount = discount;

                        }
                        else
                        {
                            objRiskModel.isVehicleRegisteredonICEcash = true;
                            if (resObject.Quotes != null)
                            {
                                if (resObject.Quotes[0].InsuranceID != null)
                                {
                                    objRiskModel.InsuranceId = resObject.Quotes[0].InsuranceID;
                                }

                                if (resObject.Quotes[0].Policy != null)
                                {
                                    if (resObject.Quotes[0].Policy.InsuranceType != null)
                                    {
                                        cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType);
                                    }

                                    if (resObject.Quotes[0].Policy.DurationMonths != null)
                                    {
                                        cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);

                                        if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                        {
                                            cmbPaymentTerm.SelectedValue = 1;
                                        }
                                        else
                                        {
                                            cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                        }
                                        //Ds 13 Feb

                                        ZinPaymentDetail.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                        if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                        {
                                            ZinPaymentDetail.SelectedValue = 1;
                                        }
                                        else
                                        {
                                            ZinPaymentDetail.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                        }


                                        RadioPaymnetTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                        if (resObject.Quotes[0].Policy.DurationMonths == "12")
                                        {
                                            RadioPaymnetTerm.SelectedValue = 1;
                                            //bindradioamout();
                                        }
                                        else
                                        {
                                            RadioPaymnetTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                            //bindradioamout();
                                        }
                                    }

                                    //this.Invoke(new Action(() => objRiskModel.Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount)));

                                    objRiskModel.Premium = resObject.Quotes[0].Policy.CoverAmount == null ? 0 : Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);

                                    objRiskModel.ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
                                    objRiskModel.StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);

                                    var discount = 0.00m;
                                    discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
                                    objRiskModel.Discount = discount;
                                }
                                if (resObject.Quotes[0].Vehicle != null)
                                {
                                    // cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType;
                                    cmbProducts.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType;


                                    //txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture;
                                    Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);
                                    cmbMake.SelectedIndex = index;

                                    int indexMake = 0;
                                    indexMake = cmbMake.SelectedIndex;
                                    if (indexMake == -1)
                                    {
                                        bindModel("0");
                                    }
                                    else
                                    {
                                        // bindModel(cmbMake.SelectedValue.ToString()); // 17 _apr

                                        bindModel(resObject.Quotes[0].Vehicle.Make);

                                    }
                                    //Populate IceCash Make and Model data 
                                    vehicleMakeTxt.Text = resObject.Quotes[0].Vehicle.Make;
                                    vehicleModeltxt.Text = resObject.Quotes[0].Vehicle.Model;

                                    Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);
                                    cmbModel.SelectedIndex = indexModel;

                                    // cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType;

                                    //  cmbProducts.SelectedValue = resObject.Quotes[0].Vehicle.VehicleTyp


                                }
                                int cmVehicleValue = 0;

                                //this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));
                                if (!this.IsHandleCreated)
                                {
                                    this.CreateHandle();
                                    if (cmbVehicleUsage.SelectedValue != null)
                                    {
                                        cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                                    }
                                }
                                else
                                {
                                    cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                                }



                                //if (cmVehicleValue != 0)
                                //{
                                //    bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
                                //}

                                if (resObject.Quotes[0].Client != null)
                                {
                                    txtFirstName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName;
                                    txtPhone.Text = "";
                                    txtAdd1.Text = resObject.Quotes[0].Client.Address1;
                                    txtAdd2.Text = resObject.Quotes[0].Client.Address2;
                                    //this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
                                    cmdCity.Text = resObject.Quotes[0].Client.Town;
                                    txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber;

                                }
                            }

                            //this.Invoke(new Action(() => cmbCoverType.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.InsuranceType)));
                            //this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = resObject.Quotes[0].Vehicle.VehicleType));
                            //int cmVehicleValue = 0;

                            //this.Invoke(new Action(() => cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue)));


                            //if (cmVehicleValue != 0)
                            //{
                            //    this.Invoke(new Action(() => bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue))));
                            //}



                            //this.Invoke(new Action(() => txtName.Text = resObject.Quotes[0].Client.FirstName + " " + resObject.Quotes[0].Client.LastName));
                            //this.Invoke(new Action(() => txtPhone.Text = ""));
                            //this.Invoke(new Action(() => txtAdd1.Text = resObject.Quotes[0].Client.Address1));
                            //this.Invoke(new Action(() => txtAdd2.Text = resObject.Quotes[0].Client.Address2));
                            //this.Invoke(new Action(() => txtCity.Text = resObject.Quotes[0].Client.Town));
                            //this.Invoke(new Action(() => txtIDNumber.Text = resObject.Quotes[0].Client.IDNumber));
                            //this.Invoke(new Action(() => txtYear.Text = resObject.Quotes[0].Vehicle.YearManufacture));

                            //Int32 index = cmbMake.FindStringExact(resObject.Quotes[0].Vehicle.Make);
                            //this.Invoke(new Action(() => cmbMake.SelectedIndex = index));
                            //int indexMake = 0;
                            //this.Invoke(new Action(() => indexMake = cmbMake.SelectedIndex));
                            //if (indexMake == -1)
                            //{
                            //    bindModel("0");
                            //}
                            //else
                            //{
                            //    this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));
                            //}

                            //this.Invoke(new Action(() => bindModel(cmbMake.SelectedValue.ToString())));

                            //Int32 indexModel = cmbModel.FindString(resObject.Quotes[0].Vehicle.Model);

                            //this.Invoke(new Action(() => cmbModel.SelectedIndex = indexModel));

                            //this.Invoke(new Action(() => objRiskModel.Premium = Convert.ToDecimal(resObject.Quotes[0].Policy.CoverAmount)));
                            //this.Invoke(new Action(() => objRiskModel.ZTSCLevy = Convert.ToDecimal(resObject.Quotes[0].Policy.GovernmentLevy)));
                            //this.Invoke(new Action(() => objRiskModel.StampDuty = Convert.ToDecimal(resObject.Quotes[0].Policy.StampDuty)));


                            //var discount = 0.00m;
                            //this.Invoke(new Action(() => discount = GetDiscount(Convert.ToDecimal(resObject.Quotes == null ? "0.00" : resObject.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue))));

                            //objRiskModel.Discount = discount;
                        }
                    }
                }
                else
                {
                    result = 2;
                    //   SetValueDuringEdit();
                }
            }
            else
            {

                NewVRN();
                //  SetValueDuringEdit();
                int cmVehicleValue = 0;
                if (!this.IsHandleCreated)
                {
                    this.CreateHandle();
                    if (cmbVehicleUsage.SelectedValue != null)
                    {
                        cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                    }
                }
                else
                {
                    cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                }
                result = 3;
            }

            return result;
        }

        private void SetValueDuringEdit()
        {
            var SumInsured = 0.00m;
            var ZTSCLevies = 0.00m;
            var StampDuty = 0.00m;
            var Discount = 0.00m;

            if (VehicalIndex != -1)
            {
                //if (this.InvokeRequired)
                //{
                //    this.Invoke(new Action(() => cmbCoverType.SelectedValue = objlistRisk[VehicalIndex].CoverTypeId));
                //    this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = objlistRisk[VehicalIndex].VehicleUsage));
                //    this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = objlistRisk[VehicalIndex].PaymentTermId));



                //    SumInsured = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].SumInsured == null ? "0" : objlistRisk[VehicalIndex].SumInsured.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);

                //    if (objlistRisk[VehicalIndex].CoverTypeId == 4)
                //    {
                //        this.Invoke(new Action(() => label2.Visible = true));
                //        this.Invoke(new Action(() => txtSumInsured.Visible = true));
                //        txtSumInsured.Text = Convert.ToString(SumInsured);
                //    }
                //    else
                //    {
                //        this.Invoke(new Action(() => label2.Visible = false));
                //        this.Invoke(new Action(() => txtSumInsured.Visible = false));
                //    }


                //    ZTSCLevies = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].ZTSCLevy == null ? "" : objlistRisk[VehicalIndex].ZTSCLevy.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);


                //    this.Invoke(new Action(() => txtZTSCLevies.Text = Convert.ToString(ZTSCLevies)));


                //    StampDuty = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].StampDuty == null ? "" : objlistRisk[VehicalIndex].StampDuty.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);

                //    this.Invoke(new Action(() => txtStampDuty.Text = Convert.ToString(StampDuty)));

                //    Discount = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].Discount == null ? "" : objlistRisk[VehicalIndex].Discount.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);


                //    this.Invoke(new Action(() => txtDiscount.Text = Convert.ToString(Discount)));
                //}
                //else
                //{

                SumInsured = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].SumInsured == null ? "0.00" : objlistRisk[VehicalIndex].SumInsured.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);
                ZTSCLevies = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].ZTSCLevy == null ? "0.00" : objlistRisk[VehicalIndex].ZTSCLevy.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);
                StampDuty = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].StampDuty == null ? "0.00" : objlistRisk[VehicalIndex].StampDuty.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);
                Discount = Math.Round(Convert.ToDecimal(objlistRisk[VehicalIndex].Discount == null ? "0.00" : objlistRisk[VehicalIndex].Discount.ToString(), System.Globalization.CultureInfo.InvariantCulture), 2);


                //this.Invoke(new Action(() => txtSumInsured.Text = Convert.ToString(SumInsured)));


                //cmbVehicleUsage.SelectedIndex = cmbVehicleUsage.FindString(objlistRisk[VehicalIndex].VehicleUsage == null ? "" : Convert.ToString(objlistRisk[VehicalIndex].VehicleUsage));
                //cmbPaymentTerm.SelectedIndex = cmbPaymentTerm.FindString(objlistRisk[VehicalIndex].PaymentTermId == 0 ? "0" : objlistRisk[VehicalIndex].PaymentTermId.ToString());
                //cmbCoverType.SelectedIndex = cmbCoverType.FindString(objlistRisk[VehicalIndex].CoverTypeId == null ? "" : objlistRisk[VehicalIndex].CoverTypeId.ToString());




                cmbCoverType.SelectedValue = objlistRisk[VehicalIndex].CoverTypeId == null ? 0 : Convert.ToInt32(objlistRisk[VehicalIndex].CoverTypeId);
                cmbVehicleUsage.SelectedValue = objlistRisk[VehicalIndex].VehicleUsage == null ? 0 : Convert.ToInt32(objlistRisk[VehicalIndex].VehicleUsage);
                cmbPaymentTerm.SelectedValue = objlistRisk[VehicalIndex].PaymentTermId == 0 ? 0 : Convert.ToInt32(objlistRisk[VehicalIndex].PaymentTermId);




                //this.Invoke(new Action(() => cmbCoverType.SelectedValue = objlistRisk[VehicalIndex].CoverTypeId)); 
                //this.Invoke(new Action(() => cmbVehicleUsage.SelectedValue = objlistRisk[VehicalIndex].VehicleUsage));
                //this.Invoke(new Action(() => cmbPaymentTerm.SelectedValue = objlistRisk[VehicalIndex].PaymentTermId));

                if (objlistRisk[VehicalIndex].CoverTypeId == 4)
                {
                    label2.Visible = true;
                    txtSumInsured.Visible = true;
                    txtSumInsured.Text = Convert.ToString(SumInsured);
                }
                else
                {
                    label2.Visible = false;
                    txtSumInsured.Visible = false;
                }

                txtZTSCLevies.Text = Convert.ToString(ZTSCLevies);
                txtStampDuty.Text = Convert.ToString(StampDuty);
                txtDiscount.Text = Convert.ToString(Discount);
                //}
            }
        }

        public ICEcashTokenResponse CheckParterTokenExpire()
        {
            if (ObjToken != null && !string.IsNullOrEmpty(ObjToken.PartnerReference))
            {
                var icevalue = ObjToken;
                string format = "yyyyMMddHHmmss";
                var IceDateNowtime = DateTime.Now;
                var IceExpery = DateTime.ParseExact(icevalue.Response.ExpireDate, format, CultureInfo.InvariantCulture);
                if (IceDateNowtime > IceExpery)
                {
                    ObjToken = IcServiceobj.getToken();
                }
                icevalue = ObjToken;
            }
            else
            {
                ObjToken = IcServiceobj.getToken();
            }
            return ObjToken;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPernalBack_Click(object sender, EventArgs e)
        {
            // pnlSum.Visible = true;
            // pnlAddMoreVehicle.Visible = true;
            pnlPersonalDetails.Visible = false;
            PnlVrn.Visible = true;
            // pnlAddMoreVehicle.Visible = true;

            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);
        }

        private void btnRiskCont_Click(object sender, EventArgs e)
        {
            //  int CoverId = Convert.ToInt32(cmbCoverType.SelectedValue);

            if (cmbProducts.SelectedValue == null)
            {
                NewerrorProvider.SetError(cmbProducts, "Please select the Products");
                cmbProducts.Focus();
                return;
            }

            if (cmbVehicleUsage.SelectedValue == null)
            {
                NewerrorProvider.SetError(cmbVehicleUsage, "Please select the vehicle usage");
                cmbVehicleUsage.Focus();
                return;
            }

            if (cmbTaxClasses.SelectedValue == null)
            {
                NewerrorProvider.SetError(cmbTaxClasses, "Please select Tax class");
                cmbTaxClasses.Focus();
                return;
            }
            //if (cmbCurrency.SelectedIndex == 0)
            //{
            //    NewerrorProvider.SetError(cmbCurrency, "Please select the currency");
            //    cmbCurrency.Focus();
            //    return;
            //}



            //if (CoverId == 4) // for comprehensive
            //{
            //    if (txtSumInsured.Text == string.Empty || txtSumInsured.Text == "0")
            //    {
            //        //MessageBox.Show("Please Enter Sum Insured");
            //        NewerrorProvider.SetError(txtSumInsured, "Please Enter Sum Insured.");
            //        txtSumInsured.Focus();
            //        return;
            //    }

            //    if (!IsNumeric(txtSumInsured.Text))
            //    {
            //        NewerrorProvider.SetError(txtSumInsured, "Please Enter Valid Number.");
            //        txtSumInsured.Focus();
            //        return;
            //    }
            //}


            // need to do   

            btnRiskCont.Text = "Processing.";

            // if error occurend into IceCash





            //var ProductId = bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
            //  var ProductId = Convert.ToInt32(cmbVehicleUsage.SelectedValue);

            var ProductId = Convert.ToInt32(cmbProducts.SelectedValue);


            if (ProductId != 0)
            {
                objRiskModel.ProductId = Convert.ToInt32(ProductId);
            }
            // GetPremiumAmount_ChangeOfCoverType();



            if (VehicalIndex == -1)
            {
                if (txtSumInsured.Text != null && cmbVehicleUsage.SelectedValue != null && cmbPaymentTerm.SelectedValue != null && cmbCoverType.SelectedValue != null)
                {
                    //objRiskModel.SumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text);

                    objRiskModel.SumInsured = Math.Round(Convert.ToDecimal(txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text, System.Globalization.CultureInfo.InvariantCulture)), 2);
                    objRiskModel.VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);

                    //objRiskModel.PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    //objRiskModel.CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);

                    objRiskModel.TaxClassId = Convert.ToInt32(cmbTaxClasses.SelectedValue);
                    objRiskModel.CurrencyId = Convert.ToInt32(cmbCurrency.SelectedValue);
                    objRiskModel.ProductId = Convert.ToInt32(cmbProducts.SelectedValue);
                    //29-jan-2019

                    //12-Feb-2019
                    //var radioamount = bindradioamout(objRiskModel.ProductId, objRiskModel.PaymentTermId);
                    //if (radioamount != null)
                    //{
                    //    objRiskModel.RadioLicenseCost = radioamount;
                    //}
                }
            }
            else
            {
                if (txtSumInsured.Text != null && cmbVehicleUsage.SelectedValue != null && cmbPaymentTerm.SelectedValue != null && cmbCoverType.SelectedValue != null)
                {
                    //objlistRisk[VehicalIndex].SumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text);

                    objlistRisk[VehicalIndex].SumInsured = Math.Round(Convert.ToDecimal(txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text, System.Globalization.CultureInfo.InvariantCulture)), 2);
                    objlistRisk[VehicalIndex].VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                    objlistRisk[VehicalIndex].PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    objlistRisk[VehicalIndex].CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                    objlistRisk[VehicalIndex].CurrencyId = Convert.ToInt32(cmbCurrency.SelectedValue);
                    objlistRisk[VehicalIndex].TaxClassId = Convert.ToInt32(cmbTaxClasses.SelectedValue);
                    objlistRisk[VehicalIndex].ProductId = Convert.ToInt32(cmbProducts.SelectedValue);
                    //29-jan-2019
                    //  ProductId = bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
                    if (ProductId != 0)
                    {
                        objlistRisk[VehicalIndex].ProductId = Convert.ToInt32(ProductId);
                    }
                    //12-Feb-2019

                    //var radioamount = bindradioamout(objRiskModel.ProductId, objRiskModel.PaymentTermId);
                    //if (radioamount != null)
                    //{
                    //    objlistRisk[VehicalIndex].RadioLicenseCost = radioamount;
                    //}
                }
            }
            //pnlConfirm.Visible = true;



            if (Convert.ToInt32(cmbCoverType.SelectedValue) == (int)eCoverType.ThirdParty)
            {
                chkRoadsideAssistance.Checked = false;
                chkRoadsideAssistance.Visible = false;
            }
            else
            {
                chkRoadsideAssistance.Visible = true;
            }

            if (_iceCashErrorMsg != "" && txtVrn.Text.Trim().ToUpper() != _tba)
            {
                objRiskModel.isVehicleRegisteredonICEcash = false;
                RequestVehicleDetails();
            }


            btnRiskCont.Text = "Continue";

            if (_iceCashErrorMsg != "")
            {
                MyMessageBox.ShowBox(_iceCashErrorMsg);
                pnlRiskDetails.Visible = false;
                PnlVrn.Visible = true;
                return;
            }


            //  pnlOptionalCover.Visible = true;
            pnlConfirm.Visible = true;
            pnlRiskDetails.Visible = false;

            if (txtVrn.Text.Trim().ToUpper() != _tba)
            {
                // vehicleModeltxt.Visible = true; // getting not selected make and model
                // vehicleMakeTxt.Visible = true;

                //cmbMake.Visible = true; // 23_jan 20
                //cmbModel.Visible = true;

                //Int32 indexMake = cmbMake.FindStringExact(objRiskModel.MakeId);
                //cmbMake.SelectedIndex = indexMake;
                //Int32 indexModel = cmbModel.FindString(objRiskModel.ModelId);
                //cmbModel.SelectedIndex = indexModel;

            }
            else
            {
                //  cmbMake.Visible = true;
                //cmbModel.Visible = true;
            }

        }


        private void btnRiskBack_Click(object sender, EventArgs e)
        {
            //PnlVrn.Visible = true;
            // pnlConfirm.Visible = true;
            // pnlInsurance.Visible = true;


            pnlRiskDetails.Visible = false;

            //if (txtVrn.Text.ToUpper() == _tba || !_insuranceAndLicense)
            //    pnlInsurance.Visible = true;
            //else
            //    pnlRadioZinara.Visible = true;

            pnlInsurance.Visible = true;


        }

        private void txtCoverStartDate_MaskInputRejected(object sender, System.Windows.Forms.MaskInputRejectedEventArgs e)
        {

        }

        private void btnPr2Back_Click(object sender, EventArgs e)
        {

        }

        private void btnPersoanlContinue_Click(object sender, EventArgs e)
        {

            //if (txtName.Text == string.Empty || txtEmailAddress.Text == string.Empty || txtPhone.Text == string.Empty)
            //{
            //    MessageBox.Show("Please Enter the required fields");
            //    return;
            //}
            if (txtFirstName.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtFirstName, "Please enter first name.");
                txtFirstName.Focus();
                return;
            }

            if (txtLastName.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtLastName, "Please enter last name.");
                txtLastName.Focus();
                return;
            }


            if (txtEmailAddress.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtEmailAddress, "Please enter the email.");
                txtEmailAddress.Focus();
                return;
            }


            if (!string.IsNullOrWhiteSpace(txtEmailAddress.Text))
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(txtEmailAddress.Text))
                {
                    NewerrorProvider.SetError(txtEmailAddress, "Please enter the valid email.");
                    txtEmailAddress.Focus();
                    return;
                }
            }


            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                NewerrorProvider.SetError(txtPhone, "Please enter the phone number.");
                txtPhone.Focus();
                return;
            }
            if (!(rdbMale.Checked || rdbFemale.Checked))
            {
                //MessageBox.Show("Please Select Gender");
                NewerrorProvider.SetError(rdbFemale, "Please select the gender");
                //rdbFemale.Focus();
                return;
            }
            //if (txtPhone.Text.Length < 10)
            //{
            //    MessageBox.Show("Please Enter a Valid Phone number");
            //    txtPhone.Focus();
            //    return;
            //}
            //if (txtPhone.Text != string.Empty)
            //{
            //    string phone = txtPhone.Text;
            //    if (phone != string.Empty)
            //    {
            //        Regex re = new Regex("^9[0-9]{9}");

            //        if (re.IsMatch(txtPhone.Text.Trim()) == false || txtPhone.Text.Length > 10)
            //        {
            //            MessageBox.Show("Invalid Mobile Number!!");
            //            txtPhone.Focus();
            //        }

            //        //var a = (Regex.Match(phone, @"^(\+[0-9]{9})$").Success);
            //        //var a = (Regex.Match(phone, @"[0-9]{3}-[0-9]{3}-[0-9]{4}").Success);           
            //        //if (a==false)
            //        //{
            //        //    MessageBox.Show("Format: 123-456-7890");
            //        //    txtPhone.Focus();
            //        //    return;
            //        //}        
            //    }
            //}

            if (txtFirstName.Text != string.Empty && txtEmailAddress.Text != string.Empty && txtPhone.Text != string.Empty)
            {
                //int result = checkEmailExist();

                //if(result==1)
                //    MessageBox.Show("Email already Exist");


                pnlPersonalDetails2.Visible = true;
                pnlPersonalDetails.Visible = false;


                //if (result == 1)
                //{
                //    MessageBox.Show("Email already Exist");
                //    NewerrorProvider.SetError(txtEmailAddress, "Email already exist");
                //    txtEmailAddress.Focus();
                //    return;
                //}
                //else
                //{
                //    pnlPersonalDetails2.Visible = true;
                //    pnlPersonalDetails.Visible = false;
                //}



            }

            string theDate = txtDOB.Value.ToString("dd/MM/yyyy");
            //if (!rdbMale.Checked)
            //{
            //    MessageBox.Show("Please select gender");
            //    return;
            //}
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnConfContinue_Click(object sender, EventArgs e)
        {
            // third screen confirm vehical details

            pictureBoxConfirm.Visible = true;


            btnConfContinue.Text = "Processing..";

            // VehicalIndex= - 1; // uncomment when it will be run for multiple vehicle

            lblmessageConf.Text = "";
            if (cmbMake.SelectedIndex == 0)
            {
                NewerrorProvider.SetError(cmbMake, "Please select the make");
                cmbMake.Focus();
                return;
            }
            if (cmbModel.SelectedIndex == 0)
            {
                NewerrorProvider.SetError(cmbModel, "Please enter the model");
                cmbModel.Focus();
                return;
            }

            SetLoadingPnlRiskDetail(true);


            objRiskModel.MakeId = Convert.ToString(cmbMake.SelectedValue);
            objRiskModel.ModelId = Convert.ToString(cmbModel.SelectedValue);
            objRiskModel.EngineNumber = Convert.ToString(txtEngine.Text);
            objRiskModel.ChasisNumber = Convert.ToString(txtChasis.Text);

            //if (VehicalIndex == -1)
            //{

            //    // objRiskModel.VehicleYear = txtYear.Text == "" ? 1900 : Convert.ToInt32(txtYear.Text);
            //    objRiskModel.MakeId = Convert.ToString(cmbMake.SelectedValue);
            //    objRiskModel.ModelId = Convert.ToString(cmbModel.SelectedValue);
            //    objRiskModel.EngineNumber = Convert.ToString(txtEngine.Text);
            //    objRiskModel.ChasisNumber = Convert.ToString(txtChasis.Text);

            //}
            //else
            //{
            //    // objlistRisk[VehicalIndex].VehicleYear = txtYear.Text == "" ? 1900 : Convert.ToInt32(txtYear.Text);
            //    objlistRisk[VehicalIndex].MakeId = cmbMake.SelectedValue == null ? "0" : Convert.ToString(cmbMake.SelectedValue);
            //    objlistRisk[VehicalIndex].ModelId = cmbModel.SelectedValue == null ? "0" : Convert.ToString(cmbModel.SelectedValue);
            //    objlistRisk[VehicalIndex].EngineNumber = Convert.ToString(txtEngine.Text);
            //    objlistRisk[VehicalIndex].ChasisNumber = Convert.ToString(txtChasis.Text);

            //}



            if (txtVrn.Text.ToUpper() == _tba)
            {
                ObjToken = IcServiceobj.getToken();
                if (ObjToken != null)
                    parternToken = ObjToken.Response.PartnerToken;


                ResultRootObject quoteresponse = IcServiceobj.RequestQuote(objRiskModel, customerInfo, parternToken);
                if (quoteresponse != null && (quoteresponse.Response.Message.Contains("Partner Token has expired") || quoteresponse.Response.Message.Contains("Invalid Partner Token")))
                {
                    ObjToken = IcServiceobj.getToken();
                    if (ObjToken != null)
                    {
                        parternToken = ObjToken.Response.PartnerToken;
                        Service_db.UpdateToken(ObjToken);
                        quoteresponse = IcServiceobj.RequestQuote(objRiskModel, customerInfo, parternToken);
                    }
                }
                CaculatePreiumForTBA(quoteresponse);
            }



            CalculatePremium();
            if (VehicalIndex != -1)
            {
                //Update vehical list
                SetValueForUpdate();
                loadVRNPanel(); // 19_feb
                VehicalIndex = -1;
            }
            else
            {

                objRiskModel.NoOfCarsCovered = 1;
                objlistRisk.Add(objRiskModel);

                isbackclicked = false;
                loadVRNPanel();
            }

            PaymentSummary();

            pnlConfirm.Visible = false;
            pnlsumary.Visible = true;
            // pnlSum.Visible = true;



            //if (prior == 0 && txtVrn.Text != _tba)
            //if (txtVrn.Text != _tba)
            //{
            //    CalculatePremiumAndZinraLicense();
            //    pictureBoxConfirm.Visible = false;

            //    pnlConfirm.Visible = false;


            //    btnConfContinue.Text = "Continue";

            //}
            //else
            //{
            //    pictureBoxConfirm.Visible = false;

            //    pnlConfirm.Visible = false;
            //    callPnlSummary();


            //    //pnlOptionalCover.Visible = true;

            //    btnConfContinue.Text = "Continue";
            //}


            SetLoadingPnlRiskDetail(false);

            btnConfContinue.Text = "Continue";
        }


        private void SetLoadingPnlRiskDetail(bool displayLoader)
        {
            if (displayLoader)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    pictureBoxConfirm.Visible = true;
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    pictureBoxConfirm.Visible = false;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
        }



        private void CaculatePreiumForTBA(ResultRootObject quoteresponse)
        {
            var response = quoteresponse.Response.Result;
            if (quoteresponse != null && quoteresponse.Response.Quotes[0] != null)
            {
                objRiskModel.isVehicleRegisteredonICEcash = true;
                objRiskModel.BasicPremiumICEcash = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                objRiskModel.Premium = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                objRiskModel.ZTSCLevy = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
                objRiskModel.StampDuty = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);


                //var discount = GetDiscount(Convert.ToDecimal(quoteresponse.Response.Quotes[0] == null ? "0.00" : quoteresponse.Response.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
                //objRiskModel.Discount = discount;

                objRiskModel.Discount = 0;

                objRiskModel.InsuranceId = quoteresponse.Response.Quotes[0].InsuranceID;

            }
        }




        private void CalculatePremiumAndZinraLicense()
        {
            var tba = ConfigurationManager.AppSettings["tba"];
            if (tba == txtVrn.Text.ToUpper())
            {
                pnlOptionalCover.Visible = false;



                CalculatePremium();
                pnlSum.Visible = true;

                if (VehicalIndex != -1)
                {
                    //Update vehical list
                    SetValueForUpdate();
                    loadVRNPanel(); // 19_feb
                    VehicalIndex = -1;
                }
                else
                {

                    objRiskModel.NoOfCarsCovered = objlistRisk.Count() + 1;
                    objlistRisk.Add(objRiskModel);
                    isbackclicked = false;
                    loadVRNPanel();

                }

                return;
            }

            //if (Convert.ToInt32(cmbCoverType.SelectedValue) != (int)eCoverType.Comprehensive && objRiskModel.isVehicleRegisteredonICEcash)
            //{
            //    if (rdbFemale.Checked)
            //        _clientIdType = "2";
            //    else
            //        _clientIdType = "1";

            //    GetDefaultZinraLiceenseFee(Convert.ToString(cmbPaymentTerm.SelectedValue), _clientIdType, customerInfo.NationalIdentificationNumber);

            //}


            // pnlRadioZinara.Visible = true;

            //if (chkRadioLicence.Checked && chkZinara.Checked)
            //{
            //    pnlRadio.Visible = true;
            //    pnlZinara.Visible = true;
            //}

            //else if (chkRadioLicence.Checked)
            //{
            //    pnlRadio.Visible = true;
            //    pnlZinara.Visible = false;

            //}
            //else if (chkZinara.Checked)
            //{
            //    pnlZinara.Visible = true;
            //    pnlRadio.Visible = false;
            //}
            //else
            //{
            //    pnlRadio.Visible = false;
            //    pnlZinara.Visible = false;

            //}
            // pnlOptionalCover.Visible = false;
            var productid = objRiskModel.ProductId;
        }

        public static bool IsNumeric(string str)
        {
            try
            {
                str = str.Trim();
                int foo = int.Parse(str);
                return (true);
            }
            catch (FormatException)
            {
                // Not a numeric value
                return (false);
            }
        }


        //public void bindAllCountryCode()
        //{
        //    var client = new RestClient(ApiURL + "GetCountry");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("password", Pwd);
        //    request.AddHeader("username", username);
        //    request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    var result = (new JavaScriptSerializer()).Deserialize<List<CountryModel>>(response.Content);
        //    if (result != null)
        //    {
        //        cmbCode.DataSource = result;
        //        cmbCode.DisplayMember = "Country";
        //        cmbCode.ValueMember = "Id";
        //    }
        //}


        public void bindAllCodes()
        {
            //  List<RootObject> code = new List<RootObject>();
            var client = new RestClient(ApiURL + "PhoneNumbers");

            // var results = new RestClient(ApiURL + "PhoneNumbers");

            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            // var result = JsonConvert.DeserializeObject(response.Content);

            CountryObject result = Newtonsoft.Json.JsonConvert.DeserializeObject<CountryObject>(response.Content);

            CountryObject resultCompamy = Newtonsoft.Json.JsonConvert.DeserializeObject<CountryObject>(response.Content);

            //code = result;
            //var result = (new JavaScriptSerializer()).Deserialize<List<RootObject>>(response.Content).ToList();
            // var result = (new JavaScriptSerializer()).Deserialize<List<CountryModel>>(response.Content);

            //string code = Convert.ToString(result);
            if (result != null)
            {
                cmbCode.DataSource = result.countries;
                cmbCode.DisplayMember = "DisplayName";
                cmbCode.ValueMember = "code";
                cmbCode.SelectedValue = "+263";

                cmbCmpCode.DataSource = resultCompamy.countries;
                cmbCmpCode.DisplayMember = "DisplayName";
                cmbCmpCode.ValueMember = "code";
                cmbCmpCode.SelectedValue = "+263";

                cmbTBAPhoneCode.DataSource = resultCompamy.countries;
                cmbTBAPhoneCode.DisplayMember = "DisplayName";
                cmbTBAPhoneCode.ValueMember = "code";
                cmbTBAPhoneCode.SelectedValue = "+263";


                // txtCmpCode
            }
        }

        private void btnConfBack_Click(object sender, EventArgs e)
        {
            //pnlRiskDetails.Visible = true;
            if (txtVrn.Text.ToUpper() == "TBA")
            {
                pnlTBAPersonalDetails.Visible = true;
                pnlConfirm.Visible = false;
                lblChas.Visible = true;
                lblEngine.Visible = true;
                txtChasis.Visible = true;
                txtEngine.Visible = true;
            }
            else
            {
                // PnlVrn.Visible = true;
                pnlRiskDetails.Visible = true;
                pnlConfirm.Visible = false;
                lblChas.Visible = false;
                lblEngine.Visible = false;
                txtChasis.Visible = false;
                txtEngine.Visible = false;
            }
        }

        private void pnlVRNtextbox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void pnlSummery_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void pnlPersonalDetails_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void pnlsumary_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void btnPerBack2_Click(object sender, EventArgs e)
        {
            pnlPersonalDetails.Visible = true;
            pnlPersonalDetails2.Visible = false;
        }

        public void btnPer2Con_Click(object sender, EventArgs e)
        {


            if (txtAdd1.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtAdd1, "Please enter the Address1");
                txtAdd1.Focus();
                return;
            }
            //if (txtAdd2.Text == string.Empty)
            //{

            //    NewerrorProvider.SetError(txtAdd2, "Please enter the Address2");
            //    txtAdd2.Focus();
            //    return;
            //}
            if (cmdCity.SelectedIndex == -1)
            {
                NewerrorProvider.SetError(cmdCity, "Please select the city");
                cmdCity.Focus();
                return;
            }
            if (cmdCity.SelectedIndex == 0)
            {
                NewerrorProvider.SetError(cmdCity, "Please select the city");
                cmdCity.Focus();
                return;
            }


            if (txtIDNumber.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtIDNumber, "Please enter the ID Number");
                txtIDNumber.Focus();
                return;
            }


            if (txtAdd1.Text != string.Empty && txtAdd2.Text != string.Empty && cmdCity.SelectedIndex != -1 && txtIDNumber.Text != string.Empty)
            {

                pnlPersonalDetails2.Visible = false;
                pnlInsurance.Visible = true;

                if (_insuranceAndLicense && txtVrn.Text.ToUpper() != _tba)
                    pnlRadioZinaraIns.Visible = true;
                else
                    pnlRadioZinaraIns.Visible = false;


                //  var strName = txtFirstName.Text.Split(' ');
                customerInfo.FirstName = txtFirstName.Text;
                customerInfo.LastName = txtLastName.Text;

                customerInfo.EmailAddress = txtEmailAddress.Text;
                customerInfo.AddressLine2 = txtAdd2.Text;
                customerInfo.DateOfBirth = Convert.ToDateTime(txtDOB.Text);
                //customerInfo.City = txtCity.Text;
                customerInfo.City = cmdCity.Text;
                customerInfo.PhoneNumber = txtPhone.Text;
                customerInfo.CountryCode = "+263";
                customerInfo.AddressLine1 = txtAdd1.Text;
                customerInfo.NationalIdentificationNumber = txtIDNumber.Text;
                customerInfo.CountryCode = cmbCode.SelectedValue.ToString();
                // customerInfo.Gender = rdbFemale

                if (rdbMale.Checked)
                    customerInfo.Gender = "Male";
                else if (rdbFemale.Checked)
                    customerInfo.Gender = "Female";

                customerInfo.Zipcode = "00263";

                customerInfo.BranchId = branchName == "" ? 0 : Convert.ToInt32(branchName);
            }



        }
        public void SetUserInput()
        {
            UserInput objU = new UserInput();
            objU.VRN = txtVrn.Text;
            objU.SumInsured = txtSumInsured.Text;



            if (cmbVehicleUsage.SelectedValue != null)
            {
                objU.VehicalUsage = Convert.ToString(cmbVehicleUsage.SelectedValue);
            }
            if (cmbPaymentTerm.SelectedValue != null)
            {
                objU.PaymentTerm = Convert.ToString(cmbPaymentTerm.SelectedValue);
            }
            if (cmbCoverType.SelectedValue != null)
            {
                objU.CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);
            }

            if (cmbCurrency.SelectedValue != null)
            {
                objU.Currency = Convert.ToInt32(cmbCurrency.SelectedValue);
            }

            //objU.VehicalUsage = Convert.ToString(cmbVehicleUsage.SelectedValue);
            //objU.PaymentTerm = Convert.ToString(cmbPaymentTerm.SelectedValue);
            //objU.CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);



            //Vehicle Type
            objU.Make = cmbMake.SelectedText;
            objU.Model = cmbModel.SelectedText;
            //objU.MakeID = Convert.ToString(cmbMake.SelectedValue);

            if (cmbMake.SelectedValue != null)
            {
                objU.MakeID = Convert.ToString(cmbMake.SelectedValue);
            }
            if (cmbModel.SelectedValue != null)
            {
                objU.ModelID = Convert.ToString(cmbModel.SelectedValue);
            }


            //objU.ModelID = Convert.ToString(cmbModel.SelectedValue);
            // objU.Year = txtYear.Text;
            objU.ChasisNumber = txtChasis.Text;
            objU.EngineNumber = txtEngine.Text;


            //PersonalDetails1
            objU.Name = txtFirstName.Text;
            objU.EmailAddress = txtEmailAddress.Text;
            objU.Phone = txtPhone.Text;
            objU.Gender = "";
            //objU.DOB = txtDOB.Text;
            objU.DOB = txtDOB.Value.ToString("MM/dd/yyyy");



            //PersonalDetails2
            objU.Address1 = txtAdd1.Text;
            objU.Address2 = txtAdd2.Text;
            //objU.City = txtCity.Text;
            objU.City = Convert.ToString(cmdCity.Text);
            //objU.Zip = txtZipCode.Text;
            objU.IDNumber = txtIDNumber.Text;


            //optionalCover
            objU.ExcessBuyback = Convert.ToInt32(chkExcessBuyback.Checked);
            objU.RoadsideAssistance = Convert.ToInt32(chkRoadsideAssistance.Checked);
            objU.MedicalExpenses = Convert.ToInt32(chkMedicalExpenses.Checked);
            // objU.PassengerAccidentalCover = Convert.ToInt32(chkPassengerAccidentalCover.Checked);
            objU.NumberOfPerson = Convert.ToInt32(cmbNoofPerson.Value);
            objListUserInput.Add(objU);
            //NewVRN();
        }

        private void btnSumBack_Click(object sender, EventArgs e)
        {

            pnlsumary.Visible = false;
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);
            // VehicalIndex = -1; 

            pnlConfirm.Visible = true;
            //if (objlistRisk[VehicalIndex].IsCorporateField)
            //{
            //    pnlCorporate.Visible = true;

            //}
            //else
            //{
            //    // pnlPersonalDetails2.Visible = true;
            //    // pnlSum.Visible = true;
            //    pnlConfirm.Visible = true;
            //}

        }

        private void frmQuote_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Pen p = new Pen(Color.Red);
            //Graphics g = e.Graphics;
            //int variance = 3;
            //g.DrawRectangle(p, new Rectangle(txtVrn.Location.X, txtVrn.Location.Y, txtVrn.Width , txtVrn.Height ));
        }

        private void txtVrn_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void txtVrn_Leave_1(object sender, EventArgs e)
        {

            if (txtVrn.Text.Length == 0)
            {
                txtVrn.Text = "Vehicle Registration Number";
                txtVrn.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtVrn_Enter_1(object sender, EventArgs e)
        {
            if (txtVrn.Text == "Vehicle Registration Number")
            {
                txtVrn.Text = "";
                txtVrn.ForeColor = SystemColors.GrayText;
            }
        }

        private void PnlVrn_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void btnContionOptionalCover_Click(object sender, EventArgs e)
        {

            // for shwing zinra licene fee
            var tba = ConfigurationManager.AppSettings["tba"];
            if (tba == txtVrn.Text)
            {
                pnlOptionalCover.Visible = false;
                pnlSum.Visible = true;

                CalculatePremium();


                if (VehicalIndex != -1)
                {
                    //Update vehical list
                    SetValueForUpdate();
                    loadVRNPanel(); // 19_feb
                    VehicalIndex = -1;
                }
                else
                {

                    objRiskModel.NoOfCarsCovered = objlistRisk.Count() + 1;
                    objlistRisk.Add(objRiskModel);
                    isbackclicked = false;
                    loadVRNPanel();

                }

                return;
            }

            if (Convert.ToInt32(cmbCoverType.SelectedValue) != (int)eCoverType.Comprehensive && objRiskModel.isVehicleRegisteredonICEcash)
            {
                if (rdbFemale.Checked)
                    _clientIdType = "2";
                else
                    _clientIdType = "1";

                GetDefaultZinraLiceenseFee(Convert.ToString(cmbPaymentTerm.SelectedValue), _clientIdType, customerInfo.NationalIdentificationNumber);

            }


            pnlRadioZinara.Visible = true;

            if (chkRadioLicence.Checked && chkZinara.Checked)
            {
                pnlRadio.Visible = true;
                pnlZinara.Visible = true;
            }

            else if (chkRadioLicence.Checked)
            {
                pnlRadio.Visible = true;
                pnlZinara.Visible = false;

            }
            else if (chkZinara.Checked)
            {
                pnlZinara.Visible = true;
                pnlRadio.Visible = false;
            }
            else
            {
                pnlRadio.Visible = false;
                pnlZinara.Visible = false;

            }
            pnlOptionalCover.Visible = false;
            var productid = objRiskModel.ProductId;

        }
        public void SetValueForUpdate()
        {
            // VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == objRiskModel.RegistrationNo);

            objlistRisk[VehicalIndex].RegistrationNo = txtVrn.Text;
            //objlistRisk[VehicalIndex].SumInsured = sum
        }

        private void btnOptionCoverBack_Click(object sender, EventArgs e)
        {
            btnAddMoreVehicle.Visible = true;
            // pnlRiskDetails.Visible = true;
            pnlConfirm.Visible = true;
            pnlOptionalCover.Visible = false;
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);
        }

        private void pnl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Graphics v = e.Graphics;
            //DrawRoundRect(v, Pens.Black, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1, 10);
            ////Without rounded corners
            ////e.Graphics.DrawRectangle(Pens.Blue, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            //base.OnPaint(e);
        }
        public void DrawRoundRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);
            gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));
            gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);
            gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);
            gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            g.DrawPath(p, gp);
            gp.Dispose();
        }

        private void btnAddvehicle_Click(object sender, EventArgs e)
        {
            txtVrn.Visible = true;
            textSearchVrn.Visible = true;
            PnlVrn.Visible = true;
            pnlSum.Visible = false;
            VRNnumForBack = txtVrn.Text; // for maintaning back to list
            txtVrn.Text = "";
            //  NewVRN();
            pnlAddMoreVehicle.Visible = false;
            VehicalIndex = -1;
            btnBacktoList.Show();
            //btnAddMoreVehicle.Visible = false;           
        }

        private void ClearRecords()
        {

        }

        private void cmbMake_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bindModel(Convert.ToString(cmbMake.SelectedValue));
        }

        public void NewVRN()
        {
            txtVrn.Text = "Vehicle Registration Number";
            txtVrn.ForeColor = SystemColors.GrayText;
            txtSumInsured.Text = string.Empty;
            cmbVehicleUsage.SelectedIndex = 0;
            cmbPaymentTerm.SelectedIndex = 0;
            cmbCoverType.SelectedIndex = 0;

            //cmbMake.SelectedIndex = 0;
            //cmbModel.SelectedIndex = -1;

            cmbMake.SelectedIndex = 1;
            cmbModel.SelectedIndex = 1;


            //txtYear.Text = string.Empty;
            txtChasis.Text = string.Empty;
            txtEngine.Text = string.Empty;

            //optionalCover
            chkExcessBuyback.Checked = false;
            chkRoadsideAssistance.Checked = false;
            chkMedicalExpenses.Checked = false;
            //  chkPassengerAccidentalCover.Checked = false;
            cmbNoofPerson.Value = 0;
            //Optional
            txtradioAmount.Text = string.Empty;
            chkRadioLicence.Checked = false;
            chkZinara.Checked = false;
            txtAccessAmount.Text = string.Empty;
            txtpenalty.Text = string.Empty;
            txtZinTotalAmount.Text = string.Empty;
            btnAddMoreVehicle.Visible = true;

        }


        public void NewObjectDuringEditOrBack()
        {
            txtSumInsured.Text = string.Empty;
            //  cmbVehicleUsage.SelectedIndex = 0;
            cmbVehicleUsage.SelectedIndex = -1;
            cmbPaymentTerm.SelectedIndex = 0;
            //   cmbTaxClasses.SelectedIndex = 0;
            cmbCoverType.SelectedIndex = 0;
            cmbCurrency.SelectedIndex = 0;


            cmbMake.SelectedIndex = 0;
            cmbModel.SelectedIndex = -1;
            //txtYear.Text = string.Empty;
            txtChasis.Text = string.Empty;
            txtEngine.Text = string.Empty;

            //optionalCover
            chkExcessBuyback.Checked = false;
            chkRoadsideAssistance.Checked = false;
            chkMedicalExpenses.Checked = false;
            //chkPassengerAccidentalCover.Checked = false;
            cmbNoofPerson.Value = 0;
            //Optional
            txtradioAmount.Text = string.Empty;
            chkRadioLicence.Checked = false;
            chkZinara.Checked = false;
            txtAccessAmount.Text = string.Empty;
            txtpenalty.Text = string.Empty;
            txtZinTotalAmount.Text = string.Empty;


            btnAddMoreVehicle.Visible = true;
        }

        private void btnSDContinue_Click(object sender, EventArgs e)
        {
            // 5 th screen vehical summary detals 
            try
            {
                if (objlistRisk.Count > 0)
                {
                    if (VehicalIndex == -1)
                    {
                        //if (objRiskModel.IsCorporateField)
                        //{
                        //    pnlCorporate.Visible = true;
                        //}
                        //else
                        //{
                        //    //  pnlPersonalDetails.Visible = true;
                        //    pnlsumary.Visible = true;
                        //}

                        pnlsumary.Visible = true;
                    }
                    else
                    {
                        //if (objlistRisk[VehicalIndex].IsCorporateField)
                        //{
                        //    pnlCorporate.Visible = true;
                        //}
                        //else
                        //{
                        //    // pnlPersonalDetails.Visible = true;
                        //    pnlsumary.Visible = true;
                        //}

                        pnlsumary.Visible = true;
                    }


                    string idNumber = "";
                    if (resObject != null && resObject.Quotes != null && resObject.Quotes[0].Client != null)
                    {
                        idNumber = resObject.Quotes[0].Client.IDNumber;
                    }


                    // txtCmpBusinessId.Text = textSearchVrn.Text == "Id Number" ? idNumber : textSearchVrn.Text;
                    pnlSum.Visible = false;
                    pnlAddMoreVehicle.Visible = false;
                    // txtDOB.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    txtDOB.Text = DateTime.Now.ToShortDateString();
                    txtDOB.MaxDate = DateTime.Today;
                    txtDOB.CalendarForeColor = Color.LightGray;
                }


                PaymentSummary();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void PaymentSummary()
        {
            if (objListUserInput.Count > 0)
            {
                UserInput objExistingInput = objListUserInput.Find(x => x.VRN == txtVrn.Text);
                if (objExistingInput != null)
                {
                    objExistingInput.VRN = txtVrn.Text;
                    objExistingInput.SumInsured = txtSumInsured.Text;

                    objExistingInput.VehicalUsage = Convert.ToString(cmbVehicleUsage.SelectedValue);
                    objExistingInput.PaymentTerm = Convert.ToString(cmbPaymentTerm.SelectedValue);
                    //objExistingInput.CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);
                    objExistingInput.CoverType = cmbCoverType.SelectedValue == null ? 0 : Convert.ToInt32(cmbCoverType.SelectedValue);

                    //Vehicle Type
                    objExistingInput.Make = cmbMake.SelectedText;
                    objExistingInput.MakeID = Convert.ToString(cmbMake.SelectedValue);
                    objExistingInput.Model = cmbModel.SelectedText;
                    objExistingInput.ModelID = Convert.ToString(cmbModel.SelectedValue);
                    // objExistingInput.Year = txtYear.Text;
                    objExistingInput.ChasisNumber = txtChasis.Text;
                    objExistingInput.EngineNumber = txtEngine.Text;


                    //PersonalDetails1
                    objExistingInput.Name = txtFirstName.Text;
                    objExistingInput.EmailAddress = txtEmailAddress.Text;
                    objExistingInput.Phone = txtPhone.Text;
                    objExistingInput.Gender = "";
                    objExistingInput.DOB = txtDOB.Value.ToString("MM/dd/yyyy");
                    //objExistingInput.DOB = txtDOB.Text;



                    //PersonalDetails2
                    objExistingInput.Address1 = txtAdd1.Text;
                    objExistingInput.Address2 = txtAdd2.Text;
                    //objExistingInput.City = txtCity.Text;
                    objExistingInput.City = Convert.ToString(cmdCity.Text);
                    //objExistingInput.Zip = txtZipCode.Text;
                    objExistingInput.IDNumber = txtIDNumber.Text;


                    //optionalCover
                    objExistingInput.ExcessBuyback = Convert.ToInt32(chkExcessBuyback.Checked);
                    objExistingInput.RoadsideAssistance = Convert.ToInt32(chkRoadsideAssistance.Checked);
                    objExistingInput.MedicalExpenses = Convert.ToInt32(chkMedicalExpenses.Checked);
                    //objExistingInput.PassengerAccidentalCover = Convert.ToInt32(chkPassengerAccidentalCover.Checked);
                    objExistingInput.NumberOfPerson = Convert.ToInt32(cmbNoofPerson.Value);
                }
                else
                {
                    SetUserInput();
                }
            }
            // calculate summary
            CaclulateSummary(objlistRisk);

        }

        private void BtnSDback_Click(object sender, EventArgs e)
        {
            //pnlOptionalCover.Visible = true;
            try
            {
                var tba = ConfigurationManager.AppSettings["tba"];
                if (isVehicalDeleted)
                {
                    //PnlVrn.Visible = true;
                    //pnlSum.Visible = false;
                    //pnlOptionalCover.Visible = false;
                    //NewVRN();
                    isVehicalDeleted = false;
                }
                else
                {
                    VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);
                }


                isbackclicked = true;




                if (tba == txtVrn.Text.ToUpper())
                {

                    if (VehicalIndex == -1)
                    {
                        objRiskModel.RadioLicenseCost = 0;
                        objRiskModel.IncludeRadioLicenseCost = false;
                        objRiskModel.VehicleLicenceFee = 0;
                    }
                    else
                    {
                        objlistRisk[VehicalIndex].RadioLicenseCost = 0;
                        objlistRisk[VehicalIndex].IncludeRadioLicenseCost = false;
                        objlistRisk[VehicalIndex].VehicleLicenceFee = 0;

                    }


                    pnlSum.Visible = false;
                    pnlAddMoreVehicle.Visible = false;
                    pnlConfirm.Visible = true;

                    return;
                }


                if (prior == 0)
                {

                    pnlSum.Visible = false;
                    pnlAddMoreVehicle.Visible = false;
                    // pnlRadioZinara.Visible = true; // 21_feb
                    //  pnlRiskDetails.Visible = true;
                    pnlRadioZinara.Visible = true;
                    btnBacktoList.Visible = false;


                    if (chkRadioLicence.Checked)
                    {
                        pnlRadio.Visible = true;
                    }
                    if (chkZinara.Checked)
                    {
                        pnlZinara.Visible = true;
                    }
                }
                else
                {
                    pnlSum.Visible = false;
                    pnlAddMoreVehicle.Visible = false;
                    // pnlRadioZinara.Visible = true; // 21_feb
                    //  pnlRiskDetails.Visible = true;
                    pnlConfirm.Visible = true;
                    btnBacktoList.Visible = false;
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbCoverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCoverType.SelectedValue != null)
            {
                int CoverType = CoverType = Convert.ToInt32(cmbCoverType.SelectedValue);
                if (CoverType == 4)
                {
                    label2.Visible = true;
                    lblRtgs.Visible = true;
                    txtSumInsured.Visible = true;
                    NewerrorProvider.Clear();
                    dtCoverStartDate.Visible = true;
                    dtCoverEndDate.Visible = true;
                    lblCoverStartDate.Visible = true;
                    lblCoverEndDate.Visible = true;
                    dtCoverStartDate.Text = DateTime.Now.ToShortDateString();

                    if (cmbPaymentTerm.SelectedValue!=null)
                    {
                        int paymentTerm = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                        DateTime endDate = DateTime.Now;
                        if(paymentTerm==1)
                            endDate= endDate.AddMonths(12);
                        else
                            endDate= endDate.AddMonths(paymentTerm);

                        dtCoverEndDate.Text = endDate.ToShortDateString();                      
                    } 

                   // dtCoverEndDate.Text= 
                }
                else
                {
                    label2.Visible = false;
                    lblRtgs.Visible = false;
                    txtSumInsured.Visible = false;
                    txtSumInsured.Text = "0.00";
                    dtCoverStartDate.Visible = false;
                    dtCoverEndDate.Visible = false;

                    lblCoverStartDate.Visible = false;
                    lblCoverEndDate.Visible = false;
                }
            }

        }


        private void GetPremiumAmount_ChangeOfCoverType()
        {
            int CoverType = 0;

            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            //picbxCoverType.Visible = true;
            try
            {
                //if (objRiskModel == null)
                //    return;

                //if (objRiskModel != null && objRiskModel.RegistrationNo == null)
                //    return;

                if (cmbPaymentTerm.SelectedValue == null && cmbCoverType.SelectedValue == null)
                    return;

                #region get ICE cash token
                //ICEcashTokenResponse ObjToken = IcServiceobj.getToken(); // uncomment this line 

                // ICEcashTokenResponse ObjToken = null;
                #endregion
                //List<RiskDetailModel> objVehicles = new List<RiskDetailModel>();

                //objVehicles.Add(new RiskDetailModel { RegistrationNo = txtVrn.Text, PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue) });



                ObjToken = CheckParterTokenExpire();
                if (ObjToken != null)
                    parternToken = ObjToken.Response.PartnerToken;


                if (parternToken != "")
                {
                    //if (String.IsNullOrEmpty(txtYear.Text))
                    //{
                    //    txtYear.Text = "1900";
                    //}
                    if (String.IsNullOrEmpty(txtSumInsured.Text))
                    {
                        txtSumInsured.Text = "0";
                    }

                    int PaymentTermId = 0;
                    int CoverTypeId = 0;
                    int VehicleUsage = 0;
                    int currencyId = 0;

                    string RegistrationNo = txtVrn.Text;
                    string suminsured = txtSumInsured.Text;

                    string make = Convert.ToString(cmbMake.Text);
                    string model = Convert.ToString(cmbModel.Text);


                    //int PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);

                    if (cmbPaymentTerm.SelectedValue != null)
                    {
                        PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    }
                    if (cmbCoverType.SelectedValue != null)
                    {
                        CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                    }
                    if (cmbVehicleUsage.SelectedValue != null)
                    {
                        VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                    }
                    if (cmbCurrency.SelectedValue != null)
                    {
                        currencyId = Convert.ToInt32(cmbCurrency.SelectedValue);
                    }


                    //int CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                    //int VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);


                    //   int VehicleYear = Convert.ToInt32(txtYear.Text);
                    string PartnerReference = ObjToken.PartnerReference;

                    //  ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 

                    objRiskModel.SumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text);
                    //  objRiskModel.MakeId = cmbMake.SelectedValue.ToString();

                    //if (cmbModel.SelectedText == "") 
                    //    objRiskModel.ModelId = objRiskModel.ModelId;
                    //else
                    //    objRiskModel.ModelId = cmbModel.SelectedValue.ToString();


                    objRiskModel.PaymentTermId = PaymentTermId;
                    objRiskModel.CoverTypeId = CoverTypeId;
                    objRiskModel.VehicleUsage = VehicleUsage;
                    objRiskModel.TaxClassId = Convert.ToInt32(cmbTaxClasses.SelectedValue);
                    objRiskModel.CurrencyId = currencyId;


                    objRiskModel.ProductId = ProductsList.FirstOrDefault(c => c.Id == objRiskModel.ProductId) == null ? 0 : ProductsList.FirstOrDefault(c => c.Id == objRiskModel.ProductId).VehicleTypeId; // For getting tax class for tba
                    quoteresponseQuote = IcServiceobj.RequestQuote(objRiskModel, (CustomerModel)customerInfo, parternToken); // uncomment this line 


                    resObject = quoteresponseQuote.Response;
                    //if token expire
                    if (resObject != null && resObject.Message.Contains("Partner Token has expired"))
                    {

                        //ObjToken = CheckParterTokenExpire();
                        ObjToken = IcServiceobj.getToken();
                        if (ObjToken != null)
                            parternToken = ObjToken.Response.PartnerToken;

                        Service_db.UpdateToken(ObjToken);

                        quoteresponseQuote = IcServiceobj.RequestQuote(objRiskModel, (CustomerModel)customerInfo, parternToken);


                        //ObjToken = IcServiceobj.getToken();
                        //if (ObjToken != null)
                        //{
                        //    parternToken = ObjToken.Response.PartnerToken;
                        //    //  quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 
                        //    quoteresponse = IcServiceobj.RequestQuote(objRiskModel, (CustomerModel)customerInfo, parternToken);
                        //}
                    }


                    _clientIdType = customerInfo.NationalIdentificationNumber;


                    // picbxCoverType.Visible = false;
                    //picbxRiskDetail.Visible = false;
                    //ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, txtVrn.Text, txtSumInsured.Text, Convert.ToString(cmbMake.SelectedValue), Convert.ToString(cmbModel.SelectedValue), Convert.ToInt32(cmbPaymentTerm.SelectedValue), Convert.ToInt32(txtYear), Convert.ToInt32(cmbCoverType.SelectedValue), Convert.ToInt32(cmbVehicleUsage.SelectedValue), "", customerInfo);
                    if (quoteresponseQuote != null)
                    {
                        response.result = quoteresponseQuote.Response.Result;
                        if (response.result == 0)
                        {
                            response.message = quoteresponseQuote.Response.Quotes[0].Message;
                        }
                        else
                        {
                            response.Data = quoteresponseQuote;
                            if (response.result != 0)
                            {
                                if (quoteresponseQuote.Response.Quotes[0] != null)
                                {
                                    ////9Jan
                                    if (quoteresponseQuote.Response.Quotes[0].Policy != null)
                                    {
                                        cmbCoverType.SelectedValue = Convert.ToInt32(quoteresponseQuote.Response.Quotes[0].Policy.InsuranceType);
                                        //cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths); // ask from sir

                                        if (quoteresponseQuote.Response.Quotes[0].Policy.DurationMonths != null)
                                        {
                                            if (quoteresponseQuote.Response.Quotes[0].Policy.DurationMonths == "12")
                                            {
                                                cmbPaymentTerm.SelectedValue = 1;
                                            }
                                            else
                                            {
                                                //cmbPaymentTerm.SelectedValue = Convert.ToInt32(resObject.Quotes[0].Policy.DurationMonths);
                                                cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponseQuote.Response.Quotes[0].Policy.DurationMonths);
                                            }
                                        }


                                        if (VehicalIndex == -1)
                                        {
                                            objRiskModel.isVehicleRegisteredonICEcash = true;
                                            objRiskModel.BasicPremiumICEcash = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                            objRiskModel.Premium = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                            objRiskModel.ZTSCLevy = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
                                            objRiskModel.StampDuty = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);

                                            var discount = GetDiscount(Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0] == null ? "0.00" : quoteresponseQuote.Response.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
                                            objRiskModel.Discount = discount;


                                            objRiskModel.InsuranceId = quoteresponseQuote.Response.Quotes[0].InsuranceID;


                                        }
                                        else
                                        {
                                            objlistRisk[VehicalIndex].isVehicleRegisteredonICEcash = true;
                                            objlistRisk[VehicalIndex].BasicPremiumICEcash = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                            objlistRisk[VehicalIndex].Premium = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                            objlistRisk[VehicalIndex].ZTSCLevy = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
                                            objlistRisk[VehicalIndex].StampDuty = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);

                                            var discount = GetDiscount(Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0] == null ? "0.00" : quoteresponseQuote.Response.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
                                            objlistRisk[VehicalIndex].Discount = discount;

                                            objlistRisk[VehicalIndex].InsuranceId = quoteresponseQuote.Response.Quotes[0].InsuranceID;
                                        }
                                    }

                                    if (quoteresponseQuote.Response.Quotes[0].Vehicle != null)
                                    {



                                    }
                                    int cmVehicleValue = 0;
                                    if (cmbVehicleUsage.SelectedValue != null)
                                    {
                                        cmVehicleValue = Convert.ToInt32(cmbVehicleUsage.SelectedValue);

                                    }

                                    if (quoteresponseQuote.Response.Quotes[0].Client != null)
                                    {
                                        txtFirstName.Text = quoteresponseQuote.Response.Quotes[0].Client.FirstName + " " + quoteresponseQuote.Response.Quotes[0].Client.LastName;
                                        txtPhone.Text = "";
                                        txtAdd1.Text = quoteresponseQuote.Response.Quotes[0].Client.Address1;
                                        txtAdd2.Text = quoteresponseQuote.Response.Quotes[0].Client.Address2;
                                        //txtCity.Text = quoteresponse.Response.Quotes[0].Client.Town;
                                        cmdCity.Text = quoteresponseQuote.Response.Quotes[0].Client.Town;
                                        //txtIDNumber.Text = quoteresponse.Response.Quotes[0].Client.IDNumber;

                                        _clientIdType = textSearchVrn.Text == "ID Number" ? resObject.Quotes[0].Client.IDNumber : textSearchVrn.Text;

                                        txtIDNumber.Text = _clientIdType;
                                    }
                                    /////End
                                    // Session["InsuranceId"] = quoteresponse.Response.Quotes[0].InsuranceID;
                                }

                            }
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                response.message = "Error occured.";
            }
        }


        private void RequestVehicleDetails()
        {

            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            //picbxCoverType.Visible = true;
            try
            {

                if (cmbPaymentTerm.SelectedValue == null && cmbCoverType.SelectedValue == null)
                    return;

                #region get ICE cash token
                //ICEcashTokenResponse ObjToken = IcServiceobj.getToken(); // uncomment this line 

                // ICEcashTokenResponse ObjToken = null;
                #endregion

                //List<RiskDetailModel> objVehicles = new List<RiskDetailModel>();
                //objVehicles.Add(new RiskDetailModel { RegistrationNo = txtVrn.Text, PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue) });
                //ObjToken = CheckParterTokenExpire();
                //if (ObjToken != null)
                //    parternToken = ObjToken.Response.PartnerToken;


                RequestToke token = Service_db.GetLatestToken();
                if (token != null)
                    parternToken = token.Token;


                if (parternToken == "") // for first time
                {
                    ObjToken = IcServiceobj.getToken();
                    parternToken = ObjToken.Response.PartnerToken;
                    Service_db.UpdateToken(ObjToken);
                }


                if (parternToken != "")
                {
                    //if (String.IsNullOrEmpty(txtYear.Text))
                    //{
                    //    txtYear.Text = "1900";
                    //}
                    if (String.IsNullOrEmpty(txtSumInsured.Text))
                    {
                        txtSumInsured.Text = "0";
                    }

                    int PaymentTermId = 0;
                    int CoverTypeId = 0;


                    string RegistrationNo = txtVrn.Text;
                    //int PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);

                    if (cmbPaymentTerm.SelectedValue != null)
                        PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);

                    if (cmbCoverType.SelectedValue != null)
                        CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);


                    // to display selected value
                    ZinPaymentDetail.SelectedValue = ZinPaymentDetail.SelectedValue;
                    RadioPaymnetTerm.SelectedValue = RadioPaymnetTerm.SelectedValue;


                    //int CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                    //int VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);


                    //  int VehicleYear = Convert.ToInt32(txtYear.Text);
                    string PartnerReference = ObjToken.PartnerReference;

                    //  ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 
                    // objRiskModel.SumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text);
                    //  objRiskModel.MakeId = cmbMake.SelectedValue.ToString();

                    //if (cmbModel.SelectedText == "") 
                    //    objRiskModel.ModelId = objRiskModel.ModelId;
                    //else
                    //    objRiskModel.ModelId = cmbModel.SelectedValue.ToString();

                    objRiskModel.PaymentTermId = PaymentTermId;
                    objRiskModel.CoverTypeId = CoverTypeId;

                    // objRiskModel.VehicleUsage = VehicleUsage;
                    //  objRiskModel.TaxClassId = Convert.ToInt32(cmbTaxClasses.SelectedValue);
                    //  objRiskModel.CurrencyId = currencyId;

                    // objRiskModel.ProductId = ProductsList.FirstOrDefault(c => c.Id == objRiskModel.ProductId) == null ? 0 : ProductsList.FirstOrDefault(c => c.Id == objRiskModel.ProductId).VehicleTypeId; // For getting tax class for tba
                    //quoteresponseQuote = IcServiceobj.RequestQuote(objRiskModel, (CustomerModel)customerInfo, parternToken); // 15_jan

                    //RadioTVUsage = item.RadioTVUsage,
                    //RadioTVFrequency = item.RadioTVFrequency

                    if (chkZinara.Checked)
                    {
                        objRiskModel.radioTvUsage = IcServiceobj.GetRadioTvUsage(Convert.ToString(objRiskModel.ProductId)).ToString();
                        objRiskModel.licenseFreequency = IcServiceobj.GetMonthKey(Convert.ToInt32(ZinPaymentDetail.SelectedValue)).ToString();
                    }

                    if (chkRadioLicence.Checked)
                    {
                        objRiskModel.radioTvUsage = IcServiceobj.GetRadioTvUsage(Convert.ToString(objRiskModel.ProductId)).ToString();
                        objRiskModel.RadioFreequency = IcServiceobj.GetMonthKey(Convert.ToInt32(RadioPaymnetTerm.SelectedValue)).ToString();
                    }

                    var product = ProductsList.FirstOrDefault(c => c.Id == objRiskModel.ProductId); // get vehilce type form table
                    var temVehicleTypeId = objRiskModel.ProductId;
                    if (product != null)
                        objRiskModel.ProductId = product.VehicleTypeId;


                    if (_insuranceAndLicense && (chkZinara.Checked && chkRadioLicence.Checked))
                        quoteresponseQuote = IcServiceobj.TPILICQuote(objRiskModel, (CustomerModel)customerInfo, parternToken); // combine insurance and license
                    else if (_insuranceAndLicense && chkZinara.Checked)
                        quoteresponseQuote = IcServiceobj.TPILICQuoteZinraOnly(objRiskModel, (CustomerModel)customerInfo, parternToken); // only for zinara
                    else
                        quoteresponseQuote = IcServiceobj.RequestQuote(objRiskModel, (CustomerModel)customerInfo, parternToken); //  insurance only


                    objRiskModel.ProductId = temVehicleTypeId;  // set selected vehilceId
                    resObject = quoteresponseQuote.Response;
                    if (resObject.Message.Contains("1 failed"))
                        _iceCashErrorMsg = resObject.Quotes == null ? "Error Occured" : resObject.Quotes[0].Message;

                    if (resObject.Message.Contains("Invalid Vehicle Type"))
                        _iceCashErrorMsg = "Please select risk details";

                    if (resObject.Message.Contains("Your account is inactive"))
                        _iceCashErrorMsg = resObject.Message;



                    //if token expire
                    int i = 5;
                    while (true)
                    {
                        i++;
                        //if token expire
                        if (resObject != null && (resObject.Message.Contains("Partner Token has expired") || resObject.Message.Contains("Invalid Partner Token")))
                        {
                            _iceCashErrorMsg = "";

                            //ObjToken = CheckParterTokenExpire();
                            ObjToken = IcServiceobj.getToken();
                            if (ObjToken != null)
                                parternToken = ObjToken.Response.PartnerToken;

                            Service_db.UpdateToken(ObjToken);


                            if (_insuranceAndLicense && (chkZinara.Checked && chkRadioLicence.Checked))
                                quoteresponseQuote = IcServiceobj.TPILICQuote(objRiskModel, (CustomerModel)customerInfo, parternToken); // combine insurance and license
                            else if (_insuranceAndLicense && chkZinara.Checked)
                                quoteresponseQuote = IcServiceobj.TPILICQuoteZinraOnly(objRiskModel, (CustomerModel)customerInfo, parternToken); // only for zinara
                            else
                                quoteresponseQuote = IcServiceobj.RequestQuote(objRiskModel, (CustomerModel)customerInfo, parternToken); //  insurance only


                            if (quoteresponseQuote.Response.Message.Contains("1 failed"))
                                _iceCashErrorMsg = quoteresponseQuote.Response.Quotes == null ? "Error Occured" : quoteresponseQuote.Response.Quotes[0].Message;

                        }

                        if (!quoteresponseQuote.Response.Message.Contains("Partner Token has expired"))
                            break;
                    }

                    //if(resObject!=null && resObject.Quotes[0]!=null && resObject.Quotes[0].Licence!=null &&  Convert.ToInt32(resObject.Quotes[0].Licence.PenaltiesAmt)>0)
                    //{
                    //    MyMessageBox.ShowBox("You have outstanding penalties, please contact our Contact Centre for assistance on 086 77 22 33 44.") ;
                    //    pnlRadioZinara.Visible = false;
                    //    PnlVrn.Visible = true;
                    //    return;

                    //}

                    //if(_iceCashErrorMsg!="")
                    //{
                    //    MyMessageBox.ShowBox(_iceCashErrorMsg);

                    //}

                    // picbxCoverType.Visible = false;
                    //picbxRiskDetail.Visible = false;
                    //ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, txtVrn.Text, txtSumInsured.Text, Convert.ToString(cmbMake.SelectedValue), Convert.ToString(cmbModel.SelectedValue), Convert.ToInt32(cmbPaymentTerm.SelectedValue), Convert.ToInt32(txtYear), Convert.ToInt32(cmbCoverType.SelectedValue), Convert.ToInt32(cmbVehicleUsage.SelectedValue), "", customerInfo);
                    if (quoteresponseQuote != null)
                    {
                        response.result = quoteresponseQuote.Response.Result;
                        if (response.result == 0)
                        {
                            response.message = quoteresponseQuote.Response.Quotes[0].Message;
                        }
                        else
                        {
                            response.Data = quoteresponseQuote;


                            if (response.result != 0)
                            {
                                if (quoteresponseQuote.Response.Quotes != null && quoteresponseQuote.Response.Quotes[0] != null)
                                {
                                    ////9Jan
                                    if (quoteresponseQuote.Response.Quotes[0].Policy != null)
                                    {

                                        _iceCashErrorMsg = "";

                                        objRiskModel.isVehicleRegisteredonICEcash = true;
                                        objRiskModel.BasicPremiumICEcash = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                        objRiskModel.Premium = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
                                        objRiskModel.ZTSCLevy = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
                                        objRiskModel.StampDuty = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);

                                        //  var discount = GetDiscount(Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0] == null ? "0.00" : quoteresponseQuote.Response.Quotes[0].Policy.CoverAmount), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
                                        objRiskModel.Discount = 0;
                                        objRiskModel.InsuranceId = quoteresponseQuote.Response.Quotes[0].InsuranceID;
                                        objRiskModel.LicenseId = quoteresponseQuote.Response.Quotes[0].LicenceID;
                                        objRiskModel.CombinedID = quoteresponseQuote.Response.Quotes[0].CombinedID;

                                        string format = "yyyyMMdd";
                                        DateTime StartDate = DateTime.ParseExact(quoteresponseQuote.Response.Quotes[0].Policy.StartDate, format, CultureInfo.InvariantCulture);
                                        DateTime EndDate = DateTime.ParseExact(quoteresponseQuote.Response.Quotes[0].Policy.EndDate, format, CultureInfo.InvariantCulture);

                                       if(objRiskModel.CoverTypeId!=(int)eCoverType.Comprehensive)
                                        {
                                            objRiskModel.CoverStartDate = StartDate;
                                            objRiskModel.CoverEndDate = EndDate;
                                        }
                                        


                                    }

                                    if (quoteresponseQuote.Response.Quotes[0].Vehicle != null)
                                    {

                                        cmbProducts.SelectedValue = quoteresponseQuote.Response.Quotes[0].Vehicle.VehicleType;

                                        if (cmbProducts.SelectedValue != null)
                                            cmbProducts.Enabled = false;
                                        else
                                            cmbProducts.Enabled = true;


                                        cmbTaxClasses.SelectedValue = quoteresponseQuote.Response.Quotes[0].Vehicle.TaxClass == null ? 0 : Convert.ToInt32(quoteresponseQuote.Response.Quotes[0].Vehicle.TaxClass);

                                        if (cmbTaxClasses.SelectedValue != null)
                                            cmbTaxClasses.Enabled = false;
                                        else
                                            cmbTaxClasses.Enabled = true;


                                        //txtYear.Text = quoteresponseQuote.Response.Quotes[0].Vehicle.YearManufacture;

                                        _TaxClass = quoteresponseQuote.Response.Quotes[0].Vehicle.TaxClass == null ? 0 : Convert.ToInt32(quoteresponseQuote.Response.Quotes[0].Vehicle.TaxClass);

                                        objRiskModel.TaxClassId = _TaxClass;
                                        // cmbTaxClasses.SelectedIndex= cmbMake.FindStringExact(_TaxClass.ToString());

                                        string make = resObject.Quotes[0].Vehicle.Make;
                                        string model = resObject.Quotes[0].Vehicle.Model;
                                        if (!string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(model))
                                        {
                                            SaveVehicalMakeAndModel(make, model);
                                            bindMake();

                                        }
                                        else
                                        {
                                            // set make and model if IceCash does not retrun
                                            resObject.Quotes[0].Vehicle.Make = "0";
                                            resObject.Quotes[0].Vehicle.Model = "0";
                                        }

                                        bindModel(quoteresponseQuote.Response.Quotes[0].Vehicle.Make);

                                        if (VehicalIndex == -1)
                                        {
                                            objRiskModel.MakeId = quoteresponseQuote.Response.Quotes[0].Vehicle.Make;
                                            objRiskModel.ModelId = quoteresponseQuote.Response.Quotes[0].Vehicle.Model;
                                        }
                                        else
                                        {
                                            objlistRisk[VehicalIndex].MakeId = quoteresponseQuote.Response.Quotes[0].Vehicle.Make; ;
                                            objlistRisk[VehicalIndex].ModelId = quoteresponseQuote.Response.Quotes[0].Vehicle.Model;
                                        }

                                        Int32 indexMake = cmbMake.FindStringExact(quoteresponseQuote.Response.Quotes[0].Vehicle.Make);
                                        cmbMake.SelectedIndex = indexMake;
                                        //if(cmbMake.SelectedIndex>0)
                                        if (quoteresponseQuote.Response.Quotes[0].Vehicle.Make != null)
                                        {
                                            // cmbMake.Visible = false;
                                            // vehicleMakeTxt.Visible = true;
                                            vehicleMakeTxt.Text = quoteresponseQuote.Response.Quotes[0].Vehicle.Make;
                                        }


                                        //vehicleMakeTxt.Text = quoteresponseQuote.Response.Quotes[0].Vehicle.Make;

                                        Int32 indexModel = cmbModel.FindString(quoteresponseQuote.Response.Quotes[0].Vehicle.Model);
                                        cmbModel.SelectedIndex = indexModel;
                                        // if(cmbModel.SelectedIndex>0)
                                        if (quoteresponseQuote.Response.Quotes[0].Vehicle.Model != null)
                                        {
                                            // cmbModel.Visible = false;
                                            // vehicleModeltxt.Visible = true;
                                            vehicleModeltxt.Text = quoteresponseQuote.Response.Quotes[0].Vehicle.Model;
                                        }

                                        // vehicleModeltxt.Text = quoteresponseQuote.Response.Quotes[0].Vehicle.Model;

                                        //_clientIdType = textSearchVrn.Text == "Id Number" ? quoteresponseQuote.Response.Quotes[0].Client.IDNumber : textSearchVrn.Text;

                                        _clientIdType = "1";

                                    }


                                    if (quoteresponseQuote.Response.Quotes[0].Licence != null)
                                    {
                                        decimal PenaltiesAmt = 0;

                                        if (chkZinara.Checked)
                                        {
                                            PenaltiesAmt = quoteresponseQuote.Response.Quotes[0].Licence.PenaltiesAmt == null ? 0 : Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Licence.PenaltiesAmt);

                                            //if (PenaltiesAmt > 0)
                                            //{
                                            //    MyMessageBox.ShowBox("You have outstanding penalties, please contact our Contact Centre for assistance on 086 77 22 33 44.");
                                            //    pnlRadioZinara.Visible = false;
                                            //    PnlVrn.Visible = true;
                                            //    return;
                                            //}

                                            objRiskModel.ArrearsAmt = quoteresponseQuote.Response.Quotes[0].Licence.ArrearsAmt == null ? 0 : Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Licence.ArrearsAmt);
                                            objRiskModel.LicTransactionAmt = quoteresponseQuote.Response.Quotes[0].Licence.TransactionAmt == null ? 0 : Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Licence.TransactionAmt);
                                            objRiskModel.AdministrationAmt = quoteresponseQuote.Response.Quotes[0].Licence.AdministrationAmt == null ? 0 : Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Licence.AdministrationAmt);

                                            // If icecash doesn't return administration amount
                                            if (PenaltiesAmt > 0 && objRiskModel.AdministrationAmt == 0)
                                                objRiskModel.AdministrationAmt = Math.Round(Convert.ToDecimal(450.00M), 2);

                                            decimal totalLicenseFee = Convert.ToDecimal(objRiskModel.ArrearsAmt + objRiskModel.LicTransactionAmt + objRiskModel.AdministrationAmt + PenaltiesAmt);


                                            objRiskModel.VehicleLicenceFee = Convert.ToDecimal(totalLicenseFee);


                                        }

                                        if (chkRadioLicence.Checked)
                                        {
                                            objRiskModel.RadioLicenseCost = Convert.ToDecimal(quoteresponseQuote.Response.Quotes[0].Licence.TotalRadioTVAmt);
                                            objRiskModel.IncludeRadioLicenseCost = true;
                                        }

                                        objRiskModel.PenaltiesAmt = PenaltiesAmt;

                                        //if (PenaltiesAmt > 0)
                                        //{
                                        //    MyMessageBox.ShowBox("You have outstanding penalties, please contact our Contact Centre for assistance on 086 77 22 33 44.");
                                        //    pnlRadioZinara.Visible = false;
                                        //    PnlVrn.Visible = true;
                                        //    return;
                                        //}


                                    }
                                }


                                // for zinara license 

                                //GetZinraLiceenseFee(cmbPaymentTerm.SelectedValue.ToString()); // old

                                // GetDefaultZinraLiceenseFee(cmbPaymentTerm.SelectedValue.ToString(), _clientIdType); // latest 15_may 2019


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Service_db.WriteIceCashLog("RequestVehicleDetails", ex.Message, "RequestVehicleDetails", objRiskModel.RegistrationNo, Convert.ToString(objRiskModel.ALMBranchId));

                response.message = "Error occured.";
            }
        }

        public string getmessageresponse(int data)
        {
            string Message = "";

            switch (data)
            {

                case 00:
                    Message = "Approved or completed successfully";
                    break;

                case 01:
                    Message = "Refer to card issuer";
                    break;


                case 02:
                    Message = " Refer to card issuer, special condition";
                    break;


                case 03:
                    Message = "Invalid merchant";
                    break;


                case 04:
                    Message = "Pick - up card";
                    break;


                case 05:
                    Message = "Do not honor";
                    break;


                case 06:
                    Message = "Error";
                    break;


                case 07:
                    Message = "Pick - up card, special condition";
                    break;

                case 08:
                    Message = "Honor with identification";
                    break;


                case 09:
                    Message = "Request in progress";
                    break;


                case 10:
                    Message = "Approved, partial";
                    break;

                case 11:
                    Message = "Approved, VIP";
                    break;

                case 12:
                    Message = "Invalid transaction";
                    break;


                case 13:
                    Message = "Invalid amount";
                    break;


                case 14:
                    Message = "Invalid card number";
                    break;


                case 15:
                    Message = "No such issuer";
                    break;


                case 16:
                    Message = "Approved, update track 3";
                    break;


                case 17:
                    Message = "Customer cancellation";
                    break;


                case 18:
                    Message = "Customer dispute";
                    break;

                case 19:
                    Message = "Re - enter transaction";
                    break;


                case 20:
                    Message = "Invalid response";
                    break;


                case 21:
                    Message = "No action taken";
                    break;



                case 22:
                    Message = "Suspected malfunction";
                    break;


                case 23:
                    Message = "Unacceptable transaction fee";
                    break;


                case 24:
                    Message = "File update not supported";
                    break;


                case 25:
                    Message = "Unable to locate record";
                    break;


                case 26:
                    Message = "Duplicate record";
                    break;


                case 27:
                    Message = "File update edit error";
                    break;


                case 28:
                    Message = "File update file locked";
                    break;


                case 29:
                    Message = "File update failed";
                    break;

                case 30:
                    Message = "Format error";
                    break;


                case 31:
                    Message = "Bank not supported";
                    break;


                case 32:
                    Message = "Completed partially";
                    break;


                case 33:
                    Message = "Expired card, pick-up";
                    break;


                case 34:
                    Message = "Suspected fraud, pick-up";
                    break;


                case 35:
                    Message = "Contact acquirer, pick-up";
                    break;


                case 36:
                    Message = "Restricted card, pick-up";
                    break;

                case 37:
                    Message = "Call acquirer security, pick - up";
                    break;


                case 38:
                    Message = "PIN tries exceeded, pick - up";
                    break;



                case 39:
                    Message = "No credit account";
                    break;


                case 40:
                    Message = "Function not supported";
                    break;


                case 41:
                    Message = "Lost card";
                    break;


                case 42:
                    Message = "No universal account";
                    break;


                case 43:
                    Message = "Stolen card";
                    break;


                case 44:
                    Message = "No investment account";
                    break;

                case 51:
                    // Message = "Not sufficient funds";
                    Message = "You can not perform transaction for amount above your Float Balance.";
                    break;
                case 52:
                    Message = "No check account";
                    break;


                case 53:
                    Message = "No savings account";
                    break;


                case 54:
                    Message = "Card expired or not yet effective";
                    break;


                case 55:
                    Message = "Incorrect PIN";
                    break;


                case 56:
                    Message = "No card record";
                    break;


                case 57:
                    Message = "Transaction not permitted to cardholder";
                    break;


                case 58:
                    Message = "Transaction not permitted on terminal";
                    break;


                case 59:
                    Message = "Suspected fraud";
                    break;


                case 60:
                    Message = "Contact acquirer";
                    break;


                case 61:
                    Message = "Exceeds withdrawal limit";
                    break;


                case 62:
                    Message = "Restricted card";
                    break;


                case 63:
                    Message = "Security violation";
                    break;


                case 64:
                    Message = "Original amount incorrect";
                    break;


                case 65:
                    Message = "Exceeds withdrawal frequency";
                    break;


                case 66:
                    Message = "Call acquirer security";
                    break;


                case 67:
                    Message = "Hard capture";
                    break;


                case 68:
                    Message = "Response received too late";
                    break;


                case 75:
                    Message = "PIN tries exceeded";
                    break;


                case 77:
                    Message = "Intervene, bank approval required";
                    break;


                case 78:
                    Message = "Intervene, bank approval required for partial amount";
                    break;


                case 90:
                    Message = "Cut - off in progress";
                    break;


                case 91:
                    Message = "Issuer or switch inoperative";
                    break;


                case 92:
                    Message = "Routing error";
                    break;


                case 93:
                    Message = "Violation of law";
                    break;


                case 94:
                    Message = "Duplicate transaction";
                    break;


                case 95:
                    Message = "Reconcile error";
                    break;


                case 96:
                    Message = "System malfunction";
                    break;


                case 98:
                    Message = "Exceeds cash limit";
                    break;



            }
            return Message;
        }

        private void cmbPaymentTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
            ////if (objRiskModel == null)
            ////    return;

            ////if (objRiskModel != null && objRiskModel.RegistrationNo == null)
            ////    return;

            ////if (objRiskModel != null && objRiskModel.RegistrationNo == null)
            ////    return;

            //if (cmbPaymentTerm.SelectedValue == null)
            //    return;


            //var paymenttermval = cmbPaymentTerm.SelectedValue;
            //var getvrntextval = txtVrn.Text;

            //checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();

            //try
            //{
            //    #region get ICE cash token

            //    //  ICEcashTokenResponse ObjToken = IcServiceobj.getToken();
            //    #endregion

            //    List<RiskDetailModel> objVehicles = new List<RiskDetailModel>();
            //    objVehicles.Add(new RiskDetailModel { RegistrationNo = txtVrn.Text, PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue) });

            //    if (parternToken != "")
            //    {
            //        if (String.IsNullOrEmpty(txtYear.Text))
            //        {
            //            txtYear.Text = "1900";
            //        }
            //        if (String.IsNullOrEmpty(txtSumInsured.Text))
            //        {
            //            txtSumInsured.Text = "0";
            //        }
            //        int PaymentTermId = 0;
            //        int CoverTypeId = 0;
            //        int VehicleUsage = 0;
            //        int VehicleYear = 0;
            //        string RegistrationNo = txtVrn.Text;
            //        string suminsured = txtSumInsured.Text;
            //        string make = Convert.ToString(cmbMake.Text);
            //        string model = Convert.ToString(cmbModel.Text);

            //        if (cmbPaymentTerm.SelectedValue != null)
            //        {
            //            PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
            //        }
            //        if (cmbCoverType.SelectedValue != null)
            //        {
            //            CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
            //        }
            //        if (cmbVehicleUsage.SelectedValue != null)
            //        {
            //            VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            //        }
            //        if (txtYear.Text != string.Empty)
            //        {
            //            VehicleYear = Convert.ToInt32(txtYear.Text);
            //        }


            //        //int PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
            //        //int VehicleYear = Convert.ToInt32(txtYear.Text);
            //        //int CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
            //        //int VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            //        //string PartnerReference = ObjToken.PartnerReference;
            //        //   string PartnerReference = ObjToken.PartnerReference;

            //        ResultRootObject quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo);


            //        resObject = quoteresponse.Response;
            //        //if token expire
            //        if (resObject != null && resObject.Message == "Partner Token has expired. ")
            //        {
            //            ObjToken = IcServiceobj.getToken();
            //            if (ObjToken != null)
            //            {
            //                parternToken = ObjToken.Response.PartnerToken;
            //                quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo);

            //                resObject = quoteresponse.Response;

            //            }

            //        }







            //        if (quoteresponse != null)
            //        {
            //            response.result = quoteresponse.Response.Result;
            //            if (response.result == 0)
            //            {
            //                response.message = quoteresponse.Response.Quotes[0].Message;
            //            }
            //            else
            //            {
            //                response.Data = quoteresponse;

            //                if (quoteresponse.Response.Quotes != null && quoteresponse.Response.Quotes[0] != null)
            //                {

            //                    //15 Feb  commented on 20_feb

            //                    //var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, quoteresponse.Response.Quotes[0].Client.IDNumber);
            //                    //var _resObjects = _quoteresponse.Response;
            //                    //if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
            //                    //{
            //                    //    //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
            //                    //    //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
            //                    //    txtAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt);
            //                    //    txtpenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt);
            //                    //    txtradioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt);
            //                    //}

            //                    //End

            //                    ////9Jan
            //                    if (quoteresponse.Response.Quotes[0].Policy != null)
            //                    {
            //                        cmbCoverType.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.InsuranceType);
            //                        //cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths);
            //                        if (quoteresponse.Response.Quotes[0].Policy.DurationMonths == "12")
            //                        {
            //                            cmbPaymentTerm.SelectedValue = 1;
            //                        }
            //                        else
            //                        {
            //                            cmbPaymentTerm.SelectedValue = Convert.ToInt32(quoteresponse.Response.Quotes[0].Policy.DurationMonths);
            //                        }


            //                        objRiskModel.isVehicleRegisteredonICEcash = true;
            //                        objRiskModel.BasicPremiumICEcash = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);

            //                        objRiskModel.Premium = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture);
            //                        objRiskModel.ZTSCLevy = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.GovernmentLevy, System.Globalization.CultureInfo.InvariantCulture);
            //                        objRiskModel.StampDuty = Convert.ToDecimal(quoteresponse.Response.Quotes[0].Policy.StampDuty, System.Globalization.CultureInfo.InvariantCulture);
            //                        var discount = GetDiscount(Convert.ToDecimal(quoteresponse.Response.Quotes[0] == null ? "0.00" : quoteresponse.Response.Quotes[0].Policy.CoverAmount, System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(cmbPaymentTerm.SelectedValue));
            //                        objRiskModel.Discount = discount;

            //                    }
            //                    if (quoteresponse.Response.Quotes[0].Vehicle != null)
            //                    {
            //                        cmbVehicleUsage.SelectedValue = quoteresponse.Response.Quotes[0].Vehicle.VehicleType;
            //                        txtYear.Text = quoteresponse.Response.Quotes[0].Vehicle.YearManufacture;
            //                        Int32 index = cmbMake.FindStringExact(quoteresponse.Response.Quotes[0].Vehicle.Make);
            //                        cmbMake.SelectedIndex = index;
            //                        bindModel(cmbMake.SelectedValue.ToString());
            //                        Int32 indexModel = cmbModel.FindString(quoteresponse.Response.Quotes[0].Vehicle.Model);
            //                        cmbModel.SelectedIndex = indexModel;
            //                    }

            //                    if (cmbVehicleUsage.SelectedValue != null)
            //                    {
            //                        bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
            //                    }
            //                    if (quoteresponse.Response.Quotes[0].Client != null)
            //                    {
            //                        txtName.Text = quoteresponse.Response.Quotes[0].Client.FirstName + " " + quoteresponse.Response.Quotes[0].Client.LastName;
            //                        txtPhone.Text = "";
            //                        txtAdd1.Text = quoteresponse.Response.Quotes[0].Client.Address1;
            //                        txtAdd2.Text = quoteresponse.Response.Quotes[0].Client.Address2;
            //                        //txtCity.Text = quoteresponse.Response.Quotes[0].Client.Town;
            //                        cmdCity.Text = quoteresponse.Response.Quotes[0].Client.Town;
            //                        txtIDNumber.Text = quoteresponse.Response.Quotes[0].Client.IDNumber;
            //                    }

            //                    /////End           
            //                }
            //            }

            //        }

            //    }

            //    //  ICEcashService.LICQuote(regNo, tokenObject.Response.PartnerToken);
            //    //  json.Data = response;
            //}
            //catch (Exception ex)
            //{
            //    response.message = "Error occured.";
            //}
        }

        //public void bindProductid(int VehicleUsageId)
        //public string bindProductid(int VehicleUsageId)
        //{
        //    string ProductId = "";
        //    var client = new RestClient(ApiURL + "GetProductId?VehicleUsageId=" + VehicleUsageId);
        //    var request = new RestRequest(Method.GET);

        //    request.AddHeader("password", Pwd);
        //    request.AddHeader("username", username);
        //    request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    var result = JsonConvert.DeserializeObject<ProductIdModel>(response.Content);
        //    objRiskModel.ProductId = result.ProductId;

        //    ProductId = Convert.ToString(result.ProductId);
        //    return ProductId;
        //}

        public void CalculatePremium()
        {
            try
            {
                //  VehicalIndex = -1; // for now it's always

                VehicleDetails obj = new VehicleDetails();
                //if (VehicalIndex != -1)
                //{
                //    objRiskModel = objlistRisk.FirstOrDefault(c => c.RegistrationNo == txtVrn.Text);
                //}


                //  objRiskModel = objlistRisk.FirstOrDefault(c => c.RegistrationNo == txtVrn.Text);


                if (cmbVehicleUsage.SelectedValue != null)
                {
                    obj.vehicleUsageId = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                }
                if (cmbCoverType.SelectedValue != null)
                {
                    obj.coverType = Convert.ToInt32(cmbCoverType.SelectedValue);
                }
                if (cmbPaymentTerm.SelectedValue != null)
                {
                    obj.PaymentTermid = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                }

                //obj.vehicleUsageId = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
                //obj.sumInsured =Convert.ToDecimal(txtSumInsured.Text);
                //obj.coverType = Convert.ToInt32(cmbCoverType.SelectedValue);
                //obj.PaymentTermid = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                obj.sumInsured = txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text, System.Globalization.CultureInfo.InvariantCulture);
                obj.NumberofPersons = cmbNoofPerson.Value == 0 ? 0 : Convert.ToInt32(cmbNoofPerson.Value);
                // obj.PassengerAccidentCover = chkPassengerAccidentalCover.Checked;
                obj.ExcessBuyBack = chkExcessBuyback.Checked;
                obj.RoadsideAssistance = chkRoadsideAssistance.Checked;
                obj.MedicalExpenses = chkMedicalExpenses.Checked;
                obj.IncludeRadioLicenseCost = objRiskModel.IncludeRadioLicenseCost;
                obj.AddThirdPartyAmount = 0.00m;



                obj.ProductId = objRiskModel.ProductId;
                obj.RadioLicenseCost = objRiskModel.RadioLicenseCost;
                obj.isVehicleRegisteredonICEcash = objRiskModel.isVehicleRegisteredonICEcash;
                obj.BasicPremiumICEcash = objRiskModel.Premium == null ? "0" : Convert.ToString(objRiskModel.Premium);
                obj.StampDutyICEcash = objRiskModel.StampDuty == null ? "0" : Convert.ToString(objRiskModel.StampDuty);
                obj.ZTSCLevyICEcash = objRiskModel.ZTSCLevy == null ? "0" : Convert.ToString(objRiskModel.ZTSCLevy);
                obj.Addthirdparty = false;
                //obj.AddThirdPartyAmount = 00;

                //var client = new RestClient(ApiURL + "CalculateTotalPremium");


                var client = new RestClient(IceCashRequestUrl + "CalculateTotalPremium");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("password", "Geninsure@123");
                request.AddHeader("username", "ameyoApi@geneinsure.com");
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(obj);
                IRestResponse response = client.Execute(request);

                var result = JsonConvert.DeserializeObject<QuoteLogic>(response.Content);

                if (result != null)
                {
                    objRiskModel.Premium = result.Premium == 0 ? 0 : Convert.ToDecimal(result.Premium, System.Globalization.CultureInfo.InvariantCulture);
                    objRiskModel.Discount = result.Discount == 0 ? 0 : Convert.ToDecimal(result.Discount, System.Globalization.CultureInfo.InvariantCulture);
                    objRiskModel.ZTSCLevy = result.ZtscLevy == 0 ? 0 : Convert.ToDecimal(result.ZtscLevy, System.Globalization.CultureInfo.InvariantCulture);
                    objRiskModel.StampDuty = result.StamDuty == 0 ? 0 : Convert.ToDecimal(result.StamDuty, System.Globalization.CultureInfo.InvariantCulture);
                    //9Jan
                    objRiskModel.AnnualRiskPremium = result.AnnualRiskPremium == 0 ? 0 : result.AnnualRiskPremium;
                    objRiskModel.TermlyRiskPremium = result.TermlyRiskPremium == 0 ? 0 : result.TermlyRiskPremium;
                    objRiskModel.QuaterlyRiskPremium = result.QuaterlyRiskPremium == 0 ? 0 : result.QuaterlyRiskPremium;

                    objRiskModel.PassengerAccidentCoverAmountPerPerson = result.PassengerAccidentCoverAmountPerPerson == 0 ? 0 : result.PassengerAccidentCoverAmountPerPerson;
                    objRiskModel.ExcessBuyBackPercentage = result.ExcessBuyBackPercentage == 0 ? 0 : result.ExcessBuyBackPercentage;
                    objRiskModel.RoadsideAssistancePercentage = result.RoadsideAssistancePercentage == 0 ? 0 : result.RoadsideAssistancePercentage;
                    objRiskModel.MedicalExpensesPercentage = result.MedicalExpensesPercentage == 0 ? 0 : result.MedicalExpensesPercentage;

                    objRiskModel.PassengerAccidentCoverAmount = result.PassengerAccidentCoverAmount == 0 ? 0 : result.PassengerAccidentCoverAmount;
                    objRiskModel.ExcessBuyBackAmount = result.ExcessBuyBackAmount == 0 ? 0 : result.ExcessBuyBackAmount;
                    objRiskModel.RoadsideAssistanceAmount = result.RoadsideAssistanceAmount == 0 ? 0 : result.RoadsideAssistanceAmount;
                    objRiskModel.MedicalExpensesAmount = result.MedicalExpensesAmount == 0 ? 0 : result.MedicalExpensesAmount;
                    objRiskModel.NumberofPersons = cmbNoofPerson.Value == 0 ? 0 : Convert.ToInt32(cmbNoofPerson.Value);
                    objRiskModel.ExcessAmount = result.ExcessAmount == 0 ? 0 : result.ExcessAmount;

                }






                //if (VehicalIndex != -1)
                //{
                //    if (result != null)
                //    {
                //        objlistRisk[VehicalIndex].Premium = result.Premium == 0 ? 0 : Convert.ToDecimal(result.Premium, System.Globalization.CultureInfo.InvariantCulture);


                //        objlistRisk[VehicalIndex].Discount = result.Discount == 0 ? 0 : Convert.ToDecimal(result.Discount, System.Globalization.CultureInfo.InvariantCulture);
                //        objlistRisk[VehicalIndex].ZTSCLevy = result.ZtscLevy == 0 ? 0 : Convert.ToDecimal(result.ZtscLevy, System.Globalization.CultureInfo.InvariantCulture);
                //        objlistRisk[VehicalIndex].StampDuty = result.StamDuty == 0 ? 0 : Convert.ToDecimal(result.StamDuty, System.Globalization.CultureInfo.InvariantCulture);
                //        //9Jan
                //        objlistRisk[VehicalIndex].AnnualRiskPremium = result.AnnualRiskPremium == 0 ? 0 : result.AnnualRiskPremium;
                //        objlistRisk[VehicalIndex].TermlyRiskPremium = result.TermlyRiskPremium == 0 ? 0 : result.TermlyRiskPremium;
                //        objlistRisk[VehicalIndex].QuaterlyRiskPremium = result.QuaterlyRiskPremium == 0 ? 0 : result.QuaterlyRiskPremium;

                //        //10Jan

                //        objlistRisk[VehicalIndex].PassengerAccidentCoverAmountPerPerson = result.PassengerAccidentCoverAmountPerPerson == 0 ? 0 : result.PassengerAccidentCoverAmountPerPerson;
                //        objlistRisk[VehicalIndex].ExcessBuyBackPercentage = result.ExcessBuyBackPercentage == 0 ? 0 : result.ExcessBuyBackPercentage;
                //        objlistRisk[VehicalIndex].RoadsideAssistancePercentage = result.RoadsideAssistancePercentage == 0 ? 0 : result.RoadsideAssistancePercentage;
                //        objlistRisk[VehicalIndex].MedicalExpensesPercentage = result.MedicalExpensesPercentage == 0 ? 0 : result.MedicalExpensesPercentage;

                //        objlistRisk[VehicalIndex].PassengerAccidentCoverAmount = result.PassengerAccidentCoverAmount == 0 ? 0 : result.PassengerAccidentCoverAmount;
                //        objlistRisk[VehicalIndex].ExcessBuyBackAmount = result.ExcessBuyBackAmount == 0 ? 0 : result.ExcessBuyBackAmount;
                //        objlistRisk[VehicalIndex].RoadsideAssistanceAmount = result.RoadsideAssistanceAmount == 0 ? 0 : result.RoadsideAssistanceAmount;
                //        objlistRisk[VehicalIndex].MedicalExpensesAmount = result.MedicalExpensesAmount == 0 ? 0 : result.MedicalExpensesAmount;
                //        objlistRisk[VehicalIndex].NumberofPersons = cmbNoofPerson.Value == 0 ? 0 : Convert.ToInt32(cmbNoofPerson.Value);
                //        objlistRisk[VehicalIndex].ExcessAmount = result.ExcessAmount == 0 ? 0 : result.ExcessAmount;

                //    }
                //}
                //else if (result != null)
                //{
                //    objRiskModel.Premium = result.Premium == 0 ? 0 : Convert.ToDecimal(result.Premium, System.Globalization.CultureInfo.InvariantCulture);
                //    objRiskModel.Discount = result.Discount == 0 ? 0 : Convert.ToDecimal(result.Discount, System.Globalization.CultureInfo.InvariantCulture);
                //    objRiskModel.ZTSCLevy = result.ZtscLevy == 0 ? 0 : Convert.ToDecimal(result.ZtscLevy, System.Globalization.CultureInfo.InvariantCulture);
                //    objRiskModel.StampDuty = result.StamDuty == 0 ? 0 : Convert.ToDecimal(result.StamDuty, System.Globalization.CultureInfo.InvariantCulture);
                //    //9Jan
                //    objRiskModel.AnnualRiskPremium = result.AnnualRiskPremium == 0 ? 0 : result.AnnualRiskPremium;
                //    objRiskModel.TermlyRiskPremium = result.TermlyRiskPremium == 0 ? 0 : result.TermlyRiskPremium;
                //    objRiskModel.QuaterlyRiskPremium = result.QuaterlyRiskPremium == 0 ? 0 : result.QuaterlyRiskPremium;



                //    objRiskModel.PassengerAccidentCoverAmountPerPerson = result.PassengerAccidentCoverAmountPerPerson == 0 ? 0 : result.PassengerAccidentCoverAmountPerPerson;
                //    objRiskModel.ExcessBuyBackPercentage = result.ExcessBuyBackPercentage == 0 ? 0 : result.ExcessBuyBackPercentage;
                //    objRiskModel.RoadsideAssistancePercentage = result.RoadsideAssistancePercentage == 0 ? 0 : result.RoadsideAssistancePercentage;
                //    objRiskModel.MedicalExpensesPercentage = result.MedicalExpensesPercentage == 0 ? 0 : result.MedicalExpensesPercentage;

                //    objRiskModel.PassengerAccidentCoverAmount = result.PassengerAccidentCoverAmount == 0 ? 0 : result.PassengerAccidentCoverAmount;
                //    objRiskModel.ExcessBuyBackAmount = result.ExcessBuyBackAmount == 0 ? 0 : result.ExcessBuyBackAmount;
                //    objRiskModel.RoadsideAssistanceAmount = result.RoadsideAssistanceAmount == 0 ? 0 : result.RoadsideAssistanceAmount;
                //    objRiskModel.MedicalExpensesAmount = result.MedicalExpensesAmount == 0 ? 0 : result.MedicalExpensesAmount;
                //    objRiskModel.NumberofPersons = cmbNoofPerson.Value == 0 ? 0 : Convert.ToInt32(cmbNoofPerson.Value);
                //    objRiskModel.ExcessAmount = result.ExcessAmount == 0 ? 0 : result.ExcessAmount;

                //}




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void Checkobject()
        {
            CoverObject test = new CoverObject();
            test.Id = 1;
            test.name = "test";

            var client = new RestClient(IceCashRequestUrl + "test1");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            //request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(test);
            IRestResponse response = client.Execute(request);


        }

        //public decimal CalculateDiscount(string coverAmount, int PaymentTermId)
        //{
        //    decimal LoyaltyDiscount = 0;

        //    var Setting = InsuranceContext.Settings.All();
        //    var DiscountOnRenewalSettings = Setting.Where(x => x.keyname == "Discount On Renewal").FirstOrDefault();
        //    var premium = premiumAmount;
        //    switch (PaymentTermId)
        //    {
        //        case 1:
        //            var AnnualRiskPremium = premium;
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.percentage))
        //            {
        //                LoyaltyDiscount = ((AnnualRiskPremium * Convert.ToDecimal(DiscountOnRenewalSettings.value)) / 100);
        //            }
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.amount))
        //            {
        //                LoyaltyDiscount = Convert.ToDecimal(DiscountOnRenewalSettings.value);
        //            }
        //            break;
        //        case 3:
        //            var QuaterlyRiskPremium = premium;
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.percentage))
        //            {
        //                LoyaltyDiscount = ((QuaterlyRiskPremium * Convert.ToDecimal(DiscountOnRenewalSettings.value)) / 100);
        //            }
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.amount))
        //            {
        //                LoyaltyDiscount = Convert.ToDecimal(DiscountOnRenewalSettings.value);
        //            }
        //            break;
        //        case 4:
        //            var TermlyRiskPremium = premium;
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.percentage))
        //            {
        //                LoyaltyDiscount = ((TermlyRiskPremium * Convert.ToDecimal(DiscountOnRenewalSettings.value)) / 100);
        //            }
        //            if (DiscountOnRenewalSettings.ValueType == Convert.ToInt32(eSettingValueType.amount))
        //            {
        //                LoyaltyDiscount = Convert.ToDecimal(DiscountOnRenewalSettings.value);
        //            }
        //            break;
        //    }
        //}

        public enum eSettingValueType
        {
            percentage = 1,
            amount = 2
        }

        //private void cmbVehicleUsage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbVehicleUsage.SelectedIndex > 0)
        //    {
        //        bindProductid(Convert.ToInt32(cmbVehicleUsage.SelectedValue));
        //        NewerrorProvider.Clear();
        //    }
        //}

        private void chkPassengerAccidentalCover_CheckedChanged(object sender, EventArgs e)
        {

            //  objRiskModel.PassengerAccidentCover = chkPassengerAccidentalCover.Checked;

            // CalculatePremium();
        }

        private void chkMedicalExpenses_CheckedChanged(object sender, EventArgs e)
        {
            objRiskModel.MedicalExpenses = chkMedicalExpenses.Checked;
            // CalculatePremium();
        }

        private void chkRoadsideAssistance_CheckedChanged(object sender, EventArgs e)
        {
            objRiskModel.RoadsideAssistance = chkRoadsideAssistance.Checked;
            //  CalculatePremium();
        }

        private void chkExcessBuyback_CheckedChanged(object sender, EventArgs e)
        {
            objRiskModel.ExcessBuyBack = chkExcessBuyback.Checked;
            //   CalculatePremium();
        }

        public SummaryDetailModel SaveCustomerVehical()
        {
            CustomerVehicalModel objPlanModel = new CustomerVehicalModel();
            SummaryDetailModel summaryDetialsModel = new SummaryDetailModel();

            PolicyDetail policyDetial = new PolicyDetail();
            objlistVehicalModel = new List<VehicalModel>();

            objPlanModel.CustomerModel = customerInfo;
            objPlanModel.RiskDetailModel = objlistRisk;
            objPlanModel.PolicyDetail = policyDetial;
            objPlanModel.SummaryDetailModel = summaryModel;


            if (objPlanModel != null)
            {
                var client = new RestClient(IceCashRequestUrl + "SaveVehicalDetails");
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

                summaryDetialsModel = JsonConvert.DeserializeObject<SummaryDetailModel>(response.Content);
                if (summaryDetialsModel != null)
                {

                }
            }

            return summaryDetialsModel;
        }




        public void CaclulateSummary(List<RiskDetailModel> objRiskDetail)
        {
            //List<RiskDetailModel> objRiskDetail = new List<RiskDetailModel>();

            //List<RiskDetailModel> objRiskDetail = new List<RiskDetailModel>
            //    {
            //        new RiskDetailModel { Premium =Convert.ToDecimal(45.67), ZTSCLevy =Convert.ToDecimal(3.24), StampDuty = Convert.ToDecimal(1.35),VehicleLicenceFee= Convert.ToDecimal(23.56),RadioLicenseCost=Convert.ToDecimal(23.56),IncludeRadioLicenseCost=true,Discount=Convert.ToDecimal(3),SumInsured=Convert.ToDecimal(2.6) },
            //        new RiskDetailModel { Premium =Convert.ToDecimal(5.67), ZTSCLevy =Convert.ToDecimal(7.4), StampDuty = Convert.ToDecimal(6.35),VehicleLicenceFee= Convert.ToDecimal(34.56),RadioLicenseCost=Convert.ToDecimal(3.56),IncludeRadioLicenseCost=false,Discount=Convert.ToDecimal(3),SumInsured=Convert.ToDecimal(33.56)}
            //    };


            summaryModel = new SummaryDetailModel();

            summaryModel.TotalPremium = 0.00m;
            summaryModel.TotalRadioLicenseCost = 0.00m;
            summaryModel.Discount = 0.00m;
            summaryModel.AmountPaid = 0.00m;
            summaryModel.TotalSumInsured = 0.00m;
            try
            {
                if (objRiskDetail != null && objRiskDetail.Count > 0)
                {



                    foreach (var item in objRiskDetail)
                    {
                        //summaryModel.TotalPremium += item.Premium + item.ZTSCLevy + item.StampDuty + item.VehicleLicenceFee; 

                        // summaryModel.TotalPremium += item.Premium + item.ZTSCLevy + item.StampDuty;

                        decimal totalPrem = Convert.ToDecimal(item.Premium) + Convert.ToDecimal(item.ZTSCLevy) + Convert.ToDecimal(item.StampDuty) + Convert.ToDecimal(item.VehicleLicenceFee);

                        summaryModel.ArrearsAmt += Convert.ToDecimal(item.ArrearsAmt);
                        summaryModel.LicTransactionAmt += Convert.ToDecimal(item.LicTransactionAmt);
                        summaryModel.AdministrationAmt += Convert.ToDecimal(item.AdministrationAmt);


                        summaryModel.TotalPremium += totalPrem;
                        summaryModel.VehicleLicencefees += Convert.ToDecimal(item.VehicleLicenceFee);

                        if (item.IncludeRadioLicenseCost)
                        {
                            summaryModel.TotalPremium += Convert.ToDecimal(item.RadioLicenseCost);
                            summaryModel.TotalRadioLicenseCost += Convert.ToDecimal(item.RadioLicenseCost);
                        }

                        summaryModel.Discount += item.Discount;
                    }


                    summaryModel.BasicPremium = objRiskDetail.Sum(c => c.Premium).Value;
                    summaryModel.PenaltiesAmt = objRiskDetail.Sum(c => c.PenaltiesAmt).Value;
                    summaryModel.TotalLicAmount += Convert.ToDecimal(summaryModel.VehicleLicencefees + summaryModel.TotalRadioLicenseCost);


                    //summaryModel.TotalRadioLicenseCost = Math.Round(Convert.ToDecimal(summaryModel.TotalRadioLicenseCost, System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.Discount = Math.Round(Convert.ToDecimal(summaryModel.Discount, System.Globalization.CultureInfo.InvariantCulture), 2);
                    var calcualatedPremium = Math.Round(Convert.ToDecimal(summaryModel.TotalPremium, System.Globalization.CultureInfo.InvariantCulture), 2);
                    //summaryModel.TotalPremium = Math.Round(Convert.ToDecimal(calcualatedPremium - summaryModel.Discount), 2);

                    //model.MaxAmounttoPaid = Math.Round(Convert.ToDecimal(model.TotalPremium), 2);
                    //summaryModel.AmountPaid = Convert.ToDecimal(summaryModel.TotalPremium, System.Globalization.CultureInfo.InvariantCulture);
                    summaryModel.AmountPaid = Convert.ToDecimal(summaryModel.TotalPremium);

                    summaryModel.TotalStampDuty = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.StampDuty), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.TotalSumInsured = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.SumInsured), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.TotalZTSCLevies = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.ZTSCLevy), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.ExcessBuyBackAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.ExcessBuyBackAmount), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.MedicalExpensesAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.MedicalExpensesAmount), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.PassengerAccidentCoverAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.PassengerAccidentCoverAmount), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.RoadsideAssistanceAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.RoadsideAssistanceAmount), System.Globalization.CultureInfo.InvariantCulture), 2);
                    summaryModel.ExcessAmount = Math.Round(Convert.ToDecimal(objRiskDetail.Sum(item => item.ExcessAmount), System.Globalization.CultureInfo.InvariantCulture), 2);


                    txtBasicPremium.Text = summaryModel.BasicPremium.ToString();
                    txtDiscount.Text = Convert.ToString(summaryModel.Discount);
                    txtTotalPremium.Text = Convert.ToString(summaryModel.TotalPremium);
                    //txtAmountDue.Text = Convert.ToString(summaryModel.AmountPaid);
                    txtTotalRadioCost.Text = Convert.ToString(summaryModel.TotalRadioLicenseCost);
                    txtStampDuty.Text = Convert.ToString(summaryModel.TotalStampDuty);
                    //txtTotalSumInsured.Text = Convert.ToString(summaryModel.TotalSumInsured);
                    //txtExcessAmount.Text = Convert.ToString(summaryModel.ExcessAmount);
                    //txtMedicalExcessAmount.Text = Convert.ToString(summaryModel.MedicalExpensesAmount);
                    // txtPassengerAccidentAmt.Text = Convert.ToString(summaryModel.PassengerAccidentCoverAmount);
                    // txtRoadsideAssitAmt.Text = Convert.ToString(summaryModel.RoadsideAssistanceAmount);
                    txtZTSCLevies.Text = Convert.ToString(summaryModel.TotalZTSCLevies);
                    //  txtExcessBuyBackAmt.Text = Convert.ToString(summaryModel.ExcessBuyBackAmount);
                    //txtRadioLicAmount.Text = Convert.ToString(summaryModel.TotalRadioLicenseCost);
                    txtArrearsAmt.Text = Convert.ToString(summaryModel.ArrearsAmt);

                    txtTransactionAmt.Text = Convert.ToString(summaryModel.LicTransactionAmt);

                    txtAdministrationAmt.Text = Convert.ToString(summaryModel.AdministrationAmt);

                    txtLicPenalties.Text = Convert.ToString(summaryModel.PenaltiesAmt);

                    txtZinaraAmount.Text = Convert.ToString(summaryModel.VehicleLicencefees);

                    txtTotalLicAmt.Text = Convert.ToString(summaryModel.TotalLicAmount);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnSumContinue_Click(object sender, EventArgs e)
        {


            pnlsumary.Visible = false;
            pnlconfimpaymeny.Visible = true;

            txttotalamuntc.Text = txtTotalPremium.Text;

            //txtPartialAmount.Text = txtTotalPremium.Text;

            //checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            //// Save all details
            //CustomerModel customerModel = new CustomerModel();
            //customerModel.FirstName = txtName.Text;
            //customerModel.EmailAddress = txtEmailAddress.Text;


            //checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            //// Save all details
            //CustomerModel customerModel = new CustomerModel();
            //customerModel.FirstName = txtName.Text;
            //customerModel.EmailAddress = txtEmailAddress.Text;

            ////pnlThankyou.Visible = true;
            ////SaveCustomerVehical();


            ////var summaryDetails = SaveCustomerVehical();

            ////Save Payment info
            //PaymentResult objResult = new PaymentResult();
            //long TransactionId = 0;
            //TransactionId = GenerateTransactionId();
            //decimal transctionAmt = Convert.ToDecimal(txtAmountDue.Text);

            //string paymentTermName = "Swipe";

            //if (radioMobile.Checked)
            //    paymentTermName = "Mobile";

            //string TransactionId1 = Convert.ToString(TransactionId); // remove after testing
            //if(summaryDetails!=null)
            //{
            //    SavePaymentinformation(TransactionId1, summaryDetails.Id);  // remove after testing
            //}


            //if(summaryDetails != null)
            //{
            //SendSymbol(TransactionId, transctionAmt, summaryDetails.Id);
            //SendSymbol(TransactionId, transctionAmt, paymentTermName);



            //}



            //End


            //List<VehicleLicQuote> obj = new List<VehicleLicQuote>();
            //try
            //{
            //    if (objlistRisk != null && objlistRisk.Count > 0)
            //    {
            //        foreach (var item in objlistRisk)
            //        {
            //            obj.Add(new VehicleLicQuote
            //            {
            //                //VRN = txtVrn.Text,
            //                VRN = item.RegistrationNo,
            //                //VRN= ,
            //                ClientIDType = "1",
            //                IDNumber = "ABCDEFGHIJ1",
            //                DurationMonths = "4",
            //                LicFrequency = "3",
            //                RadioTVUsage = "1",
            //                RadioTVFrequency = "1"
            //            });
            //        }
            //    }

            //    if (ObjToken != null)
            //    {
            //        if (ObjToken.Response.PartnerToken != null)
            //        {
            //            ResultRootObject quoteresponse = IcServiceobj.TPILICQuote(obj, ObjToken.Response.PartnerToken);
            //        }
            //    }



            //    if (quoteresponse != null)
            //    {
            //        response.result = quoteresponse.Response.Result;
            //        if (response.result == 0)
            //        {
            //            response.message = quoteresponse.Response.Quotes[0].Message;
            //        }
            //        else
            //        {
            //            response.Data = quoteresponse;

            //            if (quoteresponse.Response.Quotes != null)
            //            {
            //                if (quoteresponse.Response.Quotes.Count != 0)
            //                {
            //                    objVehicleLicense = new List<VehicleLicenseModel>();
            //                    foreach (var item in quoteresponse.Response.Quotes.ToList())
            //                    {
            //                        string format = "yyyyMMdd";
            //                        if (item.Licence != null)
            //                        {
            //                            var LicExpiryDate = DateTime.ParseExact(item.Licence.LicExpiryDate, format, CultureInfo.InvariantCulture);
            //                            objVehicleLicense.Add(new VehicleLicenseModel
            //                            {
            //                                InsuranceID = item.InsuranceID,
            //                                VRN = item.VRN,
            //                                CombinedID = item.CombinedID,
            //                                LicenceID = item.LicenceID,
            //                                TotalAmount = Convert.ToDecimal(item.Licence.TotalAmount),
            //                                RadioTVFrequency = Convert.ToInt32(item.Licence.RadioTVFrequency),
            //                                RadioTVUsage = Convert.ToInt32(item.Licence.RadioTVUsage),
            //                                LicFrequency = Convert.ToInt32(item.Licence.LicFrequency),
            //                                NettMass = Convert.ToString(item.Licence.NettMass),

            //                                LicExpiryDate = Convert.ToDateTime(LicExpiryDate),
            //                                TransactionAmt = Convert.ToInt32(item.Licence.TransactionAmt),
            //                                ArrearsAmt = Convert.ToInt32(item.Licence.ArrearsAmt),
            //                                PenaltiesAmt = Convert.ToInt32(item.Licence.PenaltiesAmt),
            //                                AdministrationAmt = Convert.ToInt32(item.Licence.AdministrationAmt),
            //                                TotalLicAmt = Convert.ToInt32(item.Licence.TotalRadioTVAmt),
            //                                RadioTVAmt = Convert.ToInt32(item.Licence.RadioTVAmt),
            //                                RadioTVArrearsAmt = Convert.ToInt32(item.Licence.RadioTVArrearsAmt),
            //                                TotalRadioTVAmt = Convert.ToInt32(item.Licence.TotalRadioTVAmt),
            //                                VehicelId = objlistVehicalModel.FirstOrDefault(c => c.VRN == item.VRN).VehicalId
            //                            });
            //                        }


            //                    }
            //                    SaveVehicleLicense(objVehicleLicense);
            //                }
            //            }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}


        }

        private void txtSumInsured_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public int CheckDuplicateVRNNumber()
        {
            int responsMsg = 0;
            string regNo = txtVrn.Text;
            //string regNo = "A233fdfd";

            var client = new RestClient(IceCashRequestUrl + "CheckDuplicateVRNNumber?regNo=" + regNo);
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<RiskDetailModel>(response.Content);
            string resutl = result.RegistrationNo;
            if (resutl != null)
            {
                responsMsg = 1;
                return responsMsg;
            }
            else
            {
                responsMsg = 2;
            }

            return responsMsg;
        }

        public int checkEmailExist()
        {
            int responsMsg = 0;
            string EmailAddress = txtEmailAddress.Text;
            var client = new RestClient(IceCashRequestUrl + "chkEmailExist?EmailAddress=" + EmailAddress);
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<EmailModel>(response.Content);
            string resutl = result == null ? "" : result.EmailAddress;
            if (resutl == "Email already Exist")
            {
                GetCustomerDetials(result);
                responsMsg = 1;
                return responsMsg;
                //MessageBox.Show("Email already Exist");
                //return;
            }
            else
            {
                ClearDetails();
                responsMsg = 2;
            }
            return responsMsg;
        }

        private void GetCustomerDetials(EmailModel customer)
        {
            txtPhone.Text = customer.PhonuNumber;
            txtDOB.Text = customer.DateOfBirth.ToString();

            if (customer.Gender == "Male")
                rdbMale.Checked = true;
            else
                rdbFemale.Checked = true;

            txtAdd1.Text = customer.Address1;
            txtAdd2.Text = customer.Address2;
            cmdCity.SelectedText = customer.City;
            //txtZipCode.Text = customer.ZipCode;
            txtIDNumber.Text = customer.IDNumber;
        }


        public int checkCompanyEmailExist()
        {
            int responsMsg = 0;
            string EmailAddress = txtCmpEmail.Text;

            var client = new RestClient(IceCashRequestUrl + "chkCompanyEmailExist?EmailAddress=" + EmailAddress);
            var request = new RestRequest(Method.POST);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<CompanyEmailModel>(response.Content);
            string resutl = result == null ? "" : result.CompanyEmail;
            if (resutl == "Email already Exist")
            {
                GetCompanyDetials(result);
                responsMsg = 1;
                return responsMsg;
                //MessageBox.Show("Email already Exist");
                //return;
            }
            else
            {
                ClearDetails();
                responsMsg = 2;
            }
            return responsMsg;
        }


        private void GetCompanyDetials(CompanyEmailModel customer)
        {

            txtCompany.Text = customer.CompanyName;
            txtCmpAddress.Text = customer.CompanyAddress;
            txtCmpBusinessId.Text = customer.CompanyBusinessId;
            txtCmpPhone.Text = customer.CompanyPhone;

            cmbCmpCity.SelectedValue = customer.CompanyCity == null ? "-1" : customer.CompanyCity;


        }


        private void ClearDetails()
        {
            txtPhone.Text = "";
            txtDOB.Text = DateTime.Now.ToShortDateString();

            rdbMale.Checked = true;
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            cmdCity.SelectedText = "";
            // txtZipCode.Text = "";
            // txtIDNumber.Text = "";
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            //Form1 obj = new Form1();
            //List<RiskDetailModel> objlistRisk = new List<RiskDetailModel>();
            //objlistRisk = null;
            //pnlThankyou.Visible = false;
            ////PnlVrn.Visible = true;
            //obj.Show();
            //this.Hide();
        }

        private void txtDOB_ValueChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        public bool SaveVehicleLicense(List<VehicleLicenseModel> objVehicleLicense)
        {
            var client = new RestClient(IceCashRequestUrl + "VehicleLicense");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(objVehicleLicense);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<Messages>(response.Content);
            return result.Suceess;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Form1 obj = new Form1();
            //List<RiskDetailModel> objlistRisk = new List<RiskDetailModel>();
            //objlistRisk = null;
            //pnlThankyou.Visible = false;
            ////PnlVrn.Visible = true;
            //obj.Show();
            //this.Hide();
        }

        private void btnHomeTab_Click(object sender, EventArgs e)
        {
            Form1 objFrm = new Form1();
            objFrm.Show();
            this.Close();
        }

        public void SaveVehicalMakeAndModel(string make, string model)
        {
            VehicalMakeModel obj = new VehicalMakeModel();
            obj.make = make;
            obj.model = model;
            var client = new RestClient(IceCashRequestUrl + "VehicalMakeAndModel");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(obj);
            IRestResponse response = client.Execute(request);
            //var result = JsonConvert.DeserializeObject<Messages>(response.Content);            
        }

        //private void btnPayNow_Click(object sender, EventArgs e)
        //{
        //    //PaymentResult objResult = new PaymentResult();
        //    //long TransactionId = 0;
        //    //TransactionId=GenerateTransactionId();
        //    //decimal transctionAmt =Convert.ToDecimal(txtAmountDue.Text);
        //    //SendSymbol(TransactionId, transctionAmt);  
        //}

        public long GenerateTransactionId()
        {
            TransactionId = 0;
            var client = new RestClient(IceCashRequestUrl + "GenerateTransactionId");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<UniqeTransactionModel>(response.Content);
            if (result != null)
            {
                TransactionId = result.TransactionId;
            }
            return TransactionId;
        }

        // For Payment
        //public static void SendSymbol(long TransactionId, decimal transctionAmt)
        //public void SendSymbol(long TransactionId, decimal transctionAmt, int summaryId)
        //      public void SendSymbol(long TransactionId, decimal transctionAmt, string paymentTermName)
        //      {
        //          string xmlString = "";
        //          //// TransactionId = 100020; // need to do remove
        //          bool isPaymentDone = false;

        //          decimal amountIncents = (int)(transctionAmt * 100);

        //          string logMsg = txtVrn.Text + "_" + paymentTermName + "_" + transctionAmt;

        //          Service_db.WriteIceCashLog("init paymeent", logMsg, "SendSymbol", txtVrn.Text, branchName);



        //          //To do  
        //          //summaryModel.PaymentStatus = true;
        //          // amountIncents = (int)(Convert.ToDecimal(txtPartialAmount.Text) * 100); // it was for partail payment


        //          //Initialze Terminal

        //          if (!CheckPosInitOrNot())
        //          {
        //              xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
        //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' Action='INIT'/></Esp:Interface>";

        //              InitializeTermianl("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

        //          }


        //          xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
        //              <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='" + TransactionId + "' Type='PURCHASE' TransactionAmount='" + amountIncents + "'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";


        //          //   isPaymentDone = SendTransaction(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString);

        //          // isPaymentDone = true;
        //          //PartialPaymentModel paymentDetail = SavePartialPayment();

        //          //decimal balanceAmount = Convert.ToDecimal(summaryModel.TotalPremium - paymentDetail.CalulatedPremium);

        //          //if (balanceAmount > 0)
        //          //{
        //          //    TransactionId = GenerateTransactionId();
        //          //    btnConfirmPayment.Enabled = true;
        //          //    pictureBox2.Visible = false;
        //          //    RadioSwipe.Checked = true;
        //          //    btnConfirmPayment.Text = "Pay.";
        //          //    txtPartialAmount.Text = balanceAmount.ToString();
        //          //    return;
        //          //}




        //          //if (rdBtnPartialPayment.Checked && txtPartialPayment.Text != "")
        //          //{
        //          //    //To do  
        //          //    summaryModel.PaymentStatus = true;
        //          //    PartialPaymentModel paymentDetail = SavePartialPayment();

        //          //    if ((summaryModel.TotalPremium - paymentDetail.CalulatedPremium) > 0)
        //          //    {
        //          //        txtPartialPayment.Text = Convert.ToString(summaryModel.TotalPremium - paymentDetail.CalulatedPremium);
        //          //    }
        //          //    return;
        //          //}


        //          string msg = "";
        //          try
        //          {
        //              if (SendTransaction(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString))
        //              {
        //                  //if (isPaymentDone) // testing condition false

        //                  // btnConfirmPayment.Text = "Saving Vehicle.";

        //                  // this.Invoke(new Action(() => btnConfirmPayment.Text = "Saving Vehicle."));


        //                  // btnConfirmPayment.Invoke((MethodInvoker)delegate { this.Text = "Saving Vehicle."; });


        //                  var summaryDetails = SaveCustomerVehical();
        //                  lblPaymentMsg.Text = "";
        //                  // Save information
        //                  if (summaryDetails != null)
        //                  {
        //                      var terninalid = ConfigurationManager.AppSettings["TerminalId"];
        //                      decimal tranjectionamt = amountIncents;

        //                      if (summaryDetails.Id == 0)
        //                      {
        //                          MyMessageBox.ShowBox("Error occur, please contact to admistrator.", "Message");
        //                          btnConfirmPayment.Enabled = true;
        //                          picImageConfirmPayment.Visible = false;
        //                          return;
        //                      }

        //                      // to handle the exception
        //                      string iceCashPolicyNumber = "";
        //                      ResultRootObject policyDetailsIceCash = new ResultRootObject();
        //                      try
        //                      {
        //                          policyDetailsIceCash = ApproveVRNToIceCash(summaryDetails.Id);
        //                          if (policyDetailsIceCash != null && policyDetailsIceCash.Response != null)
        //                          {
        //                              iceCashPolicyNumber = policyDetailsIceCash.Response.PolicyNo;
        //                          }
        //                      }
        //                      catch (Exception ex)
        //                      {
        //                          Service_db.WriteIceCashLog("ApproveIceCash 2 ", ex.Message, "during approvevrnintoIcecah", txtVrn.Text, branchName);
        //                      }


        //                      //  btnConfirmPayment.Text = "Sending email..";

        //                      SavePaymentinformation(TransactionId.ToString(), summaryDetails.Id, paymentTermName, CardDetail, terninalid, transctionAmt, iceCashPolicyNumber);

        //                      lblpayment.Text = "";
        //                      lblpayment.Text += "Transaction ID =" + TransactionId;
        //                      lblpayment.Text += "\n";
        //                      lblpayment.Text = "Sucessfully ddddd";


        //                      lblThankyou.Text = "Thank you for Registration";
        //                      lblThankyou.Text += "\n";
        //                      lblThankyou.Text += "Transaction ID =" + TransactionId;
        //                      lblThankyou.Text += "\n";
        //                      lblThankyou.Text += responseMessage;

        //                      if (policyDetailsIceCash.Response != null && !string.IsNullOrEmpty(policyDetailsIceCash.Response.PolicyNo))
        //                      {
        //                          lblThankyou.Text += "\n";
        //                          lblThankyou.Text += "Cover note number =" + policyDetailsIceCash.Response.PolicyNo;

        //                          lblThankyou.Text += "\n";
        //                          lblThankyou.Text += "VRN =" + policyDetailsIceCash.Response.VRN;

        //                          lblThankyou.Text += "\n";
        //                          lblThankyou.Text += "Status =" + policyDetailsIceCash.Response.Status;
        //                      }

        //                      pnlThankyou.Visible = true;
        //                      pnlconfimpaymeny.Visible = false;



        //                      // display on thank you page  for testing
        //                      //List<ResultLicenceIDResponse> list = new List<ResultLicenceIDResponse>();
        //                      //ResultLicenceIDResponse resresponse = new ResultLicenceIDResponse { vrn = "aas9307", licenceid = "123456789", receiptid = "r123456789", status = "approved", transactionamt = "20.00", arrearsamt = "0", penaltiesamt = "0", administrationamt = "0", totallicamt = "20.00", radiotvamt = "5.00", make = "mazda", model = "mdl0663" };
        //                      //licenseDiskList.Add(resresponse);
        //                      //loadLicenceDiskPanel(list);


        //                      //foreach (var item in objlistRisk)  // for now it's  commented
        //                      //{

        //                      //    item.Id = summaryDetails.VehicleId;  

        //                      //    if (!string.IsNullOrEmpty(item.CombinedID) && (item.CombinedID != "0"))
        //                      //    {
        //                      //        btnConfirmPayment.Text = "Approving license..";

        //                      //        DisplayLicenseDisc(item, parternToken, item.Id);
        //                      //    }
        //                      //}


        //                      var item = objlistRisk[0];
        //                      item.Id = summaryDetails.VehicleId;
        //                      //  _vehicleId = summaryDetails.VehicleId;

        //                      if (!string.IsNullOrEmpty(item.CombinedID) && (item.CombinedID != "0"))
        //                      {
        //                          btnConfirmPayment.Text = "Approving license..";
        //                          DisplayLicenseDisc(item, parternToken, item.Id);
        //                      }

        //                      //if (licenseDiskList.Count > 0)
        //                      //    btnPrint.Visible = true;
        //                      //else
        //                      //    btnPrint.Visible = false;

        //                      // btnPrint.Visible = false;
        //                      btnConfirmPayment.Text = "Pay";


        //                  }
        //              }
        //              else
        //              {


        //                  MyMessageBox.ShowBox("Error occured. " + responseMessage, "Message");
        //                  TransactionId = GenerateTransactionId();

        //                  string logMsg1 = txtVrn.Text + "_" + paymentTermName + "_" + transctionAmt;

        //                  Service_db.WriteIceCashLog("payment fail", logMsg1, "SendSymbol", txtVrn.Text, branchName);

        //                  //  pnlconfimpaymeny.Visible = false;
        //                  // pnlErrormessage.Visible = true;
        //                  //  lblErrMessage.Text = responseMessage;
        //                  //   lblErrMessage.ForeColor = Color.Red;
        //                  lblPaymentMsg.Text = "";
        //                  btnConfirmPayment.Text = "Pay";

        //              }
        //              //SendTransaction("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

        //          }
        //          catch (Exception ex)
        //          {
        //              // WriteLog("InitializeTermianl :" + ex.Message);


        //              Service_db.WriteIceCashLog("payment exception", ex.Message, "SendSymbol", txtVrn.Text, branchName);
        //              lblPaymentMsg.Text += "InitializeTermianl: " + ex.Message;

        //              //MessageBox.Show(ex.ToString());
        //          }
        //          finally
        //          {

        //              //              xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
        //              //<Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' Action ='CLOSE'/></Esp:Interface>";
        //              //              InitializeTermianl("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);


        //              SetLoadingDuringPayment(false);
        //          }
        //      }


        public void SendSymbol(long TransactionId, decimal transctionAmt, string paymentTermName)
        {
            string xmlString = "";
            //// TransactionId = 100020; // need to do remove
            bool isPaymentDone = false;

            decimal amountIncents = (int)(transctionAmt * 100);

            string logMsg = txtVrn.Text + "_" + paymentTermName + "_" + transctionAmt;

            Service_db.WriteIceCashLog("init paymeent", logMsg, "SendSymbol", txtVrn.Text, branchName);



            //To do  
            //summaryModel.PaymentStatus = true;
            // amountIncents = (int)(Convert.ToDecimal(txtPartialAmount.Text) * 100); // it was for partail payment


            //Initialze Terminal
            xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
  <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' Action='INIT'/></Esp:Interface>";

            InitializeTermianl("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

            //  lblPaymentMsg.Text = "Please swipe card.";
            //lblProcessingMsg.Text = "Please swipe card.";


            xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='" + TransactionId + "' Type='PURCHASE' TransactionAmount='" + amountIncents + "'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";


            //   isPaymentDone = SendTransaction(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString);

            // isPaymentDone = true;
            //PartialPaymentModel paymentDetail = SavePartialPayment();

            //decimal balanceAmount = Convert.ToDecimal(summaryModel.TotalPremium - paymentDetail.CalulatedPremium);

            //if (balanceAmount > 0)
            //{
            //    TransactionId = GenerateTransactionId();
            //    btnConfirmPayment.Enabled = true;
            //    pictureBox2.Visible = false;
            //    RadioSwipe.Checked = true;
            //    btnConfirmPayment.Text = "Pay.";
            //    txtPartialAmount.Text = balanceAmount.ToString();
            //    return;
            //}




            //if (rdBtnPartialPayment.Checked && txtPartialPayment.Text != "")
            //{
            //    //To do  
            //    summaryModel.PaymentStatus = true;
            //    PartialPaymentModel paymentDetail = SavePartialPayment();

            //    if ((summaryModel.TotalPremium - paymentDetail.CalulatedPremium) > 0)
            //    {
            //        txtPartialPayment.Text = Convert.ToString(summaryModel.TotalPremium - paymentDetail.CalulatedPremium);
            //    }
            //    return;
            //}


            string msg = "";
            try
            {
                if (SendTransaction(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString))
                {
                    //if (isPaymentDone) // testing condition false

                    // btnConfirmPayment.Text = "Saving Vehicle.";

                    // this.Invoke(new Action(() => btnConfirmPayment.Text = "Saving Vehicle."));


                    // btnConfirmPayment.Invoke((MethodInvoker)delegate { this.Text = "Saving Vehicle."; });


                    var summaryDetails = SaveCustomerVehical();
                    lblPaymentMsg.Text = "";
                    // Save information
                    if (summaryDetails != null)
                    {
                        var terninalid = ConfigurationManager.AppSettings["TerminalId"];
                        decimal tranjectionamt = amountIncents;

                        if (summaryDetails.Id == 0)
                        {
                            MyMessageBox.ShowBox("Error occur, please contact to admistrator.", "Message");
                            btnConfirmPayment.Enabled = true;
                            picImageConfirmPayment.Visible = false;
                            return;
                        }

                        // to handle the exception
                        string iceCashPolicyNumber = "";
                        ResultRootObject policyDetailsIceCash = new ResultRootObject();
                        try
                        {
                            policyDetailsIceCash = ApproveVRNToIceCash(summaryDetails.Id);
                            if (policyDetailsIceCash != null && policyDetailsIceCash.Response != null)
                            {
                                iceCashPolicyNumber = policyDetailsIceCash.Response.PolicyNo;
                            }
                        }
                        catch (Exception ex)
                        {
                            Service_db.WriteIceCashLog("ApproveIceCash 2 ", ex.Message, "during approvevrnintoIcecah", txtVrn.Text, branchName);
                        }


                        //  btnConfirmPayment.Text = "Sending email..";

                        SavePaymentinformation(TransactionId.ToString(), summaryDetails.Id, paymentTermName, CardDetail, terninalid, transctionAmt, iceCashPolicyNumber);

                        lblpayment.Text = "";
                        lblpayment.Text += "Transaction ID =" + TransactionId;
                        lblpayment.Text += "\n";
                        lblpayment.Text = "Sucessfully ddddd";


                        lblThankyou.Text = "Thank you for Registration";
                        lblThankyou.Text += "\n";
                        lblThankyou.Text += "Transaction ID =" + TransactionId;
                        lblThankyou.Text += "\n";
                        lblThankyou.Text += responseMessage;

                        if (policyDetailsIceCash.Response != null && !string.IsNullOrEmpty(policyDetailsIceCash.Response.PolicyNo))
                        {
                            lblThankyou.Text += "\n";
                            lblThankyou.Text += "Cover note number =" + policyDetailsIceCash.Response.PolicyNo;

                            lblThankyou.Text += "\n";
                            lblThankyou.Text += "VRN =" + policyDetailsIceCash.Response.VRN;

                            lblThankyou.Text += "\n";
                            lblThankyou.Text += "Status =" + policyDetailsIceCash.Response.Status;
                        }

                        pnlThankyou.Visible = true;
                        pnlconfimpaymeny.Visible = false;



                        // display on thank you page  for testing
                        //List<ResultLicenceIDResponse> list = new List<ResultLicenceIDResponse>();
                        //ResultLicenceIDResponse resresponse = new ResultLicenceIDResponse { vrn = "aas9307", licenceid = "123456789", receiptid = "r123456789", status = "approved", transactionamt = "20.00", arrearsamt = "0", penaltiesamt = "0", administrationamt = "0", totallicamt = "20.00", radiotvamt = "5.00", make = "mazda", model = "mdl0663" };
                        //licenseDiskList.Add(resresponse);
                        //loadLicenceDiskPanel(list);


                        //foreach (var item in objlistRisk)  // for now it's  commented
                        //{

                        //    item.Id = summaryDetails.VehicleId;  

                        //    if (!string.IsNullOrEmpty(item.CombinedID) && (item.CombinedID != "0"))
                        //    {
                        //        btnConfirmPayment.Text = "Approving license..";

                        //        DisplayLicenseDisc(item, parternToken, item.Id);
                        //    }
                        //}


                        var item = objlistRisk[0];
                        item.Id = summaryDetails.VehicleId;
                        //  _vehicleId = summaryDetails.VehicleId;

                        if (!string.IsNullOrEmpty(item.CombinedID) && (item.CombinedID != "0"))
                        {
                            btnConfirmPayment.Text = "Approving license..";
                            DisplayLicenseDisc(item, parternToken, item.Id);
                        }

                        //if (licenseDiskList.Count > 0)
                        //    btnPrint.Visible = true;
                        //else
                        //    btnPrint.Visible = false;

                        // btnPrint.Visible = false;
                        btnConfirmPayment.Text = "Pay";


                    }
                }
                else
                {


                    MyMessageBox.ShowBox("Error occured. " + responseMessage, "Message");
                    TransactionId = GenerateTransactionId();

                    string logMsg1 = txtVrn.Text + "_" + paymentTermName + "_" + transctionAmt;

                    Service_db.WriteIceCashLog("payment fail", logMsg1, "SendSymbol", txtVrn.Text, branchName);

                    //  pnlconfimpaymeny.Visible = false;
                    // pnlErrormessage.Visible = true;
                    //  lblErrMessage.Text = responseMessage;
                    //   lblErrMessage.ForeColor = Color.Red;
                    lblPaymentMsg.Text = "";
                    btnConfirmPayment.Text = "Pay";

                }
                //SendTransaction("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

            }
            catch (Exception ex)
            {
                // WriteLog("InitializeTermianl :" + ex.Message);


                Service_db.WriteIceCashLog("payment exception", ex.Message, "SendSymbol", txtVrn.Text, branchName);
                lblPaymentMsg.Text += "InitializeTermianl: " + ex.Message;

                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                //closing the terminal
                xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
  <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' Action ='CLOSE'/></Esp:Interface>";
                InitializeTermianl("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);
                //btnConfirmPayment.Enabled = true;
                //picImageConfirmPayment.Visible = false;

                SetLoadingDuringPayment(false);
            }
        }



        public bool CheckPosInitOrNot()
        {
            var client = new RestClient(IceCashRequestUrl + "UpdatePosInitilization");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<PosInitModel>(response.Content);

            if (result != null)
                return result.IsActive;
            else
                return false;
        }

        //public PartialPaymentModel SavePartialPayment()
        //{
        //    PartialPaymentModel partialPayment = new PartialPaymentModel();
        //    partialPayment.RegistratonNumber = objlistRisk[0].RegistrationNo;
        //    partialPayment.CustomerEmail = customerInfo.EmailAddress;
        //    partialPayment.PartialAmount = Convert.ToDecimal(txtPartialAmount.Text);
        //    partialPayment.CreatedOn = DateTime.Now;


        //    var client = new RestClient(IceCashRequestUrl + "SavePartailPayment");
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("content-type", "application/json");
        //    request.AddHeader("password", "Geninsure@123");
        //    request.AddHeader("username", "ameyoApi@geneinsure.com");
        //    request.RequestFormat = DataFormat.Json;
        //    request.AddJsonBody(partialPayment);
        //    IRestResponse response = client.Execute(request);

        //    PartialPaymentModel detail = JsonConvert.DeserializeObject<PartialPaymentModel>(response.Content);
        //    return detail;
        //}

        //public static string InitializeTermianl(String hostname, int port, string message)
        public static string InitializeTermianl(String hostname, string port, string message)
        {

            try
            {
                int Port = Convert.ToInt16(port);
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                TcpClient client = new TcpClient(hostname, Port);

                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);

                //first 4 bytes - length!
                writer.Write(Convert.ToByte("0"));
                writer.Write(Convert.ToByte("0"));
                writer.Write(Convert.ToByte("0"));
                writer.Write(Convert.ToByte(data.Length));
                writer.Write(data);

                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                var bytes = stream.Read(data, 0, data.Length);

                responseData = System.Text.Encoding.ASCII.GetString(data, 4, (bytes - 4));
                //Console.WriteLine("Success: " + responseData);
                //Console.ReadKey();
                // Close everything.
                stream.Close();
                client.Close();
                return responseData;
            }
            catch (ArgumentNullException e)
            {
                //Console.WriteLine("ArgumentNullException: " + e.Message);
                // Console.ReadKey();
                return "null";
            }
            catch (SocketException e)
            {
                // Console.WriteLine("SocketException: " + e.Message);
                // Console.ReadKey();
                return "null";
            }
            catch (Exception e)
            {
                ///Console.WriteLine("SocketException: " + e.Message);
                // Console.ReadKey();
                return "null";
            }
        }

        //public string SendTransaction(String hostname, string port, string message, int summaryId)
        public bool SendTransaction(String hostname, string port, string message)
        {
            bool result = false;

            try
            {

                int Port = Convert.ToInt32(port);

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                TcpClient client = new TcpClient(hostname, Port);
                String responseData = String.Empty;
                NetworkStream stream = client.GetStream();
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    //first 4 bytes - length!
                    writer.Write(System.Net.IPAddress.HostToNetworkOrder(0));
                    writer.Write(System.Net.IPAddress.HostToNetworkOrder(0));
                    writer.Write(System.Net.IPAddress.HostToNetworkOrder(0));
                    writer.Write(System.Net.IPAddress.HostToNetworkOrder(data.Length));
                    writer.Write(data);

                    data = new Byte[257 * 3];

                    // String to store the response ASCII representation.
                    var bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                    Service_db.WriteIceCashLog("request", responseData, "SendTransaction");

                    try
                    {
                        if (responseData.Contains("APPROVE"))
                        {
                            result = true;
                            CardDetail = GetMessage(responseData);
                            // TransactionAmount = GetTrasactionAmount(responseData);


                            WriteLog("Status: " + "contain");
                            // string TransactionId =TransactionId;
                        }
                        else
                        {
                            Service_db.WriteIceCashLog("failure", responseData, "SendTransaction");
                            WriteLog("Status: " + "failure");
                        }
                        responseMessage = getmessageresponse(Convert.ToInt32(GetMessageCode(responseData)));
                        lblPaymentMsg.Text = "";

                    }
                    catch (Exception ex)
                    {
                        //  MessageBox.Show("exceptoin :" + ex.ToString());
                        MyMessageBox.ShowBox(ex.Message, "Message");

                        WriteLog(ex.ToString());
                    }
                    finally
                    {
                        // fsReadXml.Close();
                    }
                    //end


                    //   Console.ReadKey();
                    // Close everything.
                }
                stream.Close();
                client.Close();
                return result;
            }
            catch (ArgumentNullException e)
            {
                MyMessageBox.ShowBox(e.Message, "Message");


                // MessageBox.Show(e.Message);
                // Console.WriteLine("ArgumentNullException: " + e.Message);
                //  Console.ReadKey();
                // return result;
            }
            catch (SocketException e)
            {

                MyMessageBox.ShowBox(e.Message, "Message");

                //Console.WriteLine("SocketException: " + e.Message);
                //Console.ReadKey();
                // return result;
            }
            catch (Exception e)
            {

                MyMessageBox.ShowBox(e.Message, "Message");
                //Console.WriteLine("SocketException: " + e.Message);
                //Console.ReadKey();
                // return result;
            }
            finally
            {
                string xmlString1 = @"<?xml version='1.0' encoding='UTF-8'?>
                <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='BIP00001' Action ='CLOSE'/></Esp:Interface>";
                InitializeTermianl("localhost", "", xmlString1);
            }
            return result;
        }


        public void WriteLog(string error)
        {
            //string message = string.Format("Error Time: {0}", DateTime.Now);
            //message += error;
            //message += "-----------------------------------------------------------";

            //message += Environment.NewLine;

            ////string path = System.Web.HttpContext.Current.Server.MapPath("~/LogFile.txt");

            //string path = @"../../LogFile.txt";

            //using (StreamWriter writer = new StreamWriter(path, true))
            //{
            //    writer.WriteLine(message);
            //    writer.Close();
            //}
        }

        public void SavePaymentinformation(string TransactionId, int summaryId, string paymentTermName, string carddetail, string trajectionid, decimal tranjectionamt, string IceCashPolicyNumber)
        {
            PaymentInformationModel objpayinfo = new PaymentInformationModel();
            objpayinfo.TransactionId = TransactionId;
            objpayinfo.SummaryDetailId = summaryId;
            objpayinfo.PaymentId = paymentTermName;
            objpayinfo.CardNumber = carddetail;
            objpayinfo.TerminalId = trajectionid;
            objpayinfo.TransactionAmount = Convert.ToString(tranjectionamt);
            objpayinfo.IceCashPolicyNumber = IceCashPolicyNumber;


            var client = new RestClient(IceCashRequestUrl + "SavePaymentinfo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(objpayinfo);
            IRestResponse response = client.Execute(request);
        }

        public ResultRootObject ApproveVRNToIceCash(int SummaryId = 0)
        {

            ResultRootObject resultPolicy = new ResultRootObject();

            // return resultPolicy;
            try
            {



                //if (ObjToken != null)
                //{
                //    parternToken = ObjToken.Response.PartnerToken;
                //}

                //ObjToken = CheckParterTokenExpire();
                //if (ObjToken != null)
                //    parternToken = ObjToken.Response.PartnerToken;

                RequestToke token = Service_db.GetLatestToken();
                if (token != null)
                    parternToken = token.Token;


                string Phonenumber = txtPhone.Text;
                if (objlistRisk != null)
                {
                    foreach (var item in objlistRisk)
                    {

                        var branchDetial = branchList.FirstOrDefault(c => c.Id == item.ALMBranchId);
                        if(branchDetial!=null)
                            item.Location_Id = branchDetial.Location_Id;


                        if (_insuranceAndLicense) // if insurance and license both need to do approve
                        {
                            if (item.CombinedID != null)
                            {
                                //ResultRootObject quoteresponse = ICEcashService.TPIQuoteUpdate(Phonenumber, item, parternToken, 1);
                                ResultRootObject quoteresponse = ICEcashService.TPILICUpdate(Phonenumber, item, parternToken, 1);


                                int j = 5;
                                while (true)
                                {
                                    j++;
                                    //if token expire
                                    if (quoteresponse.Response != null && quoteresponse.Response.Message.Contains("Partner Token has expired"))
                                    {
                                        //  ObjToken = CheckParterTokenExpire();
                                        ObjToken = IcServiceobj.getToken();
                                        if (ObjToken != null)
                                            parternToken = ObjToken.Response.PartnerToken;

                                        Service_db.UpdateToken(ObjToken);
                                        quoteresponse = ICEcashService.TPILICUpdate(Phonenumber, item, parternToken, 1);
                                    }

                                    if (!quoteresponse.Response.Message.Contains("Partner Token has expired"))
                                        break;
                                }


                                ResultLicenceIDRootObject resultPolicyResponse = ICEcashService.TPILICResult(item, parternToken);


                                int i = 5;
                                while (true)
                                {
                                    i++;
                                    //if token expire
                                    if (resultPolicyResponse.Response != null && resultPolicyResponse.Response.Message.Contains("Partner Token has expired"))
                                    {
                                        // ObjToken = CheckParterTokenExpire();
                                        ObjToken = IcServiceobj.getToken();
                                        if (ObjToken != null)
                                            parternToken = ObjToken.Response.PartnerToken;

                                        Service_db.UpdateToken(ObjToken);
                                        resultPolicyResponse = ICEcashService.TPILICResult(item, parternToken);

                                    }

                                    if (!resultPolicyResponse.Response.Message.Contains("Partner Token has expired"))
                                        break;
                                }


                                if (resultPolicyResponse.Response != null && resultPolicyResponse.Response.Message.Contains("Policy Retrieved"))
                                {
                                    VehicleUpdateModel objVehicleUpdate = new VehicleUpdateModel();
                                    objVehicleUpdate.VRN = item.RegistrationNo;
                                    objVehicleUpdate.InsuranceStatus = "Approved";
                                    objVehicleUpdate.CoverNote = resultPolicyResponse.Response.PolicyNo;
                                    objVehicleUpdate.SummaryId = Convert.ToString(SummaryId);
                                    UpdateVehicleInfo(objVehicleUpdate);

                                    resultPolicy = new ResultRootObject();
                                    resultPolicy.Response = new ResultResponse();

                                    resultPolicy.Response.PolicyNo = resultPolicyResponse.Response.PolicyNo;
                                    resultPolicy.Response.VRN = resultPolicyResponse.Response.VRN;
                                    resultPolicy.Response.Status = resultPolicyResponse.Response.Status;

                                }
                                //}

                            }

                        }
                        else
                        {
                            // insurance only
                            ResultRootObject quoteresponse = ICEcashService.TPIQuoteUpdate(Phonenumber, item, parternToken, 1);

                            int j = 5;
                            while (true)
                            {
                                j++;
                                //if token expire

                                if (quoteresponse.Response != null && quoteresponse.Response.Message.Contains("Partner Token has expired"))
                                {
                                    ObjToken = IcServiceobj.getToken();
                                    if (ObjToken != null)
                                        parternToken = ObjToken.Response.PartnerToken;

                                    Service_db.UpdateToken(ObjToken);
                                    quoteresponse = ICEcashService.TPIQuoteUpdate(Phonenumber, item, parternToken, 1);
                                }


                                if (!quoteresponse.Response.Message.Contains("Partner Token has expired"))
                                    break;
                            }








                            resultPolicy = ICEcashService.TPIPolicy(item, parternToken);


                            int i = 5;
                            while (true)
                            {
                                i++;
                                //if token expire
                                if (resultPolicy.Response != null && resultPolicy.Response.Message.Contains("Partner Token has expired"))
                                {

                                    // ObjToken = CheckParterTokenExpire();
                                    ObjToken = IcServiceobj.getToken();
                                    if (ObjToken != null)
                                        parternToken = ObjToken.Response.PartnerToken;

                                    Service_db.UpdateToken(ObjToken);
                                    resultPolicy = ICEcashService.TPIPolicy(item, parternToken);
                                }

                                if (!resultPolicy.Response.Message.Contains("Partner Token has expired"))
                                    break;
                            }


                            if (resultPolicy.Response != null)
                            {

                                VehicleUpdateModel objVehicleUpdate = new VehicleUpdateModel();
                                objVehicleUpdate.VRN = item.RegistrationNo;
                                objVehicleUpdate.InsuranceStatus = "Approved";
                                objVehicleUpdate.CoverNote = resultPolicy.Response.PolicyNo;
                                objVehicleUpdate.SummaryId = Convert.ToString(SummaryId);
                                UpdateVehicleInfo(objVehicleUpdate);

                            }




                        }





                        //if (item.LicenseId != null)
                        //{
                        //    _clientIdType = "1";
                        //    if (rdCorporate.Checked)
                        //        _clientIdType = "2";


                        //    if (item.RadioLicenseCost > 0 || item.VehicleLicenceFee > 0)
                        //    {

                        //        int licenseFreequency = IcServiceobj.GetMonthKey(Convert.ToInt32(item.PaymentTermId));

                        //        int RadioTVUsage = 1; // for private car

                        //        if (item.ProductId == 0)
                        //        {
                        //            RadioTVUsage = 1;
                        //        }
                        //        else if (item.ProductId == 3 || item.ProductId == 11) // for commercial vehicle
                        //        {
                        //            RadioTVUsage = 2;
                        //        }


                        //        List<VehicleLicQuote> obj = new List<VehicleLicQuote>();
                        //        obj.Add(new VehicleLicQuote
                        //        {
                        //            VRN = txtVrn.Text,
                        //            IDNumber = customerInfo.NationalIdentificationNumber,
                        //            ClientIDType = _clientIdType,
                        //            LicFrequency = licenseFreequency.ToString(),
                        //            RadioTVUsage = RadioTVUsage.ToString(),
                        //            RadioTVFrequency = licenseFreequency.ToString()
                        //        });



                        //        ResultRootObject quoteresponse = new ResultRootObject();
                        //        if (item.VehicleLicenceFee > 0 && item.RadioLicenseCost > 0)
                        //        {
                        //         quoteresponse = IcServiceobj.LICQuote(obj, parternToken);

                        //            if (quoteresponse != null && quoteresponse.Response.Message.Contains("Partner Token has expired"))
                        //            {
                        //                // ObjToken = IcServiceobj.getToken();

                        //                //  ObjToken = CheckParterTokenExpire();
                        //                ObjToken = IcServiceobj.getToken();
                        //                if (ObjToken != null)
                        //                    parternToken = ObjToken.Response.PartnerToken;

                        //                Service_db.UpdateToken(ObjToken);

                        //                quoteresponse = IcServiceobj.LICQuote(obj, parternToken);
                        //            }
                        //        }
                        //        else if (item.VehicleLicenceFee > 0)
                        //        {
                        //          quoteresponse = IcServiceobj.RadioQuote(obj, parternToken);

                        //            if (quoteresponse != null && quoteresponse.Response.Message.Contains("Partner Token has expired"))
                        //            {
                        //                //  ObjToken = IcServiceobj.getToken();

                        //                //  ObjToken = CheckParterTokenExpire();
                        //                ObjToken = IcServiceobj.getToken();

                        //                if (ObjToken != null)
                        //                    parternToken = ObjToken.Response.PartnerToken;

                        //                Service_db.UpdateToken(ObjToken);

                        //                quoteresponse = IcServiceobj.RadioQuote(obj, parternToken);
                        //            }
                        //        }

                        //        else if (item.RadioLicenseCost > 0)
                        //        {
                        //            quoteresponse = IcServiceobj.LICQuote(obj, parternToken);

                        //            if (quoteresponse != null && quoteresponse.Response.Message.Contains("Partner Token has expired"))
                        //            {
                        //                // ObjToken = IcServiceobj.getToken();

                        //                //  ObjToken = CheckParterTokenExpire();
                        //                ObjToken = IcServiceobj.getToken();
                        //                if (ObjToken != null)
                        //                    parternToken = ObjToken.Response.PartnerToken;

                        //                Service_db.UpdateToken(ObjToken);

                        //                quoteresponse = IcServiceobj.LICQuote(obj, parternToken);
                        //            }
                        //        }

                        //        // int licenseId = 0;
                        //        if (quoteresponse.Response != null && quoteresponse.Response.Quotes != null)
                        //        {
                        //            item.LicenseId = quoteresponse.Response.Quotes[0].LicenceID;
                        //            if (quoteresponse.Response.Quotes != null && !(string.IsNullOrEmpty(quoteresponse.Response.Quotes[0].LicenceID)))
                        //            {
                        //                _licenseId = quoteresponse.Response.Quotes[0].LicenceID;
                        //            }
                        //            VehicleUpdateModel objVehicleUpdate = new VehicleUpdateModel();
                        //            objVehicleUpdate.VRN = item.RegistrationNo;
                        //            objVehicleUpdate.SummaryId = Convert.ToString(SummaryId);
                        //            objVehicleUpdate.LicenseId = Convert.ToInt32(_licenseId);
                        //            UpdateVehicleInfo(objVehicleUpdate);
                        //        }


                        //        List<VehicleLicQuoteUpdate> vehicleLicenseList = new List<VehicleLicQuoteUpdate>();

                        //        //PaymentMethod =1 for cash 
                        //        //PaymentMethod =3 for icecash

                        //        // VehicleLicQuoteUpdate vehicleLic = new VehicleLicQuoteUpdate { LicenceID = Convert.ToInt32(item.LicenseId), PaymentMethod = 1, DeliveryMethod = 3, Status = "1", LicenceCert = 1 };
                        //        VehicleLicQuoteUpdate vehicleLic = new VehicleLicQuoteUpdate { LicenceID = Convert.ToInt32(_licenseId), PaymentMethod = 1, DeliveryMethod = 3, Status = "1", LicenceCert = 1 };
                        //        vehicleLicenseList.Add(vehicleLic);

                        //        ResultRootObject quoteresponseNew = IcServiceobj.LICQuoteUpdate(vehicleLicenseList, parternToken);

                        //        if (quoteresponseNew != null && quoteresponseNew.Response.Message.Contains("Partner Token has expired"))
                        //        {

                        //            // ObjToken = CheckParterTokenExpire();

                        //            ObjToken = IcServiceobj.getToken();

                        //            if (ObjToken != null)
                        //                parternToken = ObjToken.Response.PartnerToken;

                        //            Service_db.UpdateToken(ObjToken);

                        //            quoteresponseNew = IcServiceobj.LICQuoteUpdate(vehicleLicenseList, parternToken);
                        //            //ObjToken = IcServiceobj.getToken();
                        //            //if (ObjToken != null)
                        //            //{
                        //            //    parternToken = ObjToken.Response.PartnerToken;
                        //            //    quoteresponseNew = IcServiceobj.LICQuoteUpdate(vehicleLicenseList, ObjToken.Response.PartnerToken);
                        //            //}
                        //        }
                        //    }
                        //}
                    }
                }


            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox("Error occur during approve into IceCash.", "Message");

                Service_db.WriteIceCashLog("ApproveIceCash ", ex.Message, "approvevrnintoIcecah", txtVrn.Text, branchName);

            }

            return resultPolicy;
        }


        public void UpdateVehicleInfo(VehicleUpdateModel objVehicleUpdate)
        {

            var client = new RestClient(IceCashRequestUrl + "InsuranceStatus");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(objVehicleUpdate);
            IRestResponse response = client.Execute(request);
        }

        private void timerMessage_Tick(object sender, EventArgs e)
        {
            //Thread.Sleep(5000);
            //timerMessage.Stop();
            //pnlThankyou.Visible = false;

            //Form1 obj = new Form1();
            //obj.Show();
            //this.Hide();
        }

        private void pnlThankyou_Paint(object sender, PaintEventArgs e)
        {
            timerMessage.Enabled = true;
            timerMessage.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlAddMoreVehicle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlRadioZinara_Paint(object sender, PaintEventArgs e)
        {

        }

        public void callPnlSummary()
        {
            try
            {
                if (chkRadioLicence.Checked)
                {
                    if (!chkZinara.Checked)
                    {
                        //lblZinraErrMsg.Text = "System cannot process Radio Only.";
                        //lblZinraErrMsg.ForeColor = Color.Red;
                        MyMessageBox.ShowBox("System cannot process Radio Only.", "Message");
                        return;
                    }
                }


                if (VehicalIndex == -1)
                {
                    if (chkRadioLicence.Checked)
                    {
                        objRiskModel.RadioLicenseCost = txtradioAmount.Text == "" ? 0 : Convert.ToDecimal(txtradioAmount.Text);
                        objRiskModel.IncludeRadioLicenseCost = true;
                    }
                    else
                    {
                        objRiskModel.RadioLicenseCost = 0;
                        objRiskModel.IncludeRadioLicenseCost = false;
                    }


                    if (chkZinara.Checked)
                        objRiskModel.VehicleLicenceFee = txtZinTotalAmount.Text == "" ? 0 : Convert.ToDecimal(txtZinTotalAmount.Text);
                    else
                        objRiskModel.VehicleLicenceFee = 0;
                }
                else
                {
                    if (chkRadioLicence.Checked)
                    {
                        objlistRisk[VehicalIndex].RadioLicenseCost = txtradioAmount.Text == "" ? 0 : Convert.ToDecimal(txtradioAmount.Text);
                        objRiskModel.IncludeRadioLicenseCost = true;
                    }
                    else
                    {
                        objlistRisk[VehicalIndex].RadioLicenseCost = 0;
                        objRiskModel.IncludeRadioLicenseCost = false;
                    }


                    if (chkZinara.Checked)
                        objlistRisk[VehicalIndex].VehicleLicenceFee = txtZinTotalAmount.Text == "" ? 0 : Convert.ToDecimal(txtZinTotalAmount.Text);
                    else
                        objlistRisk[VehicalIndex].VehicleLicenceFee = 0;
                }

                if (txtpenalty.Text != "" && Convert.ToDecimal(txtpenalty.Text) > 0)  // if penality amount
                {
                    objRiskModel.VehicleLicenceFee = 0;
                    objRiskModel.RadioLicenseCost = 0;
                    objRiskModel.IncludeRadioLicenseCost = false;
                }



                pnlSum.Visible = true;
                //  pnlAddMoreVehicle.Visible = true; comment for now while multiple vehilce will not work

                pnlAddMoreVehicle.Visible = false;



                pnlRadioZinara.Visible = false;
                pnlZinara.Visible = false;
                pnlRadio.Visible = false;
                //CalculatePremium();


                var productid = objRiskModel.ProductId;

                // add vehical list

                //if (isbackclicked == false) // 21_feb
                //{
                //    CalculatePremium();
                //}


                CalculatePremium();

                //VehicalIndex = -1; // to do uncomment when it will be for multiple vehicle
                if (VehicalIndex != -1)
                {
                    //Update vehical list
                    SetValueForUpdate();
                    loadVRNPanel(); // 19_feb
                    VehicalIndex = -1;
                }
                else
                {

                    objRiskModel.NoOfCarsCovered = objlistRisk.Count() + 1;
                    objlistRisk.Add(objRiskModel);


                    //if (isbackclicked == false) // 21_feb
                    //{
                    //    // add vehical list
                    //    objRiskModel.NoOfCarsCovered = objlistRisk.Count() + 1;
                    //    objlistRisk.Add(objRiskModel);
                    //}

                    //    //if (isbackclicked ==true)
                    //    //{
                    //    //    if (objlistRisk.Count== 0)
                    //    //    {
                    //    //        objRiskModel.NoOfCarsCovered = objlistRisk.Count() + 1;
                    //    //        objlistRisk.Add(objRiskModel);
                    //    //    }
                    //    //}


                    //}
                    isbackclicked = false;
                    loadVRNPanel();

                }

                //public void checkdummydata()
                //{

                //    XmlDocument myxml = new XmlDocument();
                //    DataSet ds = new DataSet();
                //    System.IO.FileStream fsReadXml = new System.IO.FileStream
                //        (responseData, System.IO.FileMode.Open);
                //    try
                //    {
                //        ds.ReadXml(fsReadXml);
                //        if (ds != null)
                //        {
                //            if (ds.Tables[1].Rows.Count > 0)
                //            {
                //                string Status = Convert.ToString(ds.Tables[1].Rows[0]["ActionCode"]);
                //                if (Status == "APPROVE")
                //                {
                //                    WriteLog("Status: " + "APPROVE");
                //                    string TransactionId = Convert.ToString(ds.Tables[1].Rows[0]["TransactionId"]);          
                //                }
                //                else
                //                {
                //                    lblpayment.Text = "failure";
                //                }
                //            }
                //        }

                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.ToString());
                //    }
                //    finally
                //    {
                //        fsReadXml.Close();
                //    }

                //}

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);

                MyMessageBox.ShowBox(ex.Message, "Message");
            }

        }


        private void OptNext_Click(object sender, EventArgs e)
        {

            //if (!chkRadioLicence.Checked && !chkZinara.Checked)
            //{
            //    MessageBox.Show("Please select the (RadioLicence/Zinara) type");
            //    return;
            //}

            try
            {
                if (chkRadioLicence.Checked)
                {
                    if (!chkZinara.Checked)
                    {
                        //lblZinraErrMsg.Text = "System cannot process Radio Only.";
                        //lblZinraErrMsg.ForeColor = Color.Red;
                        MyMessageBox.ShowBox("System cannot process Radio Only.", "Message");
                        return;
                    }
                }


                if (chkRadioLicence.Checked)
                {
                    if (RadioPaymnetTerm.SelectedIndex == 0)
                    {
                        MyMessageBox.ShowBox("Please select payment term for radio license because you have checked radio chekbox othewise please uncheck radio chekbox.", "Message");
                        return;
                    }
                }

                if (chkZinara.Checked)
                {
                    if (ZinPaymentDetail.SelectedIndex == 0)
                    {
                        MyMessageBox.ShowBox("Please checked checkbox of zinara license because you have selected zinara payment term otherwise unselect payment term.", "Message");
                        return;
                    }
                }

                if (chkRadioLicence.Checked)
                {
                    if (RadioPaymnetTerm.SelectedIndex > 0)
                    {
                        if (!IsPaymentTermValidForInsuranceLicense(Convert.ToInt32(cmbPaymentTerm.SelectedValue), Convert.ToInt32(RadioPaymnetTerm.SelectedValue)))
                        {
                            MyMessageBox.ShowBox("Licence payment term should be equal or less than Insurance payment term.", "Message");
                            return;
                        }
                    }
                }


                if (chkZinara.Checked)
                {
                    if (ZinPaymentDetail.SelectedIndex > 0)
                    {
                        if (!IsPaymentTermValidForInsuranceLicense(Convert.ToInt32(cmbPaymentTerm.SelectedValue), Convert.ToInt32(ZinPaymentDetail.SelectedValue)))
                        {
                            MyMessageBox.ShowBox("Licence payment term should be equal or less than Insurance payment term.", "Message");
                            return;
                        }
                    }
                }


                OptNext.Text = "Processing..";

                loadingInsuraneImg.Visible = true;
                loadingInsuraneImg.WaitOnLoad = true;
                OptNext.Enabled = false;

                if (txtVrn.Text.Trim().ToUpper() != _tba)
                {
                    RequestVehicleDetails();
                }


                if (resObject.Message.Contains("1 failed"))
                    _iceCashErrorMsg = resObject.Quotes == null ? "Error Occured" : resObject.Quotes[0].Message;

                if (_iceCashErrorMsg != "")
                {
                    MyMessageBox.ShowBox(_iceCashErrorMsg);

                    if (_iceCashErrorMsg.Contains("Your account is inactive"))
                    {
                        GotoHome();
                        return;
                    }

                    loadingInsuraneImg.Visible = false;
                    OptNext.Enabled = true;
                    OptNext.Text = "Continue";

                    //pnlRadioZinara.Visible = false;
                    // PnlVrn.Visible = true;

                    // return;
                }




                //if (VehicalIndex == -1)
                //{
                //    if (chkRadioLicence.Checked)
                //    {
                //        objRiskModel.RadioLicenseCost = txtradioAmount.Text == "" ? 0 : Convert.ToDecimal(txtradioAmount.Text);
                //        objRiskModel.IncludeRadioLicenseCost = true;
                //    }
                //    else
                //    {
                //        objRiskModel.RadioLicenseCost = 0;
                //        objRiskModel.IncludeRadioLicenseCost = false;
                //    }


                //    if (chkZinara.Checked)
                //        objRiskModel.VehicleLicenceFee = txtZinTotalAmount.Text == "" ? 0 : Convert.ToDecimal(txtZinTotalAmount.Text);
                //    else
                //        objRiskModel.VehicleLicenceFee = 0;
                //}
                //else
                //{
                //    if (chkRadioLicence.Checked)
                //    {
                //        objlistRisk[VehicalIndex].RadioLicenseCost = txtradioAmount.Text == "" ? 0 : Convert.ToDecimal(txtradioAmount.Text);
                //        objRiskModel.IncludeRadioLicenseCost = true;
                //    }
                //    else
                //    {
                //        objlistRisk[VehicalIndex].RadioLicenseCost = 0;
                //        objRiskModel.IncludeRadioLicenseCost = false;
                //    }


                //    if (chkZinara.Checked)
                //        objlistRisk[VehicalIndex].VehicleLicenceFee = txtZinTotalAmount.Text == "" ? 0 : Convert.ToDecimal(txtZinTotalAmount.Text);
                //    else
                //        objlistRisk[VehicalIndex].VehicleLicenceFee = 0;
                //}

                //if (txtpenalty.Text != "" && Convert.ToDecimal(txtpenalty.Text) > 0)  // if penality amount
                //{
                //    objRiskModel.VehicleLicenceFee = 0;
                //    objRiskModel.RadioLicenseCost = 0;
                //    objRiskModel.IncludeRadioLicenseCost = false;
                //}





                //if (_iceCashErrorMsg != "")
                //{
                //    string errMsg = _iceCashErrorMsg + " You can also contact from Customer Service Centre (08677223344).";

                //    MyMessageBox.ShowBox(errMsg, "Message");
                //    btnInsCnt.Text = "Continue";
                //    GoToVrnScreen();
                //    return;
                //}


                //  pnlSum.Visible = true;
                //  pnlAddMoreVehicle.Visible = true; comment for now while multiple vehilce will not work



                pnlRiskDetails.Visible = true;
                pnlAddMoreVehicle.Visible = false;
                pnlRadioZinara.Visible = false;
                pnlZinara.Visible = false;
                pnlRadio.Visible = false;
                //CalculatePremium();


                var productid = objRiskModel.ProductId;


                loadingInsuraneImg.Visible = false;
                OptNext.Enabled = true;
                OptNext.Text = "Continue";

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);


                loadingInsuraneImg.Visible = false;
                OptNext.Enabled = true;
                OptNext.Text = "Continue";

                MyMessageBox.ShowBox(ex.Message, "Message");
            }


        }


        public bool CheckRadioAndZinara()
        {

            bool result = true;

            if (chkRadioLicence.Checked)
            {
                if (!chkZinara.Checked)
                {
                    //lblZinraErrMsg.Text = "System cannot process Radio Only.";
                    //lblZinraErrMsg.ForeColor = Color.Red;
                    MyMessageBox.ShowBox("System cannot process Radio Only.", "Message");
                    result = false;
                }
            }


            if (chkRadioLicence.Checked)
            {
                if (RadioPaymnetTerm.SelectedIndex == 0)
                {
                    MyMessageBox.ShowBox("Please select payment term for radio license because you have checked radio chekbox othewise please uncheck radio chekbox.", "Message");
                    result = false;
                }
            }

            if (chkZinara.Checked)
            {
                if (ZinPaymentDetail.SelectedIndex == 0)
                {
                    MyMessageBox.ShowBox("Please checked checkbox of zinara license because you have selected zinara payment term otherwise unselect payment term.", "Message");
                    result = false;
                }
            }

            if (chkRadioLicence.Checked)
            {
                if (RadioPaymnetTerm.SelectedIndex > 0)
                {
                    if (!IsPaymentTermValidForInsuranceLicense(Convert.ToInt32(cmbPaymentTerm.SelectedValue), Convert.ToInt32(RadioPaymnetTerm.SelectedValue)))
                    {
                        MyMessageBox.ShowBox("Licence payment term should be equal or less than Insurance payment term.", "Message");
                        result = false;
                    }
                }
            }


            if (chkZinara.Checked)
            {
                if (ZinPaymentDetail.SelectedIndex > 0)
                {
                    if (!IsPaymentTermValidForInsuranceLicense(Convert.ToInt32(cmbPaymentTerm.SelectedValue), Convert.ToInt32(ZinPaymentDetail.SelectedValue)))
                    {
                        MyMessageBox.ShowBox("Licence payment term should be equal or less than Insurance payment term.", "Message");
                        result = false;
                    }
                }
            }

            return result;
        }

        public bool IsPaymentTermValidForInsuranceLicense(int insurancePaymentTerm, int licesnePaymentTerm)
        {
            bool result = true;

            if (insurancePaymentTerm == 1)
                insurancePaymentTerm = 12;

            if (licesnePaymentTerm == 1)
                licesnePaymentTerm = 12;

            if (insurancePaymentTerm != licesnePaymentTerm)
            {
                if (licesnePaymentTerm > insurancePaymentTerm)
                {
                    result = false;
                }
            }
            return result;
        }

        private void optBack_Click(object sender, EventArgs e)
        {

            //pnlOptionalCover.Visible = true;
            // pnlConfirm.Visible = true;
            pnlInsurance.Visible = true;
            pnlRadioZinara.Visible = false;
            //pnlRadio.Visible = false;
            //pnlZinara.Visible = false;

            //chkZinara.Checked = false;
            //chkRadioLicence.Checked = false;

            //btnAddMoreVehicle.Visible = true;
            //pnlConfirm.Visible = true;
            //pnlOptionalCover.Visible = false;
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == txtVrn.Text);

        }

        private void RadiobtnRadioLicence_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRadioLicence.Checked)
            {

                pnlRadio.Visible = true;
                pnlZinara.Visible = false;
                objRiskModel.IncludeRadioLicenseCost = true;

                //var RadioLicenceAmounts = 0;
                //if (VehicalIndex == -1)
                //{
                //    //this.Invoke(new Action(() => RadioLicenceAmounts = bindradioamout(objRiskModel.ProductId, objRiskModel.PaymentTermId)));
                //    objRiskModel.RadioLicenseCost = RadioLicenceAmounts;
                //    txtradioAmount.Text = Convert.ToString(objRiskModel.RadioLicenseCost);
                //    objRiskModel.IncludeRadioLicenseCost = true;
                //}
                //else
                //{
                //    //this.Invoke(new Action(() => RadioLicenceAmounts = bindradioamout(objRiskModel.ProductId, objRiskModel.PaymentTermId)));
                //    objlistRisk[VehicalIndex].RadioLicenseCost = RadioLicenceAmounts;
                //    txtradioAmount.Text = Convert.ToString(objlistRisk[VehicalIndex].RadioLicenseCost);
                //    objlistRisk[VehicalIndex].IncludeRadioLicenseCost = true;
                //}
                // objRiskModel.IncludeZineraCost = false;

            }
            else
            {
                pnlRadio.Visible = false;
            }
        }

        private void RadiobtnZinara_CheckedChanged(object sender, EventArgs e)
        {
            if (chkZinara.Checked)
            {
                pnlZinara.Visible = true;
                pnlRadio.Visible = false;
                if (txtAccessAmount.Text != "" && txtpenalty.Text != "")
                {
                    if (VehicalIndex == -1)
                    {
                        var amount = txtAccessAmount.Text;
                        var amount1 = txtpenalty.Text;
                        var totalamouny = Convert.ToInt16(amount) + Convert.ToInt16(amount1);
                        txtZinTotalAmount.Text = Convert.ToString(totalamouny);
                    }
                    else
                    {
                        var amount = txtAccessAmount.Text;
                        var amount1 = txtpenalty.Text;
                        var totalamouny = Convert.ToInt16(amount) + Convert.ToInt16(amount1);
                        txtZinTotalAmount.Text = Convert.ToString(totalamouny);
                    }
                }
                //objRiskModel.IncludeRadioLicenseCost = false;
            }
        }


        private void GetZinraLiceenseFee(string paymentTerm)
        {
            try
            {
                if (resObject.Quotes != null && resObject.Quotes[0].Message == "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                {

                    return;
                }

                if (_iceCashErrorMsg != "")
                {
                    return;
                }

                //if (resObject != null && resObject.Message.Contains("Partner Token has expired"))
                //{

                //    //  ObjToken = CheckParterTokenExpire();

                //    ObjToken = IcServiceobj.getToken();
                //    if (ObjToken != null)
                //        parternToken = ObjToken.Response.PartnerToken;

                //    Service_db.UpdateToken(ObjToken);

                //    //ObjToken = IcServiceobj.getToken();
                //    //if (ObjToken != null)
                //    //{
                //    //    parternToken = ObjToken.Response.PartnerToken;
                //    //    quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(objRiskModel, customerInfo, parternToken);
                //    //    resObject = quoteresponse.Response;
                //    //}
                //}


                if (rdCorporate.Checked)
                    _clientIdType = "2";
                else
                    _clientIdType = "1";

                //  var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber, paymentTerm, cmbProducts.SelectedValue.ToString());
                var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, _clientIdType, paymentTerm, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo);
                var _resObjects = _quoteresponse.Response;



                //if token expire
                if (_resObjects != null && _resObjects.Message.Contains("Partner Token has expired"))
                {

                    //   ObjToken = CheckParterTokenExpire();

                    ObjToken = IcServiceobj.getToken();

                    if (ObjToken != null)
                        parternToken = ObjToken.Response.PartnerToken;

                    Service_db.UpdateToken(ObjToken);

                    _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, _clientIdType, paymentTerm, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo);
                    _resObjects = _quoteresponse.Response;




                    //ObjToken = IcServiceobj.getToken();
                    //if (ObjToken != null)
                    //{
                    //    parternToken = ObjToken.Response.PartnerToken;
                    //    //  quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 
                    //    _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber, paymentTerm, cmbProducts.SelectedValue.ToString());
                    //    _resObjects = _quoteresponse.Response;
                    //}
                }


                if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                {
                    //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
                    //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
                    txtAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt);
                    txtpenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt);
                    txtZinTotalAmount.Text = Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt + _resObjects.Quotes[0].PenaltiesAmt).ToString();


                    //   txtradioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt);

                    if (_resObjects.Quotes[0].PenaltiesAmt > 0)
                    {
                        //lblZinraErrMsg.Text = "You have outstanding penalties, please contact our Contact Centre for assistance on 086 77 22 33 44.";

                        //lblZinraErrMsg.Font = new Font("Arial", 14, FontStyle.Underline, GraphicsUnit.Point);

                        //lblZinraErrMsg.ForeColor = Color.Red;

                        MyMessageBox.ShowBox("You have outstanding penalties, please contact our Contact Centre for assistance on 086 77 22 33 44.", "Message");


                    }

                    if (_resObjects != null && _resObjects.Quotes != null)
                    {
                        if (VehicalIndex == -1)
                            objRiskModel.LicenseId = _resObjects.Quotes[0].LicenceID;
                        else
                            objlistRisk[VehicalIndex].LicenseId = _resObjects.Quotes[0].LicenceID;
                    }


                }


            }
            catch (Exception ex)
            {

            }
        }


        private void GetRadioLiceenseFee(string paymentTerm)
        {
            try
            {
                if (resObject.Quotes != null && resObject.Quotes[0].Message == "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                {
                    return;
                }

                if (_iceCashErrorMsg != "")
                {
                    return;
                }

                var requestToken = Service_db.GetLatestToken();
                if (requestToken != null)
                {
                    parternToken = requestToken.Token;
                }


                //if (resObject != null && resObject.Message.Contains("Partner Token has expired"))
                //{

                //    // ObjToken = CheckParterTokenExpire();
                //    ObjToken = IcServiceobj.getToken();

                //    if (ObjToken != null)
                //        parternToken = ObjToken.Response.PartnerToken;

                //    Service_db.UpdateToken(ObjToken);


                //    //ObjToken = IcServiceobj.getToken();
                //    //if (ObjToken != null)
                //    //{
                //    //    parternToken = ObjToken.Response.PartnerToken;
                //    //    quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(objRiskModel, customerInfo, parternToken);
                //    //    resObject = quoteresponse.Response;
                //    //}
                //}

                if (rdCorporate.Checked)
                    _clientIdType = "2";
                else
                    _clientIdType = "1";


                var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, _clientIdType, paymentTerm, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo);
                var _resObjects = _quoteresponse.Response;



                //if token expire
                if (_resObjects != null && _resObjects.Message.Contains("Partner Token has expired"))
                {

                    //  ObjToken = CheckParterTokenExpire();
                    ObjToken = IcServiceobj.getToken();
                    if (ObjToken != null)
                        parternToken = ObjToken.Response.PartnerToken;

                    Service_db.UpdateToken(ObjToken);


                    _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, _clientIdType, paymentTerm, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo);
                    _resObjects = _quoteresponse.Response;




                    //ObjToken = IcServiceobj.getToken();
                    //if (ObjToken != null)
                    //{
                    //    parternToken = ObjToken.Response.PartnerToken;
                    //    //  quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 
                    //    _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber, paymentTerm, cmbProducts.SelectedValue.ToString());
                    //    _resObjects = _quoteresponse.Response;
                    //}
                }


                if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                {
                    //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
                    //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;

                    txtradioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt);


                    if (VehicalIndex == -1)
                        objRiskModel.LicenseId = _resObjects.Quotes[0].LicenceID;
                    else
                        objlistRisk[VehicalIndex].LicenseId = _resObjects.Quotes[0].LicenceID;
                }

            }
            catch (Exception ex)
            {

            }
        }


        private void GetDefaultZinraLiceenseFee(string paymentTerm, string _clientIdType, string IDNumber)
        {

            try
            {
                //_clientIdType = "1";
                //if (rdCorporate.Checked)
                //    _clientIdType = "2";

                // IDNumber = customerInfo.NationalIdentificationNumber;


                if (resObject.Quotes != null && resObject.Quotes[0].Message == "Unable to retrieve vehicle info from Zimlic, please check the VRN is correct or try again later.")
                {
                    return;
                }


                if (_iceCashErrorMsg != "")
                {
                    return;
                }

                var requestToken = Service_db.GetLatestToken();
                if (requestToken != null)
                {
                    parternToken = requestToken.Token;
                }

                //if (resObject != null && resObject.Message.Contains("Partner Token has expired"))
                //{
                //    ObjToken = IcServiceobj.getToken();
                //    if (ObjToken != null)
                //        parternToken = ObjToken.Response.PartnerToken;

                //    Service_db.UpdateToken(ObjToken);

                //    //quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(objRiskModel, customerInfo, parternToken);
                //    //resObject = quoteresponse.Response;

                //    //ObjToken = IcServiceobj.getToken();
                //    //if (ObjToken != null)
                //    //{
                //    //    parternToken = ObjToken.Response.PartnerToken;
                //    //    quoteresponse = IcServiceobj.checkVehicleExistsWithVRN(objRiskModel, customerInfo, parternToken);
                //    //    resObject = quoteresponse.Response;
                //    //}
                //}



                // var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber, paymentTerm, cmbProducts.SelectedValue.ToString());
                var _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, _clientIdType, paymentTerm, cmbProducts.SelectedValue.ToString(), IDNumber, customerInfo);
                var _resObjects = _quoteresponse.Response;


                if (_resObjects != null && _resObjects.Message.Contains("1 failed"))
                {
                    //lblZnrErrMsg.Text = _resObjects.Quotes[0].Message;
                    //lblZnrErrMsg.ForeColor = Color.Red;


                    // MyMessageBox.ShowBox(_resObjects.Quotes[0].Message, "Error Message"); // need to do uncomment

                    if (_resObjects.Quotes != null && _resObjects.Quotes[0].Message.Contains("8: Licensing is only allowed 2 months prior"))
                    {
                        var message = _resObjects.Quotes[0].Message + " However if you wish to acquire the Insurance only please continue.";
                        MyMessageBox.ShowBox(message, "Message"); // need to do uncomment
                        prior = 1;
                    }
                    else if (_resObjects.Quotes != null)
                    {
                        var message = _resObjects.Quotes[0].Message + " please contact our Contact Centre for assistance on 086 77 22 33 44.";
                        MyMessageBox.ShowBox(message, "Message"); // need to do uncomment
                        prior = 1;
                    }


                }


                //if token expire
                if (_resObjects != null && _resObjects.Message.Contains("Partner Token has expired"))
                {
                    //  ObjToken = CheckParterTokenExpire();
                    ObjToken = IcServiceobj.getToken();
                    if (ObjToken != null)
                        parternToken = ObjToken.Response.PartnerToken;

                    Service_db.UpdateToken(ObjToken);

                    _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, _clientIdType, paymentTerm, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo);

                    _resObjects = _quoteresponse.Response;
                    //ObjToken = IcServiceobj.getToken();
                    //if (ObjToken != null)
                    //{
                    //    parternToken = ObjToken.Response.PartnerToken;
                    //    //  quoteresponse = IcServiceobj.RequestQuote(parternToken, RegistrationNo, suminsured, make, model, PaymentTermId, VehicleYear, CoverTypeId, VehicleUsage, "", (CustomerModel)customerInfo); // uncomment this line 
                    //    _quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, resObject.Quotes[0].Client.IDNumber, paymentTerm, cmbProducts.SelectedValue.ToString());

                    //    _resObjects = _quoteresponse.Response;
                    //}
                }


                if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                {
                    //objRiskModel.TotalLicAmount =Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt);
                    //objRiskModel.PenaltiesAmount = _resObjects.Quotes[0].PenaltiesAmt;
                    txtAccessAmount.Text = Convert.ToString(_resObjects.Quotes[0].TotalLicAmt);
                    txtpenalty.Text = Convert.ToString(_resObjects.Quotes[0].PenaltiesAmt);
                    txtZinTotalAmount.Text = Convert.ToDecimal(_resObjects.Quotes[0].TotalLicAmt + _resObjects.Quotes[0].PenaltiesAmt).ToString();


                    txtradioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt);

                    if (_resObjects.Quotes[0].PenaltiesAmt > 0)
                    {
                        //lblZinraErrMsg.Text = "You have outstanding penalties, please contact our Contact Centre for assistance on 086 77 22 33 44.";
                        //lblZinraErrMsg.ForeColor = Color.Red;

                        //lblZinraErrMsg.Font = new Font("Arial", 14, FontStyle.Underline, GraphicsUnit.Point);

                        MyMessageBox.ShowBox("You have outstanding penalties, please contact our Contact Centre for assistance on 086 77 22 33 44.", "Message");
                    }


                    if (VehicalIndex == -1)
                        objRiskModel.LicenseId = _resObjects.Quotes[0].LicenceID;
                    else
                        objlistRisk[VehicalIndex].LicenseId = _resObjects.Quotes[0].LicenceID;
                }
            }
            catch (Exception ex)
            {

            }
        }


        public string gemessageresponde(int data)
        {
            string Message = "";

            switch (data)
            {
                case 4000:
                    Message = "Customer cancellation";
                    break;
                case 4021:
                    Message = "Timeout waiting for response";
                    break;
                case 1006:
                    Message = "Under floor limit";
                    break;
                case 1007:
                    Message = "Stand-in processing at acquirer's option (under offline limit)";
                    break;
                case 9620:
                    Message = "EMV offline approved";
                    break;
                case 9621:
                    Message = "EMV offline declined";
                    break;
                case 9622:
                    Message = "EMV approved after online";
                    break;
                case 9623:
                    Message = "EMV declined after online";
                    break;
                case 9600:
                    Message = "System malfunction";
                    break;
                case 9601:
                    Message = "System malfunction - null action";
                    break;
                case 9602:
                    Message = "Component error request pipeline";
                    break;
                case 9603:
                    Message = "Component error response pipeline";
                    break;
                case 9604:
                    Message = "Database error";
                    break;
                case 9630:
                    Message = "Customer cancellation";
                    break;
                case 9631:
                    Message = "Operator cancellation";
                    break;
                case 9635:
                    Message = "Customer timeout";
                    break;
                case 9636:
                    Message = "Card reader retries exceeded";
                    break;
                case 9637:
                    Message = "No supported EMV applications";
                    break;
                case 9638:
                    Message = "Cardholder verification failure";
                    break;
                case 9639:
                    Message = "ICC Blocked";
                    break;
                case 9640:
                    Message = "ICC Transaction failed";
                    break;
                case 9641:
                    Message = "Device failure";
                    break;
                case 9642:
                    Message = "Fatal printer error";
                    break;
                case 9643:
                    Message = "Card still in slot";
                    break;
                case 9644:
                    Message = "Card insert retries exceeded";
                    break;

                case 9650:
                    Message = "Issuer disconnected";
                    break;

                case 9651:
                    Message = "Issuer timeout before response";
                    break;

                case 9660:
                    Message = "Signature did not match";
                    break;

                case 9670:
                    Message = "Batch Totals not available";
                    break;

                case 9680:
                    Message = "Key change in progress";
                    break;

                case 9700:
                    Message = "Missing transaction amount";
                    break;

                case 9701:
                    Message = "Missing card number";
                    break;

                case 9702:
                    Message = "Missing expiry date";
                    break;

                case 9703:
                    Message = "Missing PIN data";
                    break;

                case 9704:
                    Message = "Missing processing code";
                    break;

                case 9705:
                    Message = "Missing account";
                    break;

                case 9706:
                    Message = "Missing cashback amount";
                    break;

                case 9707:
                    Message = "Missing currency code";
                    break;

                case 9708:
                    Message = "Missing merchandise data";
                    break;

                case 9709:
                    Message = "Missing effective date";
                    break;

                case 9710:
                    Message = "Missing effective date";
                    break;

                case 9711:
                    Message = "Rejections due to missing data in database";
                    break;

                case 9720:
                    Message = "Original transaction not found";
                    break;

                case 9721:
                    Message = "Duplicate transaction";
                    //  Message = "Rejections due to message conditions:
                    break;


                case 9750:
                    Message = "Expired card";
                    break;


                case 9751:
                    Message = "No supported accounts";
                    break;


                case 9752:
                    Message = "No supported accounts for manual entry";
                    break;


                case 9753:
                    Message = "Card number failed Luhn check";
                    break;


                case 9754:
                    Message = "Card not yet effective";
                    break;


                case 9755:
                    Message = "No supported accounts for ICC fallback";
                    break;



                case 9756:
                    Message = "Not valid for transaction";
                    break;


                case 9757:
                    Message = "Consecutive usage not allowed";
                    break;


                case 9758:
                    Message = "Declined because of CVV or AVS failure";
                    break;



                case 9759:
                    Message = "Card number format invalid";
                    break;


                case 9760:
                    Message = "Purchase amount exceeds maximum allowed value";
                    break;


                case 9761:
                    Message = "Cashback amount exceeds maximum allowed value";
                    break;


                case 9762:
                    Message = "Transaction amount exceeds maximum allowed value";
                    break;


                case 9763:
                    Message = "Card sequence number format invalid";
                    break;


                case 9764:
                    Message = "Inconsistent data on the chip";
                    break;


                case 9765:
                    Message = "Inconsistent data track 2";
                    break;


                case 9766:
                    Message = "Invalid track 2 data";
                    break;


                case 9770:
                    Message = "Cashback amount exceeds transaction amount";
                    break;


                case 9771:
                    Message = " Cashback amount present in non - cashback transaction";
                    break;


                case 9772:
                    Message = "Cashback not permitted to cardholder";
                    break;


                case 9773:
                    Message = "Cashback account type is invalid.";
                    break;



                case 9774:
                    Message = "Cashback currency code is invalid";
                    break;

                case 9790:
                    Message = "Upstream response";
                    break;


                case 9791:
                    Message = "w Administrative response";
                    break;


                case 9792:
                    Message = " Advice response";
                    break;


                case 9793:
                    Message = "Suspected format error in advice - may not be resent";
                    break;


                case 9799:
                    Message = "Unknown";
                    break;

                case (9800 - 9999):
                    Message = "Values reserved for use in customized components.";
                    break;




            }


            return Message;
        }



        public string GetMessageCode(string responseData)
        {
            string messageReasonCode = "0";

            try
            {
                var listStrLineElements = responseData.Split('=').ToList();
                List<ResponseCodeObj> lst = new List<ResponseCodeObj>();
                var j = 0;


                foreach (var item in listStrLineElements)
                {
                    ResponseCodeObj obj = new ResponseCodeObj { Name = item };

                    if (j == 1)
                    {
                        var splitMessageCode = item.Split(' ');
                        if (splitMessageCode.Length > 0)
                        {
                            messageReasonCode = Regex.Replace(splitMessageCode[0], @"[^0-9a-zA-Z]+", "");
                            break;
                        }
                    }

                    lst.Add(obj);


                    //string name = "MessageReasonCode";

                    if (item.Contains("ResponseCode"))
                    {
                        j = 1;
                    }

                }


            }
            catch (Exception ex)
            {
                messageReasonCode = "0";
            }

            return messageReasonCode;
        }

        private void chkRadioLicence_CheckedChanged(object sender, EventArgs e)
        {

            if (chkRadioLicence.Checked)
            {

                pnlRadio.Visible = true;
                objRiskModel.IncludeRadioLicenseCost = true;
                if (chkZinara.Checked)
                {
                    pnlZinara.Visible = true;
                }
                else
                {
                    pnlZinara.Visible = false;
                }

            }
            else
            {
                pnlRadio.Visible = false;
                objRiskModel.IncludeRadioLicenseCost = false;
            }

        }

        private void chkZinara_CheckedChanged(object sender, EventArgs e)
        {
            if (chkZinara.Checked)
            {
                pnlZinara.Visible = true;

                if (txtAccessAmount.Text != "" && txtpenalty.Text != "")
                {
                    if (VehicalIndex == -1)
                    {
                        var amount = txtAccessAmount.Text;
                        var amount1 = txtpenalty.Text;
                        var totalamouny = Convert.ToDecimal(amount) + Convert.ToDecimal(amount1);
                        txtZinTotalAmount.Text = Convert.ToString(totalamouny);
                        if (chkRadioLicence.Checked)
                        {
                            pnlRadio.Visible = true;
                            objRiskModel.IncludeRadioLicenseCost = true;
                        }
                        else
                        {
                            pnlRadio.Visible = false;
                            objRiskModel.IncludeRadioLicenseCost = false;
                        }
                    }
                    else
                    {
                        var amount = txtAccessAmount.Text;
                        var amount1 = txtpenalty.Text;
                        var totalamouny = Convert.ToDecimal(amount) + Convert.ToDecimal(amount1);
                        txtZinTotalAmount.Text = Convert.ToString(totalamouny);
                        if (chkRadioLicence.Checked)
                        {
                            pnlRadio.Visible = true;
                            objlistRisk[VehicalIndex].IncludeRadioLicenseCost = true;

                        }
                        else
                        {
                            pnlRadio.Visible = false;
                            objlistRisk[VehicalIndex].IncludeRadioLicenseCost = false;
                        }
                    }
                }

            }
            else
            {
                pnlZinara.Visible = false;
            }
        }

        private void btnErrormeg_Click(object sender, EventArgs e)
        {
            //pnlsumary.Visible = true;
            pnlErrormessage.Visible = false;


            //  btnConfirmPayment.Click += btnConfirmPayment_Click_1; // here button1_Click is your event name

            //   System.Diagnostics.Process.GetCurrentProcess().Kill();


            btnConfirmPayment.Refresh();

            pnlconfimpaymeny.Visible = true;
        }


        public string GetMessage(string _responseData)
        {
            string CardNumber = "";

            try
            {

                var listStrLineElements = _responseData.Split('=').ToList();
                List<ResponseCodeObj> lst = new List<ResponseCodeObj>();
                var j = 0;


                foreach (var item in listStrLineElements)
                {
                    ResponseCodeObj obj = new ResponseCodeObj { Name = item };

                    if (j == 1)
                    {
                        var splitMessageCode = item.Split(' ');
                        if (splitMessageCode.Length > 0)
                        {
                            //CardReasonCode = Regex.Replace(splitMessageCode[0], @"[^0-9a-zA-Z]+", "");
                            CardNumber = Regex.Replace(splitMessageCode[0], "[^0-9A-Za-z*,]", "");
                            break;
                        }
                    }

                    lst.Add(obj);


                    //string name = "MessageReasonCode";

                    if (item.Contains("CardNumber"))
                    {
                        j = 1;
                    }

                }

            }
            catch (Exception ex)
            {

            }

            return CardNumber;
        }


        public string GetTrasactionAmount(string _responseData)
        {
            string TransactionAmount = "";

            try
            {

                var listStrLineElements = _responseData.Split('=').ToList();
                List<ResponseCodeObj> lst = new List<ResponseCodeObj>();
                var j = 0;


                foreach (var item in listStrLineElements)
                {
                    ResponseCodeObj obj = new ResponseCodeObj { Name = item };

                    if (j == 1)
                    {
                        var splitMessageCode = item.Split(' ');
                        if (splitMessageCode.Length > 0)
                        {
                            //CardReasonCode = Regex.Replace(splitMessageCode[0], @"[^0-9a-zA-Z]+", "");
                            TransactionAmount = Regex.Replace(splitMessageCode[0], @"[^0-9a-zA-Z]+", "");
                            break;
                        }
                    }

                    lst.Add(obj);


                    //string name = "MessageReasonCode";

                    if (item.Contains("TransactionAmount"))
                    {
                        j = 1;
                    }

                }

            }
            catch (Exception ex)
            {

            }

            return TransactionAmount;
        }


        private void btnconformpayBack_Click(object sender, EventArgs e)
        {
            pnlconfimpaymeny.Visible = false;
            pnlsumary.Visible = true;
        }

        private void txtVrn_TextChanged(object sender, EventArgs e)
        {
            if (txtVrn.Text != "")
            {
                NewerrorProvider.Clear();
            }
        }

        private void txtSumInsured_TextChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void txtEmailAddress_TextChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void rdbMale_CheckedChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void rdbFemale_CheckedChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void txtAdd1_TextChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void txtAdd2_TextChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void cmdCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void txtIDNumber_TextChanged(object sender, EventArgs e)
        {
            NewerrorProvider.Clear();
        }

        private void btnBacktoList_Click(object sender, EventArgs e)
        {
            VehicalIndex = objlistRisk.FindIndex(c => c.RegistrationNo == VRNnumForBack);
            txtVrn.Text = VRNnumForBack;

            PnlVrn.Visible = false;
            pnlSum.Visible = true;
            pnlAddMoreVehicle.Visible = true;

        }

        private void txtEmailAddress_Leave(object sender, EventArgs e)
        {

            int result = checkEmailExist();

            if (result == 1)
            {
                //lblEmailExist.Text = "Email already Exist";
                //lblEmailExist.ForeColor = Color.Red;

                MyMessageBox.ShowBox("Email already Exist", "Message");
            }
            else
            {
                lblEmailExist.Text = "";
            }


        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\windows\system32\osk.exe");
        }

        private void btnTBABack_Click(object sender, EventArgs e)
        {
            if (txtVrn.Text == "TBA")
            {
                pnlTBAPersonalDetails.Visible = false;
                PnlVrn.Visible = true;
                pnlAddMoreVehicle.Visible = false;
            }

        }

        private void btnTBAContinue_Click(object sender, EventArgs e)
        {
            if (txtTBAFirstName.Text == "")
            {
                NewerrorProvider.SetError(txtTBAFirstName, "Please Enter First Name.");
                return;
            }


            if (txtTBALastName.Text == "")
            {
                NewerrorProvider.SetError(txtTBALastName, "Please Enter Last Name.");
                return;
            }

            if (txtTBAAddress.Text == "")
            {
                NewerrorProvider.SetError(txtTBAAddress, "Please Enter Address.");
                return;
            }

            if (txtTBAPhone.Text == "")
            {
                NewerrorProvider.SetError(txtTBAPhone, "Please Enter Phone.");
                return;
            }


            if (txtTBAIDNumber.Text == "")
            {
                NewerrorProvider.SetError(txtTBAIDNumber, "Please Enter ID Number.");
                return;
            }


            if (txtVrn.Text == "TBA")
            {
                pnlTBAPersonalDetails.Visible = false;
                pnlConfirm.Visible = true;
            }


            customerInfo.FirstName = txtTBAFirstName.Text;
            customerInfo.LastName = txtTBALastName.Text;
            customerInfo.AddressLine1 = txtTBAAddress.Text;
            customerInfo.City = cmbTBACity.SelectedValue == null ? "0" : cmbTBACity.SelectedValue.ToString();
            customerInfo.NationalIdentificationNumber = txtTBAIDNumber.Text;
            customerInfo.PhoneNumber = txtTBAPhone.Text;
            customerInfo.CountryCode = cmbTBAPhoneCode.SelectedValue.ToString();

            txtFirstName.Text = txtTBAFirstName.Text + " " + txtTBALastName.Text;
            txtAdd1.Text = txtTBAAddress.Text;
            cmdCity.SelectedValue = cmbTBACity.SelectedValue;
            txtIDNumber.Text = txtTBAIDNumber.Text;
            txtPhone.Text = txtTBAPhone.Text;


        }

        private void pnlTBAPersonalDetails_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkCustomEmail_CheckedChanged(object sender, EventArgs e)
        {
            //   GetCustomerUniquEmail
            if (chkCustomEmail.Checked)
            {
                SetCustomEmail();
                customerInfo.IsCustomEmail = true;
            }
            else
            {
                txtEmailAddress.Text = "";
                customerInfo.IsCustomEmail = false;
            }

        }

        public string SetCustomEmail()
        {
            var CustomEmail = "";
            var client = new RestClient(ApiURL + "GetCustomerUniquEmail");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<CustomerModel>(response.Content);
            if (result != null)
            {
                CustomEmail = result.CustomEmail;
                txtEmailAddress.Text = result.CustomEmail;
            }
            return CustomEmail;
        }

        private void ZinPaymentDetail_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Get zinra license fee

            if (objRiskModel == null)
                return;

            if (objRiskModel != null && objRiskModel.RegistrationNo == null)
                return;

            if (cmbPaymentTerm.SelectedValue.ToString() != ZinPaymentDetail.SelectedValue.ToString())
            {
                if (ZinPaymentDetail.SelectedValue != null && cmbMake.SelectedIndex != 0)
                {
                    // GetZinraLiceenseFee(ZinPaymentDetail.SelectedValue.ToString());
                }
            }
        }

        private void RadioPaymnetTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objRiskModel == null)
                return;

            if (objRiskModel != null && objRiskModel.RegistrationNo == null)
                return;

            if (RadioPaymnetTerm.SelectedValue.ToString() != ZinPaymentDetail.SelectedValue.ToString())
            {
                if (RadioPaymnetTerm.SelectedValue != null && cmbMake.SelectedIndex != 0)
                {
                    // GetRadioLiceenseFee(RadioPaymnetTerm.SelectedValue.ToString());
                }
            }

        }

        //private void rdPresonal_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdPresonal.Checked)
        //    {
        //        if (VehicalIndex == -1)
        //            objRiskModel.IsCorporateField = false;
        //        else
        //            objlistRisk[VehicalIndex].IsCorporateField = false;

        //    }

        //}

        //private void rdCorporate_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdCorporate.Checked)
        //    {
        //       
        //        if (VehicalIndex == -1)
        //            objRiskModel.IsCorporateField = true;
        //        else
        //            objlistRisk[VehicalIndex].IsCorporateField = true;
        //    }
        //}

        private void btnCorporateContinue_Click(object sender, EventArgs e)
        {

            //if (txtAdd1.Text == string.Empty || txtAdd2.Text == string.Empty || cmdCity.SelectedIndex == -1 || txtIDNumber.Text == string.Empty || txtZipCode.Text == string.Empty)
            //{
            //    MessageBox.Show("Please Enter the required fields.");
            //    return;
            //}
            //if (!string.IsNullOrWhiteSpace(txtIDNumber.Text))
            //{
            //    Regex reg = new Regex(@"^([0-9]{2}-[0-9]{6,7}[a-zA-Z]{1}[0-9]{2})$");
            //    if (!reg.IsMatch(txtIDNumber.Text))
            //    {
            //        MessageBox.Show("Please Enter a valid National Identification Number");
            //        return;
            //    }
            //}

            if (txtCompany.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtCompany, "Please enter company.");
                txtCompany.Focus();
                return;
            }
            if (txtCmpEmail.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtAdd2, "Please enter email.");
                txtCmpEmail.Focus();
                return;
            }


            if (txtCmpAddress.Text == string.Empty)
            {
                NewerrorProvider.SetError(cmdCity, "Please enter address.");
                txtCmpAddress.Focus();
                return;
            }

            if (txtCmpPhone.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtIDNumber, "Please enter phone.");
                txtCmpPhone.Focus();
                return;
            }
            if (txtCmpBusinessId.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtCmpBusinessId, "Please enter Business ID.");
                txtCmpBusinessId.Focus();
                return;
            }
            //if (!string.IsNullOrWhiteSpace(txtIDNumber.Text))
            //{
            //    Regex reg = new Regex(@"^([0-9]{2}-[0-9]{6,7}[a-zA-Z]{1}[0-9]{2})$");
            //    if (!reg.IsMatch(txtIDNumber.Text))
            //    {
            //        MessageBox.Show("Please Enter a valid National Identification Number");
            //        NewerrorProvider.SetError(txtIDNumber, "Please Enter a valid National Identification Number.");
            //        txtIDNumber.Focus();
            //        return;
            //    }
            //}


            //pnlsumary.Visible = true;
            //pnlPersonalDetails2.Visible = false;
            // set personal detials


            pnlCorporate.Visible = false;
            // pnlsumary.Visible = true;
            pnlInsurance.Visible = true;

            if (txtCompany.Text != string.Empty && txtCmpEmail.Text != string.Empty && txtCmpAddress.Text != string.Empty && txtCmpPhone.Text != string.Empty && txtCmpBusinessId.Text != string.Empty)
            {

                if (_insuranceAndLicense && txtVrn.Text.ToUpper() != _tba)
                    pnlRadioZinaraIns.Visible = true;
                else
                    pnlRadioZinaraIns.Visible = false;


                customerInfo.EmailAddress = txtCmpEmail.Text;
                customerInfo.PhoneNumber = txtCmpPhone.Text;
                customerInfo.CountryCode = cmbCode.SelectedValue.ToString();
                customerInfo.FirstName = txtCompany.Text;
                customerInfo.LastName = txtCompany.Text;
                customerInfo.NationalIdentificationNumber = txtCmpBusinessId.Text;
                customerInfo.AddressLine1 = txtCmpAddress.Text;



                customerInfo.CompanyName = txtCompany.Text;
                customerInfo.CompanyEmail = txtCmpEmail.Text;
                customerInfo.CompanyAddress = txtCmpAddress.Text;
                customerInfo.CompanyPhone = txtCmpPhone.Text;
                customerInfo.CompanyCity = cmbCmpCity.SelectedValue.ToString();
                customerInfo.CompanyBusinessId = txtCmpBusinessId.Text;
                customerInfo.IsCorporate = true;

                // calculate summary
                // CaclulateSummary(objlistRisk);
            }
            //CheckToken();
        }

        private void btnCorporateBack_Click(object sender, EventArgs e)
        {
            pnlCorporate.Visible = false;
            PnlVrn.Visible = true;
            // pnlSum.Visible = true;
        }

        private void txtCmpEmail_Leave(object sender, EventArgs e)
        {
            int result = checkCompanyEmailExist();

            if (result == 1)
            {
                //lblCmpErrMsg.Text = "Email already Exist";
                //lblCmpErrMsg.ForeColor = Color.Red;
                MyMessageBox.ShowBox("Email already Exist.", "Message");
            }
            else
            {
                lblEmailExist.Text = "";
            }
        }

        private void textSearchVrn_Leave(object sender, EventArgs e)
        {
            if (textSearchVrn.Text.Length == 0)
            {
                textSearchVrn.Text = "ID Number";
                textSearchVrn.ForeColor = SystemColors.GrayText;
            }
        }

        private void textSearchVrn_Enter(object sender, EventArgs e)
        {
            if (textSearchVrn.Text == "ID Number")
            {
                textSearchVrn.Text = "";
                textSearchVrn.ForeColor = SystemColors.GrayText;
            }
            if (textSearchVrn.Text == "Business ID")
            {
                textSearchVrn.Text = "";
                textSearchVrn.ForeColor = SystemColors.GrayText;
            }
            // Id Number
        }

        public void GotoHome()
        {
            Form1 objFrm = new Form1(ObjToken);
            objFrm.Show();
            this.Close();
        }



        private void btnHomeConfirm_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeRisk_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeOptCovr_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeCorporate_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeOptional_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void pnlRadio_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void pnlZinara_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHomePernlDetail_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomePerDetail2_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeSum_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeSummary_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeSummary_Click_1(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnhomePay_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeTBA_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void rdCorporate_CheckedChanged(object sender, EventArgs e)
        {
            txtVrn.Visible = true;
            textSearchVrn.Visible = true;
            textSearchVrn.Text = "Business ID";
        }

        private void rdPresonal_CheckedChanged(object sender, EventArgs e)
        {
            txtVrn.Visible = true;
            textSearchVrn.Visible = true;
            textSearchVrn.Text = "ID Number";
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objRiskModel == null)
                return;

            int ProductId = 0;
            ProductId = Convert.ToInt32(cmbProducts.SelectedValue);
            bindVehicleUsage(ProductId);


            BindTaxClass(ProductId);


            if (objRiskModel != null && objRiskModel.isVehicleRegisteredonICEcash)
            {
                if (cmbProducts.SelectedValue != null && cmbProducts.SelectedValue.ToString() == _ProductId.ToString())
                {
                    cmbProducts.Enabled = false;

                    if (cmbTaxClasses.SelectedValue != null)
                        cmbTaxClasses.Enabled = false;
                    else
                        cmbTaxClasses.Enabled = true;

                    cmbTaxClasses.SelectedValue = _TaxClass;
                }
                else
                {
                    if (txtVrn.Text.ToUpper() == "TBA")
                    {
                        cmbProducts.Enabled = true;
                        cmbTaxClasses.Enabled = true;
                    }
                    else
                    {
                        cmbProducts.Enabled = false;
                        if (cmbTaxClasses.SelectedValue != null && cmbTaxClasses.SelectedIndex != 0)
                            cmbTaxClasses.Enabled = false;
                        else
                            cmbTaxClasses.Enabled = true;
                    }
                }
            }

        }

        private List<VehicleTaxClassModel> BindTaxClass(int vehicleType)
        {

            //  var product = InsuranceContext.Products.Single(where: $"Id='{VehicleType}'");

            int vehicleTypeId = 0;
            var product = ProductsList.FirstOrDefault(c => c.Id == vehicleType);

            if (product != null)
                vehicleTypeId = product.VehicleTypeId;


            var filtredTaxClassList = TaxClassList.Where(c => c.VehicleType == vehicleTypeId).ToList();
            filtredTaxClassList.Insert(0, new VehicleTaxClassModel { TaxClassId = 0, Description = "-Select-" });
            cmbTaxClasses.DataSource = filtredTaxClassList;
            cmbTaxClasses.DisplayMember = "Description";
            cmbTaxClasses.ValueMember = "TaxClassId";

            return filtredTaxClassList;
        }

        public void loadLicenceDiskPanel(List<ResultLicenceIDResponse> objRiskdetail)
        {

            var objlistRisk = objRiskdetail;
            pnlSummery.Controls.Clear(); //to remove all controls
            counter = objlistRisk.Count();

            for (int i = 0; i < counter; i++)
            {
                if (counter == 1)
                {
                    Panel Bottompnl = new Panel();
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
                    lblMakeModel.Text = "TransactionAmt :                         ";
                    lblMakeModel.Text += objlistRisk[i].TransactionAmt;
                    lblMakeModel.AutoSize = true;
                    lblMakeModel.BackColor = Color.Transparent;
                    lblMakeModel.Location = new Point(i, 150);
                    lblMakeModel.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblMakeModel);


                    Label lblLicenceID = new System.Windows.Forms.Label();
                    lblLicenceID.Name = lblLicenceID + i.ToString();
                    lblLicenceID.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblLicenceID.Text = "Total License Amt :                   ";
                    lblLicenceID.Text += objlistRisk[i].TotalLicAmt;
                    lblLicenceID.AutoSize = true;
                    lblLicenceID.BackColor = Color.Transparent;
                    lblLicenceID.Location = new Point(i, 200);
                    lblLicenceID.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblLicenceID);
                    Bottompnl.Size = new System.Drawing.Size(697, 1000);
                    Bottompnl.Location = new Point(i, (i * 1020));
                    pnllicenceDiskSummary.Controls.Add(Bottompnl);



                    Label lblReciptId = new System.Windows.Forms.Label();
                    lblReciptId.Name = lblReciptId + i.ToString();
                    lblReciptId.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblReciptId.Text = "Receipt Number :                   ";
                    lblReciptId.Text += objlistRisk[i].ReceiptID;
                    lblReciptId.AutoSize = true;
                    lblReciptId.BackColor = Color.Transparent;
                    lblReciptId.Location = new Point(i, 250);
                    lblReciptId.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblReciptId);
                    Bottompnl.Size = new System.Drawing.Size(697, 1000);
                    Bottompnl.Location = new Point(i, (i * 1020));
                    pnllicenceDiskSummary.Controls.Add(Bottompnl);


                }
                else if (counter == 2)
                {
                    Panel Bottompnl = new Panel();

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
                    lblMakeModel.Text = "TransactionAmt:                         ";
                    lblMakeModel.Text += objlistRisk[i].TransactionAmt;
                    lblMakeModel.AutoSize = true;
                    lblMakeModel.BackColor = Color.Transparent;
                    lblMakeModel.Location = new Point(i, 130);
                    lblMakeModel.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblMakeModel);
                    Label lblLicenceID = new System.Windows.Forms.Label();
                    lblLicenceID.Name = lblLicenceID + i.ToString();
                    lblLicenceID.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblLicenceID.Text = "Receipt Number :                         ";
                    lblLicenceID.Text += objlistRisk[i].ReceiptID;
                    lblLicenceID.AutoSize = true;
                    lblLicenceID.BackColor = Color.Transparent;
                    lblLicenceID.Location = new Point(i, 170);
                    lblLicenceID.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblLicenceID);
                    Label lblRecieptID = new System.Windows.Forms.Label();
                    lblRecieptID.Name = lblLicenceID + i.ToString();
                    lblRecieptID.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblRecieptID.Text = "Total License Amt :                         ";
                    lblRecieptID.Text += objlistRisk[i].TotalLicAmt;
                    lblRecieptID.AutoSize = true;
                    lblRecieptID.BackColor = Color.Transparent;
                    lblRecieptID.Location = new Point(i, 210);
                    lblRecieptID.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblLicenceID);
                    pnllicenceDiskSummary.Controls.Add(Bottompnl);
                }
                else
                {
                    Panel Bottompnl = new Panel();
                    Bottompnl.BackColor = Color.White;

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
                    lblMakeModel.Text += objlistRisk[i].make + " " + objlistRisk[i].model;
                    lblMakeModel.AutoSize = true;
                    lblMakeModel.BackColor = Color.Transparent;
                    lblMakeModel.Location = new Point(i, 70);
                    lblMakeModel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblMakeModel);


                    Label lblLicenceID = new System.Windows.Forms.Label();
                    lblLicenceID.Name = lblLicenceID + i.ToString();
                    lblLicenceID.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblLicenceID.Text = "Cover Type :";
                    lblLicenceID.Text += objlistRisk[i].LicenceID;
                    lblLicenceID.AutoSize = true;
                    lblLicenceID.BackColor = Color.Transparent;
                    lblLicenceID.Location = new Point(i, 100);
                    lblLicenceID.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    Bottompnl.Controls.Add(lblLicenceID);

                    Bottompnl.Size = new System.Drawing.Size(750, 200);
                    Bottompnl.Location = new Point(i, (i * 220));

                    pnllicenceDiskSummary.Controls.Add(Bottompnl);
                }
            }
        }

        //uncomment after getting response from icecash
        private List<ResultLicenceIDResponse> DisplayLicenseDisc(RiskDetailModel riskDetailModel, string parterToken, int vehicleId)
        {
            // List<ResultLicenceIDResponse> list = new List<ResultLicenceIDResponse>();

            // ResultLicenceIDRootObject quoteresponseResult = IcServiceobj.LICResult(riskDetailModel.LicenseId, parternToken);
            ResultLicenceIDRootObject quoteresponseResult = ICEcashService.TPILICResult(riskDetailModel, parternToken);

            riskDetailModel.Id = vehicleId;

            int i = 5;
            while (true)
            {
                i++;
                //if token expire
                if (quoteresponseResult.Response != null && quoteresponseResult.Response.Message.Contains("Partner Token has expired"))
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

                if (!quoteresponseResult.Response.Message.Contains("Partner Token has expired"))
                    break;
            }




            if (quoteresponseResult.Response != null && quoteresponseResult.Response.LicExpiryDate != null)
            {
                UpdateVehicleLiceneExpiryDate(vehicleId, quoteresponseResult.Response.LicExpiryDate);
            }

            if (quoteresponseResult.Response != null && quoteresponseResult.Response.LicenceCert != null)
            {
                licenseDiskList.Add(quoteresponseResult.Response);
                //var pdfPath = SavePdf(quoteresponseResult.Response.LicenceCert);
                //PdfDocument doc = new PdfDocument();
                //doc.LoadFromFile(pdfPath);
                //doc.Pages.Insert(0);
                //doc.Pages.Add();
                //doc.Pages.RemoveAt(0);//Since First page have always Red Text if use Free Version.

                //doc.SaveToFile(pdfPath);
                //MessageBox.Show("Print licence disk.");
                //printPDFWithAcrobat(pdfPath);
                //CreateLicenseFile(quoteresponseResult.Response.LicenceCert);

                this.Hide();
                CertificateSerialForm obj = new CertificateSerialForm(riskDetailModel, parternToken, quoteresponseResult.Response.LicenceCert); // hide for now
                obj.Show();

                //CreateLicenseFile(quoteresponseResult.Response.LicenceCert);//Print PDF
                //CertificateSerialForm obj = new CertificateSerialForm(riskDetailModel, parternToken);
                //obj.Show();
                //this.Hide();

                //var response = ICEcashService.LICCertConf(riskDetailModel, parternToken, quoteresponseResult.Response.ReceiptID);
                //if (response != null && response.Response.Message.Contains("Partner Token has expired"))
                //{
                //    ObjToken = IcServiceobj.getToken();
                //    parternToken = ObjToken.Response.PartnerToken;
                //    response = ICEcashService.LICCertConf(riskDetailModel, parternToken, quoteresponseResult.Response.ReceiptID);
                //}
            }
            //if (quoteresponseResult.Response.LicenceCert == null)  // for now it's commented
            //{
            //    //   MessageBox.Show("Pdf not found for this  certificate."); // r

            //    MyMessageBox.ShowBox("Pdf not found for this  certificate.", "Message");

            //}

            // 
            return licenseDiskList;
        }

        private void UpdateVehicleLiceneExpiryDate(int vehicleId, string vehicleExpiryDate)
        {
            //UpdateLicenseDate

            VehicleDetails model = new VehicleDetails();
            model.Id = vehicleId;
            model.LicenseExpiryDate = vehicleExpiryDate;


            if (model != null)
            {
                var client = new RestClient(IceCashRequestUrl + "UpdateLicenseDate");
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

            }
        }


        public void printPDFWithAcrobat(string Filepath)
        {
            // string Filepath = @"D:\Certificate120190724174642.pdf";
            try
            {
                string raderPath = ConfigurationManager.AppSettings["adobeReaderPath"];
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
                    proc.WaitForExit(2000);
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

        public void CreateLicenseFile(string base64data)
        {
            try
            {
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

            }
            catch (Exception ex)
            {
            }
        }

        //private void CreateLicenseFile(string base64Data)
        //{
        //    try
        //    {


        //        byte[] sPDFDecoded = Convert.FromBase64String(base64Data);
        //        string path  = @"../../Pdf/Certificate.pdf";
        //        File.WriteAllBytes(path, sPDFDecoded);
        //        PdfDocument doc = new PdfDocument();
        //        doc.LoadFromFile(path);
        //        doc.PrintDocument.Print();

        //        WriteLog(base64Data);

        //       // MessageBox.Show("Certificate is created on following path: " + path);

        //    }
        //    catch (Exception ex) { }
        //}


        public void GetTablesList()
        {
            var client = new RestClient(ApiURL + "TablesList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<DropdownTables>(response.Content);
            //var resultTbA = (new JavaScriptSerializer()).Deserialize<List<GetAllCities>>(response.Content);
            //var resultCorporate = (new JavaScriptSerializer()).Deserialize<List<GetAllCities>>(response.Content);
            //var resultZinra = (new JavaScriptSerializer()).Deserialize<List<CoverObject>>(response.Content);
            //var resultRadio = (new JavaScriptSerializer()).Deserialize<List<CoverObject>>(response.Content);

            var resultTbA = (new JavaScriptSerializer()).Deserialize<DropdownTables>(response.Content);
            var resultCorporate = (new JavaScriptSerializer()).Deserialize<DropdownTables>(response.Content);
            var resultZinra = (new JavaScriptSerializer()).Deserialize<DropdownTables>(response.Content);
            var resultRadio = (new JavaScriptSerializer()).Deserialize<DropdownTables>(response.Content);

            if (result != null)
            {
                var MakeList = result.MakeModel.ToList();
                var CitiesList = result.CitiesModel.ToList();
                var CoverTypeList = result.CoverTypeModel.ToList();
                var PaymentTermList = result.PaymentTermModel.ToList();
                // var ProductsList = result.ProductsModel.ToList();
                ProductsList = result.ProductsModel.ToList();
                var CurrencyList = result.CurrencyModel.ToList();
                TaxClassList = result.TaxClassModel.ToList();

                if (MakeList != null)
                {

                    result.MakeModel.Insert(0, new MakeObject { MakeCode = "0", MakeDescription = "-Select-" });

                    cmbMake.DataSource = result.MakeModel;
                    cmbMake.DisplayMember = "MakeDescription";
                    cmbMake.ValueMember = "makeCode";
                    bindModel(Convert.ToString(cmbMake.SelectedValue));
                }


                if (CitiesList != null)
                {

                    var citiesModel = result.CitiesModel;
                    citiesModel.Insert(0, new GetAllCities { Id = 0, CityName = "-Select-" });

                    cmdCity.DataSource = citiesModel;
                    cmdCity.DisplayMember = "name";
                    cmdCity.ValueMember = "CityName";

                    // TBA combobox


                    var tbaCityList = result.CitiesModel;

                    //tbaCityList.Insert(0, new GetAllCities { Id = 0, CityName = "-Select-" });

                    cmbTBACity.DataSource = tbaCityList;
                    cmbTBACity.DisplayMember = "name";
                    cmbTBACity.ValueMember = "CityName";

                    // TBA combobox

                    var corporateCityModel = result.CitiesModel;
                    //corporateCityModel.Insert(0, new GetAllCities { Id = 0, CityName = "-Select-" });
                    cmbCmpCity.DataSource = corporateCityModel;
                    cmbCmpCity.DisplayMember = "name";
                    cmbCmpCity.ValueMember = "CityName";
                }
                if (PaymentTermList != null)
                {

                    result.PaymentTermModel.Insert(0, new CoverObject { Id = 0, name = "-Select-" });
                    cmbPaymentTerm.DataSource = result.PaymentTermModel;
                    cmbPaymentTerm.DisplayMember = "name";
                    cmbPaymentTerm.ValueMember = "ID";

                    //Ds 13 Feb

                    resultZinra.PaymentTermModel.Insert(0, new CoverObject { Id = 0, name = "-Select-" });
                    ZinPaymentDetail.SelectedIndexChanged -= new EventHandler(ZinPaymentDetail_SelectedIndexChanged_1);
                    ZinPaymentDetail.DataSource = resultZinra.PaymentTermModel;
                    ZinPaymentDetail.DisplayMember = "name";
                    ZinPaymentDetail.ValueMember = "ID";
                    ZinPaymentDetail.SelectedIndexChanged += new EventHandler(ZinPaymentDetail_SelectedIndexChanged_1);

                    resultRadio.PaymentTermModel.Insert(0, new CoverObject { Id = 0, name = "-Select-" });
                    RadioPaymnetTerm.SelectedIndexChanged -= new EventHandler(RadioPaymnetTerm_SelectedIndexChanged);
                    RadioPaymnetTerm.DataSource = resultRadio.PaymentTermModel;
                    RadioPaymnetTerm.DisplayMember = "name";
                    RadioPaymnetTerm.ValueMember = "ID";
                    RadioPaymnetTerm.SelectedIndexChanged += new EventHandler(RadioPaymnetTerm_SelectedIndexChanged);
                }
                if (CoverTypeList != null)
                {
                    result.CoverTypeModel.Insert(0, new CoverObject { Id = 0, name = "-Select-" });
                    cmbCoverType.DataSource = result.CoverTypeModel;
                    cmbCoverType.DisplayMember = "name";
                    cmbCoverType.ValueMember = "ID";

                }
                if (ProductsList != null)
                {
                    result.ProductsModel.Insert(0, new ProductsModel { Id = 0, ProductName = "-Select-" });
                    cmbProducts.DataSource = result.ProductsModel;
                    cmbProducts.DisplayMember = "ProductName";
                    cmbProducts.ValueMember = "Id";
                    bindVehicleUsage(Convert.ToInt32(cmbProducts.SelectedValue));
                }
                if (CurrencyList != null)
                {
                    //result.CurrencyModel.Insert(0, new CurrencyModel { Id = 0, Name = "-Select-" });
                    cmbCurrency.DataSource = result.CurrencyModel;
                    cmbCurrency.DisplayMember = "Name";
                    cmbCurrency.ValueMember = "Id";
                }
                if (TaxClassList != null)
                {
                    //result.TaxClassModel.Insert(0, new VehicleTaxClassModel { TaxClassId = 0, Description = "-Select-" });
                    //cmbTaxClasses.DataSource = result.TaxClassModel;
                    //cmbTaxClasses.DisplayMember = "Description";
                    //cmbTaxClasses.ValueMember = "TaxClassId";
                }
            }
        }

        private void btnConfirmPayment_Click_1(object sender, EventArgs e)
        {

            //if (txtPartialAmount.Text == "")
            //{
            //    MyMessageBox.ShowBox("Please enter amount.", "Message");
            //    return;
            //}

            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            // Save all details
            CustomerModel customerModel = new CustomerModel();
            customerModel.FirstName = txtFirstName.Text;
            customerModel.EmailAddress = txtEmailAddress.Text;


            //Save Payment info
            PaymentResult objResult = new PaymentResult();
            long TransactionId = 0;
            TransactionId = GenerateTransactionId();
            decimal transctionAmt = Convert.ToDecimal(txtTotalPremium.Text);

            string paymentTermName = "Swipe";
            summaryModel.PaymentMethodId = Convert.ToInt32(ePaymentMethod.Swipe);

            if (RadioMobile.Checked)
            {
                paymentTermName = "Mobile";
                summaryModel.PaymentMethodId = Convert.ToInt32(ePaymentMethod.Mobile);
                MyMessageBox.ShowBox("Please Enter Ecocash Mobile number on PinPad.");
            }
            else
            {
                MyMessageBox.ShowBox("Please swipe the card for making the payment.");
            }

            //Button btnConfirmPayment = (Button)sender;
            //btnConfirmPayment.Text = "Processing.";
            //btnConfirmPayment.Enabled = false;
            //picImageConfirmPayment.Visible = true;
            //picImageConfirmPayment.WaitOnLoad = true;

            SetLoadingDuringPayment(true);


            SendSymbol(TransactionId, transctionAmt, paymentTermName);

        }


        //picImageConfirmPayment

        private void SetLoadingDuringPayment(bool displayLoader)
        {
            if (displayLoader)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    picImageConfirmPayment.Visible = true;
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    picImageConfirmPayment.Visible = false;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.Hide();
            PrintPreview1 dlg1 = new PrintPreview1(licenseDiskList, "");
            dlg1.ShowDialog();
        }

        private void btnThankHome_Click(object sender, EventArgs e)
        {
            //this.Hide();

            //objlistRisk = null;

            //pnlThankyou.Visible = false;
            //Form1 obj = new Form1();
            //obj.Show();

            GotoHome();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            //if (!System.Text.RegularExpressions.Regex.IsMatch(txtYear.Text, "^[0-9]*$"))
            //{
            //    MessageBox.Show("This textbox accepts only numeric value");
            //    txtYear.Text.Substring(0, txtYear.Text.Length - 1);
            //}
        }

        private void txtCmpPhone_TextChanged(object sender, EventArgs e)
        {
            //if (!System.Text.RegularExpressions.Regex.IsMatch(txtCmpPhone.Text, "^[0-9]*$"))
            //{
            //    MessageBox.Show("This textbox accepts only numeric value");
            //    txtCmpPhone.Text.Substring(0, txtCmpPhone.Text.Length - 1);
            //}
        }

        private void txtTBAPhone_TextChanged(object sender, EventArgs e)
        {

            //if (!System.Text.RegularExpressions.Regex.IsMatch(txtTBAPhone.Text, "^[0-9]*$"))
            //{
            //    MessageBox.Show("This textbox accepts only numeric value");
            //    txtTBAPhone.Text.Substring(0, txtTBAPhone.Text.Length - 1);
            //}
        }

        private void txtIDNumber_Enter(object sender, EventArgs e)
        {

            if (txtIDNumber.Text == "ID Number")
            {
                txtIDNumber.Text = "";
            }
        }

        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtradioAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInsCnt_Click(object sender, EventArgs e)
        {
            if (cmbPaymentTerm.SelectedIndex == 0)
            {
                NewerrorProvider.SetError(cmbPaymentTerm, "Please select the payment term");
                cmbPaymentTerm.Focus();
                return;
            }
            if (cmbCoverType.SelectedIndex == 0)
            {
                NewerrorProvider.SetError(cmbCoverType, "Please select the cover type");
                cmbCoverType.Focus();
                return;
            }


            int CoverId = Convert.ToInt32(cmbCoverType.SelectedValue);
            if (CoverId == 4) // for comprehensive
            {

                if (txtSumInsured.Text == string.Empty || txtSumInsured.Text == "0")
                {
                    //MessageBox.Show("Please Enter Sum Insured");
                    NewerrorProvider.SetError(txtSumInsured, "Please Enter Sum Insured.");
                    txtSumInsured.Focus();
                    return;
                }

                if (!IsNumeric(txtSumInsured.Text))
                {
                    NewerrorProvider.SetError(txtSumInsured, "Please Enter Valid Number.");
                    txtSumInsured.Focus();
                    return;
                }

                decimal amount = 490000; // minimum suminsured for alm

                if (Convert.ToDecimal(txtSumInsured.Text) < amount)
                {
                    NewerrorProvider.SetError(txtSumInsured, "Minimum sumInsured should be greater than or equal " + amount);
                    txtSumInsured.Focus();
                    return;
                }


            }

            string IceCashRequest = "";

            if (_insuranceAndLicense)
                IceCashRequest = "InsuranceAndLicense";
            else
                IceCashRequest = "Insurance";


            if (_insuranceAndLicense && txtVrn.Text.ToUpper() != _tba)
            {
                if (!CheckRadioAndZinara())
                {
                    return;
                }

                if (chkZinara.Checked == false)
                {
                    MyMessageBox.ShowBox("Please select license.", "Message");
                    return;
                }
            }

            if (VehicalIndex == -1)
             {
                if (cmbPaymentTerm.SelectedValue != null && cmbCoverType.SelectedValue != null)
                {
                    objRiskModel.PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    objRiskModel.CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                    objRiskModel.SumInsured = Math.Round(Convert.ToDecimal(txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text, System.Globalization.CultureInfo.InvariantCulture)), 2);
                    objRiskModel.IceCashRequest = IceCashRequest;

                    if(Convert.ToInt32(cmbCoverType.SelectedValue) == 4)
                    {
                        objRiskModel.CoverStartDate = Convert.ToDateTime(dtCoverStartDate.Text);
                        objRiskModel.CoverEndDate = Convert.ToDateTime(dtCoverEndDate.Text);
                    }
                    
                }
            }
            else
            {
                if (cmbPaymentTerm.SelectedValue != null && cmbCoverType.SelectedValue != null)
                {
                    objlistRisk[VehicalIndex].SumInsured = Math.Round(Convert.ToDecimal(txtSumInsured.Text == "" ? 0 : Convert.ToDecimal(txtSumInsured.Text, System.Globalization.CultureInfo.InvariantCulture)), 2);
                    objlistRisk[VehicalIndex].PaymentTermId = Convert.ToInt32(cmbPaymentTerm.SelectedValue);
                    objlistRisk[VehicalIndex].CoverTypeId = Convert.ToInt32(cmbCoverType.SelectedValue);
                    objlistRisk[VehicalIndex].IceCashRequest = IceCashRequest;

                    if (Convert.ToInt32(cmbCoverType.SelectedValue) == 4)
                    {
                        objlistRisk[VehicalIndex].CoverStartDate = Convert.ToDateTime(dtCoverStartDate.Text);
                        objlistRisk[VehicalIndex].CoverEndDate = Convert.ToDateTime(dtCoverEndDate.Text);
                    }
                        

                }
            }


            //if (!_insuranceAndLicense && txtVrn.Text.ToUpper() != _tba)
            if (txtVrn.Text.ToUpper() != _tba)
            {
                btnInsCnt.Text = "Processing..";

                SetLoadingPnlInsurance(true);
                RequestVehicleDetails();

                if (_iceCashErrorMsg != "")
                {
                    if (_iceCashErrorMsg.Contains("Licensing is only allowed"))
                    {
                        _iceCashErrorMsg = _iceCashErrorMsg + " You can get Insurance only.";

                        SetLoadingPnlInsurance(false);
                        btnInsCnt.Text = "Continue";

                        MyMessageBox.ShowBox(_iceCashErrorMsg);
                        GoToVrnScreen();
                        return;
                    }



                    MyMessageBox.ShowBox(_iceCashErrorMsg);
                    SetLoadingPnlInsurance(false);
                    btnInsCnt.Text = "Continue";
                }
                SetLoadingPnlInsurance(false);
            }

            pnlRiskDetails.Visible = true;
            pnlInsurance.Visible = false;

            //if (txtVrn.Text.Trim().ToUpper() == _tba || !_insuranceAndLicense)
            //    pnlRiskDetails.Visible = true;
            //else
            //{
            //    pnlRadioZinara.Visible = true;
            //}

            //pnlRiskDetails.Visible = true;
            btnInsCnt.Text = "Continue";
        }

        private void GoToVrnScreen()
        {
            loadingInsuraneImg.Visible = false;
            pnlInsurance.Visible = false;
            PnlVrn.Visible = true;
        }

        private void btnInsBack_Click(object sender, EventArgs e)
        {
            pnlInsurance.Visible = false;

            if (rdCorporate.Checked)
                pnlCorporate.Visible = true;
            else
                pnlPersonalDetails2.Visible = true;
        }

        private void txtDOB_DropDown(object sender, EventArgs e)
        {
            // MyDateTimePicker.OnDropDown(sender);
            // txtDOB.CalendarFont.
            // txtDOB.CalendarFont = new Font("Courier New", 25.25F, FontStyle.Italic, GraphicsUnit.Point, ((Byte)(0)));
            // txtDOB.CalendarFont = 

            // Application.EnableVisualStyles();
            // txtDOB.CalendarFont = new Font("Courier New", 25.25F, FontStyle.Italic, GraphicsUnit.Point, ((Byte)(0)));

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void txtCmpBusinessId_MouseEnter(object sender, EventArgs e)
        {
            txtCmpBusinessId.Text = string.Empty;
        }

        private void txtVrn_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ValidSpecailCharacter(e);
        }

        private bool ValidSpecailCharacter(KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
            return e.Handled;
        }

        private void txtAdd1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidSpecailCharacter(e);
        }

        private void txtAdd2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidSpecailCharacter(e);
        }

        private void txtZipCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidSpecailCharacter(e);
        }

        private void txtCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidSpecailCharacter(e);
        }

        private void txtCmpAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidSpecailCharacter(e);
        }

        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidSpecailCharacter(e);
        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidSpecailCharacter(e);
        }





        //private void rdBtnPartialPayment_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtPartialAmount.Text = "";
        //    txtPartialAmount.Enabled = true;
        //}

        //private void rdBtnFullPayment_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtPartialAmount.Text = txtTotalPremium.Text;
        //    txtPartialAmount.Enabled = false;
        //}


    }
}



