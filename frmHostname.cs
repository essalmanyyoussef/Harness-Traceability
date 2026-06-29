using Harness_Traceability.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harness_Traceability
{
    public partial class frmHostname : Form
    {
        public frmHostname()
        {
            InitializeComponent();
        }

        private void rbSN_CheckedChanged(object sender, EventArgs e)
        {
            lbltextboxt.Text = "Harness S / N:";

        }

        private void rbRef_CheckedChanged(object sender, EventArgs e)
        {
            lbltextboxt.Text = "Reference:";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtSite.Text == "K1")
            {
                ClsData.Connect("MOKEMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "K2")
            {
                ClsData.Connect("MOASMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "MFZ")
            {
                ClsData.Connect("MOFZMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "AA")
            {
                ClsData.Connect("MOAAMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "Skirat")
            {
                ClsData.Connect("MOSKMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "PortSaid")
            {
                ClsData.Connect("EGPSMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "TenthRamadan")
            {
                ClsData.Connect("EGTRMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "SixthOctober")
            {
                ClsData.Connect("EGSOMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "Alba")
            {
                ClsData.Connect("ROAIMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "Deva")
            {
                ClsData.Connect("RODVMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            if (txtSite.Text == "Monastir")
            {
                ClsData.Connect("TNMOMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            string serial = txtHarness.Text.Replace("\r", "").Replace("\n", "");
            DataTable data = new DataTable();
            if (rbSN.Checked == true)
            {
                data = ClsOrders.GetHostnameByOutBarcode(serial);
            }
            else if (rbRef.Checked==true)
            {
                data = ClsOrders.GetHostnameByRef(serial);
            }
            else if (rdTT.Checked == true)
            {
                data = ClsOrders.GetHostnameByTT(serial);
            }
            gridControl1.DataSource = data;
        }

        private void rdTT_CheckedChanged(object sender, EventArgs e)
        {
            lbltextboxt.Text = "Travel Ticket:";
        }
        public void EnableBtnSerach()
        {
            if(txtHarness.Text.Replace("\r", "").Replace("\n", "")=="" || txtSite.Text=="")
            {
                simpleButton1.Enabled = false;
            }
            else
            {
                simpleButton1.Enabled = true;
            }
        }

        private void frmHostname_Load(object sender, EventArgs e)
        {
            EnableBtnSerach();
        }

        private void txtSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableBtnSerach();
        }

        private void txtHarness_TextChanged(object sender, EventArgs e)
        {
            EnableBtnSerach();
        }
    }
}
