using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHMCManualStatementApplication
{
    public class AccountInfo
    {
        public string Facility { get; set; }
        public string AccountNumber { get; set; }
        public string PatientName { get; set; }
        public string PatientLiability { get; set; }
        public string DateRequested { get; set; }
        public string DateFirstStatement { get; set; }
        public string DateSecondStatement { get; set; }
        public string DateFinalStatement { get; set; }
        public bool IsCompleted { get; set; }

        public string DischargeDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}
