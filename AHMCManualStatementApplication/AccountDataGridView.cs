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
    public class AccountDataGridView
    {
        private Main _form;
        private readonly string _connectionString;
        public AccountDataGridView(Main form, string connectionString)
        {
            _form = form;
            _connectionString = connectionString;
        }

        private DataView view;
        private StringBuilder sb = new StringBuilder();
        private string stmntCycle = "First Statement";
        private string viewDate = String.Empty;
        private string viewDateStr = String.Empty;
        private string first = String.Empty;
        private string last = String.Empty;
        private bool isRangeDate = false;

        public void GetAccountsByViewOption()
        {
            string query = "SELECT log.DateRequested AS [Date Requested], " +
                            "fac.FacilityAbbr AS [Facility], " +
                            "log.AcctNumber AS [Account #], " +
                            "log.PatientName AS [Patient Name], " +
                            "log.PatResp AS [Patient Responsibility], " +
                            "log.DateFirstStmnt AS [First Statement], " +
                            "log.DateSecondStmnt AS [Second Statement], " +
                            "log.DateFinalStmnt AS [Final Statement], " +
                            "log.IsCompleted AS [Completed] " +
                            "FROM tblAccounts AS [log] " +
                            "LEFT JOIN tblFacilities AS [fac] " +
                            "ON log.FacilityID = fac.FacilityID " +
                            "WHERE fac.FacilityAbbr = @Facility";

                // Get account records
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.Add("@Facility", OleDbType.VarChar).Value = facility;

                using (OleDbDataAdapter a = new OleDbDataAdapter(cmd)) {
                    DataTable dt = new DataTable();
                    a.SelectCommand = cmd;
                    a.Fill(dt);

                    // Sort by descending Date Requested
                    view = dt.DefaultView;
                    view.Sort = "[Date Requested] desc";

                    // Filter by Statement Date / Completed
                    sb.Clear();
                    FilterByCompleted();
                    FilterByStatementDate();
                    //MessageBox.Show(sb.ToString());                                                                         //debug
                    view.RowFilter = sb.ToString();
                    dt = view.ToTable();

                    if (dt.Rows.Count != 0) {
                        dgv.DataSource = dt;
                        dgv.AutoResizeColumns();
                        dgv.Columns["Patient Responsibility"].DefaultCellStyle.Format = "#,##0.00";
                        lblTotalRows.Text = String.Format("Total rows: {0}", dgv.RowCount);
                        return true;
                    }
                    else {
                        MessageBox.Show($"There are no accounts to display for: {viewDate}");
                        return false;
                    }
                }
        }

        private string BuildQueryByViewOption(ToolStripMenuItem viewOption)
        {
            DateTime today = DateTime.Today;

            switch (viewOption.Text) {
                case "&Today":
                    if (today.DayOfWeek == DayOfWeek.Monday) {
                        first = new DateTime(today.Year, today.AddMonths(-1).Month, today.AddDays(-2).Day).ToShortDateString();
                        last = today.AddMonths(-1).ToShortDateString();
                        viewDateStr = $">= '{first}' AND [{stmntCycle}] <= '{last}'";
                        isRangeDate = true;
                    }
                    else {
                        viewDate = today.AddMonths(-1).ToShortDateString();
                        viewDateStr = $"= '{viewDate}'";
                    }
                    break;
                case "&Last Month":
                    var month = new DateTime(today.Year, today.Month, 1);
                    first = month.AddMonths(-1).ToShortDateString();
                    last = month.AddDays(-1).ToShortDateString();
                    viewDateStr = $">= '{first}' AND [{stmntCycle}] <= '{last}'";
                    isRangeDate = true;
                    break;
                case "&Specific Date":
                    using (SpecificDatePicker dtPicker = new SpecificDatePicker()) {
                        var result = dtPicker.ShowDialog();
                        if (result == DialogResult.OK) {
                            if (IsWeekend(dtPicker.ReturnSpecificDate)) {
                                return String.Empty;
                            }
                            else {
                                viewDate = dtPicker.ReturnSpecificDate.Value.ToShortDateString();
                                viewDateStr = $"= '{viewDate}'";
                            }
                        }
                        else {
                            return String.Empty;
                        }
                    }
                    break;
                case "&All":
                default:
                    viewDateStr = String.Empty;
                    break;
            }
        }

        private bool IsWeekend(DateTime? date)
        {
            DateTime dateValue = date.Value;
            if ((dateValue.DayOfWeek == DayOfWeek.Saturday) || (dateValue.DayOfWeek == DayOfWeek.Sunday)) {
                MessageBox.Show($"{dateValue.ToShortDateString()} is a " +
                                $"{dateValue.DayOfWeek}. There are no accounts to display.");
                return true;
            }
            else {
                return false;
            }
        }

        private void FilterByCompleted()
        {
            if (ckBoxCompletedFilter.Checked && !ckBoxUncompletedFilter.Checked) {
                sb.Append("[Completed] = True");
            }
            else if (!ckBoxCompletedFilter.Checked && ckBoxUncompletedFilter.Checked) {
                sb.Append("[Completed] = False");
            }
        }

        private void FilterByStatementDate()
        {
            if (sb.Length > 0) {
                sb.Append(" AND ");
            }

            if (viewDateStr != String.Empty) {
                if (isRangeDate) {
                    viewDateStr = $">= '{first}' AND [{stmntCycle}] <= '{last}'";
                }
                sb.Append($"[{stmntCycle}] {viewDateStr}");
            }
        }
    }
}
