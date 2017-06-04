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
    public partial class AddNote : MetroFramework.Forms.MetroForm
    {
        public AddNote()
        {
            InitializeComponent();
            this.StyleManager = msmAddNote;

            // Date note entered set to today's date
            this.txtEditNoteDate.Text = DateTime.Now.ToShortDateString();
        }
    }
}
