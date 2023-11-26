using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ComplaintTracker.JqueryDatatableParam;

namespace ComplaintTracker.Models
{
    public class ModelReport : DataTableAjaxPostModel
    {
        public string KNO { get; set; }
        public string ComplaintNo { get; set; }
        public string OfficeCode { get; set; }


        public int MOBILE_NO { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }

        public int ComplaintStatus { get; set; }
        public int ComplaintSource { get; set; }
        public int ComplaintType { get; set; }
        public string SlaType { get; set; }

        public string assigned_status { get; set; }
        
        public string count { get; set; }


        public string ID { get; set; }
        public string COLONIES { get; set; }
        public string INFORMATION_SOURCE { get; set; }
        public string PROCESS_TYPE { get; set; }
        public string SHUTDOWN_INFORMATION { get; set; }


        public string START_TIME { get; set;}
        public string BILL_MONTH { get; set; }
        public string BILL_YEAR { get; set; }
        public string CIRCLE { get; set; }


    }
}