using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelOutageReport
    {
        public string ID { get; set; }
        public string OFFICE_CODE { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string COMPLAINT_TYPE { get; set; }

        public string COLONIES { get; set; }
        public string SHUT_DOWN_INFORMATION { get; set; }
        public string INFORMATION_SOURCE { get; set; }


        public int ComplaintTypeId { get; set; }
        public List<ModelComplaintType> ComplaintTypeCollection { get; set; }

    }


}