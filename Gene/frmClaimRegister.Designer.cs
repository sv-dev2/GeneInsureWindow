namespace Gene
{
    partial class frmClaimRegister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblSearchVrnPolicy = new System.Windows.Forms.Label();
            this.txtSearchVrnPolicy = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.pnlClaimNotification = new System.Windows.Forms.Panel();
            this.lblFormat1 = new System.Windows.Forms.Label();
            this.lblformat = new System.Windows.Forms.Label();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtPolicyNo = new System.Windows.Forms.TextBox();
            this.txtVRNNo = new System.Windows.Forms.TextBox();
            this.lblVRNNumber = new System.Windows.Forms.Label();
            this.lblCoverEndDate = new System.Windows.Forms.Label();
            this.lblCoverStartDate = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblPolicyNo = new System.Windows.Forms.Label();
            this.Search = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.pnlClamantDetail = new System.Windows.Forms.Panel();
            this.comboModel = new System.Windows.Forms.ComboBox();
            this.comboMake = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtThirdPartyDamageVal = new System.Windows.Forms.TextBox();
            this.lblThirdPartyDamageVal = new System.Windows.Forms.Label();
            this.txthirdPartyEstimatValOfLoss = new System.Windows.Forms.TextBox();
            this.lblThirdPartyEstimatValOfLoss = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblMake = new System.Windows.Forms.Label();
            this.txtContactDetail = new System.Windows.Forms.TextBox();
            this.lblContactDetail = new System.Windows.Forms.Label();
            this.txtThirdPartyName = new System.Windows.Forms.TextBox();
            this.lblThirdPartyName = new System.Windows.Forms.Label();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.lblThirdPartyInvolvement = new System.Windows.Forms.Label();
            this.datePickDateOfLoss = new System.Windows.Forms.DateTimePicker();
            this.txtEstimatValofLoss = new System.Windows.Forms.TextBox();
            this.txtDescriptionofloss = new System.Windows.Forms.TextBox();
            this.txtPlaceOfLoss = new System.Windows.Forms.TextBox();
            this.txtClaimantName = new System.Windows.Forms.TextBox();
            this.lblDateOfLoss = new System.Windows.Forms.Label();
            this.lblEstimatedValueOfLoss = new System.Windows.Forms.Label();
            this.lblDescriptionOfLoss = new System.Windows.Forms.Label();
            this.lblPlaceOfLoss = new System.Windows.Forms.Label();
            this.lblClaimantName = new System.Windows.Forms.Label();
            this.lblClaimantCustomerDetail = new System.Windows.Forms.Label();
            this.PnlMsg = new System.Windows.Forms.Panel();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.lblSuccesMsg = new System.Windows.Forms.Label();
            this.timerMessage = new System.Windows.Forms.Timer(this.components);
            this.ClaimerrorPro = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlClaimNotification.SuspendLayout();
            this.pnlClamantDetail.SuspendLayout();
            this.PnlMsg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClaimerrorPro)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearchVrnPolicy
            // 
            this.lblSearchVrnPolicy.AutoSize = true;
            this.lblSearchVrnPolicy.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchVrnPolicy.ForeColor = System.Drawing.Color.White;
            this.lblSearchVrnPolicy.Location = new System.Drawing.Point(216, 32);
            this.lblSearchVrnPolicy.Name = "lblSearchVrnPolicy";
            this.lblSearchVrnPolicy.Size = new System.Drawing.Size(441, 46);
            this.lblSearchVrnPolicy.TabIndex = 0;
            this.lblSearchVrnPolicy.Text = "Search Vehicle Reg. Number";
            // 
            // txtSearchVrnPolicy
            // 
            this.txtSearchVrnPolicy.Font = new System.Drawing.Font("Arial Narrow", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchVrnPolicy.Location = new System.Drawing.Point(226, 127);
            this.txtSearchVrnPolicy.Name = "txtSearchVrnPolicy";
            this.txtSearchVrnPolicy.Size = new System.Drawing.Size(510, 57);
            this.txtSearchVrnPolicy.TabIndex = 1;
            this.txtSearchVrnPolicy.TextChanged += new System.EventHandler(this.txtSearchVrnPolicy_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(280, 621);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(258, 95);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(857, 621);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(258, 95);
            this.btnNext.TabIndex = 44;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // pnlClaimNotification
            // 
            this.pnlClaimNotification.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlClaimNotification.BackColor = System.Drawing.Color.Transparent;
            this.pnlClaimNotification.Controls.Add(this.lblFormat1);
            this.pnlClaimNotification.Controls.Add(this.lblformat);
            this.pnlClaimNotification.Controls.Add(this.txtEndDate);
            this.pnlClaimNotification.Controls.Add(this.txtStartDate);
            this.pnlClaimNotification.Controls.Add(this.txtCustomerName);
            this.pnlClaimNotification.Controls.Add(this.txtPolicyNo);
            this.pnlClaimNotification.Controls.Add(this.txtVRNNo);
            this.pnlClaimNotification.Controls.Add(this.lblVRNNumber);
            this.pnlClaimNotification.Controls.Add(this.lblCoverEndDate);
            this.pnlClaimNotification.Controls.Add(this.lblCoverStartDate);
            this.pnlClaimNotification.Controls.Add(this.lblCustomerName);
            this.pnlClaimNotification.Controls.Add(this.lblPolicyNo);
            this.pnlClaimNotification.Controls.Add(this.Search);
            this.pnlClaimNotification.Controls.Add(this.btnNext);
            this.pnlClaimNotification.Controls.Add(this.btnCancel);
            this.pnlClaimNotification.Controls.Add(this.txtSearchVrnPolicy);
            this.pnlClaimNotification.Controls.Add(this.lblSearchVrnPolicy);
            this.pnlClaimNotification.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlClaimNotification.Location = new System.Drawing.Point(596, 161);
            this.pnlClaimNotification.Name = "pnlClaimNotification";
            this.pnlClaimNotification.Size = new System.Drawing.Size(1149, 376);
            this.pnlClaimNotification.TabIndex = 0;
            this.pnlClaimNotification.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlClaimNotification_Paint);
            // 
            // lblFormat1
            // 
            this.lblFormat1.AutoSize = true;
            this.lblFormat1.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormat1.ForeColor = System.Drawing.Color.White;
            this.lblFormat1.Location = new System.Drawing.Point(978, 538);
            this.lblFormat1.Name = "lblFormat1";
            this.lblFormat1.Size = new System.Drawing.Size(152, 31);
            this.lblFormat1.TabIndex = 87;
            this.lblFormat1.Text = "(MM/DD/yyyy)";
            // 
            // lblformat
            // 
            this.lblformat.AutoSize = true;
            this.lblformat.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblformat.ForeColor = System.Drawing.Color.White;
            this.lblformat.Location = new System.Drawing.Point(965, 478);
            this.lblformat.Name = "lblformat";
            this.lblformat.Size = new System.Drawing.Size(152, 31);
            this.lblformat.TabIndex = 86;
            this.lblformat.Text = "(MM/DD/yyyy)";
            // 
            // txtEndDate
            // 
            this.txtEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.txtEndDate.Location = new System.Drawing.Point(591, 524);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.ReadOnly = true;
            this.txtEndDate.Size = new System.Drawing.Size(368, 53);
            this.txtEndDate.TabIndex = 85;
            this.txtEndDate.Text = "__/___/____";
            // 
            // txtStartDate
            // 
            this.txtStartDate.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartDate.Location = new System.Drawing.Point(595, 460);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.ReadOnly = true;
            this.txtStartDate.Size = new System.Drawing.Size(364, 53);
            this.txtStartDate.TabIndex = 84;
            this.txtStartDate.Text = "__/___/____";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.Location = new System.Drawing.Point(595, 383);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(364, 53);
            this.txtCustomerName.TabIndex = 83;
            // 
            // txtPolicyNo
            // 
            this.txtPolicyNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.txtPolicyNo.Location = new System.Drawing.Point(595, 233);
            this.txtPolicyNo.Name = "txtPolicyNo";
            this.txtPolicyNo.ReadOnly = true;
            this.txtPolicyNo.Size = new System.Drawing.Size(364, 53);
            this.txtPolicyNo.TabIndex = 82;
            // 
            // txtVRNNo
            // 
            this.txtVRNNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.txtVRNNo.Location = new System.Drawing.Point(595, 312);
            this.txtVRNNo.Name = "txtVRNNo";
            this.txtVRNNo.ReadOnly = true;
            this.txtVRNNo.Size = new System.Drawing.Size(364, 53);
            this.txtVRNNo.TabIndex = 81;
            // 
            // lblVRNNumber
            // 
            this.lblVRNNumber.AutoSize = true;
            this.lblVRNNumber.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVRNNumber.ForeColor = System.Drawing.Color.White;
            this.lblVRNNumber.Location = new System.Drawing.Point(197, 308);
            this.lblVRNNumber.Name = "lblVRNNumber";
            this.lblVRNNumber.Size = new System.Drawing.Size(215, 46);
            this.lblVRNNumber.TabIndex = 77;
            this.lblVRNNumber.Text = "VRN Number";
            // 
            // lblCoverEndDate
            // 
            this.lblCoverEndDate.AutoSize = true;
            this.lblCoverEndDate.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoverEndDate.ForeColor = System.Drawing.Color.White;
            this.lblCoverEndDate.Location = new System.Drawing.Point(197, 529);
            this.lblCoverEndDate.Name = "lblCoverEndDate";
            this.lblCoverEndDate.Size = new System.Drawing.Size(252, 46);
            this.lblCoverEndDate.TabIndex = 76;
            this.lblCoverEndDate.Text = "Cover End Date";
            // 
            // lblCoverStartDate
            // 
            this.lblCoverStartDate.AutoSize = true;
            this.lblCoverStartDate.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoverStartDate.ForeColor = System.Drawing.Color.White;
            this.lblCoverStartDate.Location = new System.Drawing.Point(194, 458);
            this.lblCoverStartDate.Name = "lblCoverStartDate";
            this.lblCoverStartDate.Size = new System.Drawing.Size(263, 46);
            this.lblCoverStartDate.TabIndex = 75;
            this.lblCoverStartDate.Text = "Cover Start Date";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerName.ForeColor = System.Drawing.Color.White;
            this.lblCustomerName.Location = new System.Drawing.Point(194, 384);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(257, 46);
            this.lblCustomerName.TabIndex = 74;
            this.lblCustomerName.Text = "Customer Name";
            // 
            // lblPolicyNo
            // 
            this.lblPolicyNo.AutoSize = true;
            this.lblPolicyNo.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPolicyNo.ForeColor = System.Drawing.Color.White;
            this.lblPolicyNo.Location = new System.Drawing.Point(197, 229);
            this.lblPolicyNo.Name = "lblPolicyNo";
            this.lblPolicyNo.Size = new System.Drawing.Size(231, 46);
            this.lblPolicyNo.TabIndex = 73;
            this.lblPolicyNo.Text = "Policy Number";
            // 
            // Search
            // 
            this.Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.Search.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search.ForeColor = System.Drawing.Color.White;
            this.Search.Location = new System.Drawing.Point(775, 111);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(219, 85);
            this.Search.TabIndex = 45;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = false;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLogo.BackgroundImage = global::Gene.Properties.Resources.gene_logo;
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLogo.Location = new System.Drawing.Point(1807, 181);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(41, 55);
            this.pnlLogo.TabIndex = 71;
            // 
            // pnlClamantDetail
            // 
            this.pnlClamantDetail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlClamantDetail.BackColor = System.Drawing.Color.Transparent;
            this.pnlClamantDetail.Controls.Add(this.comboModel);
            this.pnlClamantDetail.Controls.Add(this.comboMake);
            this.pnlClamantDetail.Controls.Add(this.btnSave);
            this.pnlClamantDetail.Controls.Add(this.btnBack);
            this.pnlClamantDetail.Controls.Add(this.txtThirdPartyDamageVal);
            this.pnlClamantDetail.Controls.Add(this.lblThirdPartyDamageVal);
            this.pnlClamantDetail.Controls.Add(this.txthirdPartyEstimatValOfLoss);
            this.pnlClamantDetail.Controls.Add(this.lblThirdPartyEstimatValOfLoss);
            this.pnlClamantDetail.Controls.Add(this.lblModel);
            this.pnlClamantDetail.Controls.Add(this.lblMake);
            this.pnlClamantDetail.Controls.Add(this.txtContactDetail);
            this.pnlClamantDetail.Controls.Add(this.lblContactDetail);
            this.pnlClamantDetail.Controls.Add(this.txtThirdPartyName);
            this.pnlClamantDetail.Controls.Add(this.lblThirdPartyName);
            this.pnlClamantDetail.Controls.Add(this.radioButtonNo);
            this.pnlClamantDetail.Controls.Add(this.radioButtonYes);
            this.pnlClamantDetail.Controls.Add(this.lblThirdPartyInvolvement);
            this.pnlClamantDetail.Controls.Add(this.datePickDateOfLoss);
            this.pnlClamantDetail.Controls.Add(this.txtEstimatValofLoss);
            this.pnlClamantDetail.Controls.Add(this.txtDescriptionofloss);
            this.pnlClamantDetail.Controls.Add(this.txtPlaceOfLoss);
            this.pnlClamantDetail.Controls.Add(this.txtClaimantName);
            this.pnlClamantDetail.Controls.Add(this.lblDateOfLoss);
            this.pnlClamantDetail.Controls.Add(this.lblEstimatedValueOfLoss);
            this.pnlClamantDetail.Controls.Add(this.lblDescriptionOfLoss);
            this.pnlClamantDetail.Controls.Add(this.lblPlaceOfLoss);
            this.pnlClamantDetail.Controls.Add(this.lblClaimantName);
            this.pnlClamantDetail.Controls.Add(this.lblClaimantCustomerDetail);
            this.pnlClamantDetail.Font = new System.Drawing.Font("Comic Sans MS", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlClamantDetail.Location = new System.Drawing.Point(239, 54);
            this.pnlClamantDetail.Name = "pnlClamantDetail";
            this.pnlClamantDetail.Size = new System.Drawing.Size(47, 45);
            this.pnlClamantDetail.TabIndex = 72;
            this.pnlClamantDetail.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlClamantDetail_Paint);
            // 
            // comboModel
            // 
            this.comboModel.DropDownHeight = 400;
            this.comboModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.comboModel.FormattingEnabled = true;
            this.comboModel.IntegralHeight = false;
            this.comboModel.Location = new System.Drawing.Point(391, 611);
            this.comboModel.Name = "comboModel";
            this.comboModel.Size = new System.Drawing.Size(214, 46);
            this.comboModel.TabIndex = 100;
            // 
            // comboMake
            // 
            this.comboMake.DropDownHeight = 400;
            this.comboMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.comboMake.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboMake.FormattingEnabled = true;
            this.comboMake.IntegralHeight = false;
            this.comboMake.Location = new System.Drawing.Point(144, 611);
            this.comboMake.Name = "comboMake";
            this.comboMake.Size = new System.Drawing.Size(219, 46);
            this.comboMake.TabIndex = 99;
            this.comboMake.SelectedIndexChanged += new System.EventHandler(this.comboMake_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(930, 692);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(224, 85);
            this.btnSave.TabIndex = 113;
            this.btnSave.Text = "Submit";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(192, 695);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(210, 82);
            this.btnBack.TabIndex = 112;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtThirdPartyDamageVal
            // 
            this.txtThirdPartyDamageVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.txtThirdPartyDamageVal.Location = new System.Drawing.Point(951, 612);
            this.txtThirdPartyDamageVal.Name = "txtThirdPartyDamageVal";
            this.txtThirdPartyDamageVal.Size = new System.Drawing.Size(211, 45);
            this.txtThirdPartyDamageVal.TabIndex = 102;
            this.txtThirdPartyDamageVal.TextChanged += new System.EventHandler(this.txtThirdPartyDamageVal_TextChanged);
            this.txtThirdPartyDamageVal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtThirdPartyDamageVal_KeyPress);
            // 
            // lblThirdPartyDamageVal
            // 
            this.lblThirdPartyDamageVal.AutoSize = true;
            this.lblThirdPartyDamageVal.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThirdPartyDamageVal.ForeColor = System.Drawing.Color.White;
            this.lblThirdPartyDamageVal.Location = new System.Drawing.Point(941, 550);
            this.lblThirdPartyDamageVal.Name = "lblThirdPartyDamageVal";
            this.lblThirdPartyDamageVal.Size = new System.Drawing.Size(173, 40);
            this.lblThirdPartyDamageVal.TabIndex = 110;
            this.lblThirdPartyDamageVal.Text = "Damage Val";
            this.lblThirdPartyDamageVal.Click += new System.EventHandler(this.lblThirdPartyDamageVal_Click);
            // 
            // txthirdPartyEstimatValOfLoss
            // 
            this.txthirdPartyEstimatValOfLoss.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.txthirdPartyEstimatValOfLoss.Location = new System.Drawing.Point(690, 609);
            this.txthirdPartyEstimatValOfLoss.Name = "txthirdPartyEstimatValOfLoss";
            this.txthirdPartyEstimatValOfLoss.Size = new System.Drawing.Size(213, 45);
            this.txthirdPartyEstimatValOfLoss.TabIndex = 101;
            this.txthirdPartyEstimatValOfLoss.TextChanged += new System.EventHandler(this.txthirdPartyEstimatValOfLoss_TextChanged);
            this.txthirdPartyEstimatValOfLoss.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txthirdPartyEstimatValOfLoss_KeyPress);
            // 
            // lblThirdPartyEstimatValOfLoss
            // 
            this.lblThirdPartyEstimatValOfLoss.AutoSize = true;
            this.lblThirdPartyEstimatValOfLoss.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThirdPartyEstimatValOfLoss.ForeColor = System.Drawing.Color.White;
            this.lblThirdPartyEstimatValOfLoss.Location = new System.Drawing.Point(670, 550);
            this.lblThirdPartyEstimatValOfLoss.Name = "lblThirdPartyEstimatValOfLoss";
            this.lblThirdPartyEstimatValOfLoss.Size = new System.Drawing.Size(168, 40);
            this.lblThirdPartyEstimatValOfLoss.TabIndex = 108;
            this.lblThirdPartyEstimatValOfLoss.Text = " Estimat Val";
            this.lblThirdPartyEstimatValOfLoss.Click += new System.EventHandler(this.lblThirdPartyEstimatValOfLoss_Click);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModel.ForeColor = System.Drawing.Color.White;
            this.lblModel.Location = new System.Drawing.Point(390, 550);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(94, 40);
            this.lblModel.TabIndex = 106;
            this.lblModel.Text = "Model";
            this.lblModel.Click += new System.EventHandler(this.lblModel_Click);
            // 
            // lblMake
            // 
            this.lblMake.AutoSize = true;
            this.lblMake.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMake.ForeColor = System.Drawing.Color.White;
            this.lblMake.Location = new System.Drawing.Point(134, 550);
            this.lblMake.Name = "lblMake";
            this.lblMake.Size = new System.Drawing.Size(86, 40);
            this.lblMake.TabIndex = 104;
            this.lblMake.Text = "Make";
            this.lblMake.Click += new System.EventHandler(this.lblMake_Click);
            // 
            // txtContactDetail
            // 
            this.txtContactDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.txtContactDetail.Location = new System.Drawing.Point(135, 484);
            this.txtContactDetail.Name = "txtContactDetail";
            this.txtContactDetail.Size = new System.Drawing.Size(322, 45);
            this.txtContactDetail.TabIndex = 97;
            this.txtContactDetail.TextChanged += new System.EventHandler(this.txtContactDetail_TextChanged);
            // 
            // lblContactDetail
            // 
            this.lblContactDetail.AutoSize = true;
            this.lblContactDetail.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactDetail.ForeColor = System.Drawing.Color.White;
            this.lblContactDetail.Location = new System.Drawing.Point(125, 423);
            this.lblContactDetail.Name = "lblContactDetail";
            this.lblContactDetail.Size = new System.Drawing.Size(195, 40);
            this.lblContactDetail.TabIndex = 102;
            this.lblContactDetail.Text = "Contact Detail";
            this.lblContactDetail.Click += new System.EventHandler(this.lblContactDetail_Click);
            // 
            // txtThirdPartyName
            // 
            this.txtThirdPartyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.txtThirdPartyName.Location = new System.Drawing.Point(701, 481);
            this.txtThirdPartyName.Name = "txtThirdPartyName";
            this.txtThirdPartyName.Size = new System.Drawing.Size(360, 45);
            this.txtThirdPartyName.TabIndex = 98;
            this.txtThirdPartyName.TextChanged += new System.EventHandler(this.txtThirdPartyName_TextChanged);
            // 
            // lblThirdPartyName
            // 
            this.lblThirdPartyName.AutoSize = true;
            this.lblThirdPartyName.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThirdPartyName.ForeColor = System.Drawing.Color.White;
            this.lblThirdPartyName.Location = new System.Drawing.Point(691, 422);
            this.lblThirdPartyName.Name = "lblThirdPartyName";
            this.lblThirdPartyName.Size = new System.Drawing.Size(238, 40);
            this.lblThirdPartyName.TabIndex = 100;
            this.lblThirdPartyName.Text = "Third Party Name";
            this.lblThirdPartyName.Click += new System.EventHandler(this.lblThirdPartyName_Click);
            // 
            // radioButtonNo
            // 
            this.radioButtonNo.AutoSize = true;
            this.radioButtonNo.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonNo.ForeColor = System.Drawing.Color.White;
            this.radioButtonNo.Location = new System.Drawing.Point(904, 359);
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.Size = new System.Drawing.Size(71, 44);
            this.radioButtonNo.TabIndex = 99;
            this.radioButtonNo.TabStop = true;
            this.radioButtonNo.Text = "No";
            this.radioButtonNo.UseVisualStyleBackColor = true;
            this.radioButtonNo.CheckedChanged += new System.EventHandler(this.radioButtonNo_CheckedChanged);
            // 
            // radioButtonYes
            // 
            this.radioButtonYes.AutoSize = true;
            this.radioButtonYes.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonYes.ForeColor = System.Drawing.Color.White;
            this.radioButtonYes.Location = new System.Drawing.Point(726, 359);
            this.radioButtonYes.Name = "radioButtonYes";
            this.radioButtonYes.Size = new System.Drawing.Size(84, 44);
            this.radioButtonYes.TabIndex = 98;
            this.radioButtonYes.TabStop = true;
            this.radioButtonYes.Text = "Yes";
            this.radioButtonYes.UseVisualStyleBackColor = true;
            this.radioButtonYes.CheckedChanged += new System.EventHandler(this.radioButtonYes_CheckedChanged);
            // 
            // lblThirdPartyInvolvement
            // 
            this.lblThirdPartyInvolvement.AutoSize = true;
            this.lblThirdPartyInvolvement.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThirdPartyInvolvement.ForeColor = System.Drawing.Color.White;
            this.lblThirdPartyInvolvement.Location = new System.Drawing.Point(125, 359);
            this.lblThirdPartyInvolvement.Name = "lblThirdPartyInvolvement";
            this.lblThirdPartyInvolvement.Size = new System.Drawing.Size(316, 40);
            this.lblThirdPartyInvolvement.TabIndex = 97;
            this.lblThirdPartyInvolvement.Text = "Third Party Involvement";
            this.lblThirdPartyInvolvement.Click += new System.EventHandler(this.lblThirdPartyInvolvement_Click);
            // 
            // datePickDateOfLoss
            // 
            this.datePickDateOfLoss.CustomFormat = " ";
            this.datePickDateOfLoss.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickDateOfLoss.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickDateOfLoss.Location = new System.Drawing.Point(506, 131);
            this.datePickDateOfLoss.Name = "datePickDateOfLoss";
            this.datePickDateOfLoss.Size = new System.Drawing.Size(302, 46);
            this.datePickDateOfLoss.TabIndex = 93;
            this.datePickDateOfLoss.ValueChanged += new System.EventHandler(this.datePickDateOfLoss_ValueChanged);
            // 
            // txtEstimatValofLoss
            // 
            this.txtEstimatValofLoss.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstimatValofLoss.Location = new System.Drawing.Point(135, 268);
            this.txtEstimatValofLoss.Name = "txtEstimatValofLoss";
            this.txtEstimatValofLoss.Size = new System.Drawing.Size(449, 46);
            this.txtEstimatValofLoss.TabIndex = 95;
            this.txtEstimatValofLoss.TextChanged += new System.EventHandler(this.txtEstimatValofLoss_TextChanged);
            this.txtEstimatValofLoss.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEstimatValofLoss_KeyPress);
            // 
            // txtDescriptionofloss
            // 
            this.txtDescriptionofloss.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.txtDescriptionofloss.Location = new System.Drawing.Point(726, 268);
            this.txtDescriptionofloss.Name = "txtDescriptionofloss";
            this.txtDescriptionofloss.Size = new System.Drawing.Size(428, 45);
            this.txtDescriptionofloss.TabIndex = 96;
            this.txtDescriptionofloss.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtPlaceOfLoss
            // 
            this.txtPlaceOfLoss.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaceOfLoss.Location = new System.Drawing.Point(862, 131);
            this.txtPlaceOfLoss.Name = "txtPlaceOfLoss";
            this.txtPlaceOfLoss.Size = new System.Drawing.Size(292, 46);
            this.txtPlaceOfLoss.TabIndex = 94;
            this.txtPlaceOfLoss.TextChanged += new System.EventHandler(this.txtPlaceOfLoss_TextChanged);
            // 
            // txtClaimantName
            // 
            this.txtClaimantName.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimantName.Location = new System.Drawing.Point(135, 131);
            this.txtClaimantName.Name = "txtClaimantName";
            this.txtClaimantName.Size = new System.Drawing.Size(293, 46);
            this.txtClaimantName.TabIndex = 92;
            this.txtClaimantName.TextChanged += new System.EventHandler(this.txtClaimantName_TextChanged);
            // 
            // lblDateOfLoss
            // 
            this.lblDateOfLoss.AutoSize = true;
            this.lblDateOfLoss.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfLoss.ForeColor = System.Drawing.Color.White;
            this.lblDateOfLoss.Location = new System.Drawing.Point(496, 81);
            this.lblDateOfLoss.Name = "lblDateOfLoss";
            this.lblDateOfLoss.Size = new System.Drawing.Size(183, 40);
            this.lblDateOfLoss.TabIndex = 90;
            this.lblDateOfLoss.Text = "Date Of Loss";
            this.lblDateOfLoss.Click += new System.EventHandler(this.lblDateOfLoss_Click);
            // 
            // lblEstimatedValueOfLoss
            // 
            this.lblEstimatedValueOfLoss.AutoSize = true;
            this.lblEstimatedValueOfLoss.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstimatedValueOfLoss.ForeColor = System.Drawing.Color.White;
            this.lblEstimatedValueOfLoss.Location = new System.Drawing.Point(125, 214);
            this.lblEstimatedValueOfLoss.Name = "lblEstimatedValueOfLoss";
            this.lblEstimatedValueOfLoss.Size = new System.Drawing.Size(298, 40);
            this.lblEstimatedValueOfLoss.TabIndex = 89;
            this.lblEstimatedValueOfLoss.Text = "Estimated Val Of Loss";
            this.lblEstimatedValueOfLoss.Click += new System.EventHandler(this.lblEstimatedValueOfLoss_Click);
            // 
            // lblDescriptionOfLoss
            // 
            this.lblDescriptionOfLoss.AutoSize = true;
            this.lblDescriptionOfLoss.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescriptionOfLoss.ForeColor = System.Drawing.Color.White;
            this.lblDescriptionOfLoss.Location = new System.Drawing.Point(716, 214);
            this.lblDescriptionOfLoss.Name = "lblDescriptionOfLoss";
            this.lblDescriptionOfLoss.Size = new System.Drawing.Size(264, 40);
            this.lblDescriptionOfLoss.TabIndex = 88;
            this.lblDescriptionOfLoss.Text = "Description Of Loss";
            this.lblDescriptionOfLoss.Click += new System.EventHandler(this.lblDescriptionOfLoss_Click);
            // 
            // lblPlaceOfLoss
            // 
            this.lblPlaceOfLoss.AutoSize = true;
            this.lblPlaceOfLoss.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaceOfLoss.ForeColor = System.Drawing.Color.White;
            this.lblPlaceOfLoss.Location = new System.Drawing.Point(852, 81);
            this.lblPlaceOfLoss.Name = "lblPlaceOfLoss";
            this.lblPlaceOfLoss.Size = new System.Drawing.Size(194, 40);
            this.lblPlaceOfLoss.TabIndex = 87;
            this.lblPlaceOfLoss.Text = "Place Of Loss";
            this.lblPlaceOfLoss.Click += new System.EventHandler(this.lblPlaceOfLoss_Click);
            // 
            // lblClaimantName
            // 
            this.lblClaimantName.AutoSize = true;
            this.lblClaimantName.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimantName.ForeColor = System.Drawing.Color.White;
            this.lblClaimantName.Location = new System.Drawing.Point(125, 81);
            this.lblClaimantName.Name = "lblClaimantName";
            this.lblClaimantName.Size = new System.Drawing.Size(211, 40);
            this.lblClaimantName.TabIndex = 86;
            this.lblClaimantName.Text = "Claimant Name";
            this.lblClaimantName.Click += new System.EventHandler(this.lblClaimantName_Click);
            // 
            // lblClaimantCustomerDetail
            // 
            this.lblClaimantCustomerDetail.AutoSize = true;
            this.lblClaimantCustomerDetail.Font = new System.Drawing.Font("Arial Narrow", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimantCustomerDetail.ForeColor = System.Drawing.Color.White;
            this.lblClaimantCustomerDetail.Location = new System.Drawing.Point(232, -7);
            this.lblClaimantCustomerDetail.Name = "lblClaimantCustomerDetail";
            this.lblClaimantCustomerDetail.Size = new System.Drawing.Size(494, 55);
            this.lblClaimantCustomerDetail.TabIndex = 0;
            this.lblClaimantCustomerDetail.Text = "Claimant Customer Detail";
            // 
            // PnlMsg
            // 
            this.PnlMsg.Controls.Add(this.lblMessage2);
            this.PnlMsg.Controls.Add(this.lblSuccesMsg);
            this.PnlMsg.Location = new System.Drawing.Point(59, 29);
            this.PnlMsg.Name = "PnlMsg";
            this.PnlMsg.Size = new System.Drawing.Size(48, 59);
            this.PnlMsg.TabIndex = 73;
            this.PnlMsg.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlMsg_Paint);
            // 
            // lblMessage2
            // 
            this.lblMessage2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage2.AutoSize = true;
            this.lblMessage2.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage2.ForeColor = System.Drawing.Color.White;
            this.lblMessage2.Location = new System.Drawing.Point(111, 334);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(619, 46);
            this.lblMessage2.TabIndex = 1;
            this.lblMessage2.Text = "Our Staff Members will Contact you soon.";
            // 
            // lblSuccesMsg
            // 
            this.lblSuccesMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSuccesMsg.Font = new System.Drawing.Font("Arial Narrow", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuccesMsg.ForeColor = System.Drawing.Color.White;
            this.lblSuccesMsg.Location = new System.Drawing.Point(132, 243);
            this.lblSuccesMsg.Name = "lblSuccesMsg";
            this.lblSuccesMsg.Size = new System.Drawing.Size(8, 23);
            this.lblSuccesMsg.TabIndex = 0;
            this.lblSuccesMsg.Text = "Thanks for submitting the details. ";
            this.lblSuccesMsg.Click += new System.EventHandler(this.lblSuccesMsg_Click);
            // 
            // timerMessage
            // 
            this.timerMessage.Interval = 1000;
            this.timerMessage.Tick += new System.EventHandler(this.timerMessage_Tick);
            // 
            // ClaimerrorPro
            // 
            this.ClaimerrorPro.BlinkRate = 500;
            this.ClaimerrorPro.ContainerControl = this;
            // 
            // frmClaimRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1908, 754);
            this.Controls.Add(this.PnlMsg);
            this.Controls.Add(this.pnlClamantDetail);
            this.Controls.Add(this.pnlLogo);
            this.Controls.Add(this.pnlClaimNotification);
            this.Name = "frmClaimRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeneInsure Claim Register";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmClaimRegister_Load);
            this.pnlClaimNotification.ResumeLayout(false);
            this.pnlClaimNotification.PerformLayout();
            this.pnlClamantDetail.ResumeLayout(false);
            this.pnlClamantDetail.PerformLayout();
            this.PnlMsg.ResumeLayout(false);
            this.PnlMsg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClaimerrorPro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSearchVrnPolicy;
        private System.Windows.Forms.TextBox txtSearchVrnPolicy;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Panel pnlClaimNotification;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtPolicyNo;
        private System.Windows.Forms.TextBox txtVRNNo;
        private System.Windows.Forms.Label lblVRNNumber;
        private System.Windows.Forms.Label lblCoverEndDate;
        private System.Windows.Forms.Label lblCoverStartDate;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblPolicyNo;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.TextBox txtEndDate;
        private System.Windows.Forms.TextBox txtStartDate;
        private System.Windows.Forms.Panel pnlClamantDetail;
        private System.Windows.Forms.Label lblClaimantCustomerDetail;
        private System.Windows.Forms.TextBox txtEstimatValofLoss;
        private System.Windows.Forms.TextBox txtDescriptionofloss;
        private System.Windows.Forms.TextBox txtPlaceOfLoss;
        private System.Windows.Forms.TextBox txtClaimantName;
        private System.Windows.Forms.Label lblDateOfLoss;
        private System.Windows.Forms.Label lblEstimatedValueOfLoss;
        private System.Windows.Forms.Label lblDescriptionOfLoss;
        private System.Windows.Forms.Label lblPlaceOfLoss;
        private System.Windows.Forms.Label lblClaimantName;
        private System.Windows.Forms.DateTimePicker datePickDateOfLoss;
        private System.Windows.Forms.Label lblThirdPartyInvolvement;
        private System.Windows.Forms.RadioButton radioButtonNo;
        private System.Windows.Forms.RadioButton radioButtonYes;
        private System.Windows.Forms.Label lblThirdPartyName;
        private System.Windows.Forms.TextBox txtThirdPartyName;
        private System.Windows.Forms.Label lblContactDetail;
        private System.Windows.Forms.TextBox txtContactDetail;
        private System.Windows.Forms.Label lblMake;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblThirdPartyEstimatValOfLoss;
        private System.Windows.Forms.TextBox txthirdPartyEstimatValOfLoss;
        private System.Windows.Forms.Label lblThirdPartyDamageVal;
        private System.Windows.Forms.TextBox txtThirdPartyDamageVal;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox comboModel;
        private System.Windows.Forms.ComboBox comboMake;
        private System.Windows.Forms.Panel PnlMsg;
        private System.Windows.Forms.Label lblSuccesMsg;
        private System.Windows.Forms.Label lblMessage2;
        private System.Windows.Forms.Timer timerMessage;
        private System.Windows.Forms.Label lblformat;
        private System.Windows.Forms.Label lblFormat1;
        private System.Windows.Forms.ErrorProvider ClaimerrorPro;
    }
}