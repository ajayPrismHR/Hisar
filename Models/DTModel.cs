using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class DTModel
    {
        public string CIRCLE { get; set; }
        public string DT_FAILED_FOR_SELECTED_MONTH { get; set; }
        public string DT_REPLACE_FOR_SELECTED_MONTH { get; set; }
        public string BALANCE_FOR_SELECTED_MONTH { get; set; }
        public string AVERAGE_REDRESSAL_TIME_IN_SELECTED_MONTH { get; set; }
        public string DT_FAILED_UPTO_SELECTED_MONTH { get; set; }
        public string DT_REPLACE_UPTO_SELECTED_MONTH { get; set; }
        public string BALANCE_FOR_UPTO_SELECTED_MONTH { get; set; }
        public string AVERAGE_REDRESSAL_TIME_UPTO_SELECTED_MONTH { get; set; }
    }
}