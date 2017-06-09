using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHMCManualStatementApplication
{
    public class AccountInfoDisplay
    {
        private Main _form;
        private AccountDataService _service;

        public AccountInfoDisplay(Main form, AccountDataService service)
        {
            _form = form;
            _form.OnShowAccountInfo += View_OnShowAccountInfo;

            _service = service;
        }

        public void Show()
        {
            _form.ShowDialog();
        }

        private void View_OnShowAccountInfo(object sender, EventArgs e)
        {
            var info =  _service.GetStatementByAccountNumber(_form.facility, _form.account);
            if (info == null) {
                MessageBox.Show("The specified account does not exist.");
                return;
            }

            _form.facility = info.Facility;
            _form.Account = info.AccountNumber;
            _form.PatientLiability = info.PatientLiability;
            _form.DateRequested = info.DateRequested;
            _form.StatementFirst = info.DateFirstStatement;
            _form.StatementSecond = info.DateSecondStatement;
            _form.StatementFinal = info.DateFinalStatement;
            _form.Completed = info.IsCompleted;

            info = _service.GetDemoByAccountNumber(_form.facility, _form.account);
            if (info == null) {
                MessageBox.Show("The specified account does not exist.");
                return;
            }

            _form.PatientName = info.PatientName;
            _form.DischargeDate = info.DischargeDate;
            _form.AddressLine1 = info.AddressLine1;
            _form.AddressLine2 = info.AddressLine2;
            _form.City = info.City;
            _form.State = info.State;
            _form.Zipcode = info.Zipcode;
        }
    }
}
