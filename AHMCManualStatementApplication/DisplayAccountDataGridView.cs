using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace AHMCManualStatementApplication
{
    public class DisplayAccountDataGridView
    {
        private Main _form;
        private AccountDataService _service;

        public DisplayAccountDataGridView(Main form, AccountDataService service)
        {
            _form = form;
            _form.OnShowAccountDataGridView += View_OnShowAccountsDataGridView;

            _service = service;
        }

        public void Show()
        {
            _form.ShowDialog();
        }

        private void View_OnShowAccountsDataGridView(object sender, EventArgs e)
        {
            var gridData = _service.GetAccountsDataGridView(_form.facility, _form.viewOption);
            if (gridData.DataGridView.Rows.Count == 0)
            {
                MessageBox.Show("There are no accounts to display for the selected date(s).");
                return;
            }

            _form.AccountsDataGridView = gridData.DataGridView;
            _form.TotalRowsLabel = gridData.TotalRowsLabel;
        }
    }
}
