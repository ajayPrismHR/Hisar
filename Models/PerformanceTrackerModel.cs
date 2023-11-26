using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class PerformanceTrackerModel
    {
        public string SdoCode { get; set; }
        public string TotalComplaintsReceived { get; set; }
        public string AverageFollowUpsPerComplaint { get; set; }
        public string TotalFollowUps { get; set; }
        public string AvgFollowUpTime { get; set; }
        public string AverageResolutionHours { get; set; }
        public string ComplaintsAcceptedByFRT { get; set; }
        public string ComplaintClosedByFRT { get; set; }
        public string RepeatComplaints { get; set; }
    }
    public class HelpDeskPerformanceTrackerModel
    {
        public string SdoCode { get; set; }
        public string TotalComplaintsReceived { get; set; }
        public string AverageFollowUpsPerComplaint { get; set; }
        public string TotalFollowUps { get; set; }
        public string AvgFollowUpTime { get; set; }
        public string AverageResolutionHours { get; set; }
        public string TotalResolutionHours { get; set; }
        public string RepeatComplaints { get; set; }
        public string TotalNumberOfShutDowns { get; set; }
        public string AvgShutDownTime { get; set; }
    }
    public class InBoundPerformanceTrackerModel
    {
        public string TotalCallOffered { get; set; }
        public string TotalCallAnswered { get; set; }
    }
    public class OutBoundPerformanceTrackerModel
    {
        public string SdoCode { get; set; }
        public string TotalComplaintReceived { get; set; }
        public string AverageFollowUpsPerComplaint { get; set; }
        public string TotalFollowUps { get; set; }
        public string AvgFollowUpTime { get; set; }
        public string AverageResolutionHours { get; set; }
        public string TotalShutDowns { get; set; }
        public string AvgShutDown { get; set; }
        public string RepeatComplaints { get; set; }
    }
}