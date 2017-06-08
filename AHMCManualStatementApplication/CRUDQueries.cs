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

        public void ReadQuery(OleDbConnection conn, Tuple<string, string> facDbInfo)
        {
            try {
                query = @"SELECT log.*, fac.FacilityAbbr 
                          FROM tblAccounts AS log 
                          LEFT JOIN tblFacilities AS fac
                          ON log.FacilityID = fac.FacilityID";

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn)) {
                    DataSet set = new DataSet();
                    adapter.Fill(set, "Accounts");

                    // Execute statement log query
                    var linqQuery = from acct in set.Tables["Accounts"].AsEnumerable()
                                    where acct.Field<string>("AcctNumber") == form.account &&
                                          acct.Field<string>("FacilityAbbr") == form.facility
                                    select acct;

                    // Fill account statement info
                    // STATEMENT HISTORY:
                    form.Facility = form.facility;
                    form.Account = form.account;
                    form.Completed = linqQuery.SingleOrDefault().Field<bool>("IsCompleted");
                    form.PatientLiability = linqQuery.SingleOrDefault().Field<double>("PatResp").ToString("#,##0.00");

                    form.DateRequested = linqQuery.SingleOrDefault().Field<DateTime?>("DateRequested").HasValue ?
                        linqQuery.SingleOrDefault().Field<DateTime?>("DateRequested").Value.ToShortDateString() : String.Empty;
                    form.StatementFirst = linqQuery.SingleOrDefault().Field<DateTime?>("DateFirstStmnt").HasValue ?
                        linqQuery.SingleOrDefault().Field<DateTime?>("DateFirstStmnt").Value.ToShortDateString() : String.Empty;
                    form.StatementSecond = linqQuery.SingleOrDefault().Field<DateTime?>("DateSecondStmnt").HasValue ?
                        linqQuery.SingleOrDefault().Field<DateTime?>("DateSecondStmnt").Value.ToShortDateString() : String.Empty;
                    form.StatementFinal = linqQuery.SingleOrDefault().Field<DateTime?>("DateFinalStmnt").HasValue ?
                        linqQuery.SingleOrDefault().Field<DateTime?>("DateFinalStmnt").Value.ToShortDateString() : String.Empty;

                    //form.CommentNote = linqQuery.SingleOrDefault().Field<string>("Comment");

                    // DEMOGRAPHICS:
                    form.DemoFacility = form.facility;
                    form.DemoAccount = form.account;
                    form.DemoPatientLiability = linqQuery.SingleOrDefault().Field<double>("PatResp").ToString("#,##0.00");
                }

                #region DataReader
                //using (OleDbConnection connDemo = new OleDbConnection(facDbInfo.Item1)) {
                //    connDemo.Open();

                //    OleDbCommand cmd = connDemo.CreateCommand();
                //    cmd.CommandType = CommandType.Text;
                //    cmd.CommandText = facDbInfo.Item2;

                //    using (OleDbDataReader reader = cmd.ExecuteReader()) {
                //        while (reader.Read()) {
                //            form.PatientName = reader.SafeGetValue("PATIENT_NAME");
                //            form.DemoPatientName = reader.SafeGetValue("PATIENT_NAME");
                //            form.DischargeDate = reader.SafeGetValue("IP1DISC_DATE");
                //            form.AddressLine1 = reader.SafeGetValue("IP1PAT_ADDR1");
                //            form.AddressLine2 = reader.SafeGetValue("IP1PAT_ADDR2");
                //            form.City = reader.SafeGetValue("IP1PAT_CITY");
                //            form.State = reader.SafeGetValue("IP1PAT_STATE");
                //            form.Zipcode = reader.SafeGetValue("IP1PAT_ZIP");
                //        }
                //    }
                //}
                #endregion

                #region Old LINQ to DataSet code
                using (OleDbConnection connDemo = new OleDbConnection(facDbInfo.Item1)) {
                    // Open connection
                    connDemo.Open();

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(facDbInfo.Item2, connDemo)) {
                        DataSet set = new DataSet();
                        adapter.Fill(set, "Demo");

                        // Execute demo query
                        //var linqQuery = from acct in set.Tables["Demo"].AsEnumerable()
                        //                where acct.Field<string>("PATIENT_NUMBER").Replace(" ", "") == form.account
                        //                select acct;

                        var linqQuery = set.Tables["Demo"].AsEnumerable()
                            .Where(acct => acct.Field<string>("PATIENT_NUMBER").Replace(" ", "") == form.account)
                            .FirstOrDefault();

                        // Fill account demo info
                        form.PatientName = linqQuery.Field<string>("PATIENT_NAME");
                        form.DemoPatientName = linqQuery.Field<string>("PATIENT_NAME");
                        form.AddressLine1 = linqQuery.Field<string>("IP1PAT_ADDR1");
                        form.AddressLine2 = linqQuery.Field<string>("IP1PAT_ADDR2");
                        form.City = linqQuery.Field<string>("IP1PAT_CITY");
                        form.State = linqQuery.Field<string>("IP1PAT_STATE");
                        form.Zipcode = linqQuery.Field<int>("IP1PAT_ZIP").ToString();

                        //form.PatientName = linqQuery.SingleOrDefault().Field<string>("PATIENT_NAME");
                        //form.DemoPatientName = linqQuery.SingleOrDefault().Field<string>("PATIENT_NAME");
                        //form.AddressLine1 = linqQuery.SingleOrDefault().Field<string>("IP1PAT_ADDR1");
                        //form.AddressLine2 = linqQuery.SingleOrDefault().Field<string>("IP1PAT_ADDR2");
                        //form.City = linqQuery.SingleOrDefault().Field<string>("IP1PAT_CITY");
                        //form.State = linqQuery.SingleOrDefault().Field<string>("IP1PAT_STATE");
                        //form.Zipcode = linqQuery.SingleOrDefault().Field<int>("IP1PAT_ZIP").ToString();

                        //form.DischargeDate = linqQuery.SingleOrDefault().Field<DateTime?>("IP1DISC_DATE").HasValue ?
                        //    linqQuery.SingleOrDefault().Field<DateTime?>("IP1DISC_DATE").Value.ToShortDateString() : String.Empty;
                    }
                }
                #endregion
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
