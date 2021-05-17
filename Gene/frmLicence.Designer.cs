//using TextBoxKeyboard;

namespace Gene
{
    partial class frmLicence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLicence));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.txtLicVrn = new System.Windows.Forms.TextBox();
            this.btnLicPrint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtLicPdfCode = new System.Windows.Forms.TextBox();
            this.btnPdf = new System.Windows.Forms.Button();
            this.txtOtp = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.ErrorImage")));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(883, 101);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(128, 106);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 113;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Location = new System.Drawing.Point(251, 76);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(158, 76);
            this.btnHome.TabIndex = 2;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // txtLicVrn
            // 
            this.txtLicVrn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicVrn.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicVrn.Location = new System.Drawing.Point(1368, 285);
            this.txtLicVrn.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.txtLicVrn.Name = "txtLicVrn";
            this.txtLicVrn.Size = new System.Drawing.Size(367, 53);
            this.txtLicVrn.TabIndex = 1;
            this.txtLicVrn.Text = "Enter Registration Number";
            this.txtLicVrn.Visible = false;
            this.txtLicVrn.TextChanged += new System.EventHandler(this.txtLicVrn_TextChanged);
            this.txtLicVrn.Enter += new System.EventHandler(this.txtLicVrn_Enter);
            // 
            // btnLicPrint
            // 
            this.btnLicPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnLicPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLicPrint.ForeColor = System.Drawing.Color.White;
            this.btnLicPrint.Location = new System.Drawing.Point(1528, 367);
            this.btnLicPrint.Name = "btnLicPrint";
            this.btnLicPrint.Size = new System.Drawing.Size(158, 76);
            this.btnLicPrint.TabIndex = 0;
            this.btnLicPrint.Text = "Submit";
            this.btnLicPrint.UseVisualStyleBackColor = false;
            this.btnLicPrint.Visible = false;
            this.btnLicPrint.Click += new System.EventHandler(this.btnLicPrint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(280, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(564, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Let\'s get your vehicle details  !!";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // printDoc
            // 
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            this.printPreviewDialog1.Load += new System.EventHandler(this.printPreviewDialog1_Load);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtLicPdfCode
            // 
            this.txtLicPdfCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicPdfCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicPdfCode.Location = new System.Drawing.Point(288, 285);
            this.txtLicPdfCode.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.txtLicPdfCode.Name = "txtLicPdfCode";
            this.txtLicPdfCode.Size = new System.Drawing.Size(614, 53);
            this.txtLicPdfCode.TabIndex = 114;
            this.txtLicPdfCode.Text = "Registration/Pdf Verification Code";
            this.txtLicPdfCode.TextChanged += new System.EventHandler(this.txtLicPdfCode_TextChanged);
            this.txtLicPdfCode.Enter += new System.EventHandler(this.txtLicPdfCode_Enter);
            this.txtLicPdfCode.Leave += new System.EventHandler(this.txtLicPdfCode_Leave);
            // 
            // btnPdf
            // 
            this.btnPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            this.btnPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPdf.ForeColor = System.Drawing.Color.White;
            this.btnPdf.Location = new System.Drawing.Point(640, 381);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Size = new System.Drawing.Size(158, 76);
            this.btnPdf.TabIndex = 115;
            this.btnPdf.Text = "Submit";
            this.btnPdf.UseVisualStyleBackColor = false;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            // 
            // txtOtp
            // 
            this.txtOtp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtp.Location = new System.Drawing.Point(954, 285);
            this.txtOtp.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.txtOtp.Name = "txtOtp";
            this.txtOtp.Size = new System.Drawing.Size(198, 53);
            this.txtOtp.TabIndex = 116;
            this.txtOtp.Text = "OTP";
            this.txtOtp.Visible = false;
            this.txtOtp.Enter += new System.EventHandler(this.txtOtp_Enter);
            // 
            // frmLicence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1730, 779);
            this.Controls.Add(this.txtOtp);
            this.Controls.Add(this.btnPdf);
            this.Controls.Add(this.txtLicPdfCode);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.txtLicVrn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLicPrint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLicence";
            this.Text = "frmLicenceQuote";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
     //   private TextBoxWithKeyboard txtLicVrn;
        private System.Windows.Forms.TextBox txtLicVrn;
        private System.Windows.Forms.Button btnLicPrint;
        private System.Windows.Forms.Label label1;
        private System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtLicPdfCode;
        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.TextBox txtOtp;
    }
}