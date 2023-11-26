using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintTracker.Controllers
{
    public class ComplaintReopenController : Controller
    {
        public ActionResult ReopenComplaints()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            List<COMPLAINT> lstComplaints = new List<COMPLAINT>();
            COMPLAINT objComplaint = new COMPLAINT();
            lstComplaints.Add(objComplaint);

            return View(lstComplaints);
        }
        public JsonResult GetComplaintSearchReopen(ModelSearchComplaint dataObject) //It will be fired from Jquery ajax call
        {
            //List<ModelSearchComplaint> data = new List<ModelSearchComplaint>();
            //data = Repository.GetReopenComplaintDetails(dataObject);
            var jsonData = "";
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReopenComplaint_Save(Int64 id)
        {
            int complaintNo = Repository.ReopenComplaint(id, Convert.ToInt32(Session["UserID"].ToString()));
            return RedirectToAction("ReopenComplaints");

        }
    }
}