using GensureAPIv2.Models;
using Insurance.Service;
using InsuranceClaim.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Gene
{
    public partial class frmLicenseQuote : Form
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
        string _licenseId = "0";
        bool _insuranceAndLicense = true;


        List<ResultLicenceIDResponse> licenseDiskList = new List<ResultLicenceIDResponse>();

        public frmLicenseQuote(string branch, ICEcashTokenResponse _ObjToken = null, bool insuranceAndLicense = true)
        {
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
            // pnlRiskDetails.Visible = false;
            pnlPersonalDetails.Visible = false;
            pnlInsurance.Visible = false;
            pnlTBAPersonalDetails.Visible = false;
            pnlsumary.Visible = false;
            // pnlSum.Visible = false;
            pnlAddMoreVehicle.Visible = false;
            PnlVrn.Visible = true;
            pnlconfimpaymeny.Visible = false;
            pnlErrormessage.Visible = false;

            // pnlSummery.
            //pnlSum.Location = new Point(350, 100);
            //pnlSum.Size = new System.Drawing.Size(1390, 638);

            //Old code
            //pnlSum.Location = new Point(335, 100);
            // pnlSum.Size = new System.Drawing.Size(1300, 1200);
            //new Code
            //  pnlSum.Location = new Point(335, 20);
            //  pnlSum.Size = new System.Drawing.Size(1300, 1200);

            pnlConfirm.Visible = false;
            //  pnlOptionalCover.Visible = false;

            //PnlVrn.Location = new Point(335, 100);
            //PnlVrn.Size = new System.Drawing.Size(739, 238);

            //Old
            //PnlVrn.Location = new Point(350, 120);
            //PnlVrn.Size = new System.Drawing.Size(2600, 638);

            //new Changes 24/05/2019
            PnlVrn.Location = new Point(350, 20);
            PnlVrn.Size = new System.Drawing.Size(2600, 638);
            //  PnlVrn.Size = new System.Drawing.Size(600, 200);


            pnlInsurance.Location = new Point(355, 20);
            pnlInsurance.Size = new System.Drawing.Size(1550, 750);

            pnlLogo.Location = new Point(this.Width - 320, this.Height - 220);
            pnlLogo.Size = new System.Drawing.Size(300, 220);

            //pnlRiskDetails.Location = new Point(120, 33);
            //pnlRiskDetails.Size = new System.Drawing.Size(900, 700);

            //Old Code
            //pnlRiskDetails.Location = new Point(335, 100);
            //pnlRiskDetails.Size = new System.Drawing.Size(1550, 750);
            //New Code 

            //  pnlRiskDetails.Location = new Point(335, 20);
            //   pnlRiskDetails.Size = new System.Drawing.Size(1550, 750);

            //pnlOptionalCover.Location = new Point(200, 33);
            //pnlOptionalCover.Size = new System.Drawing.Size(800, 1040);

            //Old Code
            //pnlOptionalCover.Location = new Point(335, 100);
            //pnlOptionalCover.Size = new System.Drawing.Size(1550, 750);
            //new code
            //  pnlOptionalCover.Location = new Point(335, 20);
            //  pnlOptionalCover.Size = new System.Drawing.Size(1550, 750);

            //pnlAddMoreVehicle.Location = new Point(994, 398);
            //pnlAddMoreVehicle.Size = new System.Drawing.Size(263, 99);

            pnlAddMoreVehicle.Location = new Point(1300, 208);
            pnlAddMoreVehicle.Size = new System.Drawing.Size(690, 200);
            //testdd

            //12Feb 

            pnlRadioZinaraSumary.Visible = false;
            // pnlRadio.Visible = false;
            // pnlZinara.Visible = false;
            pnlCorporate.Visible = false;

            pnlRadioZinaraSumary.Location = new Point(335, 20);
            //pnlRadioZinara.Size = new System.Drawing.Size(1390, 750);
            pnlRadioZinaraSumary.Size = new System.Drawing.Size(1590, 750);

            // new added
            pnlRadioZinara.Visible = false;
            pnlRadioZinara.Location = new Point(335, 20);
            //pnlRadioZinara.Size = new System.Drawing.Size(1390, 750);
            pnlRadioZinara.Size = new System.Drawing.Size(1590, 750);


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

            pnlPersonalDetails.Location = new Point(355, 100);
            pnlPersonalDetails.Size = new System.Drawing.Size(1550, 750);

            //New Code
            pnlPersonalDetails.Location = new Point(355, 20);
            pnlPersonalDetails.Size = new System.Drawing.Size(1550, 750);

            pnlTBAPersonalDetails.Location = new Point(355, 100);
            //pnlTBAPersonalDetails.Size = new System.Drawing.Size(1450, 750);
            pnlTBAPersonalDetails.Size = new System.Drawing.Size(1550, 750);

            pnlCorporate.Location = new Point(355, 100);
            //pnlCorporate.Size = new System.Drawing.Size(1450, 750);
            pnlCorporate.Size = new System.Drawing.Size(1550, 750);

            //Old Code
            //pnlPersonalDetails2.Location = new Point(355, 100);
            //pnlPersonalDetails2.Size = new System.Drawing.Size(1550, 750);

            //New  Code
            pnlPersonalDetails2.Location = new Point(355, 20);
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
            pnlConfirm.Location = new Point(335, 20);
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
            pnlconfimpaymeny.Size = new System.Drawing.Size(1380, 1040);


            txtVrn.Text = "Vehicle Registration Number";
            textSearchVrn.Text = "ID Number";

            //txtVrn.Text = "AAD333";
            txtVrn.ForeColor = SystemColors.GrayText;
            textSearchVrn.ForeColor = SystemColors.GrayText;

            //txtZipCode.Text = "00263";
            //txtZipCode.ForeColor = SystemColors.GrayText;

            // SetLocationButton();

            btnBacktoList.Hide();
            bindAllCodes();
            GetTablesList();


            //lblChas.Visible = false;
            //lblEngine.Visible = false;
            //txtChasis.Visible = false;
            //txtEngine.Visible = false;



            // bindMake();
            //bindCoverType();
            //bindPaymentType();
            //bindAllCities();
            //bindAllClasses();   
            //bindCurrency();
            // bindProduct();

            // KeyDown event.

            // this.KeyPress += new KeyEventHandler(txtYear_KeyPress);

            //  txtYear.KeyPress += new KeyPressEventHandler(txtYear_KeyPress);

            // cmbCmpCity.Height = 150;
        }

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

                    //result.PaymentTermModel.Insert(0, new CoverObject { Id = 0, name = "-Select-" });
                    //cmbPaymentTerm.DataSource = result.PaymentTermModel;
                    //cmbPaymentTerm.DisplayMember = "name";
                    //cmbPaymentTerm.ValueMember = "ID";

                    //Ds 13 Feb


                    resultZinra.PaymentTermModel.Insert(0, new CoverObject { Id = 0, name = "-Select-" });

                    // zinra detail
                    ZinPaymentTrm.DataSource = resultZinra.PaymentTermModel;
                    ZinPaymentTrm.DisplayMember = "name";
                    ZinPaymentTrm.ValueMember = "ID";

                    resultRadio.PaymentTermModel.Insert(0, new CoverObject { Id = 0, name = "-Select-" });

                    ZnrRadioPayTerm.DataSource = resultRadio.PaymentTermModel;
                    ZnrRadioPayTerm.DisplayMember = "name";
                    ZnrRadioPayTerm.ValueMember = "ID";

                    // radio summary
                    RadioPaymnetTerm.DataSource = resultRadio.PaymentTermModel;
                    RadioPaymnetTerm.DisplayMember = "name";
                    RadioPaymnetTerm.ValueMember = "ID";



                    // zinara summary
                    ZinPaymentDetail.DataSource = resultZinra.PaymentTermModel;
                    ZinPaymentDetail.DisplayMember = "name";
                    ZinPaymentDetail.ValueMember = "ID";

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
                    result.CurrencyModel = result.CurrencyModel.Where(c => c.Name.Contains("RTGS")).ToList();
                    // result.CurrencyModel.Insert(0, new CurrencyModel { Id = 0, Name = "-Select-" });
                    cmbCurrency.DataSource = result.CurrencyModel;
                    cmbCurrency.DisplayMember = "Name";
                    cmbCurrency.ValueMember = "Id";
                }
                if (TaxClassList != null)
                {
                    result.TaxClassModel.Insert(0, new VehicleTaxClassModel { TaxClassId = 0, Description = "-Select-" });
                    //cmbTaxClasses.DataSource = result.TaxClassModel;
                    //  cmbTaxClasses.DisplayMember = "Description";
                    // cmbTaxClasses.ValueMember = "TaxClassId";
                }
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

                //cmbVehicleUsage.DataSource = result;
                //  cmbVehicleUsage.DisplayMember = "vehUsage";
                // cmbVehicleUsage.ValueMember = "id";
                //cmbVehicleUsage.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }

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
            cmbModel.DisplayMember = "modeldescription";
            cmbModel.ValueMember = "ModelCode";
            cmbPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        }





        private void btnSave_Click(object sender, EventArgs e)
        {
            NewObjectDuringEditOrBack();
            btnSave.Text = "Process...";

            //Service_db _service = new Service_db();
            //if (!_service.CheckVehicleExistOrNot(txtVrn.Text))
            //{
            //    MyMessageBox.ShowBox("This vrn alrady exist.", "Message");
            //    return;
            //}


            Worker_DoWork();
            btnSave.Text = "Submit";

            var TBA = ConfigurationManager.AppSettings["tba"];
            if (txtVrn.Text == TBA)
            {
                //lblChas.Visible = true;
                //lblEngine.Visible = true;
                //txtChasis.Visible = true;
                //txtEngine.Visible = true;
            }
            else
            {
                //lblChas.Visible = false;
                //lblEngine.Visible = false;
                //txtChasis.Visible = false;
                //txtEngine.Visible = false;
            }
        }

        private void Worker_DoWork()
        {
            isbackclicked = false;
            // this.Invoke(new Action(() => pictureBox1.Visible = true));
            // first screen where enter vrn number
            if (txtVrn.Text == string.Empty || txtVrn.Text == "Car Registration Number" || txtVrn.Text.Length == 0 || (string.IsNullOrWhiteSpace(txtVrn.Text)))
            {
                //MessageBox.Show("Please Enter Registration Number");
                NewerrorProvider.SetError(txtVrn, "Please Enter Registration Number.");
                txtVrn.Focus();
                //  txtVrn.ForeColor = Color.Red;
                return;
            }
            else
            {
                txtIDNumber.Text = textSearchVrn.Text == "Id Number" ? "" : textSearchVrn.Text;
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
                        txtCmpBusinessId.Text = textSearchVrn.Text == "Id Number" ? "" : textSearchVrn.Text;
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
                    lblVrnErrMsg.Text = "Car Registration number and Id number are not correct.";
                    lblVrnErrMsg.ForeColor = Color.Red;
                }
            }



        }

        private void VrnAlredyExist()
        {
            pnlConfirm.Visible = false;
            //this.Invoke(new Action(() => pnlRiskDetails.Visible = false));
            PnlVrn.Visible = true;
            pnlAddMoreVehicle.Visible = false;
        }


        public void NewObjectDuringEditOrBack()
        {
            txtSumInsured.Text = string.Empty;
            //  cmbVehicleUsage.SelectedIndex = 0;
            //  cmbVehicleUsage.SelectedIndex = -1;
            // cmbPaymentTerm.SelectedIndex = 0;
            //   cmbTaxClasses.SelectedIndex = 0;
            cmbCoverType.SelectedIndex = 0;
            // cmbCurrency.SelectedIndex = 0;


            //cmbMake.SelectedIndex = 0;
            //cmbModel.SelectedIndex = -1;

            cmbMake.SelectedIndex = 1;
            cmbModel.SelectedIndex = 1;

            //txtYear.Text = string.Empty;
            //txtChasis.Text = string.Empty;
            //txtEngine.Text = string.Empty;

            //optionalCover
            //  chkExcessBuyback.Checked = false;
            //  chkRoadsideAssistance.Checked = false;
            //  chkMedicalExpenses.Checked = false;
            //chkPassengerAccidentalCover.Checked = false;
            // cmbNoofPerson.Value = 0;
            //Optional
            txtradioAmount.Text = string.Empty;
            //chkRadioLicence.Checked = false;
            // chkZinara.Checked = false;
            txtTransactionAmt.Text = string.Empty;
            txtLicPenalitesAmt.Text = string.Empty;
            txtTotalZinaraAmount.Text = string.Empty;

            btnAddMoreVehicle.Visible = true;
        }

        private void btnPersoanlContinue_Click(object sender, EventArgs e)
        {

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


            if (txtFirstName.Text != string.Empty && txtEmailAddress.Text != string.Empty && txtPhone.Text != string.Empty)
            {
                pnlPersonalDetails2.Visible = true;
                pnlPersonalDetails.Visible = false;
            }

            // string theDate = txtDOB.Value.ToString("dd/MM/yyyy");

        }

        private void btnPer2Con_Click(object sender, EventArgs e)
        {
            if (txtAdd1.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtAdd1, "Please enter the Address1");
                txtAdd1.Focus();
                return;
            }
            if (txtAdd2.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtAdd2, "Please enter the Address2");
                txtAdd2.Focus();
                return;
            }
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
                NewerrorProvider.SetError(txtIDNumber, "Please enter the Id number");
                txtIDNumber.Focus();
                return;
            }
            

            if (txtAdd1.Text != string.Empty && txtAdd2.Text != string.Empty && cmdCity.SelectedIndex != -1 && txtIDNumber.Text != string.Empty)
            {

                // pnlsumary.Visible = true;
                //  pnlInsurance.Visible = true;
                //  pnlZinara.Visible=true;
                pnlPersonalDetails2.Visible = false;
                pnlRadioZinara.Visible = true;

                // var strName = txtFirstName.Text.Split(' ');
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

        private void btnPerBack2_Click(object sender, EventArgs e)
        {
            pnlPersonalDetails.Visible = true;
            pnlPersonalDetails2.Visible = false;
        }

        private void OptNext_Click(object sender, EventArgs e)
        {
            pnlRadioZinaraSumary.Visible = false;
            pnlconfimpaymeny.Visible = true;
            //pnlConfirm.Visible = true;

            decimal totalVehicleLicenseAmount = 0;
            if (txtTotalZinaraAmount.Text != "")
                totalVehicleLicenseAmount = Convert.ToDecimal(txtTotalZinaraAmount.Text);

            if (txtradioAmount.Text != "")
                totalVehicleLicenseAmount += Convert.ToDecimal(txtradioAmount.Text);


            txtTotalAmount.Text = totalVehicleLicenseAmount.ToString();
            //txtPartialAmount.Text = totalVehicleLicenseAmount.ToString();

            objRiskModel.VehicleLicenceFee = totalVehicleLicenseAmount;
            objRiskModel.RadioLicenseCost = txtradioAmount.Text == "" ? 0 : Convert.ToDecimal(txtradioAmount.Text);


        }

        private void optBack_Click(object sender, EventArgs e)
        {
            // pnlZinaraRadioSummary.vai
            pnlRadioZinaraSumary.Visible = false;
            pnlRadioZinara.Visible = true;
            // pnlConfirm.Visible = true;
            // pnlPersonalDetails2.Visible = true;
        }

        private void btnConfContinue_Click(object sender, EventArgs e)
        {


            //

            if (cmbProducts.SelectedIndex == 0)
            {
                MyMessageBox.ShowBox("Please select vehicle type.", "Message");
                return;
            }

            //if (cmbVehicleUsage.SelectedIndex == 0)
            //{
            //    MyMessageBox.ShowBox("Please select vehicle usage.");
            //    return;
            //}

            //if (cmbTaxClasses.SelectedIndex == 0)
            //{
            //    MyMessageBox.ShowBox("Please select tax classes.");
            //    return;
            //}

            if (cmbMake.SelectedIndex == 0)
            {
                MyMessageBox.ShowBox("Please select make.", "Message");
                return;
            }

            if (cmbModel.SelectedIndex == 0)
            {
                MyMessageBox.ShowBox("Please select model.", "Message");
                return;
            }

            //if (cmbCurrency.SelectedIndex == 0)
            //{
            //    MyMessageBox.ShowBox("Please select currency.");
            //    return;
            //}


            //if (txtYear.Text == string.Empty)
            //{
            //    MyMessageBox.ShowBox("Please select year.");
            //    return;
            //}

            pnlConfirm.Visible = false;
            pnlRadioZinaraSumary.Visible = true;


            objRiskModel.ProductId = Convert.ToInt32(cmbProducts.SelectedValue);
            //  objRiskModel.VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            //  objRiskModel.TaxClassId = Convert.ToInt32(cmbTaxClasses.SelectedValue);
            objRiskModel.MakeId = cmbMake.SelectedValue.ToString();
            objRiskModel.ModelId = cmbModel.SelectedValue.ToString();
            objRiskModel.CurrencyId = Convert.ToInt32(cmbCurrency.SelectedValue);
            // objRiskModel.VehicleYear = Convert.ToInt32(txtYear.Text);
            objRiskModel.SumInsured = 0;
            objRiskModel.Premium = 0;
            objRiskModel.ZTSCLevy = 0;
            objRiskModel.StampDuty = 0;


            GetRadioLiceenseFee(objRiskModel.PaymentTermId.ToString());

        }


        private void GetRadioLiceenseFee(string paymentTerm)
        {
            try
            {

                var requestToken = Service_db.GetLatestToken();
                if (requestToken != null)
                {
                    parternToken = requestToken.Token;
                }

                if (rdCorporate.Checked)
                    _clientIdType = "2";
                else
                    _clientIdType = "1";

            
                var _quoteresponse = new ResultRootObject();

                if ( chkZinaraOptional.Checked && !ckhRadioOptional.Checked)
                {
                    paymentTerm = ZinPaymentTrm.SelectedValue.ToString();
                    _quoteresponse = IcServiceobj.ZineraLICQuoteOnly(txtVrn.Text, parternToken, _clientIdType, paymentTerm, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo);

                }
                else
                {
                    var radioPaymentTerm = ZnrRadioPayTerm.SelectedValue.ToString();
                    var zinPaymentTrm = ZinPaymentTrm.SelectedValue.ToString();
                    _quoteresponse = IcServiceobj.ZineraAndRadioLICQuote(txtVrn.Text, parternToken, _clientIdType, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo, zinPaymentTrm, radioPaymentTerm);

                }



                var _resObjects = _quoteresponse.Response;

                if (_resObjects.Message.Contains("1 failed"))
                    _iceCashErrorMsg = _resObjects.Quotes == null ? "Error Occured" : _resObjects.Quotes[0].Message;

                //if token expire
                if (_resObjects != null && _resObjects.Message.Contains("Partner Token has expired"))
                {
                    _iceCashErrorMsg = "";
                    //  ObjToken = CheckParterTokenExpire();
                    ObjToken = IcServiceobj.getToken();
                    if (ObjToken != null)
                        parternToken = ObjToken.Response.PartnerToken;

                    Service_db.UpdateToken(ObjToken);

                    //_quoteresponse = IcServiceobj.ZineraLICQuote(txtVrn.Text, parternToken, _clientIdType, paymentTerm, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo);

                    if (chkZinaraOptional.Checked && !ckhRadioOptional.Checked)
                    {
                        paymentTerm = ZinPaymentTrm.SelectedValue.ToString();
                        _quoteresponse = IcServiceobj.ZineraLICQuoteOnly(txtVrn.Text, parternToken, _clientIdType, paymentTerm, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo);

                    }
                    else
                    {
                        var radioPaymentTerm = ZnrRadioPayTerm.SelectedValue.ToString();
                        var zinPaymentTrm = ZinPaymentTrm.SelectedValue.ToString();
                        _quoteresponse = IcServiceobj.ZineraAndRadioLICQuote(txtVrn.Text, parternToken, _clientIdType, cmbProducts.SelectedValue.ToString(), customerInfo.NationalIdentificationNumber, customerInfo, zinPaymentTrm, radioPaymentTerm);

                    }



                    _resObjects = _quoteresponse.Response;

                    if (_resObjects.Message.Contains("1 failed"))
                        _iceCashErrorMsg = _resObjects.Quotes == null ? "Error Occured" : _resObjects.Quotes[0].Message;

                }

                if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Quotes[0].Message == "Success")
                {
                    if (chkZinaraOptional.Checked)
                    {
                        //
                      //  txtTransactionAmt.Text =   _resObjects.Quotes[0].ArrearsAmt.ToString();
                        txtArrearsAmt.Text = _resObjects.Quotes[0].ArrearsAmt.ToString();
                        objRiskModel.ArrearsAmt = Convert.ToDecimal( _resObjects.Quotes[0].ArrearsAmt.ToString());

                        txtTransactionAmt.Text = _resObjects.Quotes[0].TransactionAmt.ToString();
                        objRiskModel.LicTransactionAmt = Convert.ToDecimal(_resObjects.Quotes[0].TransactionAmt.ToString());

                        txtAdministrationAmt.Text = _resObjects.Quotes[0].AdministrationAmt.ToString();
                        objRiskModel.AdministrationAmt = Convert.ToDecimal(_resObjects.Quotes[0].AdministrationAmt.ToString());



                        txtLicPenalitesAmt.Text = _resObjects.Quotes[0].PenaltiesAmt.ToString();
                       // txtLicPenalitesAmt1.Text = _resObjects.Quotes[0].PenaltiesAmt.ToString();

                        
                        objRiskModel.PenaltiesAmt = _resObjects.Quotes[0].PenaltiesAmt;

                         var totalamount = objRiskModel.TotalLicAmount;
                        txtTotalZinaraAmount.Text = Convert.ToString(objRiskModel.TotalLicAmount);

                        objRiskModel.VehicleLicenceFee= Convert.ToDecimal(totalamount);

                        txtTotalZinaraAmount.Text = Convert.ToString(totalamount);
                        objRiskModel.TotalLicAmount = Convert.ToDecimal(totalamount);


                    }

                    if (ckhRadioOptional.Checked)
                    {
                        txtradioAmount.Text = Convert.ToString(_resObjects.Quotes[0].RadioTVAmt);
                        objRiskModel.RadioLicenseCost = _resObjects.Quotes[0].RadioTVAmt;
                        objRiskModel.IncludeRadioLicenseCost = true;
                    }

                    objRiskModel.LicenseId = _resObjects.Quotes[0].LicenceID;


                    string format = "yyyyMMdd";

                    var LicExpiryDate = DateTime.ParseExact(_resObjects.Quotes[0].LicExpiryDate, format, CultureInfo.InvariantCulture);
                    // var RadioTVExpiryDate = DateTime.ParseExact(_resObjects.Quotes[0].RadioTVExpiryDate, format, CultureInfo.InvariantCulture);

                    objRiskModel.RenewalDate = LicExpiryDate;
                    objRiskModel.LicExpiryDate = LicExpiryDate.ToShortDateString();
                    // objRiskModel.RadioTVExpiryDate = RadioTVExpiryDate.ToShortDateString();
                    objRiskModel.IceCashRequest = "License";
                }

                if (_resObjects != null && _resObjects.Quotes != null && _resObjects.Message.Contains("1 failed"))
                {
                    // lblConfirmMessage.Text = resObject.Quotes[0].Message;
                    MyMessageBox.ShowBox(_resObjects.Quotes[0].Message, "Message");
                }
            }
            catch (Exception ex)
            {

            }
        }



        private void btnConfBack_Click(object sender, EventArgs e)
        {
            // pnlsumary.Visible = false;
            pnlConfirm.Visible = false;
            pnlRadioZinara.Visible = true;
            //pnlRadioZinaraSumary.Visible = true;
        }

        private void btnSumContinue_Click(object sender, EventArgs e)
        {
            pnlsumary.Visible = false;
            pnlconfimpaymeny.Visible = true;
        }

        private void btnSumBack_Click(object sender, EventArgs e)
        {
            pnlconfimpaymeny.Visible = false;
            pnlsumary.Visible = true;
        }

        private void btnconformpayBack_Click(object sender, EventArgs e)
        {
            pnlconfimpaymeny.Visible = false;
            pnlRadioZinaraSumary.Visible = true;

        }

        private void txtVrn_Enter(object sender, EventArgs e)
        {
            if (txtVrn.Text == "Vehicle Registration Number")
            {
                txtVrn.Text = "";
                txtVrn.ForeColor = SystemColors.GrayText;
            }
        }

        private void textSearchVrn_Enter(object sender, EventArgs e)
        {
            if (textSearchVrn.Text == "ID Number")
            {
                textSearchVrn.Text = "";
                textSearchVrn.ForeColor = SystemColors.GrayText;
            }
            if (textSearchVrn.Text == "Business Id")
            {
                textSearchVrn.Text = "";
                textSearchVrn.ForeColor = SystemColors.GrayText;
            }
        }

        private void btnNextRadioZinara_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedIndex == 0)
            {
                MyMessageBox.ShowBox("Please select vehicle type.", "Message");
                return;
            }

            // to do             
            bool IsZinraCheckboxValidate = false;


            if (ckhRadioOptional.Checked)
                IsZinraCheckboxValidate = true;

            if (chkZinaraOptional.Checked)
                IsZinraCheckboxValidate = true;


            bool IsZinraValidate = false;
            if (ZnrRadioPayTerm.SelectedIndex != 0)
                IsZinraValidate = true;

            if (ZinPaymentTrm.SelectedIndex != 0)
                IsZinraValidate = true;



            // validate checkbox and payment term for radio

            if (ckhRadioOptional.Checked)
            {
                if (ZnrRadioPayTerm.SelectedIndex == 0)
                {
                    MyMessageBox.ShowBox("Please select payment term for radio license because you have checked radio chekbox othewise please uncheck radio chekbox.", "Message");
                    return;
                }
            }

            if (ZnrRadioPayTerm.SelectedIndex == 0)
            {
                if (ckhRadioOptional.Checked)
                {
                    MyMessageBox.ShowBox("Please checked checkbox of radio license because you have selected radio payment term otherwise unselect payment term.", "Message");
                    return;
                }
            }



            if (chkZinaraOptional.Checked)
            {
                if (ZinPaymentTrm.SelectedIndex == 0)
                {
                    MyMessageBox.ShowBox("Please select payment term for zinara license because you have checked zinara chekbox othewise please uncheck zinara checkobox.");
                    return;
                }
            }


            if (ZinPaymentTrm.SelectedIndex == 0)
            {
                if (chkZinaraOptional.Checked)
                {
                    MyMessageBox.ShowBox("Please checked checkbox of zinara license because you have selected zinara payment term otherwise unselect payment term.", "Message");
                    return;
                }
            }


            if (!IsZinraCheckboxValidate)
            {
                MyMessageBox.ShowBox("Please select checkbox either for zinara or radio or both.", "Message");
                return;
            }

            if (!IsZinraValidate)
            {
                MyMessageBox.ShowBox("Please select payment term either for zinara or radio or both.", "Message");
                return;
            }


            if (ckhRadioOptional.Checked)
            {
                if (!chkZinaraOptional.Checked)
                {
                    //lblZinraErrMsg.Text = "System cannot process Radio Only.";
                    //lblZinraErrMsg.ForeColor = Color.Red;
                    MyMessageBox.ShowBox("System cannot process Radio Only.", "Message");

                    //System cannot process Radio Only
                    return;
                }
            }




            if (ZnrRadioPayTerm.SelectedIndex != 0)
            {
                objRiskModel.ZinaraRadioPaymentTermId = Convert.ToInt32(ZnrRadioPayTerm.SelectedValue);
                objRiskModel.PaymentTermId = Convert.ToInt32(ZnrRadioPayTerm.SelectedValue);
            }

            if (ZinPaymentTrm.SelectedIndex != 0)
            {
                objRiskModel.ZinaraPaymentTermId = Convert.ToInt32(ZinPaymentTrm.SelectedValue);
                objRiskModel.PaymentTermId = Convert.ToInt32(ZinPaymentTrm.SelectedValue);
            }


            btnNextRadioZinara.Text = "Processing..";
            btnNextRadioZinara.Enabled = false;
            pictureBoxZinara.Visible = true;
            pictureBoxZinara.WaitOnLoad = true;




            objRiskModel.ProductId = Convert.ToInt32(cmbProducts.SelectedValue);
            //  objRiskModel.VehicleUsage = Convert.ToInt32(cmbVehicleUsage.SelectedValue);
            //  objRiskModel.TaxClassId = Convert.ToInt32(cmbTaxClasses.SelectedValue);
            // objRiskModel.MakeId = cmbMake.SelectedValue.ToString();
            // objRiskModel.ModelId = cmbModel.SelectedValue.ToString();
            objRiskModel.CurrencyId = Convert.ToInt32(cmbCurrency.SelectedValue);
            // objRiskModel.VehicleYear = Convert.ToInt32(txtYear.Text);
            objRiskModel.SumInsured = 0;
            objRiskModel.Premium = 0;
            objRiskModel.ZTSCLevy = 0;
            objRiskModel.StampDuty = 0;

           
                GetRadioLiceenseFee(objRiskModel.PaymentTermId.ToString());
            

           
           



            

            if (_iceCashErrorMsg != "")
            {
                GoToVrnScreen();
                return;
            }

            btnNextRadioZinara.Text = "Continue";
            btnNextRadioZinara.Enabled = true;


            pnlRadioZinara.Visible = false;
            pnlRadioZinaraSumary.Visible = true;
            pictureBoxZinara.Visible = false;


        }

        private void GoToVrnScreen()
        {
            pnlRadioZinara.Visible = false;
            btnNextRadioZinara.Text = "Continue";
            btnNextRadioZinara.Enabled = true;
            pictureBoxZinara.Visible = false;
            PnlVrn.Visible = true;
        }

        private void btnRadioZinaraBack_Click(object sender, EventArgs e)
        {
            pnlRadioZinara.Visible = false;

            if (rdCorporate.Checked)
                pnlCorporate.Visible = true;
            else
                pnlPersonalDetails2.Visible = true;
        }

        private void btnHomeRadioZinara_Click(object sender, EventArgs e)
        {
            GotoHome();
        }


        public void GotoHome()
        {
            Form1 objFrm = new Form1(ObjToken);
            objFrm.Show();
            this.Close();
        }

        private void btnHomePerDetail2_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnhomePay_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomePernlDetail_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeSummary_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnHomeOptional_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnThankHome_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnErrormeg_Click(object sender, EventArgs e)
        {
            pnlconfimpaymeny.Visible = true;
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



        private void cmbMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMake.SelectedIndex != 0)
            {
                bindModel(Convert.ToString(cmbMake.SelectedValue));
            }
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
                    //  cmbTaxClasses.Enabled = false;
                    //cmbTaxClasses.SelectedValue = _TaxClass;
                }
                else
                {
                    cmbProducts.Enabled = true;
                    // cmbTaxClasses.Enabled = true;
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
            //cmbTaxClasses.DataSource = filtredTaxClassList;
            //  cmbTaxClasses.DisplayMember = "Description";
            // cmbTaxClasses.ValueMember = "TaxClassId";

            return filtredTaxClassList;
        }

        private void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            //if (txtPartialAmount.Text == "" || txtPartialAmount.Text == "0")
            //{
            //    MyMessageBox.ShowBox("Please enter amount.", "Message");
            //    return;
            //}

            summaryModel = new SummaryDetailModel();

            checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
            // Save all details
            CustomerModel customerModel = new CustomerModel();
            customerModel.FirstName = txtFirstName.Text;
            customerModel.EmailAddress = txtEmailAddress.Text;


            //Save Payment info
            PaymentResult objResult = new PaymentResult();
            long TransactionId = 0;

            decimal transctionAmt = Convert.ToDecimal(txtTotalAmount.Text);

            string paymentTermName = "Swipe";
            summaryModel.PaymentMethodId = Convert.ToInt32(ePaymentMethod.Swipe);
            summaryModel.TotalPremium = transctionAmt;
            summaryModel.TotalSumInsured = 0;
            summaryModel.AmountPaid = transctionAmt;

            if (RadioMobile.Checked)
            {
                paymentTermName = "Mobile";
                summaryModel.PaymentMethodId = Convert.ToInt32(ePaymentMethod.Mobile);
                MyMessageBox.ShowBox("Please Enter Ecocash Mobile number on PinPad.", "Message");
            }
            else
            {
                MyMessageBox.ShowBox("Please swipe the card for making the payment.", "Message");
            }

            Button btnConfirmPayment = (Button)sender;
            btnConfirmPayment.Text = "Processing.";
            btnConfirmPayment.Enabled = false;
            loadingImageConfirmPayment.Visible = true;
            loadingImageConfirmPayment.WaitOnLoad = true;

            TransactionId = GenerateTransactionId();

            SendSymbol(TransactionId, transctionAmt, paymentTermName);
        }


        public void SendSymbol(long TransactionId, decimal transctionAmt, string paymentTermName)
        {
            string xmlString = "";
            //// TransactionId = 100020; // need to do remove
            bool isPaymentDone = false;

            decimal amountIncents = (int)(transctionAmt * 100);


            //To do  
            //summaryModel.PaymentStatus = true;
            //  amountIncents = (int)(Convert.ToDecimal(txtPartialAmount.Text) * 100);


            //Initialze Terminal
            xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
  <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' Action='INIT'/></Esp:Interface>";

            InitializeTermianl("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

            lblPaymentMsg.Text = "Please swipe card.";


            xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
                <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Transaction TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' TransactionId='" + TransactionId + "' Type='PURCHASE' TransactionAmount='" + amountIncents + "'><Esp:PurchasingCardData Description='blah'><Esp:LineItem Description='boh'/><Esp:LineItem Description='beh' Sign='C'><Esp:TaxAmount Type='04'/><Esp:TaxAmount Type='05'/></Esp:LineItem><Esp:Contact Type='BILL_FROM' Name='Ian'/><Esp:Contact Type='BILL_TO' Telephone='021'/><Esp:TaxAmount Type='02'/><Esp:TaxAmount Type='03'/></Esp:PurchasingCardData><Esp:PosStructuredData Name='name' Value='value'/><Esp:PosStructuredData Name='name2' Value='value2'/></Esp:Transaction></Esp:Interface>";


            // isPaymentDone = SendTransaction(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString);

            // isPaymentDone = true;
            //PartialPaymentModel paymentDetail = SavePartialPayment();

            //decimal balanceAmount = Convert.ToDecimal(summaryModel.TotalPremium - paymentDetail.CalulatedPremium);

            //if (balanceAmount > 0)
            //{
            //    // TransactionId = GenerateTransactionId();
            //    btnConfirmPayment.Enabled = true;
            //    pictureBoxZinara.Visible = false;
            //    RadioSwipe.Checked = true;
            //    btnConfirmPayment.Text = "Pay.";
            //  //  txtPartialAmount.Text = balanceAmount.ToString();
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
                if (SendTransaction(ConfigurationManager.AppSettings["url"], ConfigurationManager.AppSettings["Port"], xmlString)) // testing condition false            
                {
                    //if (isPaymentDone) // testing condition false

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
                            pictureBoxZinara.Visible = false;
                            return;
                        }

                        ResultRootObject policyDetailsIceCash = ApproveVRNToIceCash(summaryDetails.Id);

                        string iceCashPolicyNumber = "";
                        if (policyDetailsIceCash.Response != null)
                        {
                            iceCashPolicyNumber = policyDetailsIceCash.Response.PolicyNo;
                        }

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

                        var item = objlistRisk[0];

                        //foreach (var item in objlistRisk)  // for now it's  commented
                        //{
                            item.LicenseId = _licenseId; //m latest license
                            if (!string.IsNullOrEmpty(item.LicenseId) && (item.LicenseId != "0"))
                            {
                                DisplayLicenseDisc(item, parternToken, summaryDetails.VehicleId);
                            }
                        //}

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

                    //  MyMessageBox.ShowBox("Error occured. " + responseMessage, "Message");
                    //   TransactionId = GenerateTransactionId();




                    //lblPaymentMsg.Text = "";
                    //lblPaymentMsg.Text = "Transaction ID =" + TransactionId + ". " + responseMessage;
                    //lblPaymentMsg.Text += "\n";
                    pnlconfimpaymeny.Visible = false;
                    pnlErrormessage.Visible = true;
                    //lblErrMessage.Text = responseMessage;
                    lblErrMessage.Text = responseMessage;
                    lblErrMessage.ForeColor = Color.Red;
                    lblPaymentMsg.Text = "";
                    //lblPaymentMsg.Text = responseMessage;
                    //lblPaymentMsg.ForeColor = Color.Red;

                    // MessageBox.Show("Error occurred during payment.");
                    // btnConfirmPayment.Text = "Pay";

                    btnConfirmPayment.Text = "Pay";

                }
                //SendTransaction("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);

            }
            catch (Exception ex)
            {
                //TransactionId = GenerateTransactionId();
                WriteLog("InitializeTermianl :" + ex.Message);
                lblPaymentMsg.Text += "InitializeTermianl: " + ex.Message;

                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                //closing the terminal
                xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
  <Esp:Interface Version='1.0' xmlns:Esp='http://www.mosaicsoftware.com/Postilion/eSocket.POS/'><Esp:Admin TerminalId='" + ConfigurationManager.AppSettings["TerminalId"] + "' Action ='CLOSE'/></Esp:Interface>";
                InitializeTermianl("" + ConfigurationManager.AppSettings["url"] + "", ConfigurationManager.AppSettings["Port"], xmlString);
                btnConfirmPayment.Enabled = true;
                loadingImageConfirmPayment.Visible = false;
            }
        }



        private List<ResultLicenceIDResponse> DisplayLicenseDisc(RiskDetailModel riskDetailModel, string parterToken, int vehicleId)
        {
            // List<ResultLicenceIDResponse> list = new List<ResultLicenceIDResponse>();

            ResultLicenceIDRootObject quoteresponseResult = IcServiceobj.LICResult(riskDetailModel.LicenseId, parternToken);
            if (quoteresponseResult != null && quoteresponseResult.Response.Message.Contains("Partner Token has expired"))
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

            if (quoteresponseResult.Response != null && quoteresponseResult.Response.LicExpiryDate!=null)
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

            //    MyMessageBox.ShowBox("Pdf not found for this  certificate.", "Modal error message");

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

        public SummaryDetailModel SaveCustomerVehical()
        {
            CustomerVehicalModel objPlanModel = new CustomerVehicalModel();
            SummaryDetailModel summaryDetialsModel = new SummaryDetailModel();

            objlistRisk.Add(objRiskModel);

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
                    // MessageBox.Show("Policy has been sucessfully registred.");


                    //foreach (var item in result.ToList())
                    //{
                    //    objlistVehicalModel.Add(new VehicalModel
                    //    {
                    //        VehicalId = item.VehicalId,
                    //        VRN = item.VRN
                    //    });
                    //}
                }
            }

            return summaryDetialsModel;
        }


        public PartialPaymentModel SavePartialPayment()
        {
            PartialPaymentModel partialPayment = new PartialPaymentModel();
            partialPayment.RegistratonNumber = objRiskModel.RegistrationNo;
            partialPayment.CustomerEmail = customerInfo.EmailAddress;
            // partialPayment.PartialAmount = Convert.ToDecimal(txtPartialAmount.Text);
            partialPayment.CreatedOn = DateTime.Now;


            var client = new RestClient(IceCashRequestUrl + "SavePartailPayment");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("password", "Geninsure@123");
            request.AddHeader("username", "ameyoApi@geneinsure.com");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(partialPayment);
            IRestResponse response = client.Execute(request);

            PartialPaymentModel detail = JsonConvert.DeserializeObject<PartialPaymentModel>(response.Content);
            return detail;
        }

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

                    if (_insuranceAndLicense) // if insurance and license both need to do approve
                    {
                        if (item.InsuranceId != null)
                        {
                            ResultRootObject quoteresponse = ICEcashService.TPIQuoteUpdate(Phonenumber, item, parternToken, 1);
                            if (quoteresponse != null)
                            {

                                if (quoteresponse.Response != null && quoteresponse.Response.Message.Contains("Partner Token has expired"))
                                {

                                    //  ObjToken = CheckParterTokenExpire();
                                    ObjToken = IcServiceobj.getToken();
                                    if (ObjToken != null)
                                        parternToken = ObjToken.Response.PartnerToken;

                                    Service_db.UpdateToken(ObjToken);
                                    quoteresponse = ICEcashService.TPIQuoteUpdate(Phonenumber, item, parternToken, 1);



                                    //ObjToken = IcServiceobj.getToken();
                                    //if (ObjToken != null)
                                    //{
                                    //    parternToken = ObjToken.Response.PartnerToken;
                                    //    quoteresponse = ICEcashService.TPIQuoteUpdate(Phonenumber, item, parternToken, 1);
                                    //}
                                }


                                //if (quoteresponse.Response != null && quoteresponse.Response.Message != "ICEcash System Error [O]")
                                //{
                                resultPolicy = ICEcashService.TPIPolicy(item, parternToken);


                                if (resultPolicy.Response != null && resultPolicy.Response.Message.Contains("Partner Token has expired"))
                                {

                                    // ObjToken = CheckParterTokenExpire();
                                    ObjToken = IcServiceobj.getToken();
                                    if (ObjToken != null)
                                        parternToken = ObjToken.Response.PartnerToken;

                                    Service_db.UpdateToken(ObjToken);

                                    resultPolicy = ICEcashService.TPIPolicy(item, parternToken);



                                    //ObjToken = IcServiceobj.getToken();
                                    //    if (ObjToken != null)
                                    //    {
                                    //        parternToken = ObjToken.Response.PartnerToken;

                                    //        //  vichelDetails.CoverNote = ObjToken.Response.PolicyNo; // it's represent to Cover Note

                                    //        //vichelDetails.CoverNote = ObjToken.Response.PolicyNo; // it's represent to Cover Note

                                    //        resultPolicy = ICEcashService.TPIPolicy(item, parternToken);
                                    //    }
                                }


                                if (resultPolicy.Response != null && resultPolicy.Response.Message.Contains("Policy Retrieved"))
                                {
                                    VehicleUpdateModel objVehicleUpdate = new VehicleUpdateModel();
                                    objVehicleUpdate.VRN = item.RegistrationNo;
                                    objVehicleUpdate.InsuranceStatus = "Approved";

                                    objVehicleUpdate.CoverNote = resultPolicy.Response.PolicyNo;

                                    objVehicleUpdate.SummaryId = Convert.ToString(SummaryId);
                                    UpdateVehicleInfo(objVehicleUpdate);
                                }
                                //}
                            }
                        }

                    }


                    if (item.LicenseId != null)
                    {
                        _clientIdType = "1";
                        if (rdCorporate.Checked)
                            _clientIdType = "2";


                        if (item.RadioLicenseCost > 0 || item.VehicleLicenceFee > 0)
                        {

                            int licenseFreequency = IcServiceobj.GetMonthKey(Convert.ToInt32(item.PaymentTermId));

                            int RadioTVUsage = 1; // for private car

                            if (item.ProductId == 0)
                            {
                                RadioTVUsage = 1;
                            }
                            else if (item.ProductId == 3 || item.ProductId == 11) // for commercial vehicle
                            {
                                RadioTVUsage = 2;
                            }


                            List<VehicleLicQuote> obj = new List<VehicleLicQuote>();
                            obj.Add(new VehicleLicQuote
                            {
                                VRN = txtVrn.Text,
                                IDNumber = customerInfo.NationalIdentificationNumber,
                                ClientIDType = _clientIdType,
                                LicFrequency = licenseFreequency.ToString(),
                                RadioTVUsage = RadioTVUsage.ToString(),
                                RadioTVFrequency = licenseFreequency.ToString()
                            });



                            ResultRootObject quoteresponse = new ResultRootObject();
                            if (item.VehicleLicenceFee > 0 && item.RadioLicenseCost > 0)
                            {
                                quoteresponse = IcServiceobj.LICQuote(obj, parternToken);

                                if (quoteresponse != null && quoteresponse.Response.Message.Contains("Partner Token has expired"))
                                {
                                    // ObjToken = IcServiceobj.getToken();

                                    //  ObjToken = CheckParterTokenExpire();
                                    ObjToken = IcServiceobj.getToken();
                                    if (ObjToken != null)
                                        parternToken = ObjToken.Response.PartnerToken;

                                    Service_db.UpdateToken(ObjToken);

                                    quoteresponse = IcServiceobj.LICQuote(obj, parternToken);
                                }
                            }
                            else if (item.VehicleLicenceFee > 0)
                            {
                                quoteresponse = IcServiceobj.RadioQuote(obj, parternToken);

                                if (quoteresponse != null && quoteresponse.Response.Message.Contains("Partner Token has expired"))
                                {
                                    //  ObjToken = IcServiceobj.getToken();

                                    //  ObjToken = CheckParterTokenExpire();
                                    ObjToken = IcServiceobj.getToken();

                                    if (ObjToken != null)
                                        parternToken = ObjToken.Response.PartnerToken;

                                    Service_db.UpdateToken(ObjToken);

                                    quoteresponse = IcServiceobj.RadioQuote(obj, parternToken);
                                }
                            }

                            else if (item.RadioLicenseCost > 0)
                            {
                                quoteresponse = IcServiceobj.LICQuote(obj, parternToken);

                                if (quoteresponse != null && quoteresponse.Response.Message.Contains("Partner Token has expired"))
                                {
                                    // ObjToken = IcServiceobj.getToken();

                                    //  ObjToken = CheckParterTokenExpire();
                                    ObjToken = IcServiceobj.getToken();
                                    if (ObjToken != null)
                                        parternToken = ObjToken.Response.PartnerToken;

                                    Service_db.UpdateToken(ObjToken);

                                    quoteresponse = IcServiceobj.LICQuote(obj, parternToken);
                                }
                            }

                            // int licenseId = 0;
                            if (quoteresponse.Response != null && quoteresponse.Response.Quotes != null)
                            {
                                item.LicenseId = quoteresponse.Response.Quotes[0].LicenceID;
                                if (quoteresponse.Response.Quotes != null && !(string.IsNullOrEmpty(quoteresponse.Response.Quotes[0].LicenceID)))
                                {
                                    _licenseId = quoteresponse.Response.Quotes[0].LicenceID;
                                }
                                VehicleUpdateModel objVehicleUpdate = new VehicleUpdateModel();
                                objVehicleUpdate.VRN = item.RegistrationNo;
                                objVehicleUpdate.SummaryId = Convert.ToString(SummaryId);
                                objVehicleUpdate.LicenseId = Convert.ToInt32(_licenseId);
                                UpdateVehicleInfo(objVehicleUpdate);
                            }


                            List<VehicleLicQuoteUpdate> vehicleLicenseList = new List<VehicleLicQuoteUpdate>();

                            //PaymentMethod =1 for cash 
                            //PaymentMethod =3 for icecash

                            // VehicleLicQuoteUpdate vehicleLic = new VehicleLicQuoteUpdate { LicenceID = Convert.ToInt32(item.LicenseId), PaymentMethod = 1, DeliveryMethod = 3, Status = "1", LicenceCert = 1 };
                            VehicleLicQuoteUpdate vehicleLic = new VehicleLicQuoteUpdate { LicenceID = Convert.ToInt32(_licenseId), PaymentMethod = 1, DeliveryMethod = 3, Status = "1", LicenceCert = 1 };
                            vehicleLicenseList.Add(vehicleLic);

                            ResultRootObject quoteresponseNew = IcServiceobj.LICQuoteUpdate(vehicleLicenseList, parternToken);

                            if (quoteresponseNew != null && quoteresponseNew.Response.Message.Contains("Partner Token has expired"))
                            {

                                // ObjToken = CheckParterTokenExpire();

                                ObjToken = IcServiceobj.getToken();

                                if (ObjToken != null)
                                    parternToken = ObjToken.Response.PartnerToken;

                                Service_db.UpdateToken(ObjToken);

                                quoteresponseNew = IcServiceobj.LICQuoteUpdate(vehicleLicenseList, parternToken);
                                //ObjToken = IcServiceobj.getToken();
                                //if (ObjToken != null)
                                //{
                                //    parternToken = ObjToken.Response.PartnerToken;
                                //    quoteresponseNew = IcServiceobj.LICQuoteUpdate(vehicleLicenseList, ObjToken.Response.PartnerToken);
                                //}
                            }
                        }



                    }
                }
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

        private void txtEmailAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnHomeCorporate_Click(object sender, EventArgs e)
        {
            GotoHome();
        }

        private void btnCorporateBack_Click(object sender, EventArgs e)
        {
            PnlVrn.Visible = true;
            pnlCorporate.Visible = false;
        }

        private void btnCorporateContinue_Click(object sender, EventArgs e)
        {


            if (txtCompany.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtCompany, "Please enter the Company name");
                txtCompany.Focus();
                return;
            }
            if (txtCmpEmail.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtCmpEmail, "Please enter the email address");
                txtCmpEmail.Focus();
                return;
            }

            if (txtCmpAddress.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtCmpAddress, "Please enter the comapany address");
                txtCmpAddress.Focus();
                return;
            }

            if (txtCmpPhone.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtCmpPhone, "Please enter the phone number");
                txtCmpPhone.Focus();
                return;
            }


            if (cmbCmpCity.SelectedIndex == -1)
            {
                NewerrorProvider.SetError(cmbCmpCity, "Please select the city");
                cmbCmpCity.Focus();
                return;
            }



            if (txtCmpBusinessId.Text == string.Empty)
            {
                NewerrorProvider.SetError(txtIDNumber, "Please enter the Business Id");
                txtCmpBusinessId.Focus();
                return;
            }
            //if (txtZipCode.Text == string.Empty)
            //{
            //    NewerrorProvider.SetError(txtZipCode, "Please enter the zipcode");
            //    txtZipCode.Focus();
            //    return;
            //}


            // pnlsumary.Visible = true;
            //  pnlInsurance.Visible = true;
            //  pnlZinara.Visible=true;


            // var strName = txtFirstName.Text.Split(' ');
            customerInfo.FirstName = txtCompany.Text;
            customerInfo.LastName = txtCompany.Text;

            customerInfo.EmailAddress = txtCmpEmail.Text;
            customerInfo.AddressLine2 = txtCmpAddress.Text;
            // customerInfo.DateOfBirth = Convert.ToDateTime(txtDOB.Text);
            //customerInfo.City = txtCity.Text;
            customerInfo.City = cmbCmpCity.Text;
            customerInfo.PhoneNumber = txtCmpPhone.Text;
            customerInfo.CountryCode = "+263";
            customerInfo.AddressLine1 = txtCmpAddress.Text;
            customerInfo.NationalIdentificationNumber = txtCmpBusinessId.Text;
            customerInfo.CountryCode = cmbCmpCode.SelectedValue.ToString();
            customerInfo.Zipcode = "00263";
            // customerInfo.Gender = rdbFemale



            // customerInfo.Zipcode = txtZipCode.Text;
            customerInfo.BranchId = branchName == "" ? 0 : Convert.ToInt32(branchName);

            pnlCorporate.Visible = false;
            pnlRadioZinara.Visible = true;


        }

        private void txtCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidSpecailCharacter(e);
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

        private void txtVrn_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ValidSpecailCharacter(e);
        }

        private void btnPernalBack_Click(object sender, EventArgs e)
        {
            pnlPersonalDetails.Visible = false;
            PnlVrn.Visible = true;
        }

        private void txtIDNumber_Click(object sender, EventArgs e)
        {
            txtIDNumber.Text = string.Empty;
        }

        private void rdCorporate_CheckedChanged(object sender, EventArgs e)
        {
            textSearchVrn.Text = "Business ID";
        }
    }

}
