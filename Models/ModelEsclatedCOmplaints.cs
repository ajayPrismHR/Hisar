using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelEsclatedCOmplaints
    {
        public string OFFICE_CODE { get; set; }
        public string SDOName { get; set; }
        public string TotalComplaintOpen { get; set; }

        public string WithInSLA { get; set; }
        public string OUTOfSLA { get; set; }
        public string AEN { get; set; }
        public string XEN { get; set; }

        public string SE { get; set; }
        public string CORPORATE { get; set; }
        public string DT { get; set; }
        public string backcolor { get; set; }


    }
}