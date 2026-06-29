using DevExpress.XtraEditors;
using Harness_Traceability.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Harness_Traceability
{
    public partial class frmClaim : Form
    {
        // Store attachments
        List<string> attachmentFiles = new List<string>();

        // Temporary user ID until login system is added
        int LoggedUserId = 1;

        public frmClaim()
        {
            InitializeComponent();
            LoadSuppliers();
            LoadSites();
            InitializeComboBoxes();
            dateDueDate.EditValue = DateTime.Now.AddDays(5);
        }


        private void frmClaim_Load(object sender, EventArgs e)
        {

        }

        // Load suppliers from database
        private void LoadSuppliers()
        {
            DataTable dt = ClsData.ExecuteView(
                "Suppliers",
                "SupplierId, SupplierName, ContactEmail",
                "IsActive = 1"
            );

            lookupSupplier.Properties.DataSource = dt;
            lookupSupplier.Properties.DisplayMember = "SupplierName";
            lookupSupplier.Properties.ValueMember = "SupplierId";

            // Clear auto-generated columns
            lookupSupplier.Properties.Columns.Clear();

            // Add columns manually (safe)
            lookupSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", "Supplier"));
            lookupSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ContactEmail", "Email"));

            // Hide ID
            lookupSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierId", "ID", 0));
            lookupSupplier.Properties.Columns["SupplierId"].Visible = false;
        }




        // Load sites from database
        private void LoadSites()
        {
            DataTable dt = ClsData.ExecuteView("Sites", "SiteId, SiteName", "1=1");

            lookupSite.Properties.DataSource = dt;
            lookupSite.Properties.DisplayMember = "SiteName";
            lookupSite.Properties.ValueMember = "SiteId";
        }

        private void InitializeComboBoxes()
        {
            cmbIssueType.Properties.Items.AddRange(new[] { "Delivery", "Quality", "Service" });
            cmbSeverity.Properties.Items.AddRange(new[] { "Low", "Medium", "High", "Critical" });
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = true;
                dlg.Filter = "Images|*.jpg;*.jpeg;*.png;*.pdf";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in dlg.FileNames)
                    {
                        attachmentFiles.Add(file);
                        lstAttachments.Items.Add(Path.GetFileName(file));
                    }
                }
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) return false;
            if (string.IsNullOrWhiteSpace(memoDescription.Text)) return false;
            if (cmbIssueType.EditValue == null) return false;
            if (cmbSeverity.EditValue == null) return false;
            if (lookupSupplier.EditValue == null) return false;
            if (lookupSite.EditValue == null) return false;
            if (dateDueDate.EditValue == null) return false;

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                XtraMessageBox.Show("Please fill all required fields.");
                return;
            }

            // 1. Insert claim
            int claimId = ClsClaims.InsertClaim(
                txtTitle.Text.Trim(),
                memoDescription.Text.Trim(),
                cmbIssueType.Text,
                cmbSeverity.Text,
                Convert.ToInt32(lookupSupplier.EditValue),
                Convert.ToInt32(lookupSite.EditValue),
                txtConnectorPN.Text.Trim(),
                txtAttachmentPN.Text.Trim(),
                Convert.ToDateTime(dateDueDate.EditValue),
                LoggedUserId
            );

            // 2. Insert event
            ClsClaims.InsertEvent(claimId, "Claim Created", "New", LoggedUserId);

            // 3. Insert attachments
            foreach (string filePath in attachmentFiles)
            {
                byte[] data = File.ReadAllBytes(filePath);

                ClsClaims.InsertAttachment(
                    claimId,
                    Path.GetFileName(filePath),
                    Path.GetExtension(filePath).Replace(".", ""),
                    data,
                    LoggedUserId
                );
            }

            XtraMessageBox.Show("Claim created successfully.");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
