using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHMCManualStatementApplication
{
    public class InfoDisplayNewAccount
    {
        private NewAccount _form;
        private AccountDataService _service;

        public InfoDisplayNewAccount(NewAccount form, AccountDataService service)
        {
            _form = form;
            _form.OnShowAccountInfo += View_OnShowDemoAccountInfo;

            _service = service;
        }

        public void Show()
        {
            _form.ShowDialog();
        }

        private void View_OnShowDemoAccountInfo(object sender, EventArgs e)
        {
            var info = _service.GetDemoByAccountNumber(_form.Facility, _form.Account);
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
