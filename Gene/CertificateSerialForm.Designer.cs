namespace Gene
{
    partial class CertificateSerialForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificateSerialForm));
            this.PnlLicenceVrn = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtCertificateSerialNumber = new System.Windows.Forms.TextBox();
            this.lblCertificate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PnlLicenceVrn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlLicenceVrn
            // 
            this.PnlLicenceVrn.BackColor = System.Drawing.Color.Transparent;
            this.PnlLicenceVrn.Controls.Add(this.btnSave);
            this.PnlLicenceVrn.Controls.Add(this.btnHome);
            this.PnlLicenceVrn.Controls.Add(this.pictureBox2);
            this.PnlLicenceVrn.Controls.Add(this.btnScan);
            this.PnlLicenceVrn.Controls.Add(this.txtCertificateSerialNumber);
            this.PnlLicenceVrn.Controls.Add(this.lblCertificate);
            this.PnlLicenceVrn.Location = new System.Drawing.Point(-1, 3);
            this.PnlLicenceVrn.Name = "PnlLicenceVrn";
            this.PnlLicenceVrn.Size = new System.Drawing.Size(1924, 793);
            this.PnlLicenceVrn.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnSave.Font = new System.Drawing.Font("Arial Narrow", 32.25F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(968, 161);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(243, 69);
            this.btnSave.TabIndex = 114;
            this.btnSave.Text = "Submit";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnHome.Font = new System.Drawing.Font("Arial Narrow", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Location = new System.Drawing.Point(185, 9);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(164, 78);
            this.btnHome.TabIndex = 113;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Visible = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.ErrorImage")));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(615, 246);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(128, 106);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 112;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnScan.Enabled = false;
            this.btnScan.Font = new System.Drawing.Font("Arial Narrow", 32.25F);
            this.btnScan.ForeColor = System.Drawing.Color.White;
            this.btnScan.Location = new System.Drawing.Point(1314, 272);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(78, 69);
            this.btnScan.TabIndex = 4;
            this.btnScan.Text = "Submit";
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Visible = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtCertificateSerialNumber
            // 
            this.txtCertificateSerialNumber.Font = new System.Drawing.Font("Arial Narrow", 30.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCertificateSerialNumber.Location = new System.Drawing.Point(562, 161);
            this.txtCertificateSerialNumber.Name = "txtCertificateSerialNumber";
            this.txtCertificateSerialNumber.Size = new System.Drawing.Size(336, 54);
            this.txtCertificateSerialNumber.TabIndex = 3;
            this.txtCertificateSerialNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCertificateSerialNumber_KeyDown);
            this.txtCertificateSerialNumber.Leave += new System.EventHandler(this.txtCertificateSerialNumber_Leave);
            // 
            // lblCertificate
            // 
            this.lblCertificate.AutoSize = true;
            this.lblCertificate.Font = new System.Drawing.Font("Arial Narrow", 25F);
            this.lblCertificate.ForeColor = System.Drawing.Color.White;
            this.lblCertificate.Location = new System.Drawing.Point(179, 168);
            this.lblCertificate.Name = "lblCertificate";
            this.lblCertificate.Size = new System.Drawing.Size(327, 40);
            this.lblCertificate.TabIndex = 2;
            this.lblCertificate.Text = "Certificate serial Number";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CertificateSerialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1556, 779);
            this.Controls.Add(this.PnlLicenceVrn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CertificateSerialForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CertificateSerialForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Deactivate += new System.EventHandler(this.CertificateSerialForm_Deactivate);
            this.Load += new System.EventHandler(this.CertificateSerialForm_Load);
            this.PnlLicenceVrn.ResumeLayout(false);
            this.PnlLicenceVrn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlLicenceVrn;
        private System.Windows.Forms.Label lblCertificate;
        private System.Windows.Forms.TextBox txtCertificateSerialNumber;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Timer timer1;
    }
}