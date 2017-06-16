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
    public partial class SpecificDatePicker : MetroFramework.Forms.MetroForm
    {
        public SpecificDatePicker()
        {
            InitializeComponent();
        }

        public DateTime? ReturnSpecificDate { get; set; }

        private void dtPickerSpecificDate_MouseDown(object sender, MouseEventArgs e)
        {
            dtPickerSpecificDate.Open();
        }

        private void SpecificDatePicker_Load(object sender, EventArgs e)
        {
            dtPickerSpecificDate.MinDate = new DateTime(2016, 1, 1);
            dtPickerSpecificDate.MaxDate = DateTime.Now;
            dtPickerSpecificDate.Format = DateTimePickerFormat.Custom;
            dtPickerSpecificDate.CustomFormat = dtPickerSpecificDate.Value.ToString("MM/dd/yyyy");
        }

        private void btnDatePickerCancel_Click(object sender, EventArgs e)
        {
            this.ReturnSpecificDate = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDatePickerOK_Click(object sender, EventArgs e)
        {
            this.ReturnSpecificDate = dtPickerSpecificDate.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dtPickerSpecificDate_ValueChanged(object sender, EventArgs e)
        {
            dtPickerSpecificDate.CustomFormat = dtPickerSpecificDate.Value.ToString("MM/dd/yyyy");
        }
    }
}
