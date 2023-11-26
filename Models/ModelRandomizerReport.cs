using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ComplaintTracker.JqueryDatatableParam;
namespace ComplaintTracker.Models
{
    public class ModelRandomizerReport  : DataTableAjaxPostModel
    {
        public string ID { get; set; }
        public string KNO { get; set; }
        public string SDOCode { get; set; }

        public string OFFICE_CODE { get; set; }
        public string CONSUMER_NAME { get; set; }
        public string MOBILE_NO { get; set; }
        public string TIME_STAMP { get; set; }

        public string CLOSE_DATE { get; set; }
        public string DURATION { get; set; }
        public string COMPLAINT_NO { get; set; }
        public string COMPLAINT_TYPE { get; set; }
        public string SUB_COMPLAINT_TYPE { get; set; }
        public string COMPLAINT_SOURCE { get; set; }
        public string REMARKS { get; set; }
        

    }
   

}