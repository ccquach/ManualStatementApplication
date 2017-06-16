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

        public DateTime? ReturnStartDate { get; set; }
        public DateTime? ReturnEndDate { get; set; }

        private void dtPickerFromDate_MouseDown(object sender, MouseEventArgs e)
        {
            dtPickerStartDate.Open();
        }

        private void dtPickerToDate_MouseDown(object sender, MouseEventArgs e)
        {
            dtPickerEndDate.Open();
        }

        private void SpecificDateRangePicker_Load(object sender, EventArgs e)
        {
            dtPickerStartDate.MinDate = new DateTime(2016, 1, 1);
            dtPickerStartDate.MaxDate = DateTime.Now;

            dtPickerEndDate.MinDate = new DateTime(2016, 1, 1);
            dtPickerEndDate.MaxDate = DateTime.Now;
        }

        private void btnDateCancelPickerCancel_Click(object sender, EventArgs e)
        {
            this.ReturnStartDate = null;
            this.ReturnEndDate = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDateRangePickerOK_Click(object sender, EventArgs e)
        {
            this.ReturnStartDate = dtPickerStartDate.Value;
            this.ReturnEndDate = dtPickerEndDate.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
