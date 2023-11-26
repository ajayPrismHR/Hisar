using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComplaintTracker.Controllers
{
    public class NCMSController : Controller
    {
        // GET: Complaint/Create
        public ActionResult Create()
        {
            COMPLAINT obj = new COMPLAINT();
            obj.OfficeCodeCollection = Repository.GetOfficeList_Create(Session["OFFICE_ID"].ToString());
            obj.ComplaintSourceCollection = Repository.ComplaintSourceJson_Register();
            return View(obj);
        }

        // POST: Complaint/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(COMPLAINT modelComplaint)
        {
            COMPLAINT obj = new COMPLAINT();
            obj.OfficeCodeCollection = Repository.GetOfficeList(Session["OFFICE_ID"].ToString());
            obj.ComplaintSourceCollection = Repository.ComplaintSourceJson();

            if (modelComplaint.OFFICE_CODE_ID == 0 || string.IsNullOrEmpty(modelComplaint.JE_AREA))
            {
                return View(obj);
            }

            try
            {
                
                Int64 complaintNo = await Repository.SaveComplaintNCMS(modelComplaint);
                if (complaintNo > 0)
                {

                    TempData["AlertMessage"] = "Complaint No. " + complaintNo.ToString() + " Generated Successfully...!";
                    return RedirectToAction("Create", obj);
                }
                else
                {
                    TempData["AlertMessage"] = "Error in complaint Generating...!";
                    return View(obj);
                }
            }
            catch
            {
                return View(obj);
            }
        }

    }
}