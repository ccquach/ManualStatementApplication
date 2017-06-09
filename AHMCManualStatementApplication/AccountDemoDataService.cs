using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace AHMCManualStatementApplication
{
    public class AccountDemoDataService
    {
        private readonly string _connectionString;
        public AccountDemoDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public AccountInfo GetByAccountNumber(string facility, string accountNumber)
        {
            try {
                using (OleDbConnection connection = new OleDbConnection(_connectionString)) {
                    connection.Open();

                    using (OleDbCommand command = connection.CreateCommand()) {
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"
                                                SELECT  PATIENT_NUMBER, PATIENT_NAME, IP1DISC_DATE, IP1PAT_ADDR1, 
                                                        IP1PAT_ADDR2, IP1PAT_CITY, IP1PAT_STATE, IP1PAT_ZIP 
                                                FROM    @facilityDemoTable
                                                WHERE   REPLACE(PATIENT_NUMBER, ' ', '') = @Account
                                               ";

                        command.Parameters.Add("@facilityDemoTable", OleDbType.VarChar).Value = this.GetDemoFacilityName(facility);
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
            catch (Exception ex) {
                return null;
            }
        }
    }
}
