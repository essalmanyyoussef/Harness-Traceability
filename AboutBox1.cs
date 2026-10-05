using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harness_Traceability
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void AboutBox1_Load(object sender, EventArgs e)
        {
            labelProductName.Text = "Harness Traceability Software";
            labelVersion.Text = "Version : 2.0.0";
            labelCopyright.Text = "Copyright - SEWS-E Central Electrical Test Team 2026";
            textBoxDescription.Text = "Harness Traceability V2.0.0 is a centralized software solution that provides quick and reliable access to electrical harness test data across all SEWS-E sites. It enables users to retrieve traceability information, investigate defects, analyze production data, and generate reports through a single platform.\r\n\r\nWith this software, users can:\r\n     - Retrieve complete harness traceability information and review all test processes performed throughout the manufacturing journey.\r\n     - Generate professional PDF traceability reports containing test results, error details, and graphical error analysis.\r\n     - Identify and analyze defective harnesses within a selected period to support quality investigations and corrective actions.\r\n     - Retrieve all tested Harness S/Ns from a selected station and date range.\r\n     - Monitor production performance through dynamic charts, statistics, and reference distribution analysis.\r\n     - Export search results to Excel for further analysis, reporting, and information sharing.\r\n     - Use advanced filtering tools to quickly locate and analyze specific production data.\r\n\r\nThe software is designed with a modern and user-friendly interface that simplifies navigation and improves access to critical information. By centralizing traceability, defect analysis, reporting, and production analytics, Harness Traceability V2.0.0 helps reduce investigation time, improve data accuracy, and support better decision-making across Production, Quality, and Engineering teams.\r\n\r\nDeveloped by: SEWS-E Central Electrical Test Team\r\n\r\nSupport Contact:\r\nSEWS-ECentralElectricalTestEngineers@sgcci.onmicrosoft.com\r\n";
        }
    }
}
