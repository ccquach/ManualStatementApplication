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

        public void ReadQuery(IMain form, OleDbConnection conn, string account, string facility)
        {
            query = "SELECT log.*, fac.FacilityAbbr " +
                    "FROM tblManualStmntLog AS log " +
                    "LEFT JOIN tblFacility AS fac " +
                    "ON log.FacilityID = fac.FacilityID";

            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn)) {
                DataSet set = new DataSet();
                adapter.Fill(set, "Accounts");

                // Execute query
                var linqQuery = from acct in set.Tables["Accounts"].AsEnumerable()
                                where acct.Field<string>("AcctNumber") == account &&
                                      acct.Field<string>("FacilityAbbr") == facility
                                select acct;

                // Fill account info
                // STATEMENT HISTORY:
                form.Facility = facility;
                form.Account = account;

                form.PatientName = linqQuery.SingleOrDefault().Field<string>("PatientName");
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

                form.DemoPatientName = linqQuery.SingleOrDefault().Field<string>("PatientName");
                form.DemoPatientLiability= linqQuery.SingleOrDefault().Field<double>("PatResp").ToString("#,##0.00");
                
            }
        }
    }
}
