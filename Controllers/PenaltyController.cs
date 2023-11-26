using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;
using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using static ComplaintTracker.JqueryDatatableParam;

namespace ComplaintTracker.Controllers
{
    public class PenaltyController : Controller
    {
        // GET: Penalty
        public ActionResult SystemAvailability()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            ModelSystemAvaialablity modelSystemAvaialablity = new ModelSystemAvaialablity();
            modelSystemAvaialablity.SystemAvailabilityIssueCollection = Repository.GETSystemAvailabilityIssue();
            return View(modelSystemAvaialablity);
        }


        public ActionResult SaveSystemAvailability(string fromDate, string toDate, string SystemAvailabilityIssue_ID)
        {
            ModelSystemAvaialablity modelSystemAvaialablity = new ModelSystemAvaialablity();
            modelSystemAvaialablity.From_Date = fromDate;
            modelSystemAvaialablity.To_Date = toDate;
            modelSystemAvaialablity.SystemAvailabilityIssue_ID = SystemAvailabilityIssue_ID;
            modelSystemAvaialablity.EnterByUserID = Convert.ToString(Session["UserID"]);
            Repository.SaveSystemAvailabilityIssue(modelSystemAvaialablity);
            return RedirectToAction("SystemAvailability", "Penalty");
        }
        public ActionResult NonITAvailability()
        {
            ViewBag.fromDate = DateTime.Now;
            ModelNonIT modelSystemAvaialablity = new ModelNonIT();
            modelSystemAvaialablity.NonITIssueCollection = Repository.GETNonITIssue();
            return View(modelSystemAvaialablity);
        }

        public ActionResult SaveNonIT(string fromDate, string NonITIssue_ID, string number)
        {
            ModelNonIT modelNonIT = new ModelNonIT();
            modelNonIT.From_Date = fromDate;
            modelNonIT.NonITIssue_ID = NonITIssue_ID;
            modelNonIT.EnterByUserID = Convert.ToString(Session["UserID"]);
            modelNonIT.number = Convert.ToInt64(number);
            Repository.SaveNonITIssue(modelNonIT);
            return RedirectToAction("NonITAvailability", "Penalty");
        }

        public ActionResult CCCAgentShort()
        {
            ViewBag.fromDate = DateTime.Now;
            return View();
        }

        public ActionResult SaveCCCAgentShort(string fromDate, string number)
        {
            CCCAgent modelCCCAgent = new CCCAgent();
            modelCCCAgent.From_Date = fromDate;
            modelCCCAgent.EnterByUserID = Convert.ToString(Session["UserID"]);
            modelCCCAgent.number = Convert.ToInt64(number);
            Repository.SaveCCCAgent(modelCCCAgent);
            return RedirectToAction("CCCAgentShort", "Penalty");
        }

        public ActionResult UniformMissing()
        {
            ViewBag.fromDate = DateTime.Now;
            return View();
        }

        public ActionResult SaveUniformMissingShort(string fromDate, string number,string ddlUniformType)
        {
            CCCAgent modelCCCAgent = new CCCAgent();
            modelCCCAgent.From_Date = fromDate;
            modelCCCAgent.EnterByUserID = Convert.ToString(Session["UserID"]);
            modelCCCAgent.number = Convert.ToInt64(number);
            modelCCCAgent.UniformType = Convert.ToInt16(ddlUniformType);
            Repository.SaveUniformMissing(modelCCCAgent);
            return RedirectToAction("UniformMissing", "Penalty");
        }



        public ActionResult FalseComplaint()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetFalseComplaint(DataTableAjaxPostModel model)
        {

            List<ModelFalseComplaintGrid> data = new List<ModelFalseComplaintGrid>(); ;
            ModelReport dataObject = new ModelReport();
            dataObject.ComplaintNo = Convert.ToString(Request.Form.GetValues("ComplaintNo")[0]);
            dataObject.draw = model.draw;
            dataObject.start = model.start;
            dataObject.length = model.length;

            data = Repository.SEARCH_COMPLAINT_FOR_FALSE_CLOSURE(dataObject);

            return Json(new { draw = dataObject.draw, recordsFiltered = 0, recordsTotal = 0, data = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveFalseComplaint(string ComplaintNo, string Remarks)
        {
            ModelFalseComplaint modelFalseComplaint = new ModelFalseComplaint();
            modelFalseComplaint.ComplaintNo = ComplaintNo;
            modelFalseComplaint.EnterByUserID = Convert.ToString(Session["UserID"]);
            modelFalseComplaint.Remarks = Remarks;

            Repository.SaveFalseCompalint(modelFalseComplaint);
            return RedirectToAction("FalseComplaint", "Penalty");
        }
    }
}