using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AHMCManualStatementApplication
{
    public class Account
    {
        [Key]
        public DateTime DateRequested { get; set; }
        public int FacilityID { get; set; }
        public string AcctNumber { get; set; }
        public string PatientName { get; set; }
        public decimal PatResp { get; set; }
        public DateTime DateFirstStmnt { get; set; }
        public DateTime? DateSecondStmnt { get; set; }
        public DateTime? DateFinalStmnt { get; set; }
        public bool Completed { get; set; }
    }
}
