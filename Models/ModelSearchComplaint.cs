using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ComplaintTracker.JqueryDatatableParam;

namespace ComplaintTracker.Models
{
    public class ModelSearchComplaint : DataTableAjaxPostModel
    {
        public string NAME { get; set; }
        public string COMPLAINT_DATE { get; set; }
        public string COMPLAINT_NO { get; set; }
        public string LineMan_Name { get; set; }
        public string LineMan_No { get; set; }
        public string ADDRESS { get; set; }
        public string Consumer_no { get; set; }
        public string SDO { get; set; }
        public string MOBILE_NO { get; set; }
        public Int64 COMPLAINT_NO1 { get; set; }

    }
}