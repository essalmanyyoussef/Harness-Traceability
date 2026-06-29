using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Drawing.Printing;
using DevExpress.XtraPrinting.Drawing;

namespace Harness_Traceability
{
    public partial class frmMoodle : Form
    {
        private TreeList treeListSites;
        private ChartControl chartMemberRadar;
        private GridControl gridCourses;
        private GridView gridViewCourses;
        private PictureBox picCandidate;

        // Labels above the radar chart
        private Label lblName;
        private Label lblSite;
        private Label lblIssueDate;

        // Export button
        private Button btnExportPdf;

        public frmMoodle()
        {
            InitializeComponent();
        }

        // ============================================================
        // DATA MODELS
        // ============================================================

        public class Record
        {
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Site { get; set; }
            public string Course { get; set; }
            public string TestName { get; set; }
            public double FinalGrade { get; set; }
            public double PassGrade { get; set; }
            public string TrainingStatus { get; set; }
            public DateTime? DateTestTaken { get; set; }

        }

        public class SiteNode
        {
            public string Site { get; set; }
            public BindingList<MemberNode> Members { get; set; } = new BindingList<MemberNode>();
        }

        public class MemberNode
        {
            public int Id { get; set; }
            public int ParentId { get; set; }
            public string Site { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName => $"{FirstName} {LastName}";
            public List<CourseGrade> Courses { get; set; } = new List<CourseGrade>();
        }

        public class CourseGrade
        {
            public string Course { get; set; }
            public double FinalGrade { get; set; }
            public double PassGrade { get; set; }
        }

        // ============================================================
        // CSV LOADER (SEMICOLON VERSION) — SKIP ROWS WITHOUT SITE
        // ============================================================

        private List<Record> LoadCsv(string path)
        {
            var list = new List<Record>();

            if (!File.Exists(path))
            {
                MessageBox.Show("CSV file not found:\n" + path, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return list;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(path, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to read CSV file:\n" + ex.Message, "Read error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return list;
            }

            if (lines.Length <= 1)
            {
                MessageBox.Show("CSV file is empty or contains only header.", "Empty CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return list;
            }

            foreach (var raw in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(raw))
                    continue;

                var cols = raw.Split(';');

                if (cols.Length < 10)
                    continue;

                var siteValue = cols[3]?.Trim();
                if (string.IsNullOrWhiteSpace(siteValue))
                    continue;

                list.Add(new Record
                {
                    UserName = cols[0].Trim(),
                    FirstName = cols[1].Trim(),
                    LastName = cols[2].Trim(),
                    Site = siteValue,
                    Course = cols[4].Trim(),
                    TestName = cols[5].Trim(),
                    FinalGrade = double.TryParse(cols[6], out var g) ? g : 0,
                    PassGrade = double.TryParse(cols[7], out var p) ? p : 0,
                    TrainingStatus = cols[8].Trim(),
                    DateTestTaken = DateTime.TryParse(cols[9], out var d) ? d : (DateTime?)null
                });
            }

            return list;
        }

        // ============================================================
        // BUILD SITE → MEMBER → COURSE HIERARCHY
        // ============================================================

        private List<SiteNode> BuildSites(List<Record> records)
        {
            return records
                .GroupBy(r => r.Site)
                .Select(g => new SiteNode
                {
                    Site = g.Key,
                    Members = new BindingList<MemberNode>(
                        g.GroupBy(r => new { r.UserName, r.FirstName, r.LastName })
                         .Select(mg => new MemberNode
                         {
                             UserName = mg.Key.UserName,
                             FirstName = mg.Key.FirstName,
                             LastName = mg.Key.LastName,

                             // 👉 Replace this block:
                             Courses = mg.GroupBy(x => x.Course)
                                         .Select(cg => new CourseGrade
                                         {
                                             Course = cg.Key,
                                             FinalGrade = cg.Max(x => x.FinalGrade),
                                             PassGrade = cg.Max(x => x.PassGrade)
                                         }).ToList()

                         }).ToList()
                    )
                }).ToList();
        }


        // ============================================================
        // TREE BINDING
        // ============================================================

        private void BindTreeList(IEnumerable<SiteNode> sites)
        {
            var nodes = new List<object>();
            int id = 1;

            foreach (var s in sites)
            {
                var siteId = id++;

                nodes.Add(new
                {
                    Id = siteId,
                    ParentId = 0,
                    Site = s.Site,
                    FullName = s.Site,
                    UserName = "",
                    Member = (MemberNode)null
                });

                foreach (var m in s.Members)
                {
                    var node = new MemberNode
                    {
                        Id = id++,
                        ParentId = siteId,
                        Site = s.Site,
                        UserName = m.UserName,
                        FirstName = m.FirstName,
                        LastName = m.LastName,
                        Courses = m.Courses
                    };

                    nodes.Add(new
                    {
                        Id = node.Id,
                        ParentId = node.ParentId,
                        Site = node.Site,
                        FullName = node.FullName,
                        UserName = node.UserName,
                        Member = node
                    });
                }
            }

            treeListSites.DataSource = nodes;
            treeListSites.ExpandAll();
        }

        // ============================================================
        // FORM LOAD
        // ============================================================

        private void frmMoodle_Load(object sender, EventArgs e)
        {
            // Layout: left = tree, right = (top labels+chart, bottom grid)
            var mainSplit = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 100
            };
            this.Controls.Add(mainSplit);

            // Left: TreeList
            treeListSites = new TreeList
            {
                Dock = DockStyle.Fill
            };
            mainSplit.Panel1.Controls.Add(treeListSites);

            // Right: nested split (chart top, grid bottom)
            var rightSplit = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 360
            };
            mainSplit.Panel2.Controls.Add(rightSplit);

            // Top-right: panel that contains labels, export button and chart
            var topPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };
            rightSplit.Panel1.Controls.Add(topPanel);

