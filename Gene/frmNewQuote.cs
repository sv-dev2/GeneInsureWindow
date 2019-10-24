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
namespace Gene
{
    public partial class frmNewQuote : Form
    {
        String ApiURL = "http://api.gene.co.zw/api/Account/";
        String username = "ameyoApi@geneinsure.com";
        String Pwd = "Geninsure@123";
        public frmNewQuote()
        {
            InitializeComponent();
            bindCity();
        }
        public void bindCity()
        {
            var client = new RestClient(ApiURL + "AllCities");
            var request = new RestRequest(Method.GET);
            request.AddHeader("password", Pwd);
            request.AddHeader("username", username);
            request.AddParameter("application/json", "{\n\t\"Name\":\"ghj\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = (new JavaScriptSerializer()).Deserialize<List<RootObject>>(response.Content);
            cmbCity.DataSource = result;
           
            cmbCity.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox9_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkPassenger_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassenger.Checked)
            {
                lblNoPer.Visible = true;
                cmbNoofPerson.Visible = true;
            }
            else
            {

                lblNoPer.Visible = false;
                cmbNoofPerson.Visible = false;
            }
        }

        private void cmbCity_KeyDown(object sender, KeyEventArgs e)
        {
            // cmbCity.DropDown = false;
        }
    }
}
