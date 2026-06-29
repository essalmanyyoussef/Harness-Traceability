namespace Harness_Traceability
{
    partial class frmHostname
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHostname));
            this.txtHarness = new System.Windows.Forms.TextBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.rbSN = new System.Windows.Forms.RadioButton();
            this.rbRef = new System.Windows.Forms.RadioButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSite = new System.Windows.Forms.ComboBox();
            this.rdTT = new System.Windows.Forms.RadioButton();
            this.lbltextboxt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHarness
            // 
            this.txtHarness.Location = new System.Drawing.Point(128, 116);
            this.txtHarness.Name = "txtHarness";
            this.txtHarness.Size = new System.Drawing.Size(337, 20);
            this.txtHarness.TabIndex = 1;
            this.txtHarness.TextChanged += new System.EventHandler(this.txtHarness_TextChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 157);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(583, 391);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(56, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Search by:";
            // 
            // rbSN
            // 
            this.rbSN.AutoSize = true;
            this.rbSN.Checked = true;
            this.rbSN.Location = new System.Drawing.Point(134, 56);
            this.rbSN.Name = "rbSN";
            this.rbSN.Size = new System.Drawing.Size(87, 17);
            this.rbSN.TabIndex = 3;
            this.rbSN.TabStop = true;
            this.rbSN.Text = "Harness S/N";
            this.rbSN.UseVisualStyleBackColor = true;
            this.rbSN.CheckedChanged += new System.EventHandler(this.rbSN_CheckedChanged);
            // 
            // rbRef
            // 
            this.rbRef.AutoSize = true;
            this.rbRef.Location = new System.Drawing.Point(385, 56);
            this.rbRef.Name = "rbRef";
            this.rbRef.Size = new System.Drawing.Size(75, 17);
            this.rbRef.TabIndex = 5;
            this.rbRef.TabStop = true;
            this.rbRef.Text = "Reference";
            this.rbRef.UseVisualStyleBackColor = true;
            this.rbRef.CheckedChanged += new System.EventHandler(this.rbRef_CheckedChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(471, 90);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(106, 46);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.TabStop = false;
            this.simpleButton1.Text = "Find";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(86, 93);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(21, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Site:";
            // 
            // txtSite
            // 
            this.txtSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtSite.FormattingEnabled = true;
            this.txtSite.Items.AddRange(new object[] {
            "K1",
            "K2",
            "MFZ",
            "AA",
            "Skirat",
            "PortSaid",
            "TenthRamadan",
            "SixthOctober",
            "Alba",
            "Deva",
            "Monastir"});
            this.txtSite.Location = new System.Drawing.Point(128, 90);
            this.txtSite.Name = "txtSite";
            this.txtSite.Size = new System.Drawing.Size(337, 21);
            this.txtSite.TabIndex = 0;
            this.txtSite.SelectedIndexChanged += new System.EventHandler(this.txtSite_SelectedIndexChanged);
            // 
            // rdTT
            // 
            this.rdTT.AutoSize = true;
            this.rdTT.Location = new System.Drawing.Point(259, 56);
            this.rdTT.Name = "rdTT";
            this.rdTT.Size = new System.Drawing.Size(88, 17);
            this.rdTT.TabIndex = 4;
            this.rdTT.Text = "Travel Ticket";
            this.rdTT.UseVisualStyleBackColor = true;
            this.rdTT.CheckedChanged += new System.EventHandler(this.rdTT_CheckedChanged);
            // 
            // lbltextboxt
            // 
            this.lbltextboxt.Location = new System.Drawing.Point(12, 114);
            this.lbltextboxt.Name = "lbltextboxt";
            this.lbltextboxt.Size = new System.Drawing.Size(100, 23);
            this.lbltextboxt.TabIndex = 10;
            this.lbltextboxt.Text = "Harness S/N:";
            this.lbltextboxt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmHostname
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 560);
            this.Controls.Add(this.lbltextboxt);
            this.Controls.Add(this.rdTT);
            this.Controls.Add(this.txtSite);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.rbRef);
            this.Controls.Add(this.rbSN);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.txtHarness);
            this.Name = "frmHostname";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hostname Seach";
            this.Load += new System.EventHandler(this.frmHostname_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtHarness;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.RadioButton rbSN;
        private System.Windows.Forms.RadioButton rbRef;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox txtSite;
        private System.Windows.Forms.RadioButton rdTT;
        private System.Windows.Forms.Label lbltextboxt;
    }
}