using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AHMCManualStatementApplication
{
    public static class DatabaseManager
    {
        public static string GetStatementConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ManualStatementConnection"].ConnectionString;
        }

        public static string GetDemoConnectionString(string facility)
        {
            return ConfigurationManager.ConnectionStrings[$"{facility}DemoConnection"].ConnectionString;
        }
    }
}
