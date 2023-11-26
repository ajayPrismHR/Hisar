using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelComplaintDashboard
    {
        public string SDOName { get; set; }
        public string PreviousDayPending { get; set; }
        public string TodayRegister { get; set; }
        public string TotalComplaint { get; set; }
        public string PreviousResolvedComplaint { get; set; }
        public string TodayResolved { get; set; }
        public string TotalResolved { get; set; }
        
        public string TotalPending{ get; set; }

    }
}