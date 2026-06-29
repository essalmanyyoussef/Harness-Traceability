using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraTab;
using Harness_Traceability.Models;
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
    public partial class frmHarnessSearch : Form
    {
        public frmHarnessSearch()
        {
            InitializeComponent();
        }

        private async void windowsUIButtonPanel1_Click(object sender, EventArgs e)
        {
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

            if (!serverMap.TryGetValue(prefix, out string Server))
            {
                MessageBox.Show("This Hostname doesn't exist, please check and enter the correct Hostname.");
                return;
            }

            // Validate dates
            if (!DateTime.TryParse(Date1.Text + " " + Time1.Text, out DateTime startDate) ||
                !DateTime.TryParse(Date2.Text + " " + Time2.Text, out DateTime endDate))
            {
                MessageBox.Show("Invalid date or time format.");
                return;
            }

            if (startDate >= endDate)
            {
                MessageBox.Show("Start date must be earlier than end date.");
                return;
            }

            if ((endDate - startDate).TotalDays > 365)
            {
                MessageBox.Show("Date range cannot exceed 365 days.");
                return;
            }

            // Show loading UI
            PB_Loading.Visible = true;
            lblLoading.Visible = true;
            gridControl1.Visible = false;
            chartControl1.Visible = false;
            chartControl2.Visible = false;
            PB_Loading.BringToFront();

            try
            {
                // Run heavy work in background thread
                DataTable dt = await Task.Run(() =>
                {
                    // Connect to server
                    ClsData.Connect(Server, "wtr", "wtrviewuser", "alarm-S7D46S");

                    // Execute heavy query
                    return ClsOrders.Statistics(
                        hostname,
                        Date1.Text + " " + Time1.Text,
                        Date2.Text + " " + Time2.Text
                    );
                });

                // Update UI after background work
                gridControl1.DataSource = dt;
                LoadReferencePieChart();
                LoadDailyHarnessChart();

                // Build AdditionalInfo
                string cleanDate1 = Date1.Text.Split(' ')[0];
                string cleanDate2 = Date2.Text.Split(' ')[0];

                string dateTime1 = $"{cleanDate1}T{Time1.Text}";
                string dateTime2 = $"{cleanDate2}T{Time2.Text}";

                string additionalInfo = txtHostname.Text + $"- {dateTime1} - {dateTime2}";


                // Call the logging method
                Form1.WriteLogsToDatabase(
                    serialNumber: "",
                    site: "",
                    username: Form2.User_name,
                    type: "Harness Stat.",
                    additionalInfo: additionalInfo
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Restore UI
                PB_Loading.Visible = false;
                lblLoading.Visible = false;
                gridControl1.Visible = true;
                chartControl1.Visible = true;
                chartControl2.Visible = true;
            }
        }


        private void frmHarnessSearch_Load(object sender, EventArgs e)
        {
            gridControl1.Visible = false;
            chartControl1.Visible = false;
            chartControl2.Visible = false;
            lblLoading.Visible = false;
            PB_Loading.Visible = false;
        }

        private void LoadReferencePieChart()
        {
            var view = gridView1;

            Dictionary<string, int> referenceCount = new Dictionary<string, int>();

            // Collect visible rows
            for (int i = 0; i < view.RowCount; i++)
            {
                int rowHandle = view.GetVisibleRowHandle(i);

                if (rowHandle >= 0)
                {
                    string reference = view.GetRowCellValue(rowHandle, "Reference")?.ToString();

                    if (!string.IsNullOrEmpty(reference))
                    {
                        if (!referenceCount.ContainsKey(reference))
                            referenceCount[reference] = 0;

                        referenceCount[reference]++;
                    }
                }
            }

            // Sort descending by count
            var sorted = referenceCount
                .OrderByDescending(x => x.Value)
                .ToList();

            // Prepare Top 10 + Others
            Dictionary<string, int> finalData = new Dictionary<string, int>();

            int topN = 12;
            int counter = 0;
            int othersTotal = 0;

            foreach (var item in sorted)
            {
                if (counter < topN)
                {
                    finalData[item.Key] = item.Value;
                }
                else
                {
                    othersTotal += item.Value;
                }

                counter++;
            }

            // Add "Others" slice if needed
            if (othersTotal > 0)
                finalData["Others"] = othersTotal;

            // Build chart
            chartControl2.Series.Clear();
            var series = new DevExpress.XtraCharts.Series("Reference Distribution", DevExpress.XtraCharts.ViewType.Pie);

            foreach (var item in finalData)
            {
                series.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.Key, item.Value));
            }

            chartControl2.Series.Add(series);

            // Show labels with percentages
            series.Label.TextPattern = "{A}: {V} ({VP:P0})";

            // Enable connector lines
            series.Label.LineVisible = true;

            // Prevent overlap
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Improve spacing
            chartControl2.Padding.All = 20;

            // Show legend
            chartControl2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            // Slight explode for readability
            PieSeriesView viewPie = (PieSeriesView)series.View;
            viewPie.ExplodeMode = PieExplodeMode.All;
            viewPie.ExplodedDistancePercentage = 5;
        }



        private void LoadDailyHarnessChart()
        {
            var view = gridView1;

            List<DateTime> dates = new List<DateTime>();

            // Extract visible Test Dates
            for (int i = 0; i < view.RowCount; i++)
            {
                int rowHandle = view.GetVisibleRowHandle(i);

                if (rowHandle >= 0)
                {
                    object dateObj = view.GetRowCellValue(rowHandle, "Test Date");

                    if (dateObj != null && DateTime.TryParse(dateObj.ToString(), out DateTime testDate))
                    {
                        dates.Add(testDate.Date);
                    }
                }
            }

            if (dates.Count == 0)
                return;

            // Determine date range
            DateTime minDate = dates.Min();
            DateTime maxDate = dates.Max();
            double totalDays = (maxDate - minDate).TotalDays;

            chartControl1.Series.Clear();
            Series series = new Series("Harnesses", ViewType.Bar);

            // -----------------------------
            // 1️⃣ GROUP BY DAY (≤ 45 days)
            // -----------------------------
            if (totalDays <= 45)
            {
                var grouped = dates
                    .GroupBy(d => d)
                    .OrderBy(g => g.Key);

                foreach (var g in grouped)
                {
                    series.Points.Add(new SeriesPoint(g.Key.ToString("yyyy-MM-dd"), g.Count()));
                }
            }

            // -----------------------------
            // 2️⃣ GROUP BY WEEK (≤ 90 days)
            // -----------------------------
            else if (totalDays <= 90)
            {
                var grouped = dates
                    .GroupBy(d => new
                    {
                        Year = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetYear(d),
                        Week = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                            d,
                            System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                            DayOfWeek.Monday)
                    })
                    .OrderBy(g => g.Key.Year)
                    .ThenBy(g => g.Key.Week);

                foreach (var g in grouped)
                {
                    string label = $"Week {g.Key.Week} - {g.Key.Year}";
                    series.Points.Add(new SeriesPoint(label, g.Count()));
                }
            }

            // -----------------------------
            // 3️⃣ GROUP BY MONTH (> 90 days)
            // -----------------------------
            else
            {
                var grouped = dates
                    .GroupBy(d => new { d.Year, d.Month })
                    .OrderBy(g => g.Key.Year)
                    .ThenBy(g => g.Key.Month);

                foreach (var g in grouped)
                {
                    string label = $"{g.Key.Year}-{g.Key.Month:00}";
                    series.Points.Add(new SeriesPoint(label, g.Count()));
                }
            }

            chartControl1.Series.Add(series);

            // Make chart pretty
            ((BarSeriesView)series.View).ColorEach = true;
            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
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
