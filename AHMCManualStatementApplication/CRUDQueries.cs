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
        private readonly IMain form;

        #region Variables
        // VARIABLES: query
        public string query;

        // VARIABLES: statement history
        public string facility;
        public string patientName;
        public decimal ptLiability;
        public DateTime dateRequest;
        public DateTime dateStmtOne;
        public DateTime dateStmtTwo;
        public DateTime dateStmtFin;
        public DateTime dateNote;
        public string note;
        public bool completed;

        // VARIABLES: demographics
        public DateTime dateDischarge;
        public string address1;
        public string address2;
        public string city;
        public string state;
        public string zipCode;
        #endregion

        public void readQuery(IMain form, OleDbConnection conn, string account, string activeFacility)
        {
            this.form = form;
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
                                      acct.Field<string>("FacilityAbbr") == activeFacility
                                select acct;

                // Fill account info
                
            }
        }
    }
}
