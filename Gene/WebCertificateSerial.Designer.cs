namespace Gene
{
    partial class WebCertificateSerial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebCertificateSerial));
            this.btnScan = new System.Windows.Forms.Button();
            this.PnlLicenceVrn = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtCertificateSerialNumber = new System.Windows.Forms.TextBox();
            this.lblCertificate = new System.Windows.Forms.Label();
            this.PnlLicenceVrn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnScan.Enabled = false;
            this.btnScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScan.ForeColor = System.Drawing.Color.White;
            this.btnScan.Location = new System.Drawing.Point(992, 161);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(199, 68);
            this.btnScan.TabIndex = 4;
            this.btnScan.Text = "Submit";
            this.btnScan.UseVisualStyleBackColor = false;
            // 
            // PnlLicenceVrn
            // 
            this.PnlLicenceVrn.BackColor = System.Drawing.Color.Transparent;
            this.PnlLicenceVrn.Controls.Add(this.btnHome);
            this.PnlLicenceVrn.Controls.Add(this.pictureBox2);
            this.PnlLicenceVrn.Controls.Add(this.btnScan);
            this.PnlLicenceVrn.Controls.Add(this.txtCertificateSerialNumber);
            this.PnlLicenceVrn.Controls.Add(this.lblCertificate);
            this.PnlLicenceVrn.Location = new System.Drawing.Point(0, 2);
            this.PnlLicenceVrn.Name = "PnlLicenceVrn";
            this.PnlLicenceVrn.Size = new System.Drawing.Size(1924, 793);
            this.PnlLicenceVrn.TabIndex = 8;
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
            // txtCertificateSerialNumber
            // 
            this.txtCertificateSerialNumber.Font = new System.Drawing.Font("Arial Narrow", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCertificateSerialNumber.Location = new System.Drawing.Point(562, 161);
            this.txtCertificateSerialNumber.Name = "txtCertificateSerialNumber";
            this.txtCertificateSerialNumber.Size = new System.Drawing.Size(336, 56);
            this.txtCertificateSerialNumber.TabIndex = 3;
            this.txtCertificateSerialNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCertificateSerialNumber_KeyDown);
            // 
            // lblCertificate
            // 
            this.lblCertificate.AutoSize = true;
            this.lblCertificate.Font = new System.Drawing.Font("Arial Narrow", 25F);
            this.lblCertificate.ForeColor = System.Drawing.Color.White;
            this.lblCertificate.Location = new System.Drawing.Point(179, 151);
            this.lblCertificate.Name = "lblCertificate";
            this.lblCertificate.Size = new System.Drawing.Size(327, 40);
            this.lblCertificate.TabIndex = 2;
            this.lblCertificate.Text = "Certificate serial Number";
            // 
            // WebCertificateSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1854, 796);
            this.Controls.Add(this.PnlLicenceVrn);
            this.Name = "WebCertificateSerial";
            this.Text = "WebCertificateSerial";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WebCertificateSerial_Load);
            this.PnlLicenceVrn.ResumeLayout(false);
            this.PnlLicenceVrn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Panel PnlLicenceVrn;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtCertificateSerialNumber;
        private System.Windows.Forms.Label lblCertificate;
    }
}