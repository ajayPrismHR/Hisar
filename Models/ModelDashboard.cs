using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelDashboard
    {
        public string CurrentMonthTotalComplaint { get; set; }
        public string CurrentMonthResolvedComplaint { get; set; }
        public string PreviousMonthTotalComplaint { get; set; }
        public string PreviousMonthResolvedComplaint { get; set; }

        public string Hares_TOTAL_COMPLAINTS_TILL_DATE { get; set; }
        public string Hares_TOTAL_COMPLAINTS_RESOLVED_TILL_DATE { get; set; }
        public string Hares_TOTAL_COMPLAINTS_FOR_CURRENT_MONTH { get; set; }
        public string Hares_TOTAL_COMPLAINTS_RESOLVED_IN_CURRENT_MONTH { get; set; }



        public List<ComplaintSummaryGraph> ComplaintSummaries { get; set; }
        public List<CircleWiseComplaintSummary> CircleWiseComplaintSummaryData { get; set; }


    }
    public class ComplaintSummaryGraph
    {
        public string Month { get; set; }
        public string TotalComplaint { get; set; }

        public string ResolveComplaint { get; set; }

    }

    public class CircleWiseComplaintSummary
    {
        public string CircleName { get; set; }
        public string TotalComplaint { get; set; }

        public string TotalPendingComplaints { get; set; }
        public string TotalReopenComplaint { get; set; }

        public string TotalResolvedComplaints { get; set; }

    }
}