using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harness_Traceability
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private async void Form3_Load(object sender, EventArgs e)
        {

            //await webView21.EnsureCoreWebView2Async(null);
            //webView21.Source = new Uri("https://app.powerbi.com/links/wZNNNONAnc?ctid=4a1d14a7-833e-4ee8-a4d8-f45cab9c6bf9&pbi_source=linkShare");

            await webView21.EnsureCoreWebView2Async(null);

            string reportUrl = "https://app.powerbi.com/view?r=eyJrIjoiMTIzNDUifQ";
            string tableName = "ET_Output_tbl";
            string columnName = "Customer";
            string filterValue = "STL";

            string fullUrl = $"{reportUrl}&filter={tableName}/{columnName} eq '{filterValue}'";

            webView21.Source = new Uri(fullUrl);
        }
    }
}