            // --- Header: TableLayoutPanel with fixed 80x80 Picture + Labels + Export button ---
            var headerPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 90,                 // FIXED HEIGHT
                ColumnCount = 5,
                RowCount = 1,
                AutoSize = false,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            // Columns: Picture (fixed), Name, Site, IssueDate, Button
            headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F)); // FIXED 80x80 + padding
            headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            topPanel.Controls.Add(headerPanel);

            // PictureBox (80x80 FIXED)
            picCandidate = new PictureBox
            {
                Width = 80,
                Height = 80,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };
            headerPanel.Controls.Add(picCandidate, 0, 0);

            // Name label
            lblName = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Name: ",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            headerPanel.Controls.Add(lblName, 1, 0);

            // Site label
            lblSite = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Site: ",
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            headerPanel.Controls.Add(lblSite, 2, 0);

            // Issue date label
            lblIssueDate = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Issue date: " + DateTime.Now.ToString("dd/MM/yyyy"),
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            headerPanel.Controls.Add(lblIssueDate, 3, 0);

            // Export button
            btnExportPdf = new Button
            {
                Text = "Export PDF",
                AutoSize = true,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Margin = new Padding(6)
            };
            btnExportPdf.Click += BtnExportPdf_Click;
            headerPanel.Controls.Add(btnExportPdf, 4, 0);

            // Chart (fills remaining area of topPanel)
            chartMemberRadar = new ChartControl
            {
                Dock = DockStyle.Fill

            };
            topPanel.Controls.Add(chartMemberRadar);
            chartMemberRadar.BringToFront();

            // Bottom-right: Grid (bottom)
            gridCourses = new GridControl
            {
                Dock = DockStyle.Fill
            };
            gridViewCourses = new GridView(gridCourses);
            gridCourses.MainView = gridViewCourses;
            gridCourses.ViewCollection.Add(gridViewCourses);
            rightSplit.Panel2.Controls.Add(gridCourses);

            // -------------------------
            // Appearance fixes (force readable colors)
            // -------------------------
            try
            {
                treeListSites.LookAndFeel.UseDefaultLookAndFeel = false;
                treeListSites.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                treeListSites.LookAndFeel.SkinName = "DevExpress Style";

                treeListSites.Appearance.Row.ForeColor = Color.Black;
                treeListSites.Appearance.Row.BackColor = Color.White;
                treeListSites.Appearance.FocusedRow.ForeColor = Color.Black;
                treeListSites.Appearance.FocusedRow.BackColor = Color.LightBlue;
                treeListSites.Appearance.SelectedRow.ForeColor = Color.Black;
                treeListSites.Appearance.SelectedRow.BackColor = Color.LightBlue;
                treeListSites.Appearance.HideSelectionRow.ForeColor = Color.Black;
                treeListSites.Appearance.HideSelectionRow.BackColor = Color.WhiteSmoke;

                treeListSites.Appearance.HeaderPanel.ForeColor = Color.Black;
                treeListSites.Appearance.HeaderPanel.BackColor = Color.Gainsboro;
                treeListSites.Appearance.HeaderPanel.Font = new Font(treeListSites.Appearance.HeaderPanel.Font, FontStyle.Bold);

                treeListSites.OptionsView.ShowIndicator = false;
                treeListSites.OptionsView.ShowColumns = true;
                treeListSites.OptionsView.ShowHorzLines = true;
                treeListSites.OptionsView.ShowVertLines = true;
            }
            catch
            {
                // ignore appearance exceptions
            }

