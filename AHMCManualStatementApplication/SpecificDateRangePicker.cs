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
    public partial class SpecificDateRangePicker : MetroFramework.Forms.MetroForm
    {
        public SpecificDateRangePicker()
        {
            InitializeComponent();
        }

        public DateTime? ReturnFromDate { get; set; }
        public DateTime? ReturnToDate { get; set; }

        private void dtPickerFromDate_MouseDown(object sender, MouseEventArgs e)
        {
            dtPickerFromDate.Open();
        }

        private void dtPickerToDate_MouseDown(object sender, MouseEventArgs e)
        {
            dtPickerToDate.Open();
        }

        private void SpecificDateRangePicker_Load(object sender, EventArgs e)
        {
            dtPickerFromDate.MinDate = new DateTime(2016, 1, 1);
            dtPickerFromDate.MaxDate = DateTime.Now;

            dtPickerToDate.MinDate = new DateTime(2016, 1, 1);
            dtPickerToDate.MaxDate = DateTime.Now;
        }

        private void btnDateCancelPickerCancel_Click(object sender, EventArgs e)
        {
            this.ReturnFromDate = null;
            this.ReturnToDate = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDateRangePickerOK_Click(object sender, EventArgs e)
        {
            this.ReturnFromDate = dtPickerFromDate.Value;
            this.ReturnToDate = dtPickerToDate.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
