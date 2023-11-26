using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class NewConnectionModel
    {
        public string ZONE { get; set; }
        public string CIRCLE { get; set; }
        public string DIVISION { get; set; }
        public string SUB_DIVISION { get; set; }
        public string DS_COMPLAINT_RECEIVED { get; set; }
        public string NDS_COMPLAINT_RECEIVED { get; set; }
        public string TOTAL_REQUEST_RECEIVED { get; set; }
        public string DS_COMPLAINT_RESOLVED { get; set; }
        public string NDS_COMPLAINT_RESOLVED { get; set; }
        public string TOTAL_REQUEST_RESOLVED { get; set; }
        public string REQUEST_WITHDRAW_BY_CONSUMER { get; set; }
        public string FIle_Pending_Request_Pending { get; set; }
        public string FIle_Deposited_Request_Pending { get; set; }
        public string Demand_Issued_Request_Pending { get; set; }
        public string Demand_Deposited_Request_Pending { get; set; }
        public string SJO_Complated_Request_Pending { get; set; }
        public string SCO_Complated { get; set; }
    }
}