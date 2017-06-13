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
    public class AccountDataService
    {
        private readonly string _connectionStringStatement;
        private readonly string _connectionStringDemo;
        public AccountDataService(string connectionStringStatement, string connectionStringDemo)
        {
            _connectionStringStatement = connectionStringStatement;
            _connectionStringDemo = connectionStringDemo;
        }

        public AccountInfo GetStatementByAccountNumber(string facility, string accountNumber)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionStringStatement)) {
                connection.Open();

                using (OleDbCommand command = connection.CreateCommand()) {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"
                                            SELECT      log.*, fac.FacilityAbbr 
                                            FROM        tblAccounts AS log 
                                            LEFT JOIN   tblFacilities AS fac
                                            ON          log.FacilityID = fac.FacilityID
                                            WHERE       log.AcctNumber = @AccountNumber 
                                            AND         fac.FacilityAbbr = @Facility
                                            ";

                    command.Parameters.Add("@AccountNumber", OleDbType.VarChar).Value = accountNumber;
                    command.Parameters.Add("@Facility", OleDbType.VarChar).Value = facility;

                    using (OleDbDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) {
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

        public AccountInfo GetDemoByAccountNumber(string facility, string accountNumber)
        {
            using (OleDbConnection connection = new OleDbConnection(_connectionStringDemo)) {
                connection.Open();

                using (OleDbCommand command = connection.CreateCommand()) {
                    command.CommandType = CommandType.Text;

                    command.CommandText = $"SELECT PATIENT_NUMBER, PATIENT_NAME, IP1DISC_DATE, IP1PAT_ADDR1, " +
                                          $"IP1PAT_ADDR2, IP1PAT_CITY, IP1PAT_STATE, IP1PAT_ZIP " +
                                          $"FROM {this.GetDemoFacilityName(facility)}_demo_audit " +
                                          $"WHERE REPLACE(PATIENT_NUMBER, ' ', '') = @Account";

                    command.Parameters.Add("@Account", OleDbType.VarChar).Value = accountNumber;

                    using (OleDbDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            return new AccountInfo {
                                PatientName = reader.SafeGetValue("PATIENT_NAME"),
                                DischargeDate = reader.SafeGetValue("IP1DISC_DATE"),
                                AddressLine1 = reader.SafeGetValue("IP1PAT_ADDR1"),
                                AddressLine2 = reader.SafeGetValue("IP1PAT_ADDR2"),
                                City = reader.SafeGetValue("IP1PAT_CITY"),
                                State = reader.SafeGetValue("IP1PAT_STATE"),
                                Zipcode = reader.SafeGetValue("IP1PAT_ZIP")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public DataGridViewInfo GetAccountsDataGridView()
        {

        }
    }
}
