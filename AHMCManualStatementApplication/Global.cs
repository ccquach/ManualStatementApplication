using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHMCManualStatementApplication
{
    static class Global
    {
        private static string _viewDate;

        public static string ViewDate
        {
            get { return _viewDate; }
            set { _viewDate = value; }
        }
    }
}
