using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using RestSharp;
using System.Web.Script.Serialization;
using System.Drawing;

namespace Gene
{
    static class Program
    {   
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          //  Application.Run(new frmQuote());
            Application.Run(new Form1());
            //Application.Run(new frmRenewPolicy());

            //Application.Run(new frmLicenceQuote());
        }    
    }
}
