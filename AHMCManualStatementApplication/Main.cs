using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Globalization;
using MetroFramework.Controls;
using AHMCManualStatementApplication.Properties;

namespace AHMCManualStatementApplication
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        int tabIndex;
        string facility = String.Empty;

        MetroTile tile = null;
        String tileName = String.Empty;

        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                         "Data Source=W:\\ETH\\CQ Macro\\analyst\\AHMC Manual Statement\\database\\ManualStatementDatabase.accdb;" +
                         "Persist Security Info=False";
        string query = "";
        OleDbConnection conn = null;
        DataView view;
        StringBuilder sb = new StringBuilder();
        string viewDate = String.Empty;

        public Main()
        {
            InitializeComponent();

            this.StyleManager = msmMain;
        }

        // Start page: Home screen
        private void Form1_Load(object sender, EventArgs e)
        {
            tbCtrlPages.SelectTab(tbHome);

            try
            {
                conn = new OleDbConnection(connStr);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                ActivateFunctions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Form Closing: close database connection
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Make functions active when facility selected
        private void ActivateFunctions()
        {
            if (facility == String.Empty) {
                this.tileView.Enabled = false;
                this.tileGenerate.Enabled = false;
                this.tileBack.Enabled = false;
                this.tileNext.Enabled = false;
            } else {
                this.tileView.Enabled = true;
                this.tileGenerate.Enabled = true;
                this.tileBack.Enabled = true;
                this.tileNext.Enabled = true;
            }
        }

        // Go to previous page
        private void tileBack_Click(object sender, EventArgs e)
        {
            tabIndex = tbCtrlPages.SelectedIndex;
            if (tabIndex > 0)
                tbCtrlPages.SelectTab(tabIndex - 1);
        }

        // Go to next page
        private void tileNext_Click(object sender, EventArgs e)
        {
            tabIndex = tbCtrlPages.SelectedIndex;
            if (tabIndex < tbCtrlPages.TabCount - 1)
                tbCtrlPages.SelectTab(tabIndex + 1);
        }

        // Go to Home
        private void tileHome_Click(object sender, EventArgs e)
        {
            this.tbCtrlPages.SelectedTab = tbHome;
        }

        // Show view options
        private void tileView_Click(object sender, EventArgs e)
        {
            ctxViewMenu.Show(tileView, tileView.Width, 0);
        }

        // Go to Generate Statements page
        private void tileGenerate_Click(object sender, EventArgs e)
        {
            this.tbCtrlPages.SelectedTab = tbGenerateStmnt;
        }

        // Mouse Enter: show tile function name
        private void tileNav_MouseEnter(object sender, EventArgs e)
        {
            tile = sender as MetroTile;
            tileName = tile.Name;
            String txtName = String.Empty;

            switch (tileName) {
                case "tileHome":
                    txtName = "HOME";
                    break;
                case "tileView":
                    txtName = "VIEW";
                    break;
                case "tileGenerate":
                    txtName = "GENERATE";
                    break;
                case "tileAddAccount":
                    txtName = "ADD";
                    break;
                case "tileDeleteAccount":
                    txtName = "DEL";
                    break;
                case "tileExport":
                    txtName = "EXCEL";
                    break;
                default:
                    break;
            }
            tile.TileImage = null;
            tile.Text = txtName;
            tile.TextAlign = ContentAlignment.MiddleCenter;
        }

        // Mouse Leave: show tile icon
        private void tileNav_MouseLeave(object sender, EventArgs e)
        {
            tile = sender as MetroTile;
            tileName = tile.Name;
            Image tileImg = null;

            switch (tileName) {
                case "tileHome":
                    tileImg = Resources.home;
                    break;
                case "tileView":
                    tileImg = Resources.view;
                    break;
                case "tileGenerate":
                    tileImg = Resources.generate;
                    break;
                case "tileAddAccount":
                    tileImg = Resources.add;
                    break;
                case "tileDeleteAccount":
                    tileImg = Resources.delete;
                    break;
                case "tileExport":
                    tileImg = Resources.export;
                    break;
                default:
                    break;
            }
            tile.Text = String.Empty;
            tile.TileImage = tileImg;
            tile.TileImageAlign = ContentAlignment.MiddleCenter;
        }

        // Facility button clicked
        private void btnFac_Click(object sender, EventArgs e)
        {
            ClearFacilityButtons();

            // Highlight selected button
            MetroButton btnFac = sender as MetroButton;
            btnFac.BackColor = SystemColors.Highlight;

            // Set facility
            string btnName = btnFac.Name;

            switch (btnName) {
                case "btnARMC":
                    facility = "ARMC";
                    break;
                case "btnGAR":
                    facility = "GAR";
                    break;
                case "btnGEM":
                    facility = "GEM";
                    break;
                case "btnMPH":
                    facility = "MPH";
                    break;
                case "btnSGV":
                    facility = "SGV";
                    break;
                case "btnWHT":
                    facility = "WHT";
                    break;
                default:
                    facility = String.Empty;
                    break;
            }
            this.lblActiveFacility.Text = facility;
            ActivateFunctions();
        }

        private void ClearFacilityButtons() 
        {
            foreach (Control ctl in this.tbHome.Controls) {
                if (ctl is MetroButton) {
                    ctl.BackColor = SystemColors.ButtonHighlight;
                }
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                // Get view date range
                DateTime today = DateTime.Today;

                switch ((sender as ToolStripMenuItem).Text) {
                    case "&Today":
                        viewDate = "= '" + today.AddMonths(-1).ToString("M/d/yyyy") + "'";
                        break;
                    case "&Last Month":
                        var month = new DateTime(today.Year, today.Month, 1);
                        string first = month.AddMonths(-1).ToString("M/d/yyyy");
                        string last = month.AddDays(-1).ToString("M/d/yyyy");
                        viewDate = ">= '" + first + "' AND [Date Requested] <= '" + last + "'";
                        break;
                    case "&Specific Date":
                        SpecificDatePicker dtPicker = new SpecificDatePicker();
                        var result = dtPicker.ShowDialog();

                        if (result == DialogResult.OK) {
                            viewDate = "= '" + dtPicker.ReturnSpecificDate + "'";
                        } else {
                            return;
                        }
                        break;
                    case "&All":
                        viewDate = String.Empty;
                        break;
                    default:
                        viewDate = String.Empty;
                        break;
                }

                // Query accounts
                dataGridAccounts.Rows.Clear();

                Cursor.Current = Cursors.WaitCursor;
                RunQuery();
                tbCtrlPages.SelectedTab = tbAccounts;
                dataGridAccounts.Focus();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        // Query accounts
        private void RunQuery()
        {
            query = "SELECT log.DateRequested AS [Date Requested], " +
                    "fac.FacilityAbbr AS [Facility], " +
                    "log.AcctNumber AS [Account #], " +
                    "log.PatientName AS [Patient Name], " +
                    "log.PatResp AS [Patient Responsibility], " +
                    "log.Completed AS [Completed] " +
                    "FROM tblManualStmntLog AS [log] " +
                    "LEFT JOIN tblFacility AS [fac] " +
                    "ON log.FacilityID = fac.FacilityID " +
                    "WHERE fac.FacilityAbbr = @Facility";

            try {
                // Get account data
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.Add("@Facility", OleDbType.VarChar).Value = facility;

                using (OleDbDataAdapter a = new OleDbDataAdapter(cmd)) {
                    DataTable dt = new DataTable();
                    a.SelectCommand = cmd;
                    a.Fill(dt);

                    // Sort by descending Date Requested
                    view = dt.DefaultView;
                    view.Sort = "[Date Requested] desc";

                    // Filter by Statement Date / Completed
                    sb.Clear();
                    FilterByCompleted();
                    FilterByStatementDate();
                    view.RowFilter = sb.ToString();
                    dt = view.ToTable();

                    dataGridAccounts.DataSource = dt;
                    dataGridAccounts.AutoResizeColumns();
                    dataGridAccounts.Columns["Patient Responsibility"].DefaultCellStyle.Format = "#,##0.00";

                    lblTotalRows.Text = String.Format("Total rows: {0}", dataGridAccounts.RowCount);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        // Filter accounts by isCompleted
        private void FilterByCompleted()
        {
            if (ckBoxCompletedFilter.Checked && !ckBoxUncompletedFilter.Checked) {
                sb.Append("[Completed] = True");
            }
            else if (!ckBoxCompletedFilter.Checked && ckBoxUncompletedFilter.Checked) {
                sb.Append("[Completed] = False");
            }
            MessageBox.Show(sb.ToString());
        }

        // Filter by statement date
        private void FilterByStatementDate()
        {
            if (sb.Length > 0) {
                sb.Append(" AND ");
            }

            if (viewDate != String.Empty) {
                sb.Append("[Date Requested] " + viewDate);
            }
            MessageBox.Show(sb.ToString());
        }

        private void ckBoxFilter_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            RunQuery();
            Cursor.Current = Cursors.Default;
        }
    }
}
