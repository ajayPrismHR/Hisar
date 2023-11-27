using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ComplaintTracker.JqueryDatatableParam;

namespace ComplaintTracker.Models
{
    public class ModelReport : DataTableAjaxPostModel
    {
        public string ComplaintNo { get; set; }


        public int Mobile_no { get; set; }
        public string Bill_Month { get; set; }
        public string Customer_Name { get; set; }

        public string Category_Name { get; set; }
        public string LineMan_Name { get; set; }
        public string Complaint_date { get; set; }
        public string Resolved_Date { get; set; }

        public string Response_Time { get; set; }

        public Int64 UserID { get; set; }


    }
}