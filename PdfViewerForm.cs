using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Harness_Traceability
{
    public partial class PdfViewerForm : Form
    {
        public PdfViewerForm(string pdfPath)
        {
            InitializeComponent();

            // Show loading spinner
            SplashScreenManager.ShowDefaultWaitForm("Loading PDF", "Please wait...");
            SplashScreenManager.Default.SetWaitFormCaption("Loading PDF");
            SplashScreenManager.Default.SetWaitFormDescription("Opening document...");
            try
            {
                pdfViewer1.LoadDocument(pdfPath);
            }
            finally
            {
                // Always close spinner
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        private void PdfViewerForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
