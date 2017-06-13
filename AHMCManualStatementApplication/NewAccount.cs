using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using System.Data.OleDb;

namespace AHMCManualStatementApplication
{
    public partial class NewAccount : MetroFramework.Forms.MetroForm
    {
        public NewAccount(string facility)
        {
            InitializeComponent();
            this.StyleManager = msmNewAccount;

            // Default info
            this.txtNewFacility.Text = facility;
            this.txtNewDateNoteEntered.Text = DateTime.Now.ToShortDateString();
        }

        public event EventHandler OnShowAccountInfo;

        #region Properties
        public string Facility {
            get { return this.txtNewFacility.Text; }
            set { this.txtNewFacility.Text = value; }
        }
        public string Account {
            get { return this.txtNewAccount.Text; }
            set { this.txtNewAccount.Text = value; }
        }
        public string PatientName {
            get { return this.txtNewPatientName.Text; }
            set { this.txtNewPatientName.Text = value; }
        }
        public string DischargeDate {
            get { return this.txtNewDischarge.Text; }
            set { this.txtNewDischarge.Text = value; }
        }
        public string AddressLine1 {
            get { return this.txtNewAddress1.Text; }
            set { this.txtNewAddress1.Text = value; }
        }
        public string AddressLine2 {
            get { return this.txtNewAddress2.Text; }
            set { this.txtNewAddress2.Text = value; }
        }
        public string City {
            get { return this.txtNewCity.Text; }
            set { this.txtNewCity.Text = value; }
        }
        public string State {
            get { return this.txtNewState.Text; }
            set { this.txtNewState.Text = value; }
        }
        public string Zipcode {
            get { return this.txtNewZipcode.Text; }
            set { this.txtNewZipcode.Text = value; }
        }
        #endregion

        private void txtNewAccount_Leave(object sender, EventArgs e)
        {
            if (this.Account != String.Empty) {
                Cursor.Current = Cursors.WaitCursor;
                new DisplayNewAccountInfo(this, new AccountDataService(DatabaseManager.GetStatementConnectionString(), DatabaseManager.GetDemoConnectionString(this.Facility)));
                OnShowAccountInfo(this, EventArgs.Empty);
                Cursor.Current = Cursors.Default;
            }
            else {
                ClearTextBoxes(this);
            }
        }

        private void ClearTextBoxes(Control parent)
        {
            foreach (Control child in this.Controls) {
                MetroTextBox textBox = child as MetroTextBox;
                if (textBox == null) {
                    ClearTextBoxes(child);
                }
                else {
                    textBox.Text = String.Empty;
                }
            }
        }

        private void btnAddNewAccount_Click(object sender, EventArgs e)
        {
            string account = this.txtNewAccount.Text;
            var result = MessageBox.Show($"Are you sure you want to add Account # {account}?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) {
                MessageBox.Show("Yes: success");
            }
            else {
                MessageBox.Show("No: success");
            }
        }

        private void btnCancelNewAccount_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
