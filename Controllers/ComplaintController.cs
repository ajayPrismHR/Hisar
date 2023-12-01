using ComplaintTracker.DAL;
using ComplaintTracker.ExternalAPI;
using ComplaintTracker.Handler;
using ComplaintTracker.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;
using static ComplaintTracker.JqueryDatatableParam;
using static System.Net.WebRequestMethods;


namespace ComplaintTracker.Controllers
{
    [Authorize]
    public class ComplaintController : Controller
    {
        // GET: Complaint
        static readonly Serilog.ILogger log = EventLogger._log;
        public ActionResult Index()
        {
            COMPLAINT objComplaint = new COMPLAINT();
            ViewBag.fromDate = Convert.ToDateTime("1900-01-01");
            ViewBag.toDate = Convert.ToDateTime("1900-01-01");
            ViewBag.RoleID = Session["Roll_ID"];
            //objComplaint.ComplaintTypeCollection = Repository.GetComplaintTypeList("0");
            return View(objComplaint);
        }

        // GET: Complaint/Create
        public ActionResult Create()
        {
            COMPLAINT obj = new COMPLAINT();
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            obj.OfficeCodeCollection = Repository.GetOfficeList_Create(Session["UserID"].ToString());
            return View(obj);
        }

        // POST: Complaint/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(COMPLAINT modelComplaint)
        {
            COMPLAINT obj = new COMPLAINT();
            obj.OfficeCodeCollection = Repository.GetOfficeList(Session["OFFICE_ID"].ToString());

            if (modelComplaint.OFFICE_CODE_ID == 0 || string.IsNullOrEmpty(modelComplaint.JE_AREA))
            {
                return View(obj);
            }

            try
            {
                string complaintNo = await Repository.SaveComplaint(modelComplaint);
                if (complaintNo != "0")
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


        [HttpGet]
        public ActionResult EditComplaint(Int64 ComplaintNo)
        {
            COMPLAINT cOMPLAINT = new COMPLAINT();
            cOMPLAINT = Repository.GetMobileEmail(ComplaintNo);

            return PartialView("_editComplaint", cOMPLAINT);
        }

        [HttpPost]
        public async Task<ActionResult> EditComplaint(COMPLAINT modelComplaint)
        {
            COMPLAINT cOMPLAINT = new COMPLAINT();
            Int64 complaintNo = await Repository.EditComplaint(modelComplaint);
            TempData["AlertMessage"] = "Information of Complaint No. " + modelComplaint.COMPLAINT_NO.ToString() + " Saved Successfully...!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetOfficeListByParentOffice(int parentOfficeId)
        {
            List<ModelOfficeCode> lstOfficeCode = new List<ModelOfficeCode>();
            ModelOfficeCode objBlank = new ModelOfficeCode();
            //objBlank.OfficeId = "0";
            //objBlank.OfficeCode = "Select JEn Area";
            //lstOfficeCode.Insert(0, objBlank);

            SqlParameter[] param ={
                    new SqlParameter("@Parent_OfficeId",parentOfficeId) };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "GetOfficeByParent", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelOfficeCode();
                objBlank.OfficeCode = dr.ItemArray[0].ToString();
                objBlank.OfficeId = dr.ItemArray[1].ToString();
                lstOfficeCode.Add(objBlank);
            }
            return Json(lstOfficeCode, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFRTNo(int parentOfficeId)
        {
            List<ModelOfficeCode> lstOfficeCode = new List<ModelOfficeCode>();
            ModelOfficeCode objBlank = new ModelOfficeCode();
            //objBlank.OfficeId = "0";
            //objBlank.OfficeCode = "Select JEn Area";
            //lstOfficeCode.Insert(0, objBlank);

            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_ID",parentOfficeId) };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "FRT_CONTACT_DETAIL", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objBlank = new ModelOfficeCode();
                objBlank.OfficeCode = dr.ItemArray[0].ToString();
                objBlank.OfficeId = dr.ItemArray[1].ToString();
                lstOfficeCode.Add(objBlank);
            }
            return Json(lstOfficeCode, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSubComplaintByComplaintTypeId(int ComplaintTypeId)
        {
            List<ModelComplaintType> lstSubComplaintType = new List<ModelComplaintType>();

            lstSubComplaintType = Repository.GetSubComplaintTypeList(ComplaintTypeId);


            return Json(lstSubComplaintType, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetList(int OfficeCode, int ConsumerType, string Searchtype, string Searchparm)
        //{
        //    List<COMPLAINT> data = new List<COMPLAINT>();

        //    data = Repository.GetPreviousComplaint(OfficeCode, ConsumerType, Searchtype, Searchparm);
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}

        public JsonResult GetList(int OfficeCode, int ConsumerType, string Searchparm)
        {
            List<COMPLAINT> data = new List<COMPLAINT>();
            data = Repository.GetPreviousComplaint(OfficeCode, ConsumerType, Searchparm);
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ComplaintTypeJson() //It will be fired from Jquery ajax call
        {
            List<ModelComplaintType> data = new List<ModelComplaintType>();
            data = Repository.GetComplaintTypeList("0");
            var jsonData = data;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OfficeCodeJson() //It will be fired from Jquery ajax call
        {
            List<ModelOfficeCode> data = new List<ModelOfficeCode>();
            data = Repository.GetOfficeList(Session["OFFICE_ID"].ToString());
            var jsonData = data;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OfficeCodeJsonCircle() //It will be fired from Jquery ajax call
        {
            List<ModelOfficeCode> data = new List<ModelOfficeCode>();
            data = Repository.GetOfficeListCircle(Session["OFFICE_ID"].ToString());
            var jsonData = data;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ComplaintSourceJson() //It will be fired from Jquery ajax call
        {
            List<SelectListItem> data = new List<SelectListItem>();
            data = Repository.ComplaintSourceJson();
            //data.Insert(0, new SelectListItem());   
            var jsonData = data;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetComplaintSearch(DataTableAjaxPostModel model) //It will be fired from Jquery ajax call
        {
            ModelSearchComplaint dataObject = new ModelSearchComplaint();
            List<ModelSearchComplaint> data = new List<ModelSearchComplaint>();
            if (ModelState.IsValid)
            {
                try
                {
                    dataObject.UserID = Convert.ToInt64(Session["UserID"].ToString());
                    dataObject.draw = model.draw;
                    dataObject.start = model.start;
                    dataObject.length = model.length;
                    data = Repository.GetComplaintDetails(dataObject);
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

        //[HttpPost]
        //public ActionResult ExportToExcelRawComplaint(string searchComplaintno, string searchKno, string searchaccountno, string searchmobileno, string ddlOfficecode, string ddlSource, string ddlstatus, string ddlassigned, string fromdate, string todate, string ComplaintTypeId)
        //{

        //    List<ModelSearchComplaint> lstdata = new List<ModelSearchComplaint>();
        //    ModelSearchComplaint dataObject = new ModelSearchComplaint();
        //    COMPLAINT objComplaint = new COMPLAINT();

        //    dataObject.draw = 0;
        //    dataObject.start = 0;
        //    dataObject.length = 10000000;

        //    // Initialization.   
        //    dataObject.COMPLAINT_NO = String.IsNullOrEmpty(searchComplaintno) ? 0 : Convert.ToInt64(searchComplaintno);
        //    dataObject.KNO = String.IsNullOrEmpty(searchKno) ? 0 : Convert.ToInt64(searchKno);
        //    dataObject.MOBILE_NO = searchmobileno;
        //    dataObject.COMPLAINT_TYPE = ComplaintTypeId;
        //    dataObject.OFFICE_ID = ddlOfficecode;
        //    dataObject.COMPLAINT_status = ddlstatus;
        //    dataObject.COMPLAINT_SOURCE = ddlSource;
        //    dataObject.fromdate = fromdate;
        //    dataObject.todate = todate;
        //    dataObject.assigned_status = ddlassigned;
        //    if (dataObject.OFFICE_ID is null || dataObject.OFFICE_ID == "0")
        //    {
        //        dataObject.OFFICE_ID = Session["OFFICE_ID"].ToString();
        //    }
        //    lstdata = Repository.GetComplaintDetails(dataObject);


        //    List<SearchComplaintExcel> list = lstdata.AsEnumerable()
        //                              .Select(o => new SearchComplaintExcel
        //                              {
        //                                  ComplaintNo = o.COMPLAINT_NO.ToString(),
        //                                  Knumber = o.KNO.ToString(),
        //                                  ConsumerName = o.NAME.ToString(),
        //                                  Date = o.COMPLAINT_DATE.ToString(),
        //                                  CurrentStatus = o.COMPLAINT_status.ToString(),
        //                                  Duration = o.DURATION.ToString(),
        //                                  OfficeName = o.OFFICE_NAME.ToString(),
        //                                  Address = o.ADDRESS.ToString(),
        //                                  MobileNo = o.MOBILE_NO.ToString(),
        //                                  AlternateMobileNo = o.ALTERNATE_MOBILE_NO.ToString(),
        //                                  Remark=o.REMARK.ToString(),
        //                                  ComplaintType = o.COMPLAINT_TYPE.ToString(),
        //                                  Complaint = o.COMPLAINT_status.ToString(),
        //                                  SubComplaintType = o.SUB_COMPLAINT_TYPE.ToString(),
        //                                  SourceStatus = o.SOURCE_NAME.ToString(),
        //                                  AssignedTo = o.ASSIGNED_TO.ToString(),
        //                                  ClosedBy = o.CLOSED_BY.ToString(),

        //                              }).ToList();


        //    var gv = new GridView();
        //    gv.DataSource = list;
        //    gv.DataBind();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=Search_Complaint.xls");
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

        //    ViewBag.fromDate = Convert.ToDateTime("1900-01-01");
        //    ViewBag.toDate = Convert.ToDateTime("1900-01-01");
        //    ViewBag.RoleID = Session["Roll_ID"];
        //    objComplaint.ComplaintTypeCollection = Repository.GetComplaintTypeList("0");

        //    return View("Index", objComplaint);
        //}

        [HttpGet]
        public ActionResult CallToFRT(string phone) //It will be fired from Jquery ajax call
        {
            UserAPI userAPI = new UserAPI();
            ModelUser modelUser = new ModelUser();
            modelUser.User_Name = Session["User_Name"].ToString();

            if (userAPI.CallAgentUser(modelUser, phone))
            {
                int data = Repository.SaveCall(modelUser.User_Name, phone, Session["Roll_Name"].ToString(), DateTime.Now, "Call To FRT", "Start", null, "C");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CallTransfer(int RoleId) //It will be fired from Jquery ajax call
        {
            List<SelectListItem> lst = Repository.UserList(RoleId);
            ViewBag.lst = lst;
            return PartialView("_CallTransfer");
        }
        [HttpGet]
        public ActionResult DetailsSaveRemark(Int64 Id) //It will be fired from Jquery ajax call
        {
            COMPLAINT frnds = new COMPLAINT();
            frnds.COMPLAINT_NO = Id.ToString();
            DataSet ds = new DataSet();
            ds = Repository.ShowComplaintDetails(Id.ToString());

            List<COMPLAINT_REMARK> lstComplaintRemark = new List<COMPLAINT_REMARK>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                lstComplaintRemark.Add(new COMPLAINT_REMARK { ComplaintNumber = ds.Tables[1].Rows[i]["ComplaintNumber"].ToString(), REMARK_DATE_TIME = Convert.ToDateTime(ds.Tables[1].Rows[i]["REMARK_DATE_TIME"].ToString()), REMARK = ds.Tables[1].Rows[i]["REMARK"].ToString(), REMARKBY = ds.Tables[1].Rows[i]["REMARKBY"].ToString() });
            }
            frnds.ComplaintRemarkCollection = new List<COMPLAINT_REMARK>();
            frnds.ComplaintRemarkCollection = lstComplaintRemark;

            frnds.REMARKS = "";
            return PartialView("_AddRemark", frnds);
        }

        [HttpGet]
        public JsonResult SaveRemark(COMPLAINT model)
        {
            model.UserId = Convert.ToInt32(Session["UserID"].ToString());
            Response data = Repository.SaveRemark(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DetailsSaveChangeComplaint(Int64 Id) //It will be fired from Jquery ajax call
        {
            COMPLAINT obj = new COMPLAINT();
            obj.COMPLAINT_NO = Id.ToString();

            obj.REMARKS = "";
            obj = Repository.ChangeComplaintType(Id, "S", Convert.ToInt32(Session["UserID"].ToString()));
            obj.ComplaintTypeCollection = Repository.GetComplaintTypeList("");
            obj.ComplaintTypeCollection.RemoveAt(0);
            return PartialView("_ChangeComplaintType", obj);
        }

        [HttpGet]
        public JsonResult ccomplaint(COMPLAINT model)
        {
                //Save To database Logic write here
                Response data = Repository.ChangeComplaintType_Save(model, Convert.ToInt32(Session["UserID"].ToString()));
                return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult DetailsSaveAssign(Int64 Id, string MobileNo) //It will be fired from Jquery ajax call
        {
            COMPLAINT obj = new COMPLAINT();
            obj.COMPLAINT_NO = Id.ToString();

            obj.REMARKS = "";
            obj.SMS = "";
            obj.EMAIL_SEND = "";
            obj.IS_SMS = false;
            obj.IS_EMAIL = false;
            obj = Repository.GetMobileEmail(Id);
            obj.COMPLAINT_NO = Id.ToString();
            obj.Assign_FRTMobile = "";

            obj.ComplaintAssignCollection = Repository.GetAssigneeList();
            return PartialView("_Assign", obj);
        }

        [HttpGet]
        public async Task<JsonResult>  SaveAssign(COMPLAINT model)
        {        
             int Status = await Repository.ChangeAssignee_Save(model, Convert.ToInt32(Session["UserID"].ToString()));
            model.UserId = Convert.ToInt32(Session["UserID"].ToString());
            Response data = Repository.SaveRemark(model);
            if(data.status == "-1")
            {
                data.message = "error in saving Complaint No. " + model.COMPLAINT_NO.ToString() + " Error in saving ...!";
            }
            else 
            {
                data.message = "Assign done for Complaint No. " + model.COMPLAINT_NO.ToString() + " Saved Successfully ...!";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DetailsSaveClose(string Id) //It will be fired from Jquery ajax call
        {

            //ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now;
            List<SelectList> _list = new List<SelectList>();
            ModelCloseComplaint objClose = new ModelCloseComplaint();
            if(Id.Substring(0,3)=="111")
            {
                Id= "HIS"+Id.Substring(3);
            }
            if (Id.Substring(0, 3) == "222")
            {
                Id = "DH" + Id.Substring(3);
            }
            objClose.ComplaintNo = Id;
            objClose.OutageTypeCollection1 = Repository.GetOutageType();
            return PartialView("_Close", objClose);
        }

        [HttpGet]
        public async Task<JsonResult> closeComplaint(ModelCloseComplaint model)
        {
            int Status = await Repository.CloseComplaint_Save(model, Convert.ToInt32(Session["UserID"].ToString()));
            TempData["AlertMessage"] = "Closed !!! Complaint No. " + model.ComplaintNo.ToString() + " Saved Successfully...!";
            var data = new Response();
            if (Status==-1)
            {
                data.status = "-1";
                data.message = "error in saving";

            }
            else
            {
                data.status = "Closed !!! Complaint No. " + model.ComplaintNo.ToString() + " Saved Successfully...!";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetSubOutage(int OutagetypeId)
        {
            List<SelectListItem> lstSubOutageType = new List<SelectListItem>();
            lstSubOutageType = Repository.GetOutageTypeSub(OutagetypeId);
            return Json(lstSubOutageType, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetOutageCause(int OutagetypeId)
        {
            List<SelectListItem> lstSubOutageType = new List<SelectListItem>();
            lstSubOutageType = Repository.GetOutageCause(OutagetypeId);
            return Json(lstSubOutageType, JsonRequestBehavior.AllowGet);
        }
        private List<SelectListItem> GetList()
        {
            List<SelectListItem> ObjItem = new List<SelectListItem>()
            {
          new SelectListItem {Text="Select",Value="0",Selected=true },
          new SelectListItem {Text="Yes",Value="1" },
          new SelectListItem {Text="No",Value="2"},
            };

            return ObjItem;
        }


        [HttpGet]
        public ActionResult ShowComplaint(string Id) //It will be fired from Jquery ajax call
        {
            DataSet ds = new DataSet();
            ds = Repository.ShowComplaintDetails(Id);
            COMPLAINT cOMPLAINT = new COMPLAINT();
            cOMPLAINT.COMPLAINT_NO = ds.Tables[2].Rows[0]["COMPLAINT_NO"].ToString();
            cOMPLAINT.ADDRESS1 = ds.Tables[2].Rows[0]["FULL_ADDRESS"].ToString();
            cOMPLAINT.ADDRESS2 = ds.Tables[2].Rows[0]["SDO_NAME"].ToString();
            cOMPLAINT.ADDRESS3 = ds.Tables[2].Rows[0]["JE_AREA_NAME"].ToString();
            cOMPLAINT.SDO_CODE = ds.Tables[2].Rows[0]["SDO_CODE"].ToString();
            cOMPLAINT.NAME = ds.Tables[2].Rows[0]["NAME"].ToString();
            cOMPLAINT.FATHER_NAME = ds.Tables[2].Rows[0]["FATHER_NAME"].ToString();
            cOMPLAINT.LANDLINE_NO = ds.Tables[2].Rows[0]["LANDLINE_NO"].ToString();
            cOMPLAINT.LANDMARK = ds.Tables[2].Rows[0]["LANDMARK"].ToString();
            cOMPLAINT.MOBILE_NO = ds.Tables[2].Rows[0]["MOBILE_NO"].ToString();
            cOMPLAINT.KNO = ds.Tables[2].Rows[0]["KNO"].ToString();
            cOMPLAINT.CONSUMER_STATUS = ds.Tables[2].Rows[0]["CONSUMER_STATUS"].ToString();
            cOMPLAINT.EMAIL = ds.Tables[2].Rows[0]["EMAIL"].ToString();
            cOMPLAINT.ComplaintTypeName = ds.Tables[2].Rows[0]["ComplaintTypeName"].ToString();
            cOMPLAINT.ACCOUNT_NO = ds.Tables[2].Rows[0]["ACCOUNT_NO"].ToString();
            cOMPLAINT.AREA_CODE = ds.Tables[2].Rows[0]["AREA_CODE"].ToString();
            cOMPLAINT.FEEDER_NAME = ds.Tables[2].Rows[0]["FEEDER_NAME"].ToString();
            cOMPLAINT.REJECTED_CAUSE = ds.Tables[2].Rows[0]["REJECTED_CAUSE"].ToString();
            cOMPLAINT.ALTERNATE_MOBILE_NO = ds.Tables[2].Rows[0]["ALTERNATE_MOBILE_NO"].ToString();
            cOMPLAINT.REMARKS = ds.Tables[2].Rows[0]["REMARKS"].ToString();
            cOMPLAINT.Complaintdate = ds.Tables[2].Rows[0]["COMPLAINT_DATE"].ToString();
            List<COMPLAINT_LOG> lstComplaintlog = new List<COMPLAINT_LOG>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lstComplaintlog.Add(new COMPLAINT_LOG { ActionType = ds.Tables[0].Rows[i]["ActionType"].ToString(), DateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["DateTime"].ToString()), Remarks = ds.Tables[0].Rows[i]["Remarks"].ToString(), Source = ds.Tables[0].Rows[i]["Source"].ToString(), State = ds.Tables[0].Rows[i]["State"].ToString(), UserID = ds.Tables[0].Rows[i]["USERID"].ToString(), ComplaintNumber = ds.Tables[0].Rows[i]["ComplaintNumber"].ToString(), Image = ds.Tables[0].Rows[i]["Image"].ToString() });
            }
            List<COMPLAINT_REMARK> lstComplaintRemark = new List<COMPLAINT_REMARK>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                lstComplaintRemark.Add(new COMPLAINT_REMARK { ComplaintNumber = ds.Tables[1].Rows[i]["ComplaintNumber"].ToString(), REMARK_DATE_TIME = Convert.ToDateTime(ds.Tables[1].Rows[i]["REMARK_DATE_TIME"].ToString()), REMARK = ds.Tables[1].Rows[i]["REMARK"].ToString(), REMARKBY = ds.Tables[1].Rows[i]["REMARKBY"].ToString() });
            }

            cOMPLAINT.ComplaintLogCollection = new List<COMPLAINT_LOG>();
            cOMPLAINT.ComplaintLogCollection = lstComplaintlog;

            cOMPLAINT.ComplaintRemarkCollection = new List<COMPLAINT_REMARK>();
            cOMPLAINT.ComplaintRemarkCollection = lstComplaintRemark;
            return PartialView("_ComplaintDetails", cOMPLAINT);
        }


        public ActionResult ComplaintDetailsWithTile()
        {
            if (Session["UserID"] != null)
            {
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
        [HttpGet]
        public JsonResult GetComplaintTypeDetails(int OfficeCode, int ComplainttypeId)
        {
            List<ModelComplaintDashboard> data = new List<ModelComplaintDashboard>();
            data = Repository.GetComplaintTypeSummary(OfficeCode, ComplainttypeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SaveCall(string phone)
        {
            UserAPI userAPI = new UserAPI();
            ModelUser modelUser = new ModelUser();
            modelUser.User_Name = Session["User_Name"].ToString();

            if (userAPI.CallAgentUser(modelUser, phone))
            {
                int data = Repository.SaveCall(modelUser.User_Name, phone, Session["Roll_Name"].ToString(), DateTime.Now, "Call To Consumer", "Start", null, "C");
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult HoldCall(string phone)
        {
            UserAPI userAPI = new UserAPI();
            ModelUser modelUser = new ModelUser();
            modelUser.User_Name = Session["User_Name"].ToString();

            if (userAPI.HoldAgentUser(modelUser))
            {
                int data = Repository.HoldCall(modelUser.User_Name, phone, Session["Roll_Name"].ToString(), DateTime.Now, "Call To Consumer", null, "H");
            }
            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult PickCall(string phone)
        {
            UserAPI userAPI = new UserAPI();
            ModelUser modelUser = new ModelUser();
            modelUser.User_Name = Session["User_Name"].ToString();

            if (userAPI.PickAgentUser(modelUser))
            {
                int data = Repository.PickCall(modelUser.User_Name, phone, Session["Roll_Name"].ToString(), DateTime.Now, "Call To Consumer", null, "P");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult HangCall(string phone)
        {
            UserAPI userAPI = new UserAPI();
            ModelUser modelUser = new ModelUser();
            modelUser.User_Name = Session["User_Name"].ToString();

            if (userAPI.HangupAgentUser(modelUser))
            {
                int data = Repository.HangCall(modelUser.User_Name, phone, Session["Roll_Name"].ToString(), DateTime.Now, "Call To Consumer", null, "D");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ConsumerComplaintList(string searchparam)
        {
            List<COMPLAINT> cOMPLAINT = new List<COMPLAINT>();
            cOMPLAINT = Repository.GetConsumerDetails(searchparam);

            return PartialView("_ConsumerContactSelection", cOMPLAINT);
        }

        [HttpGet]
        public ActionResult ShowNewConnection(Int64 ComplaintNo)
        {
            //ComplaintNo = 11034304;
            ModelNewConnection  newConnection  = new ModelNewConnection();
            newConnection.NewConnectionStepList = Repository.GetNewConnectionSteps();
            newConnection.NewConnectionStepDetailList=Repository.GetNewConnectionStepsGrid(ComplaintNo);
            
            return PartialView("_newConnection", newConnection);
        }

        [HttpPost]
        public async Task<ActionResult> SaveNewConnection(ModelNewConnection model)
        {
            model.userId = Convert.ToString(Session["UserID"].ToString());
            int Status = await Repository.SaveNewConnection(model);

            if (Status == 1)
            {
                TempData["AlertMessage"] = " Saved Successfully...!";
            }
            if (Status == -1)
            {
                TempData["AlertMessage"] = " Error Occured in Saving...!";
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult checkPowerOutage(int officecode)
        {
            List<ModelPowerOutageList> modelPowerOutageLists    = new List<ModelPowerOutageList>();
            ModelPowerOutageList modelPowerOutages = new ModelPowerOutageList();
            ModelPowerOutage modelPowerOutage = new ModelPowerOutage(); 

            
            SqlParameter[] param ={
                    new SqlParameter("@OFFICE_CODE",officecode) };
            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "CHECK_POWER_OUTAGE", param);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                modelPowerOutages = new ModelPowerOutageList();
                modelPowerOutages.OFFICE_CODE = dr.ItemArray[0].ToString();
                modelPowerOutages.START_TIME = dr.ItemArray[1].ToString();
                modelPowerOutages.END_TIME = dr.ItemArray[2].ToString();
                modelPowerOutages.COLONIES = dr.ItemArray[3].ToString();
                modelPowerOutages.SHUT_DOWN_INFORMATION = dr.ItemArray[4].ToString();
                modelPowerOutages.INFORMATION_SOURCE = dr.ItemArray[5].ToString();
                modelPowerOutageLists.Add(modelPowerOutages);
            
            }

            if(modelPowerOutageLists.Count > 0)
            {
                modelPowerOutage.modelPowerOutages = modelPowerOutageLists;
                modelPowerOutage.IsData = 1;
                return PartialView("_powerOutage", modelPowerOutage);
            }
            else
            {
                modelPowerOutages = new ModelPowerOutageList();
                modelPowerOutageLists.Add(modelPowerOutages);
                modelPowerOutage.modelPowerOutages = modelPowerOutageLists;
                modelPowerOutage.IsData = 0;
                return null;
            }

            //return PartialView("_powerOutage", modelPowerOutage);

            //return Json(modelPowerOutages, JsonRequestBehavior.AllowGet);
        }
    }
}
