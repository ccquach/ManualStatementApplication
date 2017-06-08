using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AHMCManualStatementApplication
{
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }
        public string FacilityAbbr { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }
        public string FacilityCity { get; set; }
        public string FacilityState { get; set; }
        public string FacilityZip { get; set; }
        public string FacilityTelephone { get; set; }
        public string RemitName { get; set; }
        public string RemitAddress { get; set; }
        public string RemitCity { get; set; }
        public string RemitState { get; set; }
        public string RemitZip { get; set; }
    }
}
