using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.OleDb;

namespace AHMCManualStatementApplication
{
    public static class Extensions
    {
        #region Open DatePicker
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        private const uint WM_SYSKEYDOWN = 0x104;

        public static void Open(this MetroFramework.Controls.MetroDateTime obj)
        {
            SendMessage(obj.Handle, WM_SYSKEYDOWN, (int)Keys.Down, 0);
        }
        #endregion

        #region Check for null reader return value
        public static string SafeGetValue(this OleDbDataReader reader, string fieldName)
        {
            int colIndex = reader.GetOrdinal(fieldName);
            if (!reader.IsDBNull(colIndex)) {
                switch (Type.GetTypeCode(reader.GetFieldType(colIndex))) {
                    case TypeCode.String:
                        return reader.GetString(colIndex);
                    case TypeCode.Int32:
                        return reader.GetInt32(colIndex).ToString();
                    case TypeCode.Double:
                        return reader.GetDouble(colIndex).ToString();
                    case TypeCode.DateTime:
                        return reader.GetDateTime(colIndex).ToShortDateString();
                    default:
                        return String.Empty;
                }
            }
            else {
                return String.Empty;
            }
        }
        #endregion

        #region Get database connection string
        public static Tuple<string, string> GetFacDbInfo(this AccountDemoDataService accountDemoDataService, string facility, string account = "")
        {
            // Get facility db abbreviation
            string databaseFacilityName;
            if (facility == "ARMC") {
                databaseFacilityName = "amh";
            }
            else {
                databaseFacilityName = facility.ToLower();
            }

            // Connection string
            string manualStatementConnectionString = 
                $"Provider=Microsoft.ACE.OLEDB.12.0;" +
                $"Data Source=W:\\ETH\\CQ Macro\\analyst\\AHMC Manual Statement\\database\\demo.db\\{databaseFacilityName}_cpsi_odbc_dw.mdb;" +
                $"Persist Security Info=False;";

            // Demo query
            string facilityDemoQuery;
            if (account != "") {
                #region DataReader Query
                //facDemoQuery = $"SELECT PATIENT_NUMBER, PATIENT_NAME, IP1DISC_DATE, IP1PAT_ADDR1, " +
                //               $"IP1PAT_ADDR2, IP1PAT_CITY, IP1PAT_STATE, IP1PAT_ZIP " +
                //               $"FROM {dbFacility}_demo_audit " +
                //               $"WHERE REPLACE(PATIENT_NUMBER, ' ', '') = '{account}'";
                #endregion

                facilityDemoQuery = $"SELECT PATIENT_NUMBER, PATIENT_NAME, IP1DISC_DATE, IP1PAT_ADDR1, " +
                                    $"IP1PAT_ADDR2, IP1PAT_CITY, IP1PAT_STATE, IP1PAT_ZIP " +
                                    $"FROM {databaseFacilityName}_demo_audit";
            }
            else {
                facilityDemoQuery = $"SELECT PATIENT_NUMBER, PATIENT_NAME, IP1DISC_DATE, IP1PAT_ADDR1, " +
                                    $"IP1PAT_ADDR2, IP1PAT_CITY, IP1PAT_STATE, IP1PAT_ZIP " +
                                    $"FROM {databaseFacilityName}_demo_audit";
            }
            return Tuple.Create(manualStatementConnectionString, facilityDemoQuery);
        }
        #endregion
    }
}
