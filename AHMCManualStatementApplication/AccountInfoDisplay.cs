using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace AHMCManualStatementApplication
{
    public class AccountInfoDisplay
    {
        private Main _form;
        private AccountStatementDataService _service;

        public AccountInfoDisplay(Main form, AccountStatementDataService service)
        {
            _form = form;
            _service = service;
        }
    }
}