            // TreeList columns
            TreeListColumn colSite = treeListSites.Columns.Add();
            colSite.FieldName = "Site";
            colSite.Caption = "Site";
            colSite.Visible = true;
            colSite.Width = 220;

            TreeListColumn colFullName = treeListSites.Columns.Add();
            colFullName.FieldName = "FullName";
            colFullName.Caption = "Name";
            colFullName.Visible = true;
            colFullName.Width = 240;

            TreeListColumn colUser = treeListSites.Columns.Add();
            colUser.FieldName = "UserName";
            colUser.Caption = "UserName";
            colUser.Visible = true;
            colUser.Width = 160;

            treeListSites.KeyFieldName = "Id";
            treeListSites.ParentFieldName = "ParentId";
            treeListSites.OptionsBehavior.Editable = false;
            treeListSites.FocusedNodeChanged += treeListSites_FocusedNodeChanged;

            // GridView columns (initially empty)
            gridViewCourses.OptionsView.ShowGroupPanel = false;
            gridViewCourses.OptionsBehavior.Editable = false;
            gridViewCourses.Columns.Clear();

            var gColCourse = gridViewCourses.Columns.AddVisible("Course", "Course");
            gColCourse.Width = 360;

            var gColGrade = gridViewCourses.Columns.AddVisible("CourseGrade", "CourseGrade");
            gColGrade.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gColGrade.DisplayFormat.FormatString = "N2";
            gColGrade.Width = 120;

            var gColRank = gridViewCourses.Columns.AddVisible("Rank", "Rank");
            gColRank.Width = 80;

            // Footer: average for CourseGrade (fully qualified enum to avoid ambiguity)
            gridViewCourses.OptionsView.ShowFooter = true;
            gColGrade.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            gColGrade.SummaryItem.DisplayFormat = "Avg: {0:N2}";

            // Row style event for rank color coding
            gridViewCourses.RowCellStyle += GridViewCourses_RowCellStyle;

            // Load CSV and bind tree
            string csvPath = @"C:\My Tasks\All Data.csv";
            var records = LoadCsv(csvPath);

            var sites = BuildSites(records);
            BindTreeList(sites);

            // Ensure columns visible
            foreach (TreeListColumn c in treeListSites.Columns)
            {
                c.Visible = true;
                if (c.Width < 50) c.Width = 120;
            }

            // Expand and select first node if available
            if (treeListSites.Nodes.Count > 0)
            {
                var first = treeListSites.Nodes[0];
                treeListSites.FocusedNode = first;
                treeListSites.SelectNode(first);
            }

