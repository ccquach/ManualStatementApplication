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
    public class DisplayAccountsDataGridView
    {
        private Main _form;
        private AccountDataService _service;

        public DisplayAccountsDataGridView(Main form, AccountDataService service)
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
            var gridData = _service.GetAccountsDataGridView(_form.facility, _form.viewOption, _form.statementCycle, _form.IsCheckedCompleted, _form.IsCheckedUncompleted);
            _form.AccountsDataGridView.DataSource = gridData.AccountsDataTable;
            if (_form.AccountsDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("There are no accounts to display for the selected date(s).");
                return;
            }
            _form.AccountsDataGridView.AutoResizeColumns();
            _form.AccountsDataGridView.Columns["Patient Responsibility"].DefaultCellStyle.Format = "#,##0.00";
            _form.TotalRowsLabel = $"Total rows: {_form.AccountsDataGridView.RowCount}";
        }
    }
}
