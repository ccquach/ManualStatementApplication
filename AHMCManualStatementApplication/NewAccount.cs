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
        #region Variables
        OleDbConnection connDemo = null;
        #endregion

        public NewAccount(string facility)
        {
            InitializeComponent();
            this.StyleManager = msmNewAccount;

            // Default info
            this.txtNewFacility.Text = facility;
            this.txtNewDateNoteEntered.Text = DateTime.Now.ToShortDateString();
        }

        #region Demo Database Connection
        private void NewAccount_Load(object sender, EventArgs e)
        {
            // Connect to demo database
            try {
                //string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;" +
                //                 $"Data Source=W:\\ETH\\CQ Macro\\analyst\\AHMC Manual Statement\\database\\demo.db\\{this.txtNewFacility.Text}_cpsi_odbc_dw.mdb;" +
                //                 $"Persist Security Info=False;";

                connDemo = new OleDbConnection(this.GetFacDbInfo(this.txtNewFacility.Text).Item1);
                if (connDemo.State == ConnectionState.Closed) {
                    connDemo.Open();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close connection to demo database
            try {
                if (connDemo.State == ConnectionState.Open) {
                    connDemo.Close();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void txtNewAccount_Leave(object sender, EventArgs e)
        {
            try {
                if (this.txtNewAccount.Text != String.Empty) {
                    OleDbCommand cmd = connDemo.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = this.GetFacDbInfo(this.txtNewFacility.Text, this.txtNewAccount.Text).Item2;
                    //cmd.CommandText = $"SELECT PATIENT_NUMBER, PATIENT_NAME, IP1DISC_DATE, IP1PAT_ADDR1, " +
                    //                  $"IP1PAT_ADDR2, IP1PAT_CITY, IP1PAT_STATE, IP1PAT_ZIP " +
                    //                  $"FROM {this.txtNewFacility.Text}_demo_audit";

                    using (OleDbDataReader reader = cmd.ExecuteReader()) {
                        if (reader["PATIENT_NUMBER"] != DBNull.Value) {
                            while (reader.Read()) {
                                this.txtNewPatientName.Text = reader.SafeGetValue("PATIENT_NAME");
                                this.txtNewDischarge.Text = reader.SafeGetValue("IP1DISC_DATE");
                                this.txtNewAddress1.Text = reader.SafeGetValue("IP1PAT_ADDR1");
                                this.txtNewAddress2.Text = reader.SafeGetValue("IP1PAT_ADDR2");
                                this.txtNewCity.Text = reader.SafeGetValue("IP1PAT_CITY");
                                this.txtNewState.Text = reader.SafeGetValue("IP1PAT_STATE");
                                this.txtNewZipcode.Text = reader.SafeGetValue("IP1PAT_ZIP");
                            }
                        }
                        else {
                            MessageBox.Show("The entered account does not exist.");
                            return;
                        }
                    }
                }
                else {
                    ClearTextBoxes(this);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
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

        
    }
}