            // Initialize labels
            lblName.Text = "Name: ";
            lblSite.Text = "Site: ";
            lblIssueDate.Text = "Issue date: " + DateTime.Now.ToString("dd/MM/yyyy");
        }


        private void LoadCandidatePhoto(string firstName, string lastName)
        {
            try
            {
                string folder = @"C:\My Tasks\Persons";
                if (!Directory.Exists(folder))
                {
                    picCandidate.Image = null;
                    return;
                }

                string[] files = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly)
                                          .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                                                   || f.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                                                   || f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                                          .ToArray();

                if (files.Length == 0)
                {
                    picCandidate.Image = null;
                    return;
                }

                // Normalize names
                string fn = firstName.ToLower().Replace(" ", "").Replace("-", "").Replace("_", "");
                string ln = lastName.ToLower().Replace(" ", "").Replace("-", "").Replace("_", "");

                // Build possible patterns
                List<string> patterns = new List<string>
        {
            fn + ln,
            ln + fn,
            fn + "_" + ln,
            ln + "_" + fn,
            fn + "-" + ln,
            ln + "-" + fn,
            fn + " " + ln,
            ln + " " + fn
        };

                // Fuzzy search: pick best match
                string bestFile = null;
                int bestScore = 0;

                foreach (string file in files)
                {
                    string name = Path.GetFileNameWithoutExtension(file).ToLower()
                                    .Replace(" ", "").Replace("-", "").Replace("_", "");

                    foreach (string p in patterns)
                    {
                        int score = SimilarityScore(name, p);
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestFile = file;
                        }
                    }
                }

                // Accept match only if similarity ≥ 70%
                if (bestScore >= 70 && bestFile != null)
                {
                    picCandidate.Image = Image.FromFile(bestFile);
                }
                else
                {
                    picCandidate.Image = null; // B1: blank if not found
                }
            }
            catch
            {
                picCandidate.Image = null;
            }
        }

        private int SimilarityScore(string a, string b)
        {
            int matches = 0;
            int len = Math.Min(a.Length, b.Length);

            for (int i = 0; i < len; i++)
                if (a[i] == b[i]) matches++;

            return (int)((matches / (double)Math.Max(a.Length, b.Length)) * 100);
        }

        // ============================================================
        // TREE EVENT → UPDATE RADAR CHART + GRID + LABELS
        // ============================================================

        private void treeListSites_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null)
            {
                chartMemberRadar.Series.Clear();
                gridCourses.DataSource = null;
                lblName.Text = "Name: ";
                lblSite.Text = "Site: ";
                lblIssueDate.Text = "Issue date: " + DateTime.Now.ToString("dd/MM/yyyy");
                return;
            }

            var memberObj = e.Node.GetValue("Member") as MemberNode;

            if (memberObj != null)
            {
                // Update labels
                lblName.Text = "Name: " + memberObj.FullName;
                lblSite.Text = "Site: " + memberObj.Site;
                lblIssueDate.Text = "Issue date: " + DateTime.Now.ToString("dd/MM/yyyy");
                LoadCandidatePhoto(memberObj.FirstName, memberObj.LastName);
                ShowMemberRadar(memberObj);
                PopulateGridForMember(memberObj);
            }
            else
            {
                chartMemberRadar.Series.Clear();
                gridCourses.DataSource = null;
                lblName.Text = "Name: ";
                lblSite.Text = "Site: ";
                lblIssueDate.Text = "Issue date: " + DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        // ============================================================
        // RADAR CHART
        // ============================================================

        private void ShowMemberRadar(MemberNode member)
        {
            chartMemberRadar.Series.Clear();

            var series = new Series(member.FullName, ViewType.Line);
            series.View = new RadarLineSeriesView();

            foreach (var c in member.Courses)
            {
                double result = 0;

                // Avoid division by zero
                if (c.PassGrade > 0)
                    result = (c.PassGrade / c.FinalGrade) * 80;

                series.Points.Add(new SeriesPoint(c.Course, result));
            }

            chartMemberRadar.Series.Add(series);

            if (chartMemberRadar.Diagram is RadarDiagram diagram)
            {
                diagram.AxisY.WholeRange.SetMinMaxValues(0, 100);
                diagram.AxisX.Label.Angle = 0;
            }

            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
        }


        // ============================================================
        // GRID POPULATION
        // ============================================================

        private void PopulateGridForMember(MemberNode member)
        {
            if (member == null)
            {
                gridCourses.DataSource = null;
                return;
            }

            var dt = new DataTable();
            dt.Columns.Add("Course", typeof(string));
            dt.Columns.Add("CourseGrade", typeof(double));   // Now holds (Final/Pass)*100
            dt.Columns.Add("Rank", typeof(string));

            foreach (var c in member.Courses.OrderBy(x => x.Course))
            {
                double result = 0;

                if (c.PassGrade > 0)
                    result = (c.FinalGrade / c.PassGrade) * 100;

                var rank = GetRank(result);

                dt.Rows.Add(c.Course, result, rank);
            }

            gridCourses.DataSource = dt;
            gridViewCourses.BestFitColumns();
        }

        // ============================================================
        // RANKING LOGIC
        // ============================================================

        private string GetRank(double grade)
        {
            if (grade >= 96.0) return "S";
            if (grade >= 91.0 && grade <= 95.0) return "A";
            if (grade >= 80.0 && grade <= 90.0) return "B";
            return "C";
        }

        // ============================================================
        // GRID ROW STYLE (color coding by Rank)
        // ============================================================

        private void GridViewCourses_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return; // skip group/footer rows

            try
            {
                var view = sender as GridView;
                var rankObj = view.GetRowCellValue(e.RowHandle, "Rank");
                if (rankObj == null) return;

                var rank = rankObj.ToString();

                switch (rank)
                {
                    case "S":
                        e.Appearance.BackColor = Color.FromArgb(198, 239, 206);
                        e.Appearance.ForeColor = Color.Black;
                        break;
                    case "A":
                        e.Appearance.BackColor = Color.FromArgb(221, 235, 247);
                        e.Appearance.ForeColor = Color.Black;
                        break;
                    case "B":
                        e.Appearance.BackColor = Color.FromArgb(255, 242, 204);
                        e.Appearance.ForeColor = Color.Black;
                        break;
                    case "C":
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                        e.Appearance.ForeColor = Color.Black;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                // ignore styling errors
            }
        }

        // ============================================================
        // EXPORT TO PDF (right side only) using DevExpress CompositeLink
        // ============================================================

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure there is a selected candidate
                if (string.IsNullOrWhiteSpace(lblName.Text) || lblName.Text == "Name: ")
                {
                    MessageBox.Show("Please select a candidate from the list before exporting.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                    saveFileDialog.DefaultExt = "pdf";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.Title = "Save Candidate Details as PDF";
                    // Suggest filename using candidate name and site
                    string safeName = lblName.Text.Replace("Name: ", "").Replace(' ', '_').Replace('\\', '_').Replace('/', '_');
                    string safeSite = lblSite.Text.Replace("Site: ", "").Replace(' ', '_').Replace('\\', '_').Replace('/', '_');
                    saveFileDialog.FileName = $"{safeName}_{safeSite}_{DateTime.Now:yyyyMMdd}.pdf";

                    if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                    string filePath = saveFileDialog.FileName;

                    // Create a composite link using a new PrintingSystem
                    var printingSystem = new PrintingSystem();
                    var compositeLink = new CompositeLink(printingSystem);

                    // 1) Title / header link (labels)
                    Link headerLink = new Link();
                    headerLink.CreateDetailArea += (s, ea) =>
                    {
                        ea.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
                        ea.Graph.Font = new Font("Arial", 14, FontStyle.Bold);
                        float y = 0;

                        // Draw Name
                        ea.Graph.DrawString(lblName.Text, Color.Black, new RectangleF(0, y, ea.Graph.ClientPageSize.Width, 24), BorderSide.None);
                        y += 24;

                        // Draw Site
                        ea.Graph.Font = new Font("Arial", 12, FontStyle.Regular);
                        ea.Graph.DrawString(lblSite.Text, Color.Black, new RectangleF(0, y, ea.Graph.ClientPageSize.Width, 20), BorderSide.None);
                        y += 20;

                        // Draw Issue date
                        ea.Graph.DrawString(lblIssueDate.Text, Color.Black, new RectangleF(0, y, ea.Graph.ClientPageSize.Width, 20), BorderSide.None);
                        y += 24;

                        // Draw separator line using the correct overload
                        ea.Graph.DrawLine(
                            new PointF(0f, y),
                            new PointF(ea.Graph.ClientPageSize.Width, y),
                            Color.Gray,
                            1f);
                    };
                    compositeLink.Links.Add(headerLink);

                    // 2) Chart link (printable chart)
                    var chartLink = new PrintableComponentLink(printingSystem)
                    {
                        Component = chartMemberRadar
                    };
                    compositeLink.Links.Add(chartLink);

                    // 3) Grid link (printable grid)
                    var gridLink = new PrintableComponentLink(printingSystem)
                    {
                        Component = gridCourses
                    };
                    compositeLink.Links.Add(gridLink);

                    // Page settings: A4 portrait using DevExpress DXPaperKind
                    compositeLink.PaperKind = DXPaperKind.A4;
                    compositeLink.Landscape = false;

                    // Optional: add page numbers watermark/footer after pages are built
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
                                ForeColor = Color.Gray,
                                TextTransparency = 200,
                                ShowBehind = false
                            });
                        }
                    };

                    // Create document and export to PDF
                    compositeLink.CreateDocument();
                    compositeLink.ExportToPdf(filePath);

                    MessageBox.Show("PDF Exported successfully to:\n" + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during export:\n" + ex.Message, "Export error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
