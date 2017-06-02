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
    class CRUDQueries
    {
        #region Variables
        // VARIABLES
        private readonly IMain form;
        public string query;
        #endregion

        // Constructor
        public CRUDQueries(IMain form)
        {
            this.form = form;
        }

        public void ReadQuery(OleDbConnection conn, string account, string facility)
        {
            try {
                query = @"SELECT log.*, fac.FacilityAbbr 
                      FROM tblManualStmntLog AS log 
                      LEFT JOIN tblFacility AS fac
                      ON log.FacilityID = fac.FacilityID";

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn)) {
                    DataSet set = new DataSet();
                    adapter.Fill(set, "Accounts");

                    // Execute statement log query
                    var linqQuery = from acct in set.Tables["Accounts"].AsEnumerable()
                                    where acct.Field<string>("AcctNumber") == account &&
                                          acct.Field<string>("FacilityAbbr") == facility
                                    select acct;

                    // Fill account statement info
                    // STATEMENT HISTORY:
                    form.Facility = facility;
                    form.Account = account;
                    form.Completed = linqQuery.SingleOrDefault().Field<bool>("Completed");
                    form.PatientLiability = linqQuery.SingleOrDefault().Field<double>("PatResp").ToString("#,##0.00");

                    form.DateRequested = linqQuery.SingleOrDefault().Field<DateTime?>("DateRequested").HasValue ?
                        linqQuery.SingleOrDefault().Field<DateTime?>("DateRequested").Value.ToShortDateString() : String.Empty;
                    form.StatementFirst = linqQuery.SingleOrDefault().Field<DateTime?>("DateFirstStmnt").HasValue ?
                        linqQuery.SingleOrDefault().Field<DateTime?>("DateFirstStmnt").Value.ToShortDateString() : String.Empty;
                    form.StatementSecond = linqQuery.SingleOrDefault().Field<DateTime?>("DateSecondStmnt").HasValue ?
                        linqQuery.SingleOrDefault().Field<DateTime?>("DateSecondStmnt").Value.ToShortDateString() : String.Empty;
                    form.StatementFinal = linqQuery.SingleOrDefault().Field<DateTime?>("DateFinalStmnt").HasValue ?
                        linqQuery.SingleOrDefault().Field<DateTime?>("DateFinalStmnt").Value.ToShortDateString() : String.Empty;

                    form.DateNoteEntered = linqQuery.SingleOrDefault().Field<DateTime?>("DateNoteEntered").HasValue ?
                        linqQuery.SingleOrDefault().Field<DateTime?>("DateNoteEntered").Value.ToShortDateString() : String.Empty;
                    form.NoteEntered = linqQuery.SingleOrDefault().Field<string>("Notes");

                    // DEMOGRAPHICS:
                    form.DemoFacility = facility;
                    form.DemoAccount = account;
                    form.DemoPatientLiability = linqQuery.SingleOrDefault().Field<double>("PatResp").ToString("#,##0.00");
                }

                // Get facility db abbreviation
                string dbFacility = String.Empty;
                if (facility == "ARMC") {
                    dbFacility = "amh";
                }
                else {
                    dbFacility = facility;
                }

                // Connect to demo table in facility database
                string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;" +
                                 $"Data Source=W:\\ETH\\CQ Macro\\analyst\\AHMC Manual Statement\\database\\demo.db\\{dbFacility}_cpsi_odbc_dw.mdb;" +
                                 $"Persist Security Info=False;";

                using (OleDbConnection connDemo = new OleDbConnection(connStr)) {
                    // Open connection
                    connDemo.Open();

                    query = $"SELECT PATIENT_NUMBER, PATIENT_NAME, IP1DISC_DATE, IP1PAT_ADDR1, " +
                            $"IP1PAT_ADDR2, IP1PAT_CITY, IP1PAT_STATE, IP1PAT_ZIP " +
                            $"FROM {dbFacility}_demo_audit";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connDemo)) {
                        DataSet set = new DataSet();
                        adapter.Fill(set, "Demo");

                        // Execute demo query
                        var linqQuery = from acct in set.Tables["Demo"].AsEnumerable()
                                        where acct.Field<string>("PATIENT_NUMBER").Replace(" ","") == account
                                        select acct;

                        // Fill account demo info
                        form.PatientName = linqQuery.SingleOrDefault().Field<string>("PATIENT_NAME");
                        form.DemoPatientName = linqQuery.SingleOrDefault().Field<string>("PATIENT_NAME");
                        form.AddressLine1 = linqQuery.SingleOrDefault().Field<string>("IP1PAT_ADDR1");
                        form.AddressLine2 = linqQuery.SingleOrDefault().Field<string>("IP1PAT_ADDR2");
                        form.City = linqQuery.SingleOrDefault().Field<string>("IP1PAT_CITY");
                        form.State = linqQuery.SingleOrDefault().Field<string>("IP1PAT_STATE");
                        form.Zipcode = linqQuery.SingleOrDefault().Field<int>("IP1PAT_ZIP").ToString();

                        form.DischargeDate = linqQuery.SingleOrDefault().Field<DateTime?>("IP1DISC_DATE").HasValue ?
                            linqQuery.SingleOrDefault().Field<DateTime?>("IP1DISC_DATE").Value.ToShortDateString() : String.Empty;
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
