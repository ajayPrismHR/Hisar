using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelComplaintWiseDetailReport
    {
        public string CIRCLE { get; set; }
        public string TOTAL_RECEIEVED_IN_SELECTED_MONTH { get; set; }
        public string TOTAL_REDERSSAL_COMPLAINTS_WITHIN_TIME_IN_SELECTED_MONTH { get; set; }
        public string BALANCE_COMPLAINTS_IN_SELECTED_MONTH { get; set; }
        public string AVERAGE_REDRESSAL_TIME_IN_SELECTED_MONTH { get; set; }
        public string TOTAL_RECEIEVED_BEFORE_SELECTED_MONTH { get; set; }
        public string TOTAL_REDERSSAL_COMPLAINTS_WITHIN_TIME_BEFORE_SELECTED_MONTH { get; set; }
        public string BALANCE_COMPLAINTS_BEFORE_SELECTED_MONTH { get; set; }
        public string AVERAGE_REDRESSAL_TIME_BEFORE_SELECTED_MONTH { get; set; }
    }
}