using Harness_Traceability.Models;
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Harness_Traceability
{
    public partial class Harness_Details : Form
    {
        DataTable data = new DataTable();
        public Harness_Details()
        {
            InitializeComponent();
        }
        private void Harness_Details_Load(object sender, EventArgs e)
        {
            try
            {

                if (Form1.site == "K1")
                {
                    //Server_Database = "MOKEMLS001";
                    ClsData.Connect("MOKEMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");

                }

                if (Form1.site == "K2")
                {

                    ClsData.Connect("MOASMLS001", "wtr", "wtr", "Sews2012");
                }
                if (Form1.site == "MFZ")
                {
                    ClsData.Connect("MOFZMLS001", "wtr", "wtr", "Sews2006");
                }
                if (Form1.site == "AA")
                {
                    ClsData.Connect("MOAAMLS001", "wtr", "wtr", "Sews2012");
                }
                if (Form1.site == "SKHIRAT")
                {
                    ClsData.Connect("MOSKMLS001", "wtr", "wtr", "Sews2012");
                }
                if (Form1.site == "Port Said")
                {
                    ClsData.Connect("EGPSMLS001", "wtr", "wtr", "Sews2012");
                }
                if (Form1.site == "10 Ramadan")
                {
                    ClsData.Connect("EGTRMLS001", "wtr", "wtr", "Sews2012");
                }
                if (Form1.site == "6 October")
                {
                    ClsData.Connect("EGSOMLS001", "wtr", "wtr", "Sews2012");
                }
                if (Form1.site == "Alba")
                {
                    ClsData.Connect("ROAIMLS001", "wtr", "wtr", "Sews2012");
                }
                if (Form1.site == "Deva")
                {
                    ClsData.Connect("RODVMLS001", "wtr", "wtr", "Sews2012");
                }
                if (Form1.site == "Monastir")
                {
                    ClsData.Connect("TNMOMLS001", "wtr", "wtr", "Sews2012");
                }
                data = ClsOrders.Harness_Details(Form1.Harness_ID, Form1.Hostname);

                if (data.Rows.Count > 0)
                {
                    dataGridView1.DataSource = data;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[4].Visible = false;
                    lblHost.Text = data.Rows[0].ItemArray[0].ToString();
                    lblName.Text = data.Rows[0].ItemArray[1].ToString();
                    lblReference.Text = data.Rows[0].ItemArray[2].ToString();
                    lblTT.Text = data.Rows[0].ItemArray[4].ToString();
                }
                else
                {
                    MessageBox.Show("There is no Details in this stations .. Test Data not synchronized with server from this station");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


    }
}
