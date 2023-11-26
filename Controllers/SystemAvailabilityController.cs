using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintTracker.Controllers
{
    public class SystemAvailabilityController : Controller
    {
        // GET: SystemAvailability
        public ActionResult CreateSystemAvailability()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            ModelSystemAvaialablity modelSystemAvaialablity = new ModelSystemAvaialablity();
            modelSystemAvaialablity.SystemAvailabilityIssueCollection = Repository.GETSystemAvailabilityIssue();
            return View(modelSystemAvaialablity);
        }


        public ActionResult SaveSystemAvailability(string fromDate,string toDate, string SystemAvailabilityIssue_ID)
        {
            ModelSystemAvaialablity modelSystemAvaialablity = new ModelSystemAvaialablity();
            modelSystemAvaialablity.From_Date = fromDate;
            modelSystemAvaialablity.To_Date = toDate;
            modelSystemAvaialablity.SystemAvailabilityIssue_ID = SystemAvailabilityIssue_ID;
            modelSystemAvaialablity.EnterByUserID = Convert.ToString(Session["UserID"]);
            Repository.SaveSystemAvailabilityIssue(modelSystemAvaialablity);
            return RedirectToAction("CreateSystemAvailability", "SystemAvailability");
        }


       

    }
}