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
        public decimal PatientLiability { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime DateFirstStatement { get; set; }
        public DateTime? DateSecondStatement { get; set; }
        public DateTime? DateFinalStatement { get; set; }

        public DateTime DischargeDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }

    }
}
