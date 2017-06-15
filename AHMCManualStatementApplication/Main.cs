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
    
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public Main()
        {
            InitializeComponent();
            this.StyleManager = msmMain;

            // Default values
            facility = String.Empty;
            account = String.Empty;
        }

        #region Member Variables
        public event EventHandler OnShowAccountInfo;
        public event EventHandler OnShowAccountDataGridView;

        int tabIndex;
        MetroTile tile = null;
        String tileName = String.Empty;
        #endregion

        #region Properties
        public string facility { get; set; }
        public string account { get; set; }
        public string viewOption { get; set; }
        public string statementCycle { get; set; }

        // Accounts DataGridView
        public MetroGrid AccountsDataGridView {
            get { return this.dataGridAccounts; }
            set { this.dataGridAccounts = value; }
        }
        public string TotalRowsLabel {
            get { return lblTotalRows.Text; }
            set { lblTotalRows.Text = value; }
        }
        public bool IsCheckedCompleted {
            get { return this.ckBoxCompletedFilter.Checked; }
            set { this.ckBoxCompletedFilter.Checked = value; }
        }
        public bool IsCheckedUncompleted {
            get { return this.ckBoxUncompletedFilter.Checked; }
            set { this.ckBoxUncompletedFilter.Checked = value; }
        }

        // Statement History
        public string Facility {
            get { return this.txtFacility.Text; }
            set {
                this.txtFacility.Text = value;
                this.txtDemoFacility.Text = value;
            }
        }
        public string Account {
            get { return this.txtAccount.Text; }
            set {
                this.txtAccount.Text = value;
                this.txtDemoAccount.Text = value;
            }
        }
        public string PatientName {
            get { return this.txtPatientName.Text; }
            set {
                this.txtPatientName.Text = value;
                this.txtDemoPtName.Text = value;
            }
        }
        public string PatientLiability {
            get { return this.txtPtLiab.Text; }
            set {
                this.txtPtLiab.Text = value;
                this.txtDemoPtLiab.Text = value;
            }
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

        #region Button Accessibility
        // Start page: Home screen
        private void Main_Load(object sender, EventArgs e)
        {
            tbCtrlPages.SelectTab(tbHome);
            try
            {
                ActivateFunctions();
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
        // Get view date
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewOption = (sender as ToolStripMenuItem).Text;
            try {
                Cursor.Current = Cursors.WaitCursor;
                new DisplayAccountsDataGridView(this, new AccountDataService(DatabaseManager.GetStatementConnectionString(), DatabaseManager.GetDemoConnectionString(this.facility)));
                OnShowAccountDataGridView(this, EventArgs.Empty);

                if (dataGridAccounts.Rows.Count != 0)
                {
                    tbCtrlPages.SelectedTab = tbAccounts;
                    dataGridAccounts.Focus();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        // Get statement cycle
        private void btnStmntCycle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MetroRadioButton btnStmnt = sender as MetroRadioButton;
                if (btnStmnt.Checked)
                {
                    statementCycle = btnStmnt.Text;

                    Cursor.Current = Cursors.WaitCursor;
                    new DisplayAccountsDataGridView(this, new AccountDataService(DatabaseManager.GetStatementConnectionString(), DatabaseManager.GetDemoConnectionString(this.facility)));
                    OnShowAccountDataGridView(this, EventArgs.Empty);
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        // Get Completed checkbox selections
        private void ckBoxCompletedFilter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Completed checkbox
                if (ckBoxCompletedFilter.Checked)
                    IsCheckedCompleted = true;
                else
                    IsCheckedCompleted = false;

                // Uncompleted checkbox
                if (ckBoxUncompletedFilter.Checked)
                    IsCheckedUncompleted = true;
                else
                    IsCheckedUncompleted = false;

                Cursor.Current = Cursors.WaitCursor;
                new DisplayAccountsDataGridView(this, new AccountDataService(DatabaseManager.GetStatementConnectionString(), DatabaseManager.GetDemoConnectionString(this.facility)));
                OnShowAccountDataGridView(this, EventArgs.Empty);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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
                        new DisplayStatementAccountInfo(this, new AccountDataService(DatabaseManager.GetStatementConnectionString(), DatabaseManager.GetDemoConnectionString(this.facility)));
                        OnShowAccountInfo(this, EventArgs.Empty);
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
