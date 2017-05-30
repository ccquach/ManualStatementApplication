﻿using System;
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

        private void dtPickerSpecificDate_MouseDown(object sender, MouseEventArgs e)
        {
            dtPickerSpecificDate.MaxDate = DateTime.Now;
        }

        private void btnDatePickerCancel_Click(object sender, EventArgs e)
        {
            this.ReturnSpecificDate = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dtPickerSpecificDate_ValueChanged(object sender, EventArgs e)
        {
            this.ReturnSpecificDate = dtPickerSpecificDate.Value.ToShortDateString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string ReturnSpecificDate { get; set; }
    }
}
