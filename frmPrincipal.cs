using DevExpress.XtraBars;
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
    public partial class frmPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form1 frm = new Form1();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            // Create an instance of Form2
            this.Text = "Harness Traceability Software V 2.0.0 - Developped by Central Test Enginnering";
            this.Hide();
            Form2 form2 = new Form2();

            // Show Form2 as a dialog
            var dialogResult = form2.ShowDialog();

            // Check the result after Form2 closes
            if (dialogResult == DialogResult.OK)
            {
                // Login was successful, show Form1
                this.Show();
            }
            else
            {
                // Login failed or was canceled, close Form1
                this.Close();
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmHarnessSearch frm = new frmHarnessSearch();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Statistics frm = new Statistics();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            string email = "SEWS-ECentralElectricalTestEngineers@sgcci.onmicrosoft.com";
            string subject = "Harness Traceability Software";

            string mailto = $"mailto:{email}?subject={Uri.EscapeDataString(subject)}";

            try
            {
                System.Diagnostics.Process.Start(mailto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create Outlook email.\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            new AboutBox1().ShowDialog();
        }
    }
}