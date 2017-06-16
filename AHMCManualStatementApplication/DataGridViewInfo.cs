using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using System.Data;

namespace AHMCManualStatementApplication
{
    public class DataGridViewInfo
    {
        private string viewDateStr;
        public DateTime first;
        public DateTime last;

        private StringBuilder _filterStringBuilder;
        private string _viewOption;
        private string _statementCycle;
        private bool _isCheckedCompleted;
        private bool _isCheckedUncompleted;
        private bool _isCancel;
        private MetroDateTime _startDate;
        private MetroDateTime _endDate;

        public DataGridViewInfo(string viewOption, string statementCycle, bool isCheckedCompleted, bool isCheckedUncompleted, MetroDateTime startDate, MetroDateTime endDate)
        {
            _viewOption = viewOption;

            if (statementCycle == null)
                _statementCycle = "First statement";
            else
                _statementCycle = statementCycle;
            
            _isCheckedCompleted = isCheckedCompleted;
            _isCheckedUncompleted = isCheckedUncompleted;
            _filterStringBuilder = new StringBuilder();
            _isCancel = false;
            _startDate = startDate;
            _endDate = endDate;

            viewDateStr = String.Empty;
        }

        public DataTable AccountsDataTable { get; set; }
        public StringBuilder FilterStringBuilder {
            get { return _filterStringBuilder; }
        }
        public bool IsCancel {
            get { return _isCancel; }
        }

        public void BuildAccountQuery()
        {
            _filterStringBuilder.Clear();
            FilterByViewOption();
            FilterByCompleted();
        }

        private void FilterByViewOption()
        {
            DateTime today = DateTime.Today;

            if (!_startDate.Checked && !_endDate.Checked)
            {
                switch (_viewOption)
                {
                    case "&Today":
                        if (today.DayOfWeek == DayOfWeek.Monday)
                        {
                            first = new DateTime(today.Year, today.AddMonths(-1).Month, today.AddDays(-2).Day);
                            last = today.AddMonths(-1);
                        }
                        else
                        {
                            first = today.AddMonths(-1);
                            last = first;
                        }
                        break;
                    case "&Last Month":
                        var month = new DateTime(today.Year, today.Month, 1);
                        first = month.AddMonths(-1);
                        last = month.AddDays(-1);
                        break;
                    case "&Specific Date":
                        using (SpecificDatePicker dtPicker = new SpecificDatePicker())
                        {
                            var result = dtPicker.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                if (IsWeekend(dtPicker.ReturnSpecificDate))
                                {
                                    _isCancel = true;
                                    return;
                                }
                                else
                                {
                                    first = dtPicker.ReturnSpecificDate.Value;
                                    last = first;
                                }
                            }
                            else
                            {
                                _isCancel = true;
                                return;
                            }
                        }
                        break;
                    case "&Range of Dates":
                        using (SpecificDateRangePicker dtPicker = new SpecificDateRangePicker())
                        {
                            var result = dtPicker.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                first = dtPicker.ReturnStartDate.Value;
                                last = dtPicker.ReturnEndDate.Value;
                            }
                            else
                            {
                                _isCancel = true;
                                return;
                            }
                        }
                        break;
                    case "&By Account Number":

                        break;
                    case "&All":
                    default:
                        first = new DateTime(2016, 1, 1);
                        last = DateTime.Now;
                        break;
                }
            }
            else
            {
                first = _startDate.Value;
                last = _endDate.Value;
            }

            viewDateStr = $"[{_statementCycle}] >= '{first.ToShortDateString()}' AND [{_statementCycle}] <= '{last.ToShortDateString()}'";

            if (viewDateStr != String.Empty)
                _filterStringBuilder.Append(viewDateStr);
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
            string completedFilterString = String.Empty;

            if (_isCheckedCompleted && !_isCheckedUncompleted)
            {
                completedFilterString = "[Completed] = True";
            }
            else if (!_isCheckedCompleted && _isCheckedUncompleted)
            {
                completedFilterString = "[Completed] = False";
            }

            if (completedFilterString != String.Empty)
            {
                if (_filterStringBuilder.Length > 0)
                    _filterStringBuilder.Append(" AND ");

                _filterStringBuilder.Append(completedFilterString);
            }

        }
    }
}
