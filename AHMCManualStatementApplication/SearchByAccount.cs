using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHMCManualStatementApplication
{
    public partial class SearchByAccount : MetroFramework.Forms.MetroForm
    {
        public SearchByAccount()
        {
            InitializeComponent();
        }

        public string ReturnAccountNumber { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool accountExists = new AccountDataService(DatabaseManager.GetStatementConnectionString(), DatabaseManager.GetDemoConnectionString(String.Empty)).AccountExists(this.txtSearchAccountNumber.Text);
            if (accountExists)
            {
                this.ReturnAccountNumber = this.txtSearchAccountNumber.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("The selected account does not exist in the Manual Statement database. Please try again.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ReturnAccountNumber = String.Empty;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
