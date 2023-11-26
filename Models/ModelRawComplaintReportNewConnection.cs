using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ComplaintTracker.JqueryDatatableParam;

namespace ComplaintTracker.Models
{
    public class ModelRawComplaintReportNewConnection : DataTableAjaxPostModel
    {
        public string COMPLAINT_NO { get; set; }
        public string SDO_CODE { get; set; }
        public string SDO_NAME { get; set; }
        public string KNO { get; set; }
        public string NAME { get; set; }
        public string FATHER_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string MOBILE_NO { get; set; }
        public string ALTERNATE_MOBILE_NO { get; set; }
        public string COMPLAINT_TYPE { get; set; }
        public string SUB_COMPLAINT_TYPE { get; set; }
        public string OUTAGE_TYPE { get; set; }
        public string SUB_OUTAGE_TYPE { get; set; }
        public string DS_NDS { get; set; }
        public string COMPLAINT_DATE_TIME { get; set; }
        public string CLOSED_DATE_TIME { get; set; }
        public string DURATION { get; set; }
        public string COMPLAINT_STATUS { get; set; }
        public string CURRENT_STATUS { get; set; }
        public string CREATED_BY_USER { get; set; }
        public string CLOSED_BY_USER { get; set; }
        public int Total { get; set; }

    }
}