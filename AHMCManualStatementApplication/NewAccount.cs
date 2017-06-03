using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace AHMCManualStatementApplication
{
    public partial class NewAccount : MetroFramework.Forms.MetroForm
    {
        public NewAccount(string facility)
        {
            InitializeComponent();
            this.StyleManager = msmNewAccount;
        }

        private void NewAccount_Load(object sender, EventArgs e)
        {
            // Default info
            
            this.txtNewDateNoteEntered.Text = DateTime.Now.ToShortDateString();
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
