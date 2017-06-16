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

        private void dtPickerDate_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as MetroDateTime).Open();
        }

        private void SpecificDateRangePicker_Load(object sender, EventArgs e)
        {
            FormatDateTimePicker(this);
        }

        private void FormatDateTimePicker(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                MetroDateTime dateTimePicker = child as MetroDateTime;
                if (dateTimePicker == null)
                {
                    FormatDateTimePicker(child);
                }
                else
                {
                    dateTimePicker.MinDate = new DateTime(2016, 1, 1);
                    dateTimePicker.MaxDate = DateTime.Now;
                    dateTimePicker.Format = DateTimePickerFormat.Custom;
                    dateTimePicker.CustomFormat = dateTimePicker.Value.ToString("MM/dd/yyyy");
                }
            }
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

        private void dtPicker_ValueChanged(object sender, EventArgs e)
        {
            MetroDateTime dateTimePicker = sender as MetroDateTime;
            dateTimePicker.CustomFormat = dateTimePicker.Value.ToString("MM/dd/yyyy");
        }
    }
}
