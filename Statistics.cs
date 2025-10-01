using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Harness_Traceability.Models;
using DevExpress.XtraCharts;
using DevExpress.Charts.Native;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.DashboardCommon;

namespace Harness_Traceability
{
    public partial class Statistics : Form
    {
        DataTable data = new DataTable();
        public Statistics()
        {
            InitializeComponent();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {

            //ClsData.Connect("MOKEMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");

            //// Retrieve the data
            //DataTable data = ClsOrders.Statistics();

            //// Clear existing series
            //chart1.Series.Clear();

            //// Create a new series and set its properties
            //Series series = new Series
            //{
            //    Name = "Statistics",
            //    XValueMember = "Ref",
            //    YValueMembers = "Qty",
            //    ChartType = SeriesChartType.Column,
            //    Color = Color.SteelBlue // Use a modern color
            //};

            //// Customize series appearance
            //series.IsValueShownAsLabel = true; // Show data labels
            //series.LabelForeColor = Color.Black; // Label color
            //series.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Custom font
            //series.ToolTip = "#VALX: #VALY"; // Add tooltips

            //// Add the series to the chart
            //chart1.Series.Add(series);

            //// Set the data source for the chart
            //chart1.DataSource = data;

            //// Bind the data to the chart
            //chart1.DataBind();

            //// Customize chart area
            //ChartArea chartArea = chart1.ChartAreas[0];
            //chartArea.BackColor = Color.LightGray; // Background color
            //chartArea.AxisX.Title = "Ref";
            //chartArea.AxisX.TitleFont = new Font("Segoe UI", 12, FontStyle.Bold);
            //chartArea.AxisX.LabelStyle.ForeColor = Color.Black;
            //chartArea.AxisX.TitleForeColor = Color.Black;
            //chartArea.AxisX.LabelStyle.Angle = -90; // Rotate labels by 90 degrees
            //chartArea.AxisX.Interval = 1;

            //chartArea.AxisY.Title = "Qty";
            //chartArea.AxisY.TitleFont = new Font("Segoe UI", 12, FontStyle.Bold);
            //chartArea.AxisY.LabelStyle.ForeColor = Color.Black;
            //chartArea.AxisY.TitleForeColor = Color.Black;

            //// Customize grid lines
            //chartArea.AxisX.MajorGrid.LineColor = Color.Gray;
            //chartArea.AxisY.MajorGrid.LineColor = Color.Gray;

            //// Customize chart title
            //chart1.Titles.Clear(); // Clear any existing titles
            //Title title = new Title
            //{
            //    Text = "Statistics",
            //    Font = new Font("Segoe UI", 16, FontStyle.Bold),
            //    ForeColor = Color.Black
            //};
            //chart1.Titles.Add(title);

            //// Customize legend
            //chart1.Legends.Clear(); // Clear default legend
            //Legend legend = new Legend
            //{
            //    BackColor = Color.LightGray,
            //    ForeColor = Color.Black,
            //    Font = new Font("Segoe UI", 12, FontStyle.Bold),
            //    Docking = Docking.Top
            //};
            //chart1.Legends.Add(legend);




            // Connect to the database and retrieve data
            // Connect to the database and retrieve data


        }

        private void windowsUIButtonPanel1_Click(object sender, EventArgs e)
        {
            //ClsData.Connect("MOKEMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
            //DataTable data = ClsOrders.Statistics(Date1.Text + " " + Time1.Text, Date2.Text + " " + Time2.Text);

            //// Clear existing series
            //chartControl1.Series.Clear();

            //// Create a new series and set its properties
            //Series series = new Series("Statistics", ViewType.Bar)
            //{
            //    ArgumentDataMember = "Ref"
            //};
            //series.ValueDataMembers.AddRange("Qty");

            //// Add the series to the chart
            //chartControl1.Series.Add(series);

            //// Set the data source for the chart
            //chartControl1.DataSource = data;

            //// Customize the x-axis to display all Ref values
            //XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            //diagram.AxisX.Label.Angle = -90; // Rotate labels by 90 degrees
            //diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            //diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            //diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
            //diagram.AxisX.QualitativeScaleOptions.AutoGrid = false;
            ////diagram.AxisX.QualitativeScaleOptions.GridAlignment = QualitativeScaleGridAlignment.Tickmark;
            //diagram.AxisX.Tickmarks.MinorVisible = false;
            //diagram.AxisX.Tickmarks.Visible = false;
            //diagram.AxisX.Label.Staggered = false;
            //diagram.AxisX.Label.Visible = true;

            //// Adjust axis interval to ensure all labels are shown
            //diagram.AxisX.QualitativeScaleOptions.AutoGrid = false;
            //diagram.AxisX.QualitativeScaleOptions.GridSpacing = 1;

            Sync_Data();
        }

        private async void Sync_Data()
        {
            try
            {
                // Show the ProgressPanel
                progressPanel1.Visible = true;

                // Set the ProgressPanel to be on top and show it
                progressPanel1.BringToFront();
                textBox1.Text = "select Reference, BC AS [Travel Ticket Label], Test AS Station, \r\n       IN_Barcode_1 AS [2nd Label], IN_Barcode_2 AS [3rd Travel Ticket Label], \r\n       IN_Barcode_3 AS [4th Travel Ticket Label], OUT_Barcode AS [Final Label], \r\n       DateEnd AS [Test Date], Rework AS Reworked, Rework_ID AS [Rework ID] , Hostname from correlations_trial whre Hostname = 'MOKEETS001' AND[DateEnd] >= " + Date1.Text + " " + Time1.Text + "AND[DateEnd] <" + Date2.Text + " " + Time2.Text;

                // Perform the data loading and chart update operations asynchronously
                await Task.Run(() =>
                {
                    //ClsData.Connect("MOKEMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                    //DataTable data = ClsOrders.Statistics("MOKEETS001 ",Date1.Text + " " + Time1.Text, Date2.Text + " " + Time2.Text);
                    // Update UI components on the UI thread
                    Invoke(new Action(() =>
                    {
                        //gridControl1.DataSource = data;
                        //// Clear existing series
                        //chartControl1.Series.Clear();

                        //// Create a new series and set its properties
                        //Series series = new Series("Statistics", ViewType.Bar)
                        //{
                        //    ArgumentDataMember = "Ref"
                        //};
                        //series.ValueDataMembers.AddRange("Qty");

                        //// Add the series to the chart
                        //chartControl1.Series.Add(series);

                        //// Set the data source for the chart
                        //chartControl1.DataSource = data;

                        //// Customize the x-axis to display all Ref values
                        //XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                        //diagram.AxisX.Label.Angle = -90; // Rotate labels by 90 degrees
                        //diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                        //diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                        //diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
                        //diagram.AxisX.QualitativeScaleOptions.AutoGrid = false;
                        //diagram.AxisX.Tickmarks.MinorVisible = false;
                        //diagram.AxisX.Tickmarks.Visible = false;
                        //diagram.AxisX.Label.Staggered = false;
                        //diagram.AxisX.Label.Visible = true;

                        //// Adjust axis interval to ensure all labels are shown
                        //diagram.AxisX.QualitativeScaleOptions.AutoGrid = false;
                        //diagram.AxisX.QualitativeScaleOptions.GridSpacing = 1;

                        //// Calculate group summaries.
                        //GridGroupSummaryItem item = new GridGroupSummaryItem();
                        //GridColumn colQty = gridView1.Columns["Qty"];
                        //item.FieldName = colQty.FieldName;
                        ////item.SummaryType = SummaryType.Sum;
                        //item.DisplayFormat = "group total={0:c2}";
                        //item.ShowInGroupColumnFooter = colQty;
                        //gridView1.GroupSummary.Add(item);
                        //DashboardObjectDataSource dataSource = new DashboardObjectDataSource("Statistics Data", data);

                    }));
                });
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Hide the ProgressPanel
                progressPanel1.Visible = false;
            }
        }

        private void dashboardDesigner1_Load(object sender, EventArgs e)
        {

        }

        private void Date1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

}
