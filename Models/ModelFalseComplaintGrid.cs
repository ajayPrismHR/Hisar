using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using static ComplaintTracker.JqueryDatatableParam;

namespace ComplaintTracker.Models
{
    public class ModelFalseComplaintGrid : DataTableAjaxPostModel
    {
        public string KNO { get; set; }
        public string NAME { get; set; }
        public string MOBILE_NO { get; set; }
        public string COMPLAINT_DATE { get; set; }
        public string COMPLAINT_NO { get; set; }
        public string OFFICE_NAME { get; set; }
        public string COMPLAINT_TYPE { get; set; }

    }


}