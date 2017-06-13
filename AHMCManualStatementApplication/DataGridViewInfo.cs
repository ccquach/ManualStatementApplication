using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace AHMCManualStatementApplication
{
    public class DataGridViewInfo
    {
        public string viewDate;
        private string viewDateStr;
        private string first;
        private string last;
        private bool isRangeDate;

        private StringBuilder _filterStringBuilding;
        private string _viewOption;
        private string _statementCycle;
        private bool _isCheckedCompleted;
        private bool _isCheckedUncompleted;

        public DataGridViewInfo(string viewOption, string statementCycle, bool isCheckedCompleted, bool isCheckedUncompleted)
        {
            _viewOption = viewOption;

            if (statementCycle == null)
                _statementCycle = "First statement";
            else
                _statementCycle = statementCycle;
            
            _isCheckedCompleted = isCheckedCompleted;
            _isCheckedUncompleted = isCheckedUncompleted;
            _filterStringBuilding = new StringBuilder();

            viewDate = String.Empty;
            viewDateStr = String.Empty;
            first = String.Empty;
            last = String.Empty;
            isRangeDate = false;
        }

        public DataTable AccountsDataTable { get; set; } 
        //public DataGridView AccountsDataGridView { get; set; }
        //public string TotalRowsLabel { get; set; }
        public StringBuilder FilterStringBuilder { get { return _filterStringBuilding; } }

        public void BuildAccountQuery()
        {
            _filterStringBuilding.Clear();
            FilterByViewOption();
            FilterByStatementDate();
            FilterByCompleted();
        }

        private string FilterByViewOption()
        {
            DateTime today = DateTime.Today;

            switch (_viewOption)
            {
                case "&Today":
                    if (today.DayOfWeek == DayOfWeek.Monday)
                    {
                        first = new DateTime(today.Year, today.AddMonths(-1).Month, today.AddDays(-2).Day).ToShortDateString();
                        last = today.AddMonths(-1).ToShortDateString();
                        viewDateStr = $">= '{first}' AND [{_statementCycle}] <= '{last}'";
                        isRangeDate = true;
                    }
                    else
                    {
                        viewDate = today.AddMonths(-1).ToShortDateString();
                        viewDateStr = $"[{_statementCycle}] = '{viewDate}'";
                    }
                    break;
                case "&Last Month":
                    var month = new DateTime(today.Year, today.Month, 1);
                    first = month.AddMonths(-1).ToShortDateString();
                    last = month.AddDays(-1).ToShortDateString();
                    viewDateStr = $">= '{first}' AND [{_statementCycle}] <= '{last}'";
                    isRangeDate = true;
                    break;
                case "&Specific Date":
                    using (SpecificDatePicker dtPicker = new SpecificDatePicker())
                    {
                        var result = dtPicker.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            if (IsWeekend(dtPicker.ReturnSpecificDate))
                            {
                                return String.Empty;
                            }
                            else
                            {
                                viewDate = dtPicker.ReturnSpecificDate.Value.ToShortDateString();
                                viewDateStr = $"[{_statementCycle}] = '{viewDate}'";
                            }
                        }
                        else
                        {
                            return String.Empty;
                        }
                    }
                    break;
                case "&Range of Dates":

                    break;
                case "&By Account Number":

                    break;
                case "&All":
                default:
                    viewDateStr = String.Empty;
                    break;
            }
            return viewDateStr;
        }

        private bool IsWeekend(DateTime? date)
        {
            DateTime dateValue = date.Value;
            if ((dateValue.DayOfWeek == DayOfWeek.Saturday) || (dateValue.DayOfWeek == DayOfWeek.Sunday))
            {
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
            if (_isCheckedCompleted && !_isCheckedUncompleted)
            {
                _filterStringBuilding.Append("[Completed] = True");
            }
            else if (!_isCheckedCompleted && _isCheckedUncompleted)
            {
                _filterStringBuilding.Append("[Completed] = False");
            }
        }

        private void FilterByStatementDate()
        {
            if (_filterStringBuilding.Length > 0)
            {
                _filterStringBuilding.Append(" AND ");
            }

            if (viewDateStr != String.Empty)
            {
                if (isRangeDate)
                {
                    viewDateStr = $"[{_statementCycle}] >= '{first}' AND [{_statementCycle}] <= '{last}'";
                }
                _filterStringBuilding.Append($"{viewDateStr}");
            }
        }
    }
}
