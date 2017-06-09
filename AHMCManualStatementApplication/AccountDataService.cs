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
    public class AccountStatementDataService
    {
        private readonly string _connectionString;
        public AccountStatementDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AccountInfo GetByAccountNumber(string facility, string accountNumber)
        {
            try {
                using (OleDbConnection connection = new OleDbConnection(_connectionString)) {
                    connection.Open();

                    string readQuery = @"SELECT log.*, fac.FacilityAbbr 
                                         FROM tblAccounts AS log 
                                         LEFT JOIN tblFacilities AS fac
                                         ON log.FacilityID = fac.FacilityID
                                         WHERE log.AcctNumber = @AccountNumber 
                                         AND fac.FacilityAbbr = @Facility";

                    using (OleDbCommand command = connection.CreateCommand()) {
                        command.CommandType = CommandType.Text;
                        command.CommandText = readQuery;
                        command.Parameters.Add("@AccountNumber", OleDbType.VarChar).Value = accountNumber;
                        command.Parameters.Add("@Facility", OleDbType.VarChar).Value = facility;

                        using (OleDbDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                return new AccountInfo {
                                    Facility = facility,
                                    AccountNumber = reader.SafeGetValue("AcctNumber"),
                                    PatientLiability = reader.SafeGetValue("PatResp"),
                                    DateRequested = reader.SafeGetValue("DateRequested"),
                                    DateFirstStatement = reader.SafeGetValue("DateFirstStmnt"),
                                    DateSecondStatement = reader.SafeGetValue("DateSecondStmnt"),
                                    DateFinalStatement = reader.SafeGetValue("DateFinalStmnt"),
                                    IsCompleted = (bool)reader["IsCompleted"]
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex) {
                return null;
            }
        }
    }
}
