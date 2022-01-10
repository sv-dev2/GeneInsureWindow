using GensureAPIv2.Models;
using Insurance.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gene
{

    public partial class Coverdatepopup : Form
    {
        checkVRNwithICEcashResponse response = new checkVRNwithICEcashResponse();
       
        RiskDetailModel RiskModel=new RiskDetailModel();
        List<RiskDetailModel> objlistRisk;
        ResultRootObject quoteresponseQuote;
        public Coverdatepopup()
        {
            InitializeComponent();
           
        }

        private void btnpopup_Click(object sender, EventArgs e)
        {
            Coverdatepopup dtpickr = new Coverdatepopup();
            btnpopup.Text = "Processing.";
            objlistRisk = new List<RiskDetailModel>();
            if (dtpopupstart.Text == null)
            {
                NewerrorProvider.SetError(dtpopupstart, "Please Enter the Startdate");
                dtpopupstart.Focus();
                return;
            }
            if (dtpopupend.Text == null)
            {
                NewerrorProvider.SetError(dtpopupend, "Please Enter the Startdate");
                dtpopupend.Focus();
                return;
            }

            var dtStart = dtpopupstart.AccessibilityObject.Value;
            var dtEnd = dtpopupend.AccessibilityObject.Value;
            var startDate = Convert.ToDateTime(dtStart);
            var endDate = Convert.ToDateTime(dtEnd);
            string enddate = dtpopupstart.ToString();
             
            dtpopupstart.Text = startDate.ToShortDateString();
            dtpopupend.Text = endDate.ToShortDateString();
            //RiskDetailModel objRiskModel = new RiskDetailModel();

            cverpopup.cvrstartdate = dtStart;

            cverpopup.cvrenddate = dtEnd;
            this.Hide();

        }
        public static class cverpopup
        {
            public static string cvrstartdate;
            public static string cvrenddate;
        }
        //private void cmbPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
        private void Coverdatepopup_Load(object sender, EventArgs e)
        {
            //var modal = modal.Text;
            Coverdatepopup dtpickr = new Coverdatepopup();
             //DateTime enddateval = DateTime.Now;
            string s = (sender as Gene.Coverdatepopup ).Text;
            dtpopupend.Text = s;
        }
    }
}
