using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelSystemAvaialablity
    {
        public string ID { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string SystemAvailabilityIssue_ID { get; set; }

        public List<SystemAvailabilityIssue> SystemAvailabilityIssueCollection { get; set; }
        
        public string EnterByUserID { get; set; }
        public string Is_Active { get; set; }
        public string Is_Deleted { get; set; }
    }

    public class SystemAvailabilityIssue
    {
        public string ID { get; set; }
        public string IssueType { get; set; }
    }

    public class ModelNonIT
    {
        public string ID { get; set; }
        public string From_Date { get; set; }
        public string NonITIssue_ID { get; set; }

        public List<NoNITIssue> NonITIssueCollection { get; set; }
        public Int64 number { get; set; }

        public string EnterByUserID { get; set; }
        public string Is_Active { get; set; }
        public string Is_Deleted { get; set; }
    }

    public class NoNITIssue
    {
        public string ID { get; set; }
        public string IssueType { get; set; }
    }
    public class CCCAgent
    {
        public string ID { get; set; }
        public string From_Date { get; set; }
        public Int64 number { get; set; }

        public string EnterByUserID { get; set; }
        public string Is_Active { get; set; }
        public string Is_Deleted { get; set; }
        public int UniformType { get; set; }

    }

    public class ModelFalseComplaint
    {
        public string ComplaintNo { get; set; }
        public string Remarks { get; set; }

        public string EnterByUserID { get; set; }

    }
}