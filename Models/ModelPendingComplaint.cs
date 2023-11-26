using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelPendingComplaint
    {
           public string SlaType { get; set; }

            public string OfficeCode { get; set; }

            public string Source { get; set; }
            public string Complainttype { get; set; }

            public string OfficeName { get; set; }
            public string TotalComplaintReceived { get; set; }
            public string TodayPending { get; set; }
            public string PreviousDayPending { get; set; }
        public string TodayResolved { get; set; }
        public string OverAllPending { get; set; }
        public string OverAllResolved { get; set; }

    }
}