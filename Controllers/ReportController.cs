using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;
using static ComplaintTracker.JqueryDatatableParam;
using System.Reflection;
using System.Windows;

namespace ComplaintTracker.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportComplaintAnalysis()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportCallQueueWaiting()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportInBoundPerformance()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportSystemAvailabilityPenalty()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportUniformPenalty()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportNonITPenalty()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }

        public ActionResult ReportCCCAgentPenalty()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportFalseClosurePenalty()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportCallAbandonment()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }

        public ActionResult ReportFRTPanalty()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }

        public ActionResult ReportEscalatedComplaint()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportRawComplaint()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            COMPLAINT objComplaint = new COMPLAINT();
            return View(objComplaint);
        }

        public ActionResult ReportRawComplaintNewConnection()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            COMPLAINT objComplaint = new COMPLAINT();
            return View(objComplaint);
        }
        public ActionResult ReportFRTPerformance()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            COMPLAINT objComplaint = new COMPLAINT();
            objComplaint.ComplaintTypeCollection = Repository.GetComplaintTypeList("0");
            return View(objComplaint);
        }

        public ActionResult ReportHelpdeskPerformance()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            COMPLAINT objComplaint = new COMPLAINT();
            objComplaint.ComplaintTypeCollection = Repository.GetComplaintTypeList("0");
            return View(objComplaint);
        }
        public ActionResult ReportOutboundPerformance()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            COMPLAINT objComplaint = new COMPLAINT();
            objComplaint.ComplaintTypeCollection = Repository.GetComplaintTypeList("0");
            return View(objComplaint);
        }

        //[Authorize(Roles = "Outbound")]
        public ActionResult ReportRedressal()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportHourly()
        {

            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);

            return View();
        }
        public ActionResult ReportPendingComplaint()
        {
            return View();
        }
        public ActionResult ReportOutageInformation()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            ModelOutageReport obj = new ModelOutageReport();
            obj.ComplaintTypeCollection = Repository.GetComplaintTypeList("0");
            return View(obj);
        }
        public ActionResult ReportRandomizer()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportRepeatedComplaint()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportTransformer()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        //========================Performance Report============================
        public ActionResult ReportFRT()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportHelpDesk()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportInbound()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public ActionResult ReportOutbound()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }

        public ActionResult ReportComplaintWiseDetailReport()
        {
            return View();
        }

        public ActionResult ReportDTReport()
        {
            return View();
        }

        public ActionResult ReportNewConnection()
        {
            return View();
        }
        public ActionResult ReportComplaintSourceWiseDetailReport()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReportRepetedComplaint()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            List<ModelComplaintType> data = new List<ModelComplaintType>();
            data = Repository.GetComplaintTypeList("0");
            List<SelectListItem> customerList = new List<SelectListItem>();
            foreach (var item in data)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text= item.ComplaintType.ToString();
                selectListItem.Value = item.ComplaintTypeId.ToString();
                customerList.Add(selectListItem);
            }


            ViewBag.ItemList = customerList;
            DataTable dt = new DataTable();
            return View(dt);
        }

        public ActionResult ReportFRTTracking()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }

        public ActionResult ReportPriceVariation()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }



        //====================================================================
        #region ComplaintAnalysis Report
        //public JsonResult ReportComplaintAnalysisSearch(ModelReport dataObject)
        //{
        //    List<ModelComplaintAnalysisReport> data = new List<ModelComplaintAnalysisReport>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportComplaintAnalysisData(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Esclated Complaint Report
        //public JsonResult ReportEscalatedSearch(ModelReport dataObject)
        //{
        //    List<EsclationComplaint> data = new List<EsclationComplaint>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportEscalatedComplaint(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        #region FRT Performance Report
        //public JsonResult SearchReportFRTPerformance(ModelReport dataObject)
        //{
        //    List<PerformanceTrackerModel> data = new List<PerformanceTrackerModel>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = "1000000";
        //    }
        //    data = Repository.ReportFRTPerformance(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Helpdesk Performance Report
        //public JsonResult SearchReportHelpDeskPerformance(ModelReport dataObject)
        //{
        //    List<HelpDeskPerformanceTrackerModel> data = new List<HelpDeskPerformanceTrackerModel>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportHelpdeskPerformance(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Outbound Performance Report
        //public JsonResult SearchReportOutboundPerformance(ModelReport dataObject)
        //{
        //    List<OutBoundPerformanceTrackerModel> data = new List<OutBoundPerformanceTrackerModel>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportOutboundPerformance(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region FRT Panalty Report
        //public JsonResult ReportFRTPaneltySearch(ModelReport dataObject)
        //{
        //    List<FRTPanelty> data = new List<FRTPanelty>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportFRTPanalty(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        //#region Call Queue Waiting Report
        //public JsonResult ReportCallQueueWaitingSearch(ModelReport dataObject)
        //{
        //    List<CallQueueWaitingModel> data = new List<CallQueueWaitingModel>();
            
        //    data = Repository.ReportCallQueueWaiting(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region Inbound Performance Report
        //public JsonResult ReportInBoundPerformanceSearch(ModelReport dataObject)
        //{
        //    List<InBoundPerformanceTrackerModel> data = new List<InBoundPerformanceTrackerModel>();

        //    data = Repository.ReportInBoundPerformance(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region Non IT Penalty Report
        //public JsonResult ReportNonITPenaltySearch(ModelReport dataObject)
        //{
        //    List<NonITPanalty> data = new List<NonITPanalty>();

        //    data = Repository.ReportNonITPenalty(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region Uniform Penalty Report
        //public JsonResult ReportUniformPenaltySearch(ModelReport dataObject)
        //{
        //    List<UniformPanalty> data = new List<UniformPanalty>();

        //    data = Repository.ReportUniformPenalty(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region Missing Agents Penalty Report
        //public JsonResult ReportMissingAgentsPenaltySearch(ModelReport dataObject)
        //{
        //    List<UniformPanalty> data = new List<UniformPanalty>();

        //    data = Repository.ReportMissingAgentsPenalty(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region System Availability Report
        //public JsonResult ReportSystemAvailabilitySearch(ModelReport dataObject)
        //{
        //    List<SystemAvailability> data = new List<SystemAvailability>();

        //    data = Repository.ReportSystemAvailability(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region False Close Report
        //public JsonResult ReportFalseCloseSearch(ModelReport dataObject)
        //{
        //    List<FalseClouse> data = new List<FalseClouse>();

        //    data = Repository.ReportFalseClose(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region Call Queue Abandonment
        //public JsonResult ReportCallAbanonmentSearch(ModelReport dataObject)
        //{
        //    List<CallQueueAbandonmentModel> data = new List<CallQueueAbandonmentModel>();

        //    data = Repository.ReportCallAbandonment(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        #region Hourly Report
        //public JsonResult ReportComplaintHourlySearch(ModelReport dataObject)
        //{
        //    List<ModelComplaintHourlyReport> data = new List<ModelComplaintHourlyReport>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportComplaintHourlyData(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public ActionResult ShowHourlyRawDetails(ModelReport dataObject) //It will be fired from Jquery ajax call
        //{
        //    List<COMPLAINT> lstComplaints = new List<COMPLAINT>();
        //    lstComplaints = Repository.ReportComplaintHourlyRawData(dataObject);
        //    return PartialView("_HourlyPopup", lstComplaints);
        //}
        #endregion
        #region Raw Complaint
        [HttpPost]
        [Authorize]
        public JsonResult ReportRawComplaintSearch(DataTableAjaxPostModel model)
        {
            ModelReport dataObject = new ModelReport();
            List<ModelRawComplaintReport> data = new List<ModelRawComplaintReport>();
            if (ModelState.IsValid)
            {
                try
                {

                    dataObject.draw = model.draw;
                    dataObject.start = model.start;
                    dataObject.length = model.length;

                    // Initialization.   
                    dataObject.UserID = Convert.ToInt64(Session["UserID"].ToString());
                    dataObject.Bill_Month = Convert.ToString(Request.Form.GetValues("Bill_Month")[0]);
                    data = Repository.ReportRawComplaintData(dataObject);
                    int count = data.Count() > 0 ? data[0].Total : 0;
                    return Json(new { draw = dataObject.draw, recordsFiltered = count, recordsTotal = count, data = data }, JsonRequestBehavior.AllowGet);

                }
                catch
                {
                    return Json(new { draw = dataObject.draw, recordsFiltered = 0, recordsTotal = 0, data = data }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }

        }

        //public JsonResult ReportRawComplaintNewConnectionSearch(DataTableAjaxPostModel model)
        //{
        //    ModelReport dataObject = new ModelReport();
        //    List<ModelRawComplaintReportNewConnection> data = new List<ModelRawComplaintReportNewConnection>();
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            dataObject.draw = model.draw;
        //            dataObject.start = model.start;
        //            dataObject.length = model.length;

        //            // Initialization.   
        //            dataObject.fromdate = Convert.ToString(Request.Form.GetValues("fromdate")[0]);
        //            dataObject.todate = Convert.ToString(Request.Form.GetValues("todate")[0]);
        //            dataObject.OfficeCode = Convert.ToString(Request.Form.GetValues("OfficeCode")[0]);
        //            if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //            {
        //                dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //            }
        //            data = Repository.ReportRawComplaintNewConnectionData(dataObject);
        //            int count = data.Count() > 0 ? data[0].Total : 0;
        //            return Json(new { draw = dataObject.draw, recordsFiltered = count, recordsTotal = count, data = data }, JsonRequestBehavior.AllowGet);

        //        }
        //        catch
        //        {
        //            return Json(new { draw = dataObject.draw, recordsFiltered = 0, recordsTotal = 0, data = data }, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    else
        //    {
        //        return Json("error", JsonRequestBehavior.AllowGet);
        //    }

        //}
        #endregion
        #region Pending Complaint
        //public JsonResult ReportPendingComplaintSearch(ModelReport dataObject)
        //{
        //    List<ModelPendingComplaint> data = new List<ModelPendingComplaint>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReporttPendingComplaint(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Randomizer Report
        //public JsonResult ReportRandomizerSearch(ModelReport dataObject)
        //{
        //    List<ModelRandomizerReport> data = new List<ModelRandomizerReport>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }

        //    data = Repository.ReportRandomizerData(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult ReportRandomizerSearch(DataTableAjaxPostModel model) //It will be fired from Jquery ajax call
        //{
        //    ModelReport dataObject = new ModelReport();
        //    List<ModelRandomizerReport> data = new List<ModelRandomizerReport>();
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            dataObject.draw = model.draw;
        //            dataObject.start = model.start;
        //            dataObject.length = model.length;

        //            // Initialization.   
        //            dataObject.fromdate = String.IsNullOrEmpty(Request.Form.GetValues("fromdate")[0]) ? string.Empty : Request.Form.GetValues("fromdate")[0];
        //            dataObject.todate = String.IsNullOrEmpty(Request.Form.GetValues("todate")[0]) ? string.Empty : Request.Form.GetValues("todate")[0];
        //            dataObject.count = Convert.ToString(Request.Form.GetValues("count")[0]);

        //            data = Repository.ReportRandomizerData(dataObject);
        //            int count = data.Count() > 0 ? data[0].Total : 0;
        //            return Json(new { draw = dataObject.draw, recordsFiltered = count, recordsTotal = count, data = data }, JsonRequestBehavior.AllowGet);

        //        }
        //        catch
        //        {
        //            return Json(new { draw = dataObject.draw, recordsFiltered = 0, recordsTotal = 0, data = data }, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    else
        //    {
        //        return Json("error", JsonRequestBehavior.AllowGet);
        //    }
        //}


        #endregion


        #region Outage Report
        //public JsonResult ReportOutage(ModelReport dataObject)
        //{
        //    List<ModelOutageReport> data = new List<ModelOutageReport>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }

        //    data = Repository.ReportOutageData(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region Rederssal Report
        //public JsonResult ReportRedressalSearch(ModelReport dataObject)
        //{
        //    List<ModelRedressal> data = new List<ModelRedressal>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportRedressalData(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        #region Complaint Wise
        //public JsonResult ReportComplaintWiseDetailSearch(ModelReport dataObject)
        //{
        //    List<ModelComplaintWiseDetailReport> data = new List<ModelComplaintWiseDetailReport>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportComplaintWiseDetail(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region DT
        //public JsonResult ReportDTSearch(ModelReport dataObject)
        //{
        //    List<DTModel> data = new List<DTModel>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportDT(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region NewConnection
        //public JsonResult ReportNewConnectionSearch(ModelReport dataObject)
        //{
        //    List<NewConnectionModel> data = new List<NewConnectionModel>();
        //    if (dataObject.OfficeCode is null || dataObject.OfficeCode == "0")
        //    {
        //        dataObject.OfficeCode = Session["OFFICE_ID"].ToString();
        //    }
        //    data = Repository.ReportNewConnection(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region  Source Wise
        //public JsonResult ReportComplaintSourceWiseDetailSearch(ModelReport dataObject)
        //{
        //    List<ModelComplaintSourceWiseDetailReport> data = new List<ModelComplaintSourceWiseDetailReport>();
        //    data = Repository.ReportComplaintSourceWiseDetail(dataObject);
        //    var jsonData = data;
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult ShowComplaintSourceWise(string sDate, string ComplaintSource, string OfficeCode)
        {
            List<ModelComplaintSourceWiseDetailReport> data = new List<ModelComplaintSourceWiseDetailReport>();
            data = Repository.ComplaintSourceWiseDetailPopUp(sDate, ComplaintSource, OfficeCode);

            var jsonData = data;
            return PartialView("_ComplaintSourceWiseDetails", jsonData);
        }
        #endregion

        //#region  ReportRepetedComplaint
        //[HttpPost]
        //public ActionResult ReportRepetedComplaint(FormCollection form)
        //{



        //    DateTime from = Convert.ToDateTime(form["fromDate"]);
        //    DateTime to = Convert.ToDateTime(form["toDate"]);
        //    string ddlOfficecode = Convert.ToString(form["ddlOfficecode"]);
        //    string ddlType = Convert.ToString(form["ddlType"]);
        //    if (ddlOfficecode is null || ddlOfficecode == "0")
        //    {
        //        ddlOfficecode = Session["OFFICE_ID"].ToString();
        //    }
        //    ModelReport modelReport = new ModelReport();
        //    modelReport.Bill_Month = from.ToString("yyyy-MM-dd");/* Convert.ToString(from);*/
        //    //modelReport.todate = to.ToString("yyyy-MM-dd"); /*Convert.ToString(to);*/
        //    //modelReport.OfficeCode = ddlOfficecode;
        //    //modelReport.ComplaintType = Convert.ToInt16(ddlType);
        //    ViewBag.fromDate = from;
        //    ViewBag.toDate = to;
        //    List<ModelComplaintType> data = new List<ModelComplaintType>();
        //    data = Repository.GetComplaintTypeList("0");
        //    List<SelectListItem> customerList = new List<SelectListItem>();
        //    foreach (var item in data)
        //    {
        //        SelectListItem selectListItem = new SelectListItem();
        //        selectListItem.Text = item.ComplaintType.ToString();
        //        selectListItem.Value = item.ComplaintTypeId.ToString();
        //        if (selectListItem.Value == ddlType)
        //            selectListItem.Selected = true;
        //        customerList.Add(selectListItem);
        //    }


        //    ViewBag.ItemList = customerList;

        //    DataTable dt = Repository.ComplaintRepeat(modelReport);
        //    return View(dt);
        //}
        //#endregion


        [HttpPost]
        public ActionResult ExportToExcelRawComplaint(string ddlMY)
        {
            
            List<ModelRawComplaintReport> lstdata = new List<ModelRawComplaintReport>();
            ModelReport dataObject = new ModelReport();

            dataObject.draw = 0;
            dataObject.start = 0;
            dataObject.length = 10000000;
            dataObject.UserID = Convert.ToInt64(Session["UserID"].ToString());
            // Initialization.   
            dataObject.Bill_Month = ddlMY;
            lstdata = Repository.ReportRawComplaintData(dataObject);
            List<RawComplaintExcel> list = lstdata.AsEnumerable()
                                      .Select(o => new RawComplaintExcel
                                      {
                                          ComplaintNo = o.ComplaintNo,
                                          Customer_Name = o.Customer_Name,
                                          Mobile_no = o.Mobile_no,
                                          Category_Name = o.Category_Name,
                                          LineMan_Name = o.LineMan_Name,
                                          Complaint_date = o.Complaint_date,
                                          Resolved_Date = o.Resolved_Date,
                                          Response_Time = o.Response_Time

                                      }).ToList();

            var gv = new GridView();
            gv.DataSource = list;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Raw_Complaint.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    gv.RenderControl(htw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View("Index");
        }

        //[HttpPost]
        //public ActionResult ExportToExcelRawComplaintNewConnection(string fromDate, string toDate, string ddlOfficecode)
        //{

        //    List<ModelRawComplaintReportNewConnection> lstdata = new List<ModelRawComplaintReportNewConnection>();
        //    ModelReport dataObject = new ModelReport();

        //    dataObject.draw = 0;
        //    dataObject.start = 0;
        //    dataObject.length = 10000000;

        //    // Initialization.   
        //    dataObject.fromdate = fromDate;
        //    dataObject.todate = toDate;
        //    dataObject.OfficeCode = Convert.ToString(ddlOfficecode);

        //    lstdata = Repository.ReportRawComplaintNewConnectionData(dataObject);
        //    List<RawComplaintNewConnectionExcel> list = lstdata.AsEnumerable()
        //                              .Select(o => new RawComplaintNewConnectionExcel
        //                              {
        //                                  COMPLAINT_NO = o.COMPLAINT_NO,
        //                                  SDO_CODE = o.SDO_CODE,
        //                                  SDO_NAME = o.SDO_NAME,
        //                                  KNO = o.KNO,
        //                                  NAME = o.NAME,
        //                                  FATHER_NAME = o.FATHER_NAME,
        //                                  ADDRESS = o.ADDRESS,
        //                                  MOBILE_NO = o.MOBILE_NO,
        //                                  ALTERNATE_MOBILE_NO = o.ALTERNATE_MOBILE_NO,
        //                                  COMPLAINT_TYPE = o.COMPLAINT_TYPE,
        //                                  SUB_COMPLAINT_TYPE = o.SUB_COMPLAINT_TYPE,
        //                                  OUTAGE_TYPE = o.OUTAGE_TYPE,
        //                                  SUB_OUTAGE_TYPE = o.SUB_OUTAGE_TYPE,
        //                                  DS_NDS = o.DS_NDS,
        //                                  COMPLAINT_DATE_TIME = o.COMPLAINT_DATE_TIME,
        //                                  CLOSED_DATE_TIME = o.CLOSED_DATE_TIME,
        //                                  DURATION = o.DURATION,
        //                                  COMPLAINT_STATUS = o.COMPLAINT_STATUS,
        //                                  CURRENT_STATUS = o.CURRENT_STATUS,
        //                                  CREATED_BY_USER = o.CREATED_BY_USER,
        //                                  CLOSED_BY_USER = o.CLOSED_BY_USER

        //                              }).ToList();

        //    var gv = new GridView();
        //    gv.DataSource = list;
        //    gv.DataBind();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=Raw_Complaint.xls");
        //    Response.ContentType = "application/ms-excel";
        //    Response.Charset = "";

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        //        {
        //            gv.RenderControl(htw);
        //            Response.Output.Write(sw.ToString());
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //    return View("Index");
        //}

        //[HttpGet]
        //public ActionResult GetRepeatedComplaints_RAW(ModelReport modelReport) //It will be fired from Jquery ajax call
        //{
        //    List<ModelDashboardHaresment> modelDashboardHaresments = new List<ModelDashboardHaresment>();
        //    modelDashboardHaresments = Repository.GetRepeatedComplaints_RAW(modelReport);
        //    return PartialView("_repetedComplaintDetails", modelDashboardHaresments);
        //}
    }
}


//var dict = new Dictionary<string, string>
//{
//    { "name", "Foobar" },
//    { "url", "admin@foobar.com" }
//};

//var json = new JsonResult()
//{
//    Data = dict
//};

//return Json(json, JsonRequestBehavior.AllowGet);