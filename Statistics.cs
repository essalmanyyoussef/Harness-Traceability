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
using DevExpress.DashboardCommon.Viewer;
using DevExpress.XtraGrid.Views.Grid;

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
            gridView1.ColumnFilterChanged += GridView1_ColumnFilterChanged;
            gridControl1.Visible = false;
            chartControl1.Visible = false;
            chartControl2.Visible = false;
            lblLoading.Visible = false;
            PB_Loading.Visible = false;

        }
        string Error_Type;

        private async void windowsUIButtonPanel1_Click(object sender, EventArgs e)
        {
            // Show loading UI

            string hostname = txtHostname.Text.Trim();

            if (hostname.Length < 4)
            {
                MessageBox.Show("Hostname is too short. Please enter a valid Hostname.");
                return;
            }

            string prefix = hostname.Substring(0, 4).ToUpper();
            Dictionary<string, string> serverMap = new Dictionary<string, string>
{
                { "MOAS", "MOASMLS001" },
                { "MOFZ", "MOFZMLS001" },
                { "MOKE", "MOKEMLS001" },
                { "MOAA", "MOAAMLS001" },
                { "MOSK", "MOSKMLS001" },
                { "EGPS", "EGPSMLS001" },
                { "EGTR", "EGTRMLS001" },
                { "EGSO", "EGSOMLS001" },
                { "ROAI", "ROAIMLS001" },
                { "RODV", "RODVMLS001" },
                { "TNMO", "TNMOMLS001" }
            };

            string Server;

            if (serverMap.TryGetValue(prefix, out Server))
            {
                // Valid hostname → connect
                ClsData.Connect(Server, "wtr", "wtrviewuser", "alarm-S7D46S");
            }
            else
            {
                // Invalid hostname → show error
                MessageBox.Show("This Hostname doesn't exist, please check and enter the correct Hostname.");
                return;
            }

            DateTime startDate;
            DateTime endDate;

            bool ok1 = DateTime.TryParse(Date1.Text + " " + Time1.Text, out startDate);
            bool ok2 = DateTime.TryParse(Date2.Text + " " + Time2.Text, out endDate);

            if (!ok1 || !ok2)
            {
                MessageBox.Show("Invalid date or time format.");
                return;
            }

            // Check order
            if (startDate >= endDate)
            {
                MessageBox.Show("Start date must be earlier than end date.");
                return;
            }

            // Check 30‑day range
            if ((endDate - startDate).TotalDays > 30)
            {
                MessageBox.Show("Date range cannot exceed 30 days.");
                return;
            }


            PB_Loading.Visible = true;
            lblLoading.Visible = true;
            gridControl1.Visible = false;
            chartControl1.Visible = false;
            chartControl2.Visible = false;

            PB_Loading.BringToFront();

            Error_Type = "";



            try
            {
                // Run heavy work on background thread
                DataTable data = await Task.Run(() =>
                {
                    // Build AdditionalInfo
                    string cleanDate1 = Date1.Text.Split(' ')[0];
                    string cleanDate2 = Date2.Text.Split(' ')[0];

                    string dateTime1 = $"{cleanDate1}T{Time1.Text}";
                    string dateTime2 = $"{cleanDate2}T{Time2.Text}";

                    string additionalInfo =txtHostname.Text +$"- {dateTime1} - {dateTime2}";


                    // Call the logging method
                    Form1.WriteLogsToDatabase(
                        serialNumber: "",
                        site: "",
                        username: Form2.User_name,
                        type: "Def. Harness",
                        additionalInfo: additionalInfo
                    );


                    //ClsData.Connect("MOASMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");

                    DataTable dt = ClsOrders.Harness_SN(
                        txtHostname.Text,
                        Date1.Text + " " + Time1.Text,
                        Date2.Text + " " + Time2.Text
                    );

                    if (!dt.Columns.Contains("Error Description"))
                        dt.Columns.Add("Error Description", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string code = row["Error Code"].ToString().Trim();
                        if (errorDescriptions.ContainsKey(code))
                            row["Error Description"] = errorDescriptions[code];
                        else
                            row["Error Description"] = "Unknown";
                    }

                    return dt;
                });

                // Update UI after background work
                gridControl1.DataSource = data;
                richTextBox1.Text = $"{txtHostname.Text}\n{Date1.Text} {Time1.Text}\n{Date2.Text} {Time2.Text}\n{Error_Type}";

                LoadReferenceChart();
                UpdatePieChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Hide loading UI
                PB_Loading.Visible = false;
                lblLoading.Visible = false;
                gridControl1.Visible = true;
                chartControl1.Visible = true;
                chartControl2.Visible = true;
            }
        }

        private async void Sync_Data()
        {
            try
            {
                // Show the ProgressPanel
                progressPanel1.Visible = true;

                // Set the ProgressPanel to be on top and show it
                progressPanel1.BringToFront();
                txtHostname.Text = "select Reference, BC AS [Travel Ticket Label], Test AS Station, \r\n       IN_Barcode_1 AS [2nd Label], IN_Barcode_2 AS [3rd Travel Ticket Label], \r\n       IN_Barcode_3 AS [4th Travel Ticket Label], OUT_Barcode AS [Final Label], \r\n       DateEnd AS [Test Date], Rework AS Reworked, Rework_ID AS [Rework ID] , Hostname from correlations_trial whre Hostname = 'MOKEETS001' AND[DateEnd] >= " + Date1.Text + " " + Time1.Text + "AND[DateEnd] <" + Date2.Text + " " + Time2.Text;

                // Perform the data loading and chart update operations asynchronously
                await Task.Run(() =>
                {
                    Invoke(new Action(() =>
                    {

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
            if (Date1.EditValue is DateTimeOffset start)
            {
                // If Date2 is already selected, validate it
                if (Date2.EditValue is DateTimeOffset end)
                {
                    if (end < start)
                    {
                        MessageBox.Show("End date cannot be earlier than start date.");
                        Date2.EditValue = start;
                    }
                    else if ((end - start).TotalDays > 30)
                    {
                        MessageBox.Show("End date cannot exceed 30 days after start date.");
                        Date2.EditValue = start.AddDays(30);
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        // Call this method after GridControl is filled
        private void LoadReferenceChart()
        {
            chartControl1.ClearSelection();
            GridView view = gridControl1.MainView as GridView;

            // Extract data from GridControl into a DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("Hostname");
            dt.Columns.Add("Reference");
            dt.Columns.Add("TT Label");
            dt.Columns.Add("HarnessSN");

            for (int i = 0; i < view.RowCount; i++)
            {
                DataRow row = dt.NewRow();
                row["Hostname"] = view.GetRowCellValue(i, "Hostname");
                row["Reference"] = view.GetRowCellValue(i, "Reference");
                row["TT Label"] = view.GetRowCellValue(i, "TT Label");
                row["HarnessSN"] = view.GetRowCellValue(i, "Harness S/N");
                dt.Rows.Add(row);
            }

            // Group by Reference and count
            var grouped = dt.AsEnumerable()
                            .GroupBy(r => r.Field<string>("Reference"))
                            .Select(g => new
                            {
                                Reference = g.Key,
                                Count = g.Count()
                            })
                            .ToList();

            // Bind to ChartControl
            chartControl1.Series.Clear();
            Series series = new Series("Reference Count", ViewType.Bar);

            foreach (var item in grouped)
            {
                series.Points.Add(new SeriesPoint(item.Reference, item.Count));
            }

            chartControl1.Series.Add(series);

            // Optional: make it look nicer
            ((BarSeriesView)series.View).ColorEach = true;
            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
        }
        private void GridView1_ColumnFilterChanged(object sender, EventArgs e) 
        {
            LoadReferenceChart();
            UpdatePieChart();
        }

        private void UpdatePieChart()
        {
            var view = gridView1;

            // Count error types from visible rows
            Dictionary<string, int> errorCount = new Dictionary<string, int>();

            for (int i = 0; i < view.RowCount; i++)
            {
                int rowHandle = view.GetVisibleRowHandle(i);

                if (rowHandle >= 0)
                {
                    string errorType = view.GetRowCellValue(rowHandle, "Error Description")?.ToString();

                    if (!string.IsNullOrEmpty(errorType))
                    {
                        if (!errorCount.ContainsKey(errorType))
                            errorCount[errorType] = 0;

                        errorCount[errorType]++;
                    }
                }
            }

            // Update chartControl2 (Pie Chart)
            chartControl2.Series.Clear();
            var series = new DevExpress.XtraCharts.Series("Errors", DevExpress.XtraCharts.ViewType.Pie);

            foreach (var item in errorCount)
            {
                series.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.Key, item.Value));
            }

            chartControl2.Series.Add(series);

            // Optional: show labels with percentages
            var pieView = (DevExpress.XtraCharts.PieSeriesView)series.View;
            pieView.RuntimeExploding = true;

            series.Label.TextPattern = "{A}: {V} ({VP:P0})";
        }

        Dictionary<string, string> errorDescriptions = new Dictionary<string, string>()
{
    { "0", "Missing Detection" },
    { "1", "Extra Detection" },
    { "2", "Missing Connector" },
    { "3", "Extra Connector" },
    { "4", "Short Through Diode" },
    { "5", "Open In Diode" },
    { "6", "Diode Inverted" },
    { "7", "Inversion (Misallocation)" },
    { "8", "Possible Inversion" },
    { "9", "Short-circuit" },
    { "10", "No Continuity" },
    { "11", "Bad End (Wrong Cavity)" }
};

        private void Date2_EditValueChanged(object sender, EventArgs e)
        {
            if (Date1.EditValue is DateTimeOffset start &&
    Date2.EditValue is DateTimeOffset end)
            {
                if (end < start)
                {
                    MessageBox.Show("End date cannot be earlier than start date.");
                    Date2.EditValue = start;
                }
                else if ((end - start).TotalDays > 30)
                {
                    MessageBox.Show("End date cannot exceed 30 days after start date.");
                    Date2.EditValue = start.AddDays(30);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new frmHostname().ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (gridControl1 == null)
            {
                MessageBox.Show("Grid is not available.");
                return;
            }

            // Get the main view (usually GridView)
            var view = gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;

            // Check if view exists and has rows
            if (view == null || view.RowCount == 0)
            {
                MessageBox.Show("There is no data to export.");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Title = "Export to Excel",
                FileName = "ExportedData.xlsx"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                gridControl1.ExportToXlsx(saveDialog.FileName);
                MessageBox.Show("Export completed successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed: " + ex.Message);
            }
        }
    }

}
