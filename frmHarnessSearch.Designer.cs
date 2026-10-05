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
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHarnessSearch));
            this.chartControl2 = new DevExpress.XtraCharts.ChartControl();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.Time1 = new DevExpress.XtraEditors.TimeEdit();
            this.Date1 = new DevExpress.XtraEditors.DateTimeOffsetEdit();
            this.lblLoading = new System.Windows.Forms.Label();
            this.PB_Loading = new System.Windows.Forms.PictureBox();
            this.Time2 = new DevExpress.XtraEditors.TimeEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Date2 = new DevExpress.XtraEditors.DateTimeOffsetEdit();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl2
            // 
            this.chartControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl2.BackColor = System.Drawing.Color.Transparent;
            this.chartControl2.IndicatorsPaletteName = "Chameleon";
            this.chartControl2.Location = new System.Drawing.Point(1101, 4);
            this.chartControl2.Name = "chartControl2";
            this.chartControl2.PaletteName = "Slipstream";
            series1.Name = "Series 1";
            series1.SeriesID = 0;
            series1.View = pieSeriesView1;
            this.chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl2.Size = new System.Drawing.Size(739, 379);
            this.chartControl2.TabIndex = 27;
            // 
            // txtHostname
            // 
            this.txtHostname.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHostname.Location = new System.Drawing.Point(458, 24);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(170, 29);
            this.txtHostname.TabIndex = 16;
            // 
            // Time1
            // 
            this.Time1.EditValue = new System.DateTime(2024, 7, 29, 0, 0, 0, 0);
            this.Time1.Location = new System.Drawing.Point(718, 65);
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
            this.Date1.Location = new System.Drawing.Point(458, 65);
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
            this.lblLoading.Location = new System.Drawing.Point(393, 389);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(135, 24);
            this.lblLoading.TabIndex = 29;
            this.lblLoading.Text = "Loading data ..";
            // 
            // PB_Loading
            // 
            this.PB_Loading.Image = global::Harness_Traceability.Properties.Resources.Loading;
            this.PB_Loading.Location = new System.Drawing.Point(383, 243);
            this.PB_Loading.Name = "PB_Loading";
            this.PB_Loading.Size = new System.Drawing.Size(159, 143);
            this.PB_Loading.TabIndex = 28;
            this.PB_Loading.TabStop = false;
            // 
            // Time2
            // 
            this.Time2.EditValue = new System.DateTime(2024, 7, 29, 0, 0, 0, 0);
            this.Time2.Location = new System.Drawing.Point(718, 109);
            this.Time2.Name = "Time2";
            this.Time2.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time2.Properties.Appearance.Options.UseFont = true;
            this.Time2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Time2.Size = new System.Drawing.Size(112, 38);
            this.Time2.TabIndex = 14;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(12, 218);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1083, 651);
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
            // Date2
            // 
            this.Date2.EditValue = null;
            this.Date2.Location = new System.Drawing.Point(458, 109);
            this.Date2.Name = "Date2";
            this.Date2.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date2.Properties.Appearance.Options.UseFont = true;
            this.Date2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date2.Properties.MaskSettings.Set("mask", "yyyy-MM-dd");
            this.Date2.Size = new System.Drawing.Size(170, 38);
            this.Date2.TabIndex = 5;
            // 
            // chartControl1
            // 
            this.chartControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl1.BackColor = System.Drawing.Color.Transparent;
            this.chartControl1.IndicatorsPaletteName = "Chameleon";
            this.chartControl1.Location = new System.Drawing.Point(1101, 389);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.PaletteName = "Slipstream";
            series2.Name = "Series 1";
            series2.SeriesID = 0;
            series2.View = pieSeriesView2;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartControl1.Size = new System.Drawing.Size(732, 480);
            this.chartControl1.TabIndex = 30;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.label9);
            this.groupControl2.Controls.Add(this.pictureBox2);
            this.groupControl2.Controls.Add(this.label10);
            this.groupControl2.Controls.Add(this.label11);
            this.groupControl2.Controls.Add(this.txtHostname);
            this.groupControl2.Controls.Add(this.Time2);
            this.groupControl2.Controls.Add(this.simpleButton5);
            this.groupControl2.Controls.Add(this.Time1);
            this.groupControl2.Controls.Add(this.simpleButton4);
            this.groupControl2.Controls.Add(this.simpleButton3);
            this.groupControl2.Controls.Add(this.simpleButton2);
            this.groupControl2.Controls.Add(this.simpleButton6);
            this.groupControl2.Controls.Add(this.Date2);
            this.groupControl2.Controls.Add(this.richTextBox2);
            this.groupControl2.Controls.Add(this.Date1);
            this.groupControl2.Controls.Add(this.label12);
            this.groupControl2.Controls.Add(this.label13);
            this.groupControl2.Controls.Add(this.label14);
            this.groupControl2.Location = new System.Drawing.Point(12, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1083, 200);
            this.groupControl2.TabIndex = 31;
            this.groupControl2.Text = "User selection";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Berlin Sans FB", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(332, 32);
            this.label9.TabIndex = 35;
            this.label9.Text = "Station Production History";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Harness_Traceability.Properties.Resources.Statistics;
            this.pictureBox2.Location = new System.Drawing.Point(11, 23);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(304, 128);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 34;
            this.pictureBox2.TabStop = false;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(659, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 20);
            this.label10.TabIndex = 33;
            this.label10.Text = "Time :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(659, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 20);
            this.label11.TabIndex = 32;
            this.label11.Text = "Time :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // simpleButton5
            // 
            this.simpleButton5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton5.ImageOptions.Image")));
            this.simpleButton5.Location = new System.Drawing.Point(662, 24);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(173, 33);
            this.simpleButton5.TabIndex = 8;
            this.simpleButton5.Text = "Hostname Search";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
            this.simpleButton4.Location = new System.Drawing.Point(686, 153);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(149, 33);
            this.simpleButton4.TabIndex = 7;
            this.simpleButton4.Text = "Close";
            // 
            // simpleButton3
            // 
            this.simpleButton3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(529, 153);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(149, 33);
            this.simpleButton3.TabIndex = 6;
            this.simpleButton3.Text = "Export";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(372, 153);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(149, 33);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Search";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton6
            // 
            this.simpleButton6.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton6.ImageOptions.Image")));
            this.simpleButton6.Location = new System.Drawing.Point(12, 487);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(981, 42);
            this.simpleButton6.TabIndex = 26;
            this.simpleButton6.Text = "Export to Excel ";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(1676, 23);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(75, 207);
            this.richTextBox2.TabIndex = 20;
            this.richTextBox2.Text = "";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(369, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 38);
            this.label12.TabIndex = 15;
            this.label12.Text = "Hostname :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(414, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 20);
            this.label13.TabIndex = 9;
            this.label13.Text = "To :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(375, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 24);
            this.label14.TabIndex = 8;
            this.label14.Text = "From :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmHarnessSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1845, 881);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.PB_Loading);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.chartControl2);
            this.Name = "frmHarnessSearch";
            this.Text = "Harness S/N Search";
            this.Load += new System.EventHandler(this.frmHarnessSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private DevExpress.XtraEditors.TimeEdit Time2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.DateTimeOffsetEdit Date2;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}