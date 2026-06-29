using Harness_Traceability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harness_Traceability
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void   Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 F = new Form1();
            F.Visible = false;
            Application.Run(new frmPrincipal());
            //Application.Run(new frmMoodle());
            
            
            //================== Claim Test
            //ClsData.Connect("MOKEMLS001", "ET_DT", "ETDTAdmin", "O}jo75n%iGJ9");
            //Application.Run(new frmClaim());
        }
    }
}
