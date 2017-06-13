using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHMCManualStatementApplication
{
    public class DataGridViewInfo
    {
        private string _viewOption;
        public DataGridViewInfo(ToolStripMenuItem viewOption)
        {
            _viewOption = viewOption.Text;
        }

        private StringBuilder sb = new StringBuilder();
        private string stmntCycle = "First Statement";
        private string viewDate = String.Empty;
        private string viewDateStr = String.Empty;
        private string first = String.Empty;
        private string last = String.Empty;
        private bool isRangeDate = false;

        public DataGridView DataGridView { get; set; }
        public string TotalRowsLabel { get; set; }

        public string BuildQueryByViewOption()
        {
            DateTime today = DateTime.Today;

            switch (_viewOption) {
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

        public bool IsWeekend(DateTime? date)
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

        public void FilterByCompleted()
        {
            if (ckBoxCompletedFilter.Checked && !ckBoxUncompletedFilter.Checked) {
                sb.Append("[Completed] = True");
            }
            else if (!ckBoxCompletedFilter.Checked && ckBoxUncompletedFilter.Checked) {
                sb.Append("[Completed] = False");
            }
        }

        public void FilterByStatementDate()
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
