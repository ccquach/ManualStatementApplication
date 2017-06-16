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
            try
            {
                var gridData = _service.GetAccountsDataGridView(_form.facility, _form.viewOption, _form.statementCycle, _form.IsCheckedCompleted, _form.IsCheckedUncompleted, _form.StartDate, _form.EndDate);

                DataView view = gridData.AccountsDataTable.DefaultView;
                view.Sort = "[Date Requested] desc";

                gridData.BuildAccountQuery();
                if (gridData.IsCancel == true)
                {
                    return;
                }

                MessageBox.Show(gridData.FilterStringBuilder.ToString());                                               //debug
                view.RowFilter = gridData.FilterStringBuilder.ToString();
                gridData.AccountsDataTable = view.ToTable();

                if (gridData.AccountsDataTable.Rows.Count == 0)
                {
                    MessageBox.Show($"There are no accounts to display for the selected date(s).");
                    return;
                }

                _form.AccountsDataGridView.DataSource = gridData.AccountsDataTable;
                _form.AccountsDataGridView.AutoResizeColumns();
                _form.AccountsDataGridView.Columns["Patient Responsibility"].DefaultCellStyle.Format = "#,##0.00";
                _form.TotalRowsLabel = $"Total rows: {_form.AccountsDataGridView.RowCount}";

                _form.StartDate.Value = gridData.first;
                _form.StartDate.Format = DateTimePickerFormat.Custom;
                _form.StartDate.CustomFormat = gridData.first.ToString("MM/dd/yyyy");
                _form.StartDate.Checked = true;

                _form.EndDate.Value = gridData.last;
                _form.EndDate.Format = DateTimePickerFormat.Custom;
                _form.EndDate.CustomFormat = gridData.last.ToString("MM/dd/yyyy");
                _form.EndDate.Checked = true;
            }
            finally
            {
                _form.OnShowAccountDataGridView -= View_OnShowAccountsDataGridView;
            }
        }
    }
}
