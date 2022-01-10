namespace Gene
{
    partial class Coverdatepopup
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpopupstart = new System.Windows.Forms.DateTimePicker();
            this.btnpopup = new System.Windows.Forms.Button();
            this.dtpopupend = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cover Start Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cover End Date";
            // 
            // dtpopupstart
            // 
            this.dtpopupstart.CustomFormat = "";
            this.dtpopupstart.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpopupstart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpopupstart.Location = new System.Drawing.Point(284, 71);
            this.dtpopupstart.Name = "dtpopupstart";
            this.dtpopupstart.Size = new System.Drawing.Size(200, 35);
            this.dtpopupstart.TabIndex = 2;
            // 
            // btnpopup
            // 
            this.btnpopup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpopup.Location = new System.Drawing.Point(284, 201);
            this.btnpopup.Name = "btnpopup";
            this.btnpopup.Size = new System.Drawing.Size(145, 49);
            this.btnpopup.TabIndex = 4;
            this.btnpopup.Text = "Continue";
            this.btnpopup.UseVisualStyleBackColor = true;
            this.btnpopup.Click += new System.EventHandler(this.btnpopup_Click);
            // 
            // dtpopupend
            // 
            this.dtpopupend.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpopupend.CustomFormat = "";
            this.dtpopupend.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpopupend.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpopupend.Location = new System.Drawing.Point(284, 126);
            this.dtpopupend.Name = "dtpopupend";
            this.dtpopupend.Size = new System.Drawing.Size(200, 35);
            this.dtpopupend.TabIndex = 5;
            // 
            // Coverdatepopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 296);
            this.ControlBox = false;
            this.Controls.Add(this.dtpopupend);
            this.Controls.Add(this.btnpopup);
            this.Controls.Add(this.dtpopupstart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Coverdatepopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coverdatepopup";
            this.Load += new System.EventHandler(this.Coverdatepopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpopupstart;
        private System.Windows.Forms.DateTimePicker dtpopupend;
        private System.Windows.Forms.ComboBox cmbPaymentTerms;
        private System.Windows.Forms.ComboBox cmbPaymentTerm;
        private System.Windows.Forms.Button btnpopup;
        private System.Windows.Forms.ErrorProvider NewerrorProvider;
        
    }
}