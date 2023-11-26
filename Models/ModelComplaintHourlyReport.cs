using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelComplaintHourlyReport
    {


        public string OFFICE_CODE { get; set; }
        public string OFFICE_NAME { get; set; }
        public string ONE_HOUR { get; set; }
        public string TWO_HOUR { get; set; }
        public string THREE_HOUR { get; set; }
        public string FOUR_HOUR { get; set; }

        public string FIVE_HOUR { get; set; }
        public string MOTE_THEN_FIVE_HOUR { get; set; }


    }
}