using DevExpress.CodeParser;
using DevExpress.Data.NetCompatibility.Extensions;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
//using DevExpress.CodeParser.Diagnostics;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
//using DevExpress.XtraRichEdit;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraPrintingLinks;
using Harness_Traceability.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;



namespace Harness_Traceability
{
    
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //RepositoryItemHyperLinkEdit linkVisionReport;
        private RepositoryItemHyperLinkEdit linkVisionReport;
        public static string site;
        public string connectionString;
        public string TT_Label1;
        public string TT_Label2;
        public string TT_Label3;
        public string TT_Label4;
        public string Server_Database;
        public static string Harness_ID;
        public static string Hostname;
        public string SN_Details = "Final Label";
        public string SN_Details_All = "False";
        string Reference;
        string Vision_Hostname = "";
        DateTime Vision_Datetime;
        string Harness_Serial_Number;

        public static Form1 frm_PPl = new Form1();
        public Form1()
        {
            InitializeComponent();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void UpdateDataGrid(DataGridView grid, DataTable table)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => UpdateDataGrid(grid, table)));
                }
                else
                {
                    grid.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowMessageBox(string message)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => ShowMessageBox(message)));
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Enabled = false;
                this.Text = "Harness Traceability Search";
                btnExport.Enabled = false;




                //// Create an instance of Form2
                //this.Hide();
                //Form2 form2 = new Form2();

                //// Show Form2 as a dialog
                //var dialogResult = form2.ShowDialog();

                //// Check the result after Form2 closes
                //if (dialogResult == DialogResult.OK)
                //{
                //    // Login was successful, show Form1
                //    this.Show();
                //}
                //else
                //{
                //    // Login failed or was canceled, close Form1
                //    this.Close();
                //}

                if (Form2.Site_List != null && Form2.Site_List.Rows.Count > 0)
                {
                    // Set the DataSource of comboBox2 to comboBoxDataTable
                    comboBox1.DataSource = Form2.Site_List;
                    comboBox1.DisplayMember = "SiteName";
                }
                else
                {
                    MessageBox.Show("DataTable is empty or not initialized.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void Solver_Errors(string Travel_Ticket)
        {


        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }





        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.SelectedRows[0].Index;
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "")
                {
                    return;
                }
                else
                {
                    Harness_ID = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    Hostname = dataGridView1.Rows[i].Cells[10].Value.ToString();
                    new Harness_Details().ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        int rownum = 0;
        private void dataGridView1_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            try
            {
                e.ContextMenuStrip = contextMenuStrip1;
                rownum = e.RowIndex;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.dataGridView1.Rows[e.RowIndex].Selected = true;
                    this.rownum = e.RowIndex;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
                    this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //=============================PDF


        private void ExportDataGridViewToPdf( string filePath)
        {
            try
            {// Create a new PDF document in landscape mode
             // Check if the file already exists, and delete it if necessary
             // Attempt to delete the file if it exists
             // Temporary file path to avoid conflicts with locked file
                string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");

            // Retry mechanism: Try to export up to 3 times if the file is locked
            int retries = 3;
            bool exportSuccess = false;
            while (retries > 0 && !exportSuccess)
            {
                try
                {
                    // Export gridControl1 to a temporary file
                    gridControl1.ExportToPdf(tempFilePath);

                    // If export is successful, move the temp file to the desired location
                    if (File.Exists(tempFilePath))
                    {
                        File.Move(tempFilePath, filePath);
                    }

                    exportSuccess = true;
                }
                catch (IOException)
                {
                    // If the file is locked, retry after waiting for a short period
                    retries--;
                    Thread.Sleep(500); // Wait for 500ms before retrying
                }
            }

            if (!exportSuccess)
            {
                // If export failed after retries, show an error
                MessageBox.Show("The file is still in use after several attempts. Please ensure the file is not open in another application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("PDF Exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static string Harness_Details;



     
 

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Harness_Serial_Number = "";
                labelControl1.Visible = false;
                labelControl2.Visible = false;
                tileControl1.Visible = false;
                btn_Contact.Visible = false;
                if (txtOutBarcode.Text.Length > 10)
                {
                    chartControl1.Series.Clear(); // Clear chart if there's no data

                    gridView2.OptionsBehavior.Editable = false;
                    chartControl1.Visible = false;
                    btnExport.Enabled = false;
                    btnSearch.Enabled = false;
                    site = comboBox1.Text;
                    string serial = txtOutBarcode.Text.Replace("\r", "").Replace("\n", "");
                    lbl_Harness1.Visible = false;
                    lbl_Harness2.Visible = false;
                    lblErrors.Visible = false;
                    lbl_More_Details.Visible = false;


                    DataTable data = new DataTable();
                    DataTable data1 = new DataTable();
                    //DataTable data2 = new DataTable();

                    //Chaine_Connexion();
                    //ShowProgressBar(true
                    //
                    IMG_Searching.Visible = true;
                    //btn_Search.Enabled = false;
                    txtOutBarcode.Enabled = false;
                    gridControl1.Visible = false; // Changed from dataGridView1 to gridControl1
                    dataGridView2.Visible = false; // Assuming this is still a DataGridView
                    gridControl1.DataSource = null; // Changed from dataGridView1 to gridControl1
                    dataGridView2.DataSource = null;
                    dataGridView1.DataSource = null;
                    gridControl2.Visible = false;
                    gridControl2.DataSource = null;

                    await Task.Run(async () =>
                    {
                        if (site == "")
                        {
                            MessageBox.Show("Please select the Site");
                            btnSearch.Enabled = true;
                            return;
                        }
                        else
                        {
                            if (txtOutBarcode.Text == "" || txtOutBarcode.Text == " " || txtOutBarcode.Text == "  ")
                            {
                                MessageBox.Show("Please enter the TT/Final Label");
                                btnSearch.Enabled = true;
                                return;
                            }
                            else
                            {
                                if (site == "K1")
                                {
                                    ClsData.Connect("MOKEMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "K2")
                                {
                                    ClsData.Connect("MOASMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "MFZ")
                                {
                                    ClsData.Connect("MOFZMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "AA")
                                {
                                    ClsData.Connect("MOAAMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "Skirat")
                                {
                                    ClsData.Connect("MOSKMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "PortSaid")
                                {
                                    ClsData.Connect("EGPSMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "TenthRamadan")
                                {
                                    ClsData.Connect("EGTRMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "SixthOctober")
                                {
                                    ClsData.Connect("EGSOMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "Alba")
                                {
                                    ClsData.Connect("ROAIMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "Deva")
                                {
                                    ClsData.Connect("RODVMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                if (site == "Monastir")
                                {
                                    ClsData.Connect("TNMOMLS001", "wtr", "wtrviewuser", "alarm-S7D46S");
                                }
                                //WriteLogs("S/N \t\t\t : " + txtOutBarcode.Text + "  " + SN_Details + "  " + SN_Details_All + "\nSite \t\t\t : " + site + "\nUsername \t\t : " + Form2.User_name + "\nDate & Time \t : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n---------------------------------------------------------------------------");
                                WriteLogsToDatabase(
    serialNumber: txtOutBarcode.Text,
    site: site,
    username: Form2.User_name,
    type: SN_Details_All,
    additionalInfo: SN_Details 
);

                                if (RDB_OUT.Checked == true)
                                {
                                    data = ClsOrders.GetTraceability(serial);
                                    //dataGridView3.DataSource = data; // Assuming this is still a DataGridView
                                    if (data.Rows.Count > 0)
                                    {
                                        string p1 = data.Rows[0].ItemArray[1].ToString();
                                        string p2 = data.Rows[0].ItemArray[3].ToString();
                                        string p3 = data.Rows[0].ItemArray[4].ToString();
                                        Reference = data.Rows[0].ItemArray[0].ToString();

                                        if (p1.Length > 11)
                                        {
                                            data = ClsOrders.GetTraceability2(p1);
                                            if (data.Rows.Count > 15)
                                            {
                                                data = ClsOrders.GetTraceability(serial);
                                            }
                                        }
                                        if (CheckBox_Searching.Checked == true)
                                        {
                                            if (p1.Length > 11)
                                            {
                                                data1 = ClsOrders.SelvedError(p1);
                                            }
                                            if (data1.Rows.Count == 0)
                                            {
                                                if (p2.Length > 11)
                                                {
                                                    data1 = ClsOrders.SelvedError(p2);
                                                }

                                                if (data1.Rows.Count == 0)
                                                {
                                                    if (p3.Length > 11)
                                                    {
                                                        data1 = ClsOrders.SelvedError(p3);
                                                    }
                                                }
                                            }


                                        }
                                        else
                                        {
                                            
                                            ET_Errors();
                                        }


                                    }
                                }
                                else
                                {
                                    string p1 = txtOutBarcode.Text;
                                    data = ClsOrders.GetTraceability2(p1);
                                    data1 = ClsOrders.SelvedError(p1);

                                    //ET_Errors();



                                }
                            }
                        }
                        });

                    //gridControl1.DataSource = data; // Changed from dataGridView1 to gridControl1

                    DataTable mergedData = MergeDuplicateRows(data);
                    gridControl1.DataSource = mergedData;


                    dataGridView1.DataSource = data;

 //***************************************************-- Vision Report --*****************************************************************
                    //SetupVisionReportFeature(); // Vision Report Viewer
//***************************************************************************************************************************************



                    string customer;
                    string project;
                    string family;
                    string line;
                    string bank;

                    if (GetHarnessFamilyInformation(
                        out customer,
                        out project,
                        out family,
                        out line,
                        out bank))
                    {
                        labelControl2.Text = $"{Harness_Serial_Number}\r\n" +
                            $"{customer}\r\n" +
                            $"{project}\r\n" +
                            $"{family}\r\n" +
                            $"{line}";
                        labelControl1.Visible = true;
                        labelControl2.Visible = true;
                    }
                    else
                    {
                        labelControl1.Visible = false;
                        labelControl2.Visible = false;
                    }


                    if (RDBTT.Checked == true || CheckBox_Searching.Checked == true)
                    {
                        dataGridView2.DataSource = data1;


                        //gridControl2.DataSource = data1;
                        DataTable mergedEroorData = MergeDuplicateRows(data1);
                        gridControl2.DataSource = mergedEroorData;



                        Chart_Traceability();
                        BuildErrorSummaryTiles();
                        chartControl1.Visible = true;
                    }
                    //gridControl2.DataSource = data1;
                    if (data.Rows.Count > 0)
                    {
                        gridView1.OptionsBehavior.Editable = false;
                        gridView1.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridView1.Columns[7].DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss";
                    }



                    // Assuming you have a GridControl named gridControl1
                    GridView view = gridControl1.MainView as GridView;
                    if (view != null)
                    {
                        view.OptionsView.ColumnAutoWidth = false; // Disable automatic column width adjustment
                        view.BestFitColumns(); // Adjust columns to fit their content
                    }



                    btnExport.Enabled = true;
                    btnSearch.Enabled = true;
                    IMG_Searching.Visible = false;
                    //btn_Search.Enabled = true;
                    txtOutBarcode.Enabled = true;
                    //if (data1.Rows.Count > 0 || dataGridView2.Rows.Count > 0 || dataGridView2.DataSource != null)
                    if (data1.Rows.Count > 0)
                    {
                        HideEmptyColumns();

                        //dataGridView1.Columns[10].Visible = false; // Adjust as necessary for gridControl1
                        dataGridView2.Visible = false; // Assuming this is still a DataGridView
                        gridControl2.Visible = true;
                        lblErrors.Visible = false;
                        //if (gridControl2.DataSource != null||data1.Rows.Count>0)
                        if (data1.Rows.Count > 0)
                        {
                            gridView2.Columns[11].DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss";
                            gridView2.Columns[12].DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss";
                        }
                        GridView view2 = gridControl2.MainView as GridView;
                        if (view2 != null)
                        {
                            view2.OptionsView.ColumnAutoWidth = false; // Disable automatic column width adjustment
                            view2.BestFitColumns(); // Adjust columns to fit their content
                        }
                        if (RDB_OUT.Checked == true)
                        {
                            Harness_Details =
                                "\n                       Harness Barcode                  :     " + txtOutBarcode.Text
                                + "\n                   Travel Ticket                             :     " + serial
                                + "\n                   Reference                                 :     " + Reference;
                        }
                        else
                        {
                            // Additional logic if needed
                        }
                    }
                    else
                    {

                        dataGridView2.Visible = false; // Assuming this is still a DataGridView
                        gridControl2.Visible = false;
                        lblErrors.Visible = true;
                        if (RDB_OUT.Checked == true)
                        {
                            if (CheckBox_Searching.Checked == true)
                            {

                            }
                            else
                            {
                                ET_Errors();
                                Chart_Traceability();
                                BuildErrorSummaryTiles();
                                chartControl1.Visible = true;

                                //gridControl2.Visible = true;
                                //lblErrors.Visible = false;
                            }
                        }
                    }
                    if (data.Rows.Count > 0)
                    {
                        gridControl1.Visible = true; // Changed from dataGridView1 to gridControl1
                        lbl_Harness1.Visible = false;
                        lbl_Harness2.Visible = false;
                        lbl_More_Details.Visible = true;
                    }
                    else
                    {
                        gridControl1.Visible = false; // Changed from dataGridView1 to gridControl1
                        lbl_Harness1.Visible = true;
                        lbl_Harness2.Visible = true;
                        lbl_More_Details.Visible = false;
                    }

                    if (gridControl2.DataSource != null)
                    {
                        HideEmptyColumns();
                    }
                    Chart_Traceability();
                    BuildErrorSummaryTiles();

                }
                else
                {
                    MessageBox.Show("Please enter the full S/N. It must contain more than 10 characters!", "Harness S/N error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtOutBarcode.Enabled = true;
                txtOutBarcode.ReadOnly = false;
                IMG_Searching.Visible = false;
                string message = ex.Message;
                //throw ex;
            }

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // Check if there is a valid row under the double-click
                var hitInfo = gridView1.CalcHitInfo(gridControl1.PointToClient(Control.MousePosition));
                if (hitInfo.InRow || hitInfo.InRowCell)
                {
                    // Get the data from the selected row (example using the first column as ID)
                    var selectedRow = gridView1.GetDataRow(hitInfo.RowHandle);
                    if (selectedRow != null)
                    {
                        // Retrieve values from the row as needed
                        var valueInColumn = selectedRow[1]; // Replace with actual column name

                        // Open the new form, passing the required data
                        Harness_ID = selectedRow[1].ToString();
                        Hostname = selectedRow[10].ToString();
                        //MessageBox.Show("Harness ID : " + selectedRow[1].ToString() + "  \nHostname   : " + selectedRow[10].ToString());
                        new Harness_Details().ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
            //System.Diagnostics.Process.Start(new ProcessStartInfo
            //{
            //    FileName = "https://app.powerbi.com/links/wZNNNONAnc?ctid=4a1d14a7-833e-4ee8-a4d8-f45cab9c6bf9&pbi_source=linkShare",
            //    UseShellExecute = true
            //});
        }


 

        private void button1_Click1(object sender, EventArgs e)
        {
            try
            {

                // Create a composite link
                CompositeLink compositeLink = new CompositeLink(new PrintingSystem());

                // Add the first grid control to the composite link

                Link Document_Title = new Link();
                Document_Title.CreateDetailArea += (customSender, customE) =>
                {
                    customE.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
                    customE.Graph.Font = new Font("Berlin Sans FB", 20, FontStyle.Bold);

                    // Draw the logo image from resources in its original size
                    Image logo = Harness_Traceability.Properties.Resources.sews_logo;
                    customE.Graph.DrawImage(logo, new RectangleF(0, 0, logo.Width, logo.Height), BorderSide.None, Color.Transparent);

                    // Draw the title text below the logo
                    customE.Graph.DrawString("Harness Traceability\n\n", Color.Black, new RectangleF(0, logo.Height, customE.Graph.ClientPageSize.Width, 30), BorderSide.None);
                };
                compositeLink.Links.Add(Document_Title);

                Link Harness_SN = new Link();
                Harness_SN.CreateDetailArea += (customSender, customE) =>
                {
                    customE.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
                    customE.Graph.Font = new Font("Arial", 14, FontStyle.Bold);

                    customE.Graph.DrawString("Harness S/N\t:   " + txtOutBarcode.Text + "\nSite\t:   SEWS " + comboBox1.Text, Color.Black, new RectangleF(0, 0, customE.Graph.ClientPageSize.Width, 50), BorderSide.None);
                };
                compositeLink.Links.Add(Harness_SN);

                PrintableComponentLink link1 = new PrintableComponentLink();
                link1.Component = gridControl1;
                compositeLink.Links.Add(link1);

                // Add a custom link for the text between the grids
                Link customTextLink = new Link();
                customTextLink.CreateDetailArea += (customSender, customE) =>
                {
                    customE.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
                    customE.Graph.Font = new Font("Arial", 16, FontStyle.Bold);
                    customE.Graph.DrawString("\nList of the Errors\n\n", Color.Black, new RectangleF(0, 0, customE.Graph.ClientPageSize.Width, 50), BorderSide.None);
                };
                compositeLink.Links.Add(customTextLink);

                // Add the second grid control to the composite link
                PrintableComponentLink link2 = new PrintableComponentLink();
                link2.Component = gridControl2;
                compositeLink.Links.Add(link2);

                // Set the page settings
                compositeLink.PaperKind = (DevExpress.Drawing.Printing.DXPaperKind)PaperKind.A3;
                compositeLink.Landscape = true;

                // Flag to check if the title has been printed
                bool titlePrinted = false;

                // Update the footer after pages are built
                compositeLink.PrintingSystem.AfterBuildPages += (psSender, psE) =>
                {
                    int totalPages = compositeLink.PrintingSystem.Pages.Count;
                    for (int i = 0; i < totalPages; i++)
                    {
                        var page = compositeLink.PrintingSystem.Pages[i];
                        page.AssignWatermark(new PageWatermark()
                        {
                            Text = $"Page {i + 1} of {totalPages}",
                            Font = new Font("Arial", 10),
                            ForeColor = Color.Black,
                            TextTransparency = 150,
                            ShowBehind = false
                        });
                    }
                };

                // Export to PDF
                compositeLink.ExportToPdf(@"C:\Emdep\TEST\MyGrid.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }



        //1111240922530136610-63UA0

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                GridView view = gridControl1.MainView as GridView;
                if (view != null)
                {
                    view.BestFitColumns();
                }
                GridView view2 = gridControl2.MainView as GridView;
                if (view2 != null)
                {
                    view2.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void HideEmptyColumns()
        {
            try
            {
                GridControl gridControl2 = this.gridControl2; // Assuming 'this' refers to your form or control containing the grid
                GridView gridView = gridControl2.MainView as GridView;
                if (gridView != null)
                {
                    // Check if column 16 is empty
                    bool isColumn16Empty = true;
                    for (int i = 0; i < gridView.DataRowCount; i++)
                    {
                        if (gridView.GetRowCellValue(i, gridView.Columns[15]) != null && !string.IsNullOrEmpty(gridView.GetRowCellValue(i, gridView.Columns[15]).ToString()))
                        {
                            isColumn16Empty = false;
                            break;
                        }
                    }

                    // Check if column 17 is empty
                    bool isColumn17Empty = true;
                    for (int i = 0; i < gridView.DataRowCount; i++)
                    {
                        if (gridView.GetRowCellValue(i, gridView.Columns[16]) != null && !string.IsNullOrEmpty(gridView.GetRowCellValue(i, gridView.Columns[16]).ToString()))
                        {
                            isColumn17Empty = false;
                            break;
                        }
                    }

                    // Hide columns if they are empty
                    gridView.Columns[15].Visible = !isColumn16Empty;
                    gridView.Columns[16].Visible = !isColumn17Empty;
                    gridView.Columns[14].Visible = !isColumn16Empty;
                    gridView.Columns[17].Visible = !isColumn17Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ET_Errors()
        {
            try
            {
                // Get the GridView associated with gridControl1
                var gridView = gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (gridView != null)
                {
                    DataTable masterTable = null;

                    for (int i = 0; i < gridView.RowCount; i++)
                    {
                        string column3Value = gridView.GetRowCellValue(i, gridView.Columns[2])?.ToString();

                        if (!string.IsNullOrEmpty(column3Value) &&
                            (column3Value.Contains("ET") ||
                             column3Value.Contains("ELEC") ||
                             column3Value.Contains("ELECTRIC") ||
                             column3Value.Contains("Test Electric")))
                        {
                            // Read date from Grid #1 (column 7)
                            DateTime gridDate = Convert.ToDateTime(gridView.GetRowCellValue(i, gridView.Columns[7]));

                            // Get date range from GetDates()
                            DataTable DD = ClsOrders.GetDates(
                                gridView.GetRowCellValue(i, gridView.Columns[10])?.ToString(),
                                gridDate
                            );

                            if (DD != null && DD.Rows.Count > 0)
                            {
                                // Extract D1 and D2 from DD
                                DateTime D1 = Convert.ToDateTime(DD.Rows[0].ItemArray[7]);
                                DateTime D2 = Convert.ToDateTime(DD.Rows[0].ItemArray[8]);

                                // ⭐ NEW RULE: Skip if gridDate is more than 5 hours later than D1
                                if ((gridDate - D1).TotalHours > 5)
                                {
                                    // Skip this row and continue to next
                                    continue;
                                }

                                // Get details for this row
                                DataTable DD1 = ClsOrders.SelvedError_by_Date(
                                    D1,
                                    D2,
                                    gridView.GetRowCellValue(i, gridView.Columns[10])?.ToString()
                                );

                                if (DD1 != null && DD1.Rows.Count > 0)
                                {
                                    // Initialize master table structure once
                                    if (masterTable == null)
                                        masterTable = DD1.Clone();

                                    // Append rows
                                    foreach (DataRow row in DD1.Rows)
                                        masterTable.ImportRow(row);
                                }
                            }
                        }
                    }

                    // Bind final combined results
                    if (masterTable != null)
                    {
                        gridControl2.DataSource = masterTable;

                        gridView2.Columns[11].DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss";
                        gridView2.Columns[12].DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss";

                        GridView view2 = gridControl2.MainView as GridView;
                        if (view2 != null)
                        {
                            view2.OptionsView.ColumnAutoWidth = false;
                            view2.BestFitColumns();

                            view2.OptionsView.RowAutoHeight = false;
                            // Reduce data row height
                            view2.RowHeight = 16;

                            // Reduce column header height
                            view2.ColumnPanelRowHeight = 20;

                        }

                        gridControl2.Visible = true;
                        Chart_Traceability();
                        chartControl1.Visible = true;
                        BuildErrorSummaryTiles();
                    }
                }


                else
                {
                    MessageBox.Show("The MainView of gridControl1 is not a GridView. Please check the configuration.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BuildErrorSummaryTiles()
        {
            if (!(gridControl2.DataSource is DataTable dt))
                return;

            Dictionary<int, string> errorTypeDescriptions =
                new Dictionary<int, string>()
            {
        { 0, "Missing Detection" },
        { 1, "Extra Detection" },
        { 2, "Missing Connector" },
        { 3, "Extra Connector" },
        { 4, "Short Through Diode" },
        { 5, "Open In Diode" },
        { 6, "Diode Inverted" },
        { 7, "Inversion" },
        { 8, "Possible Inversion" },
        { 9, "Short-circuit" },
        { 10, "No Continuity" },
        { 11, "Bad End" }
            };

            tileControl1.BeginUpdate();

            try
            {
                tileControl1.Groups.Clear();

                TileGroup group = new TileGroup();
                tileControl1.Groups.Add(group);

                //-------------------------------------------------
                // TOTAL ERRORS
                //-------------------------------------------------
                TileItem totalTile = new TileItem();

                totalTile.ItemSize = TileItemSize.Medium;
                totalTile.AppearanceItem.Normal.BackColor = Color.SteelBlue;
                totalTile.AppearanceItem.Normal.BorderColor = Color.SteelBlue;

                TileItemElement totalTitle = new TileItemElement();
                totalTitle.Text = "TOTAL ERRORS";
                totalTitle.TextAlignment = TileItemContentAlignment.TopCenter;
                totalTitle.Appearance.Normal.Font =
                    new Font("Segoe UI", 9, FontStyle.Bold);

                TileItemElement totalCount = new TileItemElement();
                totalCount.Text = dt.Rows.Count.ToString();
                totalCount.TextAlignment = TileItemContentAlignment.MiddleCenter;
                totalCount.Appearance.Normal.Font =
                    new Font("Segoe UI", 16, FontStyle.Bold);

                totalTile.Elements.Add(totalTitle);
                totalTile.Elements.Add(totalCount);

                group.Items.Add(totalTile);

                //-------------------------------------------------
                // ERROR TYPES
                //-------------------------------------------------
                var summary = dt.AsEnumerable()
                    .GroupBy(r => Convert.ToInt32(r["Error Type"]))
                    .Select(g => new
                    {
                        ErrorCode = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(x => x.Count);

                foreach (var item in summary)
                {
                    string description =
                        errorTypeDescriptions.ContainsKey(item.ErrorCode)
                            ? errorTypeDescriptions[item.ErrorCode]
                            : $"Unknown ({item.ErrorCode})";

                    TileItem tile = new TileItem();

                    tile.ItemSize = TileItemSize.Medium;

                    // Colors
                    switch (item.ErrorCode)
                    {
                        case 10: // No Continuity
                            tile.AppearanceItem.Normal.BackColor = Color.IndianRed;
                            break;

                        case 0: // Missing Detection
                            tile.AppearanceItem.Normal.BackColor = Color.DarkOrange;
                            break;

                        default:
                            tile.AppearanceItem.Normal.BackColor = Color.SeaGreen;
                            break;
                    }

                    tile.AppearanceItem.Normal.BorderColor =
                        tile.AppearanceItem.Normal.BackColor;

                    TileItemElement title = new TileItemElement();
                    title.Text = description.ToUpper();
                    title.TextAlignment = TileItemContentAlignment.TopCenter;
                    title.Appearance.Normal.Font =
                        new Font("Segoe UI", 8, FontStyle.Bold);

                    TileItemElement value = new TileItemElement();
                    value.Text = item.Count.ToString();
                    value.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    value.Appearance.Normal.Font =
                        new Font("Segoe UI", 16, FontStyle.Bold);

                    tile.Elements.Add(title);
                    tile.Elements.Add(value);

                    group.Items.Add(tile);
                }

                tileControl1.Orientation = Orientation.Horizontal;
                tileControl1.Visible = true;
            }
            finally
            {
                tileControl1.EndUpdate();
            }
        }
        public void Chart_Traceability()
        {
            Dictionary<int, string> errorTypeDescriptions = new Dictionary<int, string>()
{
    { 0, "Missing Detection" },
    { 1, "Extra Detection" },
    { 2, "Missing Connector" },
    { 3, "Extra Connector" },
    { 4, "Short Through Diode" },
    { 5, "Open In Diode" },
    { 6, "Diode Inverted" },
    { 7, "Inversion (Misallocation)" },
    { 8, "Possible Inversion" },
    { 9, "Short-circuit" },
    { 10, "No Continuity" },
    { 11, "Bad End (Wrong Cavity)" }
};

            // Get the DataTable from gridControl1
            DataTable sourceTable = gridControl2.DataSource as DataTable;
            if (sourceTable == null) return;

            // Group and map Error Types
            var groupedData = sourceTable.AsEnumerable()
                .GroupBy(row => row.Field<int>("Error Type"))
                .Select(g => new
                {
                    ErrorTypeLabel = errorTypeDescriptions.ContainsKey(g.Key) ? errorTypeDescriptions[g.Key] : $"Unknown ({g.Key})",
                    Count = g.Count()
                })
                .ToList();

            // Create and bind pie chart
            chartControl1.Series.Clear();
            Series series = new Series("Error Frequencies", ViewType.Pie);
            series.DataSource = groupedData;
            series.ArgumentDataMember = "ErrorTypeLabel"; // Pie slices
            series.ValueDataMembers.AddRange("Count");    // Slice size

            chartControl1.Series.Add(series);

            // Pie appearance settings
            PieSeriesLabel label = series.Label as PieSeriesLabel;
            if (label != null)
            {
                label.TextPattern = "{A}: {V} ({VP:P0})"; // Label: Name: Count (Percentage)
                label.Position = PieSeriesLabelPosition.TwoColumns;
                label.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            }

            ((PieSeriesView)series.View).ExplodedDistancePercentage = 10;
            ((PieSeriesView)series.View).ExplodeMode = PieExplodeMode.All;

            // Show legend
            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;



            chartControl1.Visible = false;
        }

        public static void WriteLogs(string textToWrite)
        {
            string filePath = @"\\ukhqits001\Public\Electrical Test\Harness Traceability App\Application Files\Harness Traceability_1_1_0_0\SystemConfig.syslog";
             

            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    // Create the file and write the text
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine(textToWrite);
                    }
                }
                else
                {
                    // File exists, append the text
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(textToWrite);
                    }
                }

                //Console.WriteLine("Text written successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            string email = "SEWS-ECentralElectricalTestEngineers@sgcci.onmicrosoft.com";
            string subject = "Harness Traceability Software";

            string mailto = $"mailto:{email}?subject={Uri.EscapeDataString(subject)}";

            try
            {
                System.Diagnostics.Process.Start(mailto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create Outlook email.\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!gridControl1.Visible)
                {
                    MessageBox.Show("Please enter S/N of the harness and start searching to generate the Traceability report!");
                }
                else
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                        saveFileDialog.DefaultExt = "pdf";
                        saveFileDialog.AddExtension = true;
                        saveFileDialog.Title = "Save PDF File";
                        saveFileDialog.FileName = txtOutBarcode.Text.Replace('\\', '_') + "_Traceability.pdf";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = saveFileDialog.FileName;
                            // Create a composite link
                            CompositeLink compositeLink = new CompositeLink(new PrintingSystem());

                            // Add the first grid control to the composite link
                            Link Document_Title = new Link();
                            Document_Title.CreateDetailArea += (customSender, customE) =>
                            {
                                customE.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
                                customE.Graph.Font = new Font("Arial", 35, FontStyle.Bold);

                                // Draw the logo image from resources in its original size
                                Image logo = Harness_Traceability.Properties.Resources.sews_logo;
                                customE.Graph.DrawImage(logo, new RectangleF(0, 0, logo.Width, logo.Height), BorderSide.None, Color.Transparent);

                                // Draw the title text next to the logo
                                float textX = logo.Width; // Adjust the X position to be next to the logo with some padding
                                customE.Graph.DrawString("\n\tHarness Traceability\n\n", Color.Black, new RectangleF(textX, 0, customE.Graph.ClientPageSize.Width - textX, logo.Height), BorderSide.None);
                            };
                            compositeLink.Links.Add(Document_Title);

                            Link Harness_SN = new Link();
                            Harness_SN.CreateDetailArea += (customSender, customE) =>
                            {
                                customE.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
                                customE.Graph.Font = new Font("Arial", 14, FontStyle.Bold);

                                customE.Graph.DrawString("Harness S/N\t:   " + txtOutBarcode.Text + "\t\t\tSite\t:   SEWS-" + comboBox1.Text + "\nPrinted by\t:   " + Form2.User_name + "\t\t\tE-mail\t:   " + Form2.User_Mail + "\n\n\n\n\n", Color.Black, new RectangleF(0, 0, customE.Graph.ClientPageSize.Width, 50), BorderSide.None);
                            };
                            compositeLink.Links.Add(Harness_SN);

                            PrintableComponentLink link1 = new PrintableComponentLink();
                            link1.Component = gridControl1;
                            compositeLink.Links.Add(link1);

                            // Add a custom link for the text between the grids
                            Link customTextLink = new Link();
                            customTextLink.CreateDetailArea += (customSender, customE) =>
                            {
                                customE.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
                                customE.Graph.Font = new Font("Arial", 16, FontStyle.Bold);
                                customE.Graph.DrawString("\nList of the Errors\n\n", Color.Black, new RectangleF(0, 0, customE.Graph.ClientPageSize.Width, 50), BorderSide.None);
                            };
                            compositeLink.Links.Add(customTextLink);

                            // Add the second grid control to the composite link


                            GridView view2 = gridControl2.MainView as GridView;
                            if (view2 != null)
                            {
                                // Set the print appearance with a smaller font
                                view2.AppearancePrint.Row.Font = new Font("Arial", 8, FontStyle.Regular);
                                view2.AppearancePrint.HeaderPanel.Font = new Font("Arial", 8, FontStyle.Bold);
                            }

                            // Add the second grid control to the composite link
                            PrintableComponentLink link2 = new PrintableComponentLink();
                            link2.Component = gridControl2;
                            compositeLink.Links.Add(link2);


                            //PrintableComponentLink link2 = new PrintableComponentLink();
                            //link2.Component = gridControl2;
                            //link2.CreateDetailArea += (customSender, customE) =>
                            //{
                            //    customE.Graph.Font = new Font("Arial", 6, FontStyle.Regular); // Reduced font size
                            //};

                            //compositeLink.Links.Add(link2);

                            // Add the error code descriptions as a final custom link
                            Link errorCodesLink = new Link();
                            errorCodesLink.CreateDetailArea += (customSender, customE) =>
                            {
                                customE.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
                                customE.Graph.Font = new Font("Arial", 12, FontStyle.Regular);
                                string errorCodesText = "Error Type Code:\n\t" + "0   - Missing Detection \t 1   - Extra Detection \t 2   - Missing Connector\n"
                                + "\t3   - Extra Connector \t 4   - Short Through Diode \t 5   - Open In Diode\n"
                                + "\t6   - Diode Inverted \t\t 7   - Inversion (Misallocation)  \t 8   - Possible Inversion\n"
                                + "\t9   - Short-circuit \t\t 10  - No Continuity \t\t 11  - Bad End (Wrong Cavity)";




                                // Adjust rectangle size and position relative to the second grid
                                float yPosition = 0; // Start at the current Y position
                                customE.Graph.DrawString(errorCodesText, Color.Black, new RectangleF(0, yPosition, customE.Graph.ClientPageSize.Width, 100), BorderSide.None);
                            };
                            compositeLink.Links.Add(errorCodesLink);

                            PrintableComponentLink chartLink = new PrintableComponentLink();
                            chartLink.Component = chartControl1;
                            compositeLink.Links.Add(chartLink);



                            // Set the page settings
                            compositeLink.PaperKind = (DevExpress.Drawing.Printing.DXPaperKind)PaperKind.A2;
                            compositeLink.Landscape = true;

                            // Flag to check if the title has been printed
                            bool titlePrinted = false;

                            // Update the footer after pages are built
                            compositeLink.PrintingSystem.AfterBuildPages += (psSender, psE) =>
                            {
                                int totalPages = compositeLink.PrintingSystem.Pages.Count;
                                for (int i = 0; i < totalPages; i++)
                                {
                                    var page = compositeLink.PrintingSystem.Pages[i];
                                    page.AssignWatermark(new PageWatermark()
                                    {
                                        Text = $"Page {i + 1} of {totalPages}",
                                        Font = new Font("Arial", 30),
                                        ForeColor = Color.Black,
                                        TextTransparency = 150,
                                        ShowBehind = false
                                    });
                                }
                            };

                            // Export to PDF
                            compositeLink.ExportToPdf(@filePath);
                            MessageBox.Show("PDF Exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Contact_Click(object sender, EventArgs e)
        {

        }

        private void txtOutBarcode_TextChanged(object sender, EventArgs e)
        {
            if(txtOutBarcode.Text.Replace(" ","")=="")
            {
                btnSearch.Enabled = false;
            }
            else
            {
                btnSearch.Enabled = true;
            }
        }

        private void RDBTT_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Searching.Checked = false;
            CheckBox_Searching.Visible = false;
            SN_Details = "TT";
            SN_Details_All = "";
    }

        private void RDB_OUT_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Searching.Visible = true;
            SN_Details = "FL";
            SN_Details_All = "False";
        }

        private void CheckBox_Searching_CheckedChanged(object sender, EventArgs e)
        {
            SN_Details_All = "True";
        }

        private void SetupVisionReportFeature()
        {
            if (gridView1.Columns["VisionReport"] == null)
            {
                var col = new DevExpress.XtraGrid.Columns.GridColumn()
                {
                    Caption = "Vision Report",
                    FieldName = "VisionReport",
                    UnboundType = DevExpress.Data.UnboundColumnType.String,
                    Visible = true,
                    Width = 120
                };

                gridView1.Columns.Add(col);
            }

            // Just for visual hyperlink
            var link = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            link.Caption = "Open Report";
            gridControl1.RepositoryItems.Add(link);
            gridView1.Columns["VisionReport"].ColumnEdit = link;

            // Unbound data
            gridView1.CustomUnboundColumnData += GridView1_CustomUnboundColumnData;

            // IMPORTANT: handle click at grid level
            gridView1.RowCellClick += GridView1_RowCellClick;
        }


        private void GridView1_CustomUnboundColumnData(object sender,
            DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "VisionReport" && e.IsGetData)
            {
                string station = gridView1.GetRowCellValue(e.ListSourceRowIndex, "Station")?.ToString();

                if (!string.IsNullOrEmpty(station) &&
                    station.IndexOf("Vision", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.Value = "Open Report";
                }
                else
                {
                    e.Value = null;
                }
            }
        }

        private void GridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName != "VisionReport")
                return;

            int rowHandle = e.RowHandle;

            string station = gridView1.GetRowCellValue(rowHandle, "Station")?.ToString();
            if (string.IsNullOrEmpty(station) ||
                station.IndexOf("Vision", StringComparison.OrdinalIgnoreCase) < 0)
                return;

            // Hostname
            string hostname = gridView1.GetRowCellValue(rowHandle, "Hostname")?.ToString();

            // Raw date
            string rawDate = gridView1.GetRowCellValue(rowHandle, "Test Date")?.ToString();
            MessageBox.Show("RAW Test Date = [" + rawDate + "]");

            // Supported formats
            string[] formats =
            {
        "dd-MM-yyyy HH:mm:ss",
        "dd/MM/yyyy HH:mm:ss",
        "yyyy-MM-dd HH:mm:ss",
        "yyyy/MM/dd HH:mm:ss",
        "dd-MM-yyyy",
        "dd/MM/yyyy",
        "yyyy-MM-dd",
        "yyyy/MM/dd",
        "dd.MM.yyyy HH:mm:ss",
        "dd.MM.yyyy"
    };

            // Parse into LOCAL variable
            DateTime parsedDate;
            if (!DateTime.TryParseExact(
                    rawDate,
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedDate))
            {
                MessageBox.Show("❌ Failed to parse Test Date: [" + rawDate + "]");
                return;
            }

            MessageBox.Show("Parsed Vision_Datetime = " + parsedDate.ToString("dd-MM-yyyy HH:mm:ss"));

            // Call with CORRECT parsed date
            OpenVisionReport(hostname, parsedDate);
        }

        private readonly Dictionary<string, string> VisionBasePaths = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    { "MOKE", @"\\mokebks001\ETArchive$" },
    { "MOAS", @"\\moasvcs001\ETArchive$" },
    { "MOMZ", @"\\mofzbks001\ETArchive$" },
    { "MOAA", @"\\moaabks001\ETArchive$" },
    { "MOSK", @"\\moskvcs001\ETArchive$" },
    { "TNMO", @"\\tnmobks001\ETArchive$" },
    { "ESPS", @"\\egpsbks001\ETArchive$" },
    { "RODV", @"\\rodvbks001\ETArchive$" },
    { "ESSO", @"\\egpsbks001\ETArchive$" },
    { "ROAI", @"\\roaibks001\ETArchive$" }
};

        private void OpenVisionReport(string Vision_Hostname, DateTime Vision_Datetime)
        {
            try
            {
                MessageBox.Show("OpenVisionReport received date = " + Vision_Datetime.ToString("dd-MM-yyyy HH:mm:ss"));

                // 1. Check hostname prefix
                // Determine base path based on hostname prefix
                string basePath = null;

                foreach (var kvp in VisionBasePaths)
                {
                    if (Vision_Hostname.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        basePath = kvp.Value;
                        break;
                    }
                }

                if (basePath == null)
                {
                    MessageBox.Show("Unknown Vision hostname prefix: " + Vision_Hostname);
                    return;
                }


                // 3. Find machine folder
                string[] matchingFolders = Directory.GetDirectories(basePath, Vision_Hostname + "*");
                if (matchingFolders.Length == 0)
                {
                    MessageBox.Show("No folder found starting with: " + Vision_Hostname);
                    return;
                }

                string machineFolder = matchingFolders[0];

                // 4. Find validation folder
                string validationPath = Directory.GetDirectories(machineFolder)
                    .FirstOrDefault(d => Path.GetFileName(d)
                        .IndexOf("validation", StringComparison.OrdinalIgnoreCase) >= 0);

                if (validationPath == null)
                {
                    MessageBox.Show("No validation folder found in:\n" + machineFolder);
                    return;
                }

                // 5. YEAR-MONTH FOLDER
                string expectedYearMonth = $"{Vision_Datetime:yyyy_MM}";
                string exactYearMonthPath = Path.Combine(validationPath, expectedYearMonth);

                string bestYearMonthFolder = Directory.Exists(exactYearMonthPath)
                    ? exactYearMonthPath
                    : Directory.GetDirectories(validationPath)
                        .OrderBy(f =>
                        {
                            string digits = Path.GetFileName(f).Replace("_", "");
                            return int.TryParse(digits, out int val)
                                ? Math.Abs(val - (Vision_Datetime.Year * 100 + Vision_Datetime.Month))
                                : int.MaxValue;
                        })
                        .FirstOrDefault();

                if (bestYearMonthFolder == null)
                {
                    MessageBox.Show("No valid Year_Month folder found");
                    return;
                }

                // 6. DAY FOLDER (match last 2 chars)
                string dayString = Vision_Datetime.Day.ToString("00");
                string selectedDayFolder = Directory.GetDirectories(bestYearMonthFolder)
                    .FirstOrDefault(f => Path.GetFileName(f).EndsWith(dayString));

                if (selectedDayFolder == null)
                {
                    MessageBox.Show("The PDF report dosen't exist! \n " + dayString);
                    return;
                }

                // 7. PDF files
                string[] pdfFiles = Directory.GetFiles(selectedDayFolder, "*.pdf");
                if (pdfFiles.Length == 0)
                {
                    MessageBox.Show("The PDF report dosen't exist! \n " + selectedDayFolder);
                    return;
                }

                // 8. Find closest BEFORE Vision_Datetime
                FileInfo bestMatch = pdfFiles
                    .Select(f => new FileInfo(f))
                    .Where(fi => fi.LastWriteTime <= Vision_Datetime)
                    .OrderBy(fi => (Vision_Datetime - fi.LastWriteTime))
                    .FirstOrDefault();

                // fallback: closest after
                if (bestMatch == null)
                {
                    bestMatch = pdfFiles
                        .Select(f => new FileInfo(f))
                        .OrderBy(fi => Math.Abs((fi.LastWriteTime - Vision_Datetime).Ticks))
                        .FirstOrDefault();
                }

                if (bestMatch == null)
                {
                    MessageBox.Show("No suitable PDF found.");
                    return;
                }

                //System.Diagnostics.Process.Start(bestMatch.FullName);
                var viewer = new PdfViewerForm(bestMatch.FullName);
                viewer.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        public static void WriteLogsToDatabase(
            string serialNumber,
            string site,
            string username,
            string type,
            string additionalInfo)
        {
            //MessageBox.Show("DEBUG: WriteLogsToDatabase called");

            try
            {
                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                additionalInfo = additionalInfo + " - " + Environment.MachineName;
                //MessageBox.Show("DEBUG: Formatted Date = " + formattedDate);

                ClsTrackingLog.InsertTrackingLog(
                    serialNumber,
                    site,
                    username,
                    DateTime.Parse(formattedDate),
                    additionalInfo + " - " + Form2.Software_Version,
                    type
                );

                //MessageBox.Show("DEBUG: InsertTrackingLog finished");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR inserting tracking log: " + ex.Message);
            }
        }


        private bool GetHarnessFamilyInformation(
            out string customer,
            out string project,
            out string family,
            out string line,
            out string bank)
        {
            customer = "";
            project = "";
            family = "";
            line = "";
            bank = "";

            GridView gridView = gridControl1.MainView as GridView;

            if (gridView == null)
                return false;

            for (int i = 0; i < gridView.RowCount; i++)
            {
                string station =
                    gridView.GetRowCellValue(i, gridView.Columns[2])?.ToString();

                if (string.IsNullOrWhiteSpace(station))
                    continue;

                bool isETStation =
                    station.IndexOf("ET", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    station.IndexOf("ELEC", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    station.IndexOf("ELECTRIC", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    station.IndexOf("Test Electric", StringComparison.OrdinalIgnoreCase) >= 0;

                if (!isETStation)
                    continue;

                string hostname =
    gridView.GetRowCellValue(i, gridView.Columns[10])?.ToString();

                Harness_Serial_Number =
                    gridView.GetRowCellValue(i, gridView.Columns[6])?.ToString();

                if (string.IsNullOrWhiteSpace(hostname))
                    continue;

                object dtValue =
                    gridView.GetRowCellValue(i, gridView.Columns[7]);

                if (dtValue == null)
                    continue;

                DateTime testDate = Convert.ToDateTime(dtValue);

                DataTable dtBank = null;

                // Support merged hostnames:
                // MOASETS019/MOASETS130/MOASETS150
                string[] hostnames = hostname.Split(
                    new[] { '/' },
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (string host in hostnames)
                {
                    dtBank = ClsOrders.GetStationFamily(
                        host.Trim(),
                        testDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    if (dtBank != null && dtBank.Rows.Count > 0)
                    {
                        break; // First valid hostname found
                    }
                }

                if (dtBank == null || dtBank.Rows.Count == 0)
                    continue;
                bank = dtBank.Rows[0]["Bank"]?.ToString();

                if (string.IsNullOrWhiteSpace(bank))
                    continue;

                //------------------------------------
                // VALIDATE BANK FORMAT
                //------------------------------------
                string[] parts = bank.Split('_');

                // Example:
                // STL_EBEP_TURBO_L2_ET
                if (parts.Length < 5)
                    continue; // <-- Try next ET row

                string stationType = parts[parts.Length - 1];
                string parsedLine = parts[parts.Length - 2];

                if (!parsedLine.StartsWith("L", StringComparison.OrdinalIgnoreCase))
                    continue; // <-- Try next ET row

                if (!stationType.Equals("ET", StringComparison.OrdinalIgnoreCase) &&
                    !stationType.Equals("CT", StringComparison.OrdinalIgnoreCase))
                {
                    continue; // <-- Try next ET row
                }

                //------------------------------------
                // CUSTOMER MAPPING
                //------------------------------------
                string customerCode = parts[0];

                if (customerCode.Equals("PSA", StringComparison.OrdinalIgnoreCase) ||
                    customerCode.Equals("STL", StringComparison.OrdinalIgnoreCase))
                {
                    customer = "STELLANTIS";
                }
                else if (customerCode.Equals("TYT", StringComparison.OrdinalIgnoreCase) ||
                         customerCode.Equals("TOY", StringComparison.OrdinalIgnoreCase))
                {
                    customer = "TOYOTA";
                }
                else if (customerCode.Equals("SUZ", StringComparison.OrdinalIgnoreCase))
                {
                    customer = "SUZUKI";
                }
                else if (customerCode.Contains("RNLT", StringComparison.OrdinalIgnoreCase) ||
                         customerCode.Contains("RENA", StringComparison.OrdinalIgnoreCase) ||
                         customerCode.Contains("RSA", StringComparison.OrdinalIgnoreCase) ||
                         customerCode.Contains("RNL", StringComparison.OrdinalIgnoreCase) ||
                         customerCode.Contains("RNT", StringComparison.OrdinalIgnoreCase))
                {
                    customer = "RENAULT";
                }
                else if (customerCode.Contains("NIS", StringComparison.OrdinalIgnoreCase) ||
                         customerCode.Contains("NISS", StringComparison.OrdinalIgnoreCase) ||
                         customerCode.Contains("NSN", StringComparison.OrdinalIgnoreCase)) 
                {
                    customer = "NISSAN";
                }
                else
                {
                    customer = customerCode;
                }

                project = parts[1];

                family = string.Join("_",
                    parts.Skip(2).Take(parts.Length - 4));

                line = parsedLine;

                return true; // First VALID bank found
            }

            return false;
        }


        private DataTable MergeDuplicateRows(DataTable dt)
        {
            DataTable result = dt.Clone();

            var compareColumns = dt.Columns.Cast<DataColumn>()
                                           .Where(c => c.ColumnName != "Hostname")
                                           .ToList();

            var groups = dt.AsEnumerable()
                           .GroupBy(row =>
                               string.Join("§",
                                   compareColumns.Select(c =>
                                       row[c] == DBNull.Value ? "" : row[c].ToString())));

            foreach (var group in groups)
            {
                DataRow newRow = result.NewRow();

                foreach (DataColumn col in dt.Columns)
                {
                    newRow[col.ColumnName] = group.First()[col.ColumnName];
                }

                newRow["Hostname"] = string.Join("/",
                    group.Select(r => r["Hostname"]?.ToString())
                         .Where(h => !string.IsNullOrWhiteSpace(h))
                         .Distinct());

                result.Rows.Add(newRow);
            }

            return result;
        }

        private void chartControl1_Click(object sender, EventArgs e)
        {

        }
    }
}