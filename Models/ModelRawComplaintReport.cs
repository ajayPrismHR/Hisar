using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ComplaintTracker.JqueryDatatableParam;

namespace ComplaintTracker.Models
{
    public class ModelRawComplaintReport : DataTableAjaxPostModel
    {
        public string SDO_CODE { get; set; }
        public string AreaCode { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string AlternateNo { get; set; }
        public string MobileNo { get; set; }
        public string KNO { get; set; }
        public string SubDivisionName { get; set; }
        public string ComplaintType { get; set; }
        public string SubComplaintType { get; set; }
        public string CreatedUserID { get; set; }
        public string ComplaintDate { get; set; }
        public string ClosedDate { get; set; }
        public string Duration { get; set; }
        public string COMPLAINT_NO { get; set; }
        public string ComplaintStatus { get; set; }
        public string SOURCE_NAME { get; set; }
        public string CurrentStatus { get; set; }
        public string ClosedUserID { get; set; }
        
    }
}