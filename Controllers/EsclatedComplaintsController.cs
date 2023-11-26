using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;

namespace ComplaintTracker.Controllers
{
    public class EsclatedComplaintsController : Controller
    {
        
        public ActionResult EsclatedComplaints()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.RoleID = Session["Roll_ID"];
                List<ModelOfficeCode> objComp = new List<ModelOfficeCode>();

                objComp = Repository.GetOfficeList(Session["OFFICE_ID"].ToString());

                List<ModelComplaintType> obj = new List<ModelComplaintType>();
                obj = Repository.GetComplaintTypeList("0");
                obj[0].lstComplaint = objComp;
                if (obj.Count > 0)
                {
                    ViewBag.Title = "AVVNL Dashboard";
                }
                return View(obj);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public JsonResult GetComplaintTypeDetails(int OfficeCode, int ComplainttypeId, int SLAType)
        {
            if (OfficeCode == 0)
            {
                OfficeCode = Convert.ToInt32(Session["OFFICE_ID"].ToString());
            }
            List<ModelEsclatedCOmplaints> data = new List<ModelEsclatedCOmplaints>();
            data = Repository.GetExclatedComplaintSummary(OfficeCode, ComplainttypeId, SLAType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ShowEsclatedComplaint(string OfficeId, string complaintStatus,string ComplainttypeId) //It will be fired from Jquery ajax call
        {
            Int64 officeSession= Convert.ToInt64(Session["OFFICE_ID"].ToString());
            Int64 officeIdClick = Convert.ToInt64(OfficeId);
            if(officeIdClick< officeSession)
            {
                OfficeId= Session["OFFICE_ID"].ToString();
            }
            List <COMPLAINT> lstComplaints = new List<COMPLAINT>();
            lstComplaints = Repository.ShowEsclatedComplaint(OfficeId, complaintStatus, ComplainttypeId);
            return PartialView("_EsclatedComplaint", lstComplaints);
        }
        [HttpGet]
        public ActionResult ExportToExcel(string OfficeId, string complaintStatus, string ComplainttypeId)
        {
            List<COMPLAINT> lstComplaints = new List<COMPLAINT>();
            lstComplaints= Repository.ShowEsclatedComplaint(OfficeId, complaintStatus, ComplainttypeId);
            List<EsclationComplaintExcel> list = lstComplaints.AsEnumerable()
                                      .Select(o => new EsclationComplaintExcel
                                      {
                                          OfficeName = o.OfficeName,
                                          COMPLAINT_NO = o.COMPLAINT_NO,
                                          KNO=o.KNO,
                                          NAME=o.NAME,
                                          MOBILE_NO=o.MOBILE_NO,
                                          ALTERNATE_MOBILE_NO = o.ALTERNATE_MOBILE_NO   ,
                                          Complaintdate = o.Complaintdate,
                                          ComplaintTypeName = o.ComplaintTypeName,
                                          ComplaintSource = o.ComplaintSource ,
                                          ASSIGNEEName=o.ASSIGNEEName,
                                          Esclated_BY = o.Esclated_BY,
                                          Esclation_time = o.Esclation_time,

                                      }).ToList();

            var gv = new GridView();
            gv.DataSource = list;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=EsclationComplaintStatus.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Index");
        }
    }
}