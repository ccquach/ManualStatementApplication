using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AHMCManualStatementApplication
{
    public class Comment
    {
        [Key]
        public DateTime DateRequested { get; set; }
        public int FacilityID { get; set; }
        public string AcctNumber { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentNote { get; set; }
    }
}
