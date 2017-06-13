using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace AHMCManualStatementApplication
{
    public class AccountDataGridView
    {
        private readonly string _connectionString;
        public AccountDataGridView(string connectionString)
        {
            _connectionString = connectionString;
        }


    }
}
