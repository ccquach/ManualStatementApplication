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
    interface IMain
    {
        string facility { get; set; }
        string account { get; set; }
        string Facility { get; set; }
        string Account { get; set; }
        string PatientName { get; set; }
        string PatientLiability { get; set; }
        string DateRequested { get; set; }
        string StatementFirst { get; set; }
        string StatementSecond { get; set; }
        string StatementFinal { get; set; }
        string CommentNote { get; set; }
        bool Completed { get; set; }
        string DemoFacility { get; set; }
        string DemoAccount { get; set; }
        string DemoPatientName { get; set; }
        string DemoPatientLiability { get; set; }
        string DischargeDate { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zipcode { get; set; }
    }
    
    public partial class Main : MetroFramework.Forms.MetroForm, IMain
    {
        #region Variables
        int tabIndex;

        MetroTile tile = null;
        String tileName = String.Empty;

        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                         "Data Source=W:\\ETH\\CQ Macro\\analyst\\AHMC Manual Statement\\database\\ManualStatementDatabase.accdb;" +
                         "Persist Security Info=False;";
        string query = "";
        OleDbConnection conn = null;
        DataView view;
        StringBuilder sb = new StringBuilder();

        string stmntCycle = "First Statement";
        string viewDate = String.Empty;
        string viewDateStr = String.Empty;
        string first = String.Empty;
        string last = String.Empty;
        bool isRangeDate = false;
        #endregion

        #region Properties
        public string facility { get; set; }
        public string account { get; set; }

        // Statement History
        public string Facility {
            get { return this.txtFacility.Text; }
            set { this.txtFacility.Text = value; }
        }

        public string Account {
            get { return this.txtAccount.Text; }
            set { this.txtAccount.Text = value; }
        }

        public string PatientName {
            get { return this.txtPatientName.Text; }
            set { this.txtPatientName.Text = value; }
        }

        public string PatientLiability {
            get { return this.txtPtLiab.Text; }
            set { this.txtPtLiab.Text = value; }
        }

        public string DateRequested {
            get { return this.txtRequestedDate.Text; }
            set { this.txtRequestedDate.Text = value; }
        }

        public string StatementFirst {
            get { return this.txtFirstStmnt.Text; }
            set { this.txtFirstStmnt.Text = value; }
        }

        public string StatementSecond {
            get { return this.txtSecondStmnt.Text; }
            set { this.txtSecondStmnt.Text = value; }
        }

        public string StatementFinal {
            get { return this.txtFinalStmnt.Text; }
            set { this.txtFinalStmnt.Text = value; }
        }

        public string CommentNote {
            get { return this.txtNote.Text; }
            set { this.txtNote.Text = value; }
        }

        public bool Completed {
            get { return this.ckBoxCompleted.Checked; }
            set { this.ckBoxCompleted.Checked = value; }
        }

        // Demographics
        public string DemoFacility {
            get { return this.txtDemoFacility.Text; }
            set { this.txtDemoFacility.Text = value; }
        }

        public string DemoAccount {
            get { return this.txtDemoAccount.Text; }
            set { this.txtDemoAccount.Text = value; }
        }

        public string DemoPatientName {
            get { return this.txtDemoPtName.Text; }
            set { this.txtDemoPtName.Text = value; }
        }

        public string DemoPatientLiability {
            get { return this.txtDemoPtLiab.Text; }
            set { this.txtDemoPtLiab.Text = value; }
        }
        public string DischargeDate {
            get { return this.txtDischarge.Text; }
            set { this.txtDischarge.Text = value; }
        }

        public string AddressLine1 {
            get { return this.txtAddress1.Text; }
            set { this.txtAddress1.Text = value; }
        }

        public string AddressLine2 {
            get { return this.txtAddress2.Text; }
            set { this.txtAddress2.Text = value; }
        }

        public string City {
            get { return this.txtCity.Text; }
            set { this.txtCity.Text = value; }
        }

        public string State {
            get { return this.txtState.Text; }
            set { this.txtState.Text = value; }
        }

        public string Zipcode {
            get { return this.txtZipcode.Text; }
            set { this.txtZipcode.Text = value; }
        }
        #endregion

        public Main()
        {
            InitializeComponent();
            this.StyleManager = msmMain;
        }

        #region Manual Statement Database Connection
        // Start page: Home screen
        private void Form1_Load(object sender, EventArgs e)
        {
            tbCtrlPages.SelectTab(tbHome);
            try
            {
                conn = new OleDbConnection(connStr);
                if (conn.State == ConnectionState.Closed) {
                    conn.Open();
                }
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
                if (conn.State == ConnectionState.Open) {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Button Accessibility
        // Make functions active when facility selected
        private void ActivateFunctions()
        {
            if (facility == String.Empty) {
                this.tileView.Enabled = false;
                this.tileGenerate.Enabled = false;
                this.tileBack.Enabled = false;
                this.tileNext.Enabled = false;
            }
            else {
                this.tileView.Enabled = true;
                this.tileGenerate.Enabled = true;
                this.tileBack.Enabled = true;
                this.tileNext.Enabled = true;
            }
        }
        #endregion

        #region Button Navigation
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
                case "tileEdit":
                    txtName = "EDIT";
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
                case "tileEdit":
                    tileImg = Resources.edit;
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
        #endregion

        #region Facility Selection
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
        #endregion

        #region Date view selection
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                // Get view date range
                DateTime today = DateTime.Today;

                switch ((sender as ToolStripMenuItem).Text) {
                    case "&Today":
                        if (today.DayOfWeek == DayOfWeek.Monday) {
                            first = new DateTime(today.Year, today.AddMonths(-1).Month, today.AddDays(-2).Day).ToShortDateString();
                            last = today.AddMonths(-1).ToShortDateString();
                            viewDateStr = $">= '{first}' AND [{stmntCycle}] <= '{last}'";
                            isRangeDate = true;
                        }
                        else {
                            viewDate = today.AddMonths(-1).ToShortDateString();
                            viewDateStr = $"= '{viewDate}'";
                        }
                        break;
                    case "&Last Month":
                        var month = new DateTime(today.Year, today.Month, 1);
                        first = month.AddMonths(-1).ToShortDateString();
                        last = month.AddDays(-1).ToShortDateString();
                        viewDateStr = $">= '{first}' AND [{stmntCycle}] <= '{last}'";
                        isRangeDate = true;
                        break;
                    case "&Specific Date":
                        using (SpecificDatePicker dtPicker = new SpecificDatePicker()) {
                            var result = dtPicker.ShowDialog();
                            if (result == DialogResult.OK) {
                                if (IsWeekend(dtPicker.ReturnSpecificDate)) {
                                    return;
                                }
                                else {
                                    viewDate = dtPicker.ReturnSpecificDate.Value.ToShortDateString();
                                    viewDateStr = $"= '{viewDate}'";
                                }
                            }
                            else {
                                return;
                            }
                        }
                        break;
                    case "&All":
                    default:
                        viewDateStr = String.Empty;
                        break;
                }

                // Query accounts
                Cursor.Current = Cursors.WaitCursor;
                if (RunQuery()) {
                    tbCtrlPages.SelectedTab = tbAccounts;
                    dataGridAccounts.Focus();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        // Check if selected specific date is a weekend
        private bool IsWeekend(DateTime? reviewDate)
        {
            DateTime reviewDateValue = reviewDate.Value;
            if ((reviewDateValue.DayOfWeek == DayOfWeek.Saturday) || (reviewDateValue.DayOfWeek == DayOfWeek.Sunday)) {
                MessageBox.Show($"{reviewDateValue.ToShortDateString()} is a " +
                                $"{reviewDateValue.DayOfWeek}. There are no accounts to display.");
                return true;
            }
            else {
                return false;
            }
        }

        // Query accounts
        private bool RunQuery()
        {
            query = "SELECT log.DateRequested AS [Date Requested], " +
                    "fac.FacilityAbbr AS [Facility], " +
                    "log.AcctNumber AS [Account #], " +
                    "log.PatientName AS [Patient Name], " +
                    "log.PatResp AS [Patient Responsibility], " +
                    "log.DateFirstStmnt AS [First Statement], " +
                    "log.DateSecondStmnt AS [Second Statement], " +
                    "log.DateFinalStmnt AS [Final Statement], " +
                    "log.IsCompleted AS [Completed] " +
                    "FROM tblAccounts AS [log] " +
                    "LEFT JOIN tblFacilities AS [fac] " +
                    "ON log.FacilityID = fac.FacilityID " +
                    "WHERE fac.FacilityAbbr = @Facility";

            try {
                // Get account records
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
                    //MessageBox.Show(sb.ToString());                                                                         //debug
                    view.RowFilter = sb.ToString();
                    dt = view.ToTable();

                    if (dt.Rows.Count != 0) {
                        dataGridAccounts.DataSource = dt;
                        dataGridAccounts.AutoResizeColumns();
                        dataGridAccounts.Columns["Patient Responsibility"].DefaultCellStyle.Format = "#,##0.00";
                        lblTotalRows.Text = String.Format("Total rows: {0}", dataGridAccounts.RowCount);
                        return true;
                    }
                    else {
                        MessageBox.Show($"There are no accounts to display for: {viewDate}");
                        return false;
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
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
        }

        // Get statement cycle
        private void btnStmntCycle_CheckedChanged(object sender, EventArgs e)
        {
            MetroRadioButton btnStmnt = sender as MetroRadioButton;
            if (btnStmnt.Checked) {
                stmntCycle = btnStmnt.Text;
                Cursor.Current = Cursors.WaitCursor;
                RunQuery();
                Cursor.Current = Cursors.Default;
            }
        }

        // Filter by statement date
        private void FilterByStatementDate()
        {
            if (sb.Length > 0) {
                sb.Append(" AND ");
            }

            if (viewDateStr != String.Empty) {
                if (isRangeDate) {
                    viewDateStr = $">= '{first}' AND [{stmntCycle}] <= '{last}'";
                }
                sb.Append($"[{stmntCycle}] {viewDateStr}");
            }
        }

        private void ckBoxCompletedFilter_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            RunQuery();
            Cursor.Current = Cursors.Default;
        }
        #endregion

        private void dataGridAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var g = sender as DataGridView;
            if (g != null) {
                try {
                    var p = g.PointToClient(MousePosition);
                    var hti = g.HitTest(p.X, p.Y);

                    if (hti.Type == DataGridViewHitTestType.ColumnHeader) {
                        var columnIndex = hti.ColumnIndex;
                    }
                    else if (dataGridAccounts.SelectedRows.Count != 0) {
                        // Get selected account number
                        DataGridViewRow row = this.dataGridAccounts.SelectedRows[0];
                        account = row.Cells["Account #"].Value.ToString();

                        // Query account info
                        Cursor.Current = Cursors.WaitCursor;
                        CRUDQueries crud = new CRUDQueries(this);
                        crud.ReadQuery(conn, this.GetFacDbInfo(facility, account));
                        Cursor.Current = Cursors.Default;
                        tbCtrlPages.SelectedTab = tbStatementHistory;
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #region User CRUD Functions
        private void tileAddAccount_Click(object sender, EventArgs e)
        {
            try {
                using (NewAccount newAcct = new NewAccount(facility)) {
                    var result = newAcct.ShowDialog();
                    if (result == DialogResult.Yes) {
                        MessageBox.Show("Test Successful");
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void tileEdit_Click(object sender, EventArgs e)
        {
            if (account != null) {
                try {
                    foreach (Control ctl in this.tbStatementHistory.Controls) {
                        switch (ctl.Name) {
                            case "txtPtLiab":
                            case "txtRequestedDate":
                            case "txtFirstStmnt":
                            case "txtSecondStmnt":
                            case "txtFinalStmnt":
                            case "ckBoxCompleted":
                                if (ctl.GetType() == typeof(MetroTextBox)) {
                                    ((MetroTextBox)ctl).ReadOnly = false;
                                }
                                else if (ctl.GetType() == typeof(MetroCheckBox)) {
                                    ((MetroCheckBox)ctl).Enabled = true;
                                }
                                break;
                            default:
                                ctl.BackColor = Color.FromArgb(224, 224, 224);
                                break;
                        }
                    }
                    this.btnAccountGenerate.Visible = false;
                    this.btnEditAddNote.Visible = true;
                    this.btnEditApplyChanges.Visible = true;
                    this.btnEditCancel.Visible = true;
                    this.tileHome.Enabled = false;
                    this.tileView.Enabled = false;
                    this.tileGenerate.Enabled = false;
                    this.tileBack.Enabled = false;
                    this.tileNext.Enabled = false;
                    this.tbCtrlPages.SelectedTab = tbStatementHistory;

                    //using (EditAccount editAcct = new EditAccount()) {
                    //    var result = editAcct.ShowDialog();
                    //}
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
                else {
                    MessageBox.Show("Please select an account to edit.");
                    return;
                }
        }

        // TODO: Implement SQL INSERT command to Comments table
        private void btnEditAddNote_Click(object sender, EventArgs e)
        {
            using (AddNote addNote = new AddNote()) {
                var result = addNote.ShowDialog();
            }
        }

        // TODO: Implement SQL UPDATE command
        private void btnEditChanges_Click(object sender, EventArgs e)
        {
            // TODO: If Apply Changes, UPDATE command; if Cancel, clear textboxes and requery info
            // TODO: Validate data before applying changes
            foreach (Control ctl in this.tbStatementHistory.Controls) {
                switch (ctl.Name) {
                    case "txtPtLiab":
                    case "txtRequestedDate":
                    case "txtFirstStmnt":
                    case "txtSecondStmnt":
                    case "txtFinalStmnt":
                    case "ckBoxCompleted":
                        if (ctl.GetType() == typeof(MetroTextBox)) {
                            ((MetroTextBox)ctl).ReadOnly = true;
                        }
                        else if (ctl.GetType() == typeof(MetroCheckBox)) {
                            ((MetroCheckBox)ctl).Enabled = false;
                        }
                        break;
                    default:
                        ctl.BackColor = SystemColors.ButtonHighlight;
                        break;
                }
            }
            this.btnAccountGenerate.Visible = true;
            this.btnEditAddNote.Visible = false;
            this.btnEditApplyChanges.Visible = false;
            this.btnEditCancel.Visible = false;
            this.tileHome.Enabled = true;
            this.tileView.Enabled = true;
            this.tileGenerate.Enabled = true;
            this.tileBack.Enabled = true;
            this.tileNext.Enabled = true;
        }

        // TODO: implement DELETE command
        private void tileDeleteAccount_Click(object sender, EventArgs e)
        {
            try {
                if (account != null) {
                    DialogResult result = MessageBox.Show($"Are you sure you want to permanently delete Account # {account}?", "Confirmation", MessageBoxButtons.YesNo);
                    if (DialogResult == DialogResult.Yes) {

                    }
                    else {
                        return;
                    }
                }
                else {
                    MessageBox.Show("Please select an account to delete.");
                    return;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        // TODO: Download manual statement log table as Excel file; prompt for save directory
        private void tileExport_Click(object sender, EventArgs e)
        {

        }
        #endregion
}
}
