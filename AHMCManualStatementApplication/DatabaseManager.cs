using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AHMCManualStatementApplication
{
    public class DatabaseManager
    {
        private static DatabaseManager _instance;
        public DatabaseManager()
        {
        }

        static DatabaseManager()
        {
            _instance = new DatabaseManager();
        }

        public DatabaseManager Instance {
            get { return _instance; }
        }

        public string GetStatementConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ManualStatementConnection"].ConnectionString;
        }

        public string GetDemoConnectionString(string facility)
        {
            return ConfigurationManager.ConnectionStrings[$"{facility}DemoConnection"].ConnectionString;
        }
    }
}
