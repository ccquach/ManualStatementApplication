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
        public static string GetDemoFacilityName(this AccountDemoDataService accountDemoDataService, string facility)
        {
            string databaseFacilityName;
            if (facility == "ARMC") {
                return databaseFacilityName = "amh";
            }
            else {
                return databaseFacilityName = facility.ToLower();
            }
        }
        #endregion
    }
}
