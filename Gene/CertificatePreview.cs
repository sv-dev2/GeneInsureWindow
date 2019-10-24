using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gene
{
    public partial class CertificatePreview : Form
    {

        string _filePath = "";
        public CertificatePreview(string filePath)
        {
            InitializeComponent();

            _filePath = filePath;
            //  webBrowser1.Navigate(filePath);
        }

        private void CertificatePreview_Load(object sender, EventArgs e)
        {
            // webBrowser1.Navigate(new Uri(_filePath));

            //   PrintHelpPage(_filePath);
            //  printButton_Click(sender, e);

            PrintReport(_filePath);

        }



        private void printButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Print();
        }




        private void PrintReport(string path)
        {
            // Create a WebBrowser instance. 
            // WebBrowser webBrowserForPrinting = new WebBrowser();

            // Add an event handler that prints the document after it loads.
            //webBrowser1.DocumentCompleted +=
            //    new WebBrowserDocumentCompletedEventHandler(PrintDocument);

            // Set the Url property to load the document.
            webBrowser1.Url = new Uri(path);
        }

        private void PrintDocument(object sender,
            WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }

        //public  void PrintReport(string path)
        //{

        //    webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        //    webBrowser1.Navigate(path);
        //}

        private  void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           // WebBrowser wb = (WebBrowser)sender;
            //if (webBrowser1.ReadyState.Equals(WebBrowserReadyState.Complete))
            //    webBrowser1.ShowPrintDialog();
        }



    }
}
