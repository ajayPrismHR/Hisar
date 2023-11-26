using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ComplaintTracker.Models
{
    public class ModelDashboardHaresment
    {
        public string COMPLAINT_NO { get; set; }
        public string KNO { get; set; }
        public string COMPLAINT_DATE { get; set; }
        public string COMPLAINT_TYPE { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string MOBILE_NO { get; set; }
        public string STATUS { get; set; }

    }
}