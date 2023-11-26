using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class CallQueueWaitingModel
    {
        public string TotalCallAttended { get; set; }
        public string TotalCallAttendedIn60Sec { get; set; }
        public string TotalCallAttendedAfter60Sec { get; set; }
        public string PercentageCallAttendedWithIn60Sec { get; set; }
        public string PercentageCallAttendedAfter60Sec { get; set; }
        public string TotalPaneltyAmount { get; set; }
    }

    public class CallQueueAbandonmentModel
    {
        public string TotalCall { get; set; }
        public string TotalCallAbandon { get; set; }
        public string PercentageCallAbandon { get; set; }
        public string TotalPaneltyAmount { get; set; }
    }

    public class EsclationComplaint
    {
        public string TotalEsclatedComplaint { get; set; }
        public string TotalPaneltyAmount { get; set; }
    }
    public class FRTPanelty
    {
        public string Type { get; set; }
        public string NoOfDefaultsUpTo2Slots { get; set; }
        public string NoOfDefaultsMoreThan2Slots { get; set; }
    }
    public class FalseClouse
    {
        public string FalseCount { get; set; }
        public string TotalPaneltyAmount { get; set; }
    }
    public class SystemAvailability
    {
        public string TotalInstance { get; set; }
        public string TotalPaneltyAmount { get; set; }
    }
    public class UniformPanalty
    {
        public string TotalAgents { get; set; }
        public string TotalPaneltyAmount { get; set; }
    }
    public class NonITPanalty
    {
        public string Type { get; set; }
        public string TotalPaneltyAmount { get; set; }
    }
}