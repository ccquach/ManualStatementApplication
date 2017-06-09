using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace AHMCManualStatementApplication
{
    public class AccountDataService
    {
        private readonly string _connectionString;
        public AccountDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AccountInfo GetByAccountNumber(string facility, string accountNumber)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionString)) {

            }
        }
    }
}
