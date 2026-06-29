namespace Harness_Traceability
{
    partial class frmHarnessSearch
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
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView3 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView4 = new DevExpress.XtraCharts.PieSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHarnessSearch));
            this.chartControl2 = new DevExpress.XtraCharts.ChartControl();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.Time1 = new DevExpress.XtraEditors.TimeEdit();
            this.Date1 = new DevExpress.XtraEditors.DateTimeOffsetEdit();
            this.lblLoading = new System.Windows.Forms.Label();
            this.PB_Loading = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Time2 = new DevExpress.XtraEditors.TimeEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.windowsUIButtonPanel1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.Date2 = new DevExpress.XtraEditors.DateTimeOffsetEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.label8 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView4)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl2
            // 
            this.chartControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl2.BackColor = System.Drawing.Color.Transparent;
            this.chartControl2.IndicatorsPaletteName = "Chameleon";
            this.chartControl2.Location = new System.Drawing.Point(1170, 4);
            this.chartControl2.Name = "chartControl2";
            this.chartControl2.PaletteName = "Slipstream";
            series3.Name = "Series 1";
            series3.SeriesID = 0;
            series3.View = pieSeriesView3;
            this.chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.chartControl2.Size = new System.Drawing.Size(669, 303);
            this.chartControl2.TabIndex = 27;
            // 
            // txtHostname
            // 
            this.txtHostname.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHostname.Location = new System.Drawing.Point(123, 33);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(170, 29);
            this.txtHostname.TabIndex = 16;
            // 
            // Time1
            // 
            this.Time1.EditValue = new System.DateTime(2024, 7, 29, 0, 0, 0, 0);
            this.Time1.Location = new System.Drawing.Point(360, 74);
            this.Time1.Name = "Time1";
            this.Time1.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time1.Properties.Appearance.Options.UseFont = true;
            this.Time1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Time1.Size = new System.Drawing.Size(112, 38);
            this.Time1.TabIndex = 13;
            // 
            // Date1
            // 
            this.Date1.EditValue = null;
            this.Date1.Location = new System.Drawing.Point(123, 76);
            this.Date1.Name = "Date1";
            this.Date1.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date1.Properties.Appearance.Options.UseFont = true;
            this.Date1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date1.Properties.MaskSettings.Set("mask", "yyyy-MM-dd");
            this.Date1.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.Date1.Properties.MaskSettings.Set("culture", "en-GB");
            this.Date1.Properties.UseMaskAsDisplayFormat = true;
            this.Date1.Size = new System.Drawing.Size(170, 38);
            this.Date1.TabIndex = 4;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("Ubuntu", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.Location = new System.Drawing.Point(251, 426);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(135, 24);
            this.lblLoading.TabIndex = 29;
            this.lblLoading.Text = "Loading data ..";
            // 
            // PB_Loading
            // 
            this.PB_Loading.Image = global::Harness_Traceability.Properties.Resources.Loading;
            this.PB_Loading.Location = new System.Drawing.Point(246, 280);
            this.PB_Loading.Name = "PB_Loading";
            this.PB_Loading.Size = new System.Drawing.Size(148, 143);
            this.PB_Loading.TabIndex = 28;
            this.PB_Loading.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Hostname :";
            // 
            // Time2
            // 
            this.Time2.EditValue = new System.DateTime(2024, 7, 29, 0, 0, 0, 0);
            this.Time2.Location = new System.Drawing.Point(360, 122);
            this.Time2.Name = "Time2";
            this.Time2.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time2.Properties.Appearance.Options.UseFont = true;
            this.Time2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Time2.Size = new System.Drawing.Size(112, 38);
            this.Time2.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "From :";
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(12, 203);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1152, 666);
            this.gridControl1.TabIndex = 26;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gridView1.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // windowsUIButtonPanel1
            // 
            this.windowsUIButtonPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            windowsUIButtonImageOptions2.Image = global::Harness_Traceability.Properties.Resources.system_search;
            this.windowsUIButtonPanel1.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Search", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton()});
            this.windowsUIButtonPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButtonPanel1.Location = new System.Drawing.Point(478, 89);
            this.windowsUIButtonPanel1.Name = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.Size = new System.Drawing.Size(79, 71);
            this.windowsUIButtonPanel1.TabIndex = 10;
            this.windowsUIButtonPanel1.Text = "windowsUIButtonPanel1";
            this.windowsUIButtonPanel1.Click += new System.EventHandler(this.windowsUIButtonPanel1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "To :";
            // 
            // Date2
            // 
            this.Date2.EditValue = null;
            this.Date2.Location = new System.Drawing.Point(123, 122);
            this.Date2.Name = "Date2";
            this.Date2.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date2.Properties.Appearance.Options.UseFont = true;
            this.Date2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date2.Properties.MaskSettings.Set("mask", "yyyy-MM-dd");
            this.Date2.Size = new System.Drawing.Size(170, 38);
            this.Date2.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(520, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Time :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(318, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Time :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date :";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.btnExport);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.richTextBox1);
            this.groupControl1.Controls.Add(this.txtHostname);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.Time2);
            this.groupControl1.Controls.Add(this.Time1);
            this.groupControl1.Controls.Add(this.windowsUIButtonPanel1);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.Date2);
            this.groupControl1.Controls.Add(this.Date1);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1152, 185);
            this.groupControl1.TabIndex = 25;
            this.groupControl1.Text = "User selection";
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(299, 33);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButton1.Size = new System.Drawing.Size(192, 35);
            this.simpleButton1.TabIndex = 24;
            this.simpleButton1.Text = "Get Station Hostname";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(318, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Time :";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1676, 23);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(75, 207);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            // 
            // chartControl1
            // 
            this.chartControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl1.BackColor = System.Drawing.Color.Transparent;
            this.chartControl1.IndicatorsPaletteName = "Chameleon";
            this.chartControl1.Location = new System.Drawing.Point(1170, 313);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.PaletteName = "Slipstream";
            series4.Name = "Series 1";
            series4.SeriesID = 0;
            series4.View = pieSeriesView4;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series4};
            this.chartControl1.Size = new System.Drawing.Size(663, 556);
            this.chartControl1.TabIndex = 30;
            // 
            // btnExport
            // 
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.btnExport.Location = new System.Drawing.Point(563, 89);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(159, 71);
            this.btnExport.TabIndex = 25;
            this.btnExport.Text = "Export to Excel ";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // frmHarnessSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1845, 881);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.PB_Loading);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.chartControl2);
            this.Name = "frmHarnessSearch";
            this.Text = "Harness S/N Search";
            this.Load += new System.EventHandler(this.frmHarnessSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl2;
        private System.Windows.Forms.TextBox txtHostname;
        private DevExpress.XtraEditors.TimeEdit Time1;
        private DevExpress.XtraEditors.DateTimeOffsetEdit Date1;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.PictureBox PB_Loading;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TimeEdit Time2;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButtonPanel1;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.DateTimeOffsetEdit Date2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnExport;
    }
}