using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ComplaintTracker.DAL;
using ComplaintTracker.ExternalAPI;
using ComplaintTracker.Models;
using RestSharp;

namespace ComplaintTracker.Controllers
{
    public class GetUsersController : Controller
    {
        // GET: GetUsers
        public ActionResult Index()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            if (Session["UserID"] != null)
            {
                List<ModelUser> obj = new List<ModelUser>();
                obj = Repository.GetUserList();
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
        public ActionResult EditUser(int id)
        {
            var test = System.Web.HttpContext.Current.Request.QueryString.Get("id");
            ModelUser obj = new ModelUser();
            obj = Repository.EditUser(id);
            obj.RolesCollection = Repository.GetRolesList();
            if (obj == null)
            {
                ViewBag.Title = "AVVNL Dashboard";
            }

            return View(obj);

        }
        [HttpPost]
        public ActionResult EditUser(ModelUser user, int id)
        {
            user.RolesCollection = Repository.GetRolesList();
            int complaintNo = Repository.SaveUser(user, "U");

            TempData["AlertMessage"] = "User Updated Successfully...!";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Delete(ModelUser user, int id)
        {
            user.User_id = id;
            int complaintNo = Repository.SaveUser(user, "D");
            if (complaintNo > 0)
            {
                UserAPI userAPI = new UserAPI();
                userAPI.DeleteAgentUser(user);
                TempData["AlertMessage"] = "User Deleted Successfully...!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(User);
            }




        }
        public ActionResult Create()
        {
            ModelUser obj = new ModelUser();
            obj.RolesCollection = Repository.GetRolesList();

            return View(obj);
        }

        // POST: Complaint/Create
        [HttpPost]
        public ActionResult Create(ModelUser User)
        {
            User.RolesCollection = Repository.GetRolesList();
            try
            {

                // TODO: Add insert logic here
                int complaintNo = Repository.SaveUser(User, "I");
                TempData["AlertMessage"] = "User Created Successfully...!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(User);
            }
        }
        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            ModelUser obj = new ModelUser();
            obj = Repository.EditUser(id);
            ViewBag.User_Name = obj.User_Name;
            ViewBag.Password = string.Empty;
            ViewBag.Confirm_Password = string.Empty;
            Session["Change_password_UserID"] = id;
            //return View(obj);
            return PartialView("_ChangePassword", obj);
        }

        [HttpPost]
        public JsonResult ChangePassword(ModelUser User)
        {
            try
            {
                User.User_id = Convert.ToInt32(Session["Change_password_UserID"].ToString());
                String Status = Repository.ChangePassword(User);

                TempData["AlertMessage"] = Status;
                Session["Change_password_UserID"] = "";
                //return RedirectToAction("ChangePassword", "GetUsers");
                //return RedirectToAction("Index");
                //return View(User);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CreateUser()
        {
            ModelUser obj = new ModelUser();
            obj.RolesCollection = Repository.GetRolesList();
            obj.OfficeCodeCollection = Repository.GetOfficeList("0");
            return View(obj);
        }

        // POST: Complaint/Create
        [HttpPost]
        public ActionResult CreateUser(ModelUser User)
        {
            User.RolesCollection = Repository.GetRolesList();
            User.OfficeCodeCollection = Repository.GetOfficeList("0");
            try
            {
                if(User.RoleID==1 || User.RoleID==2)
                {
                    User.DIALER_STATUS = 1; // 1 for inbound/outbound 0 for Not 2 Deleted from Dialer
                    if (User.RoleID == 1)
                    {
                        User.agent_type = "inbound";
                    }
                    else
                    {
                        User.agent_type = "outbound";
                    }
                }
                else
                {
                    User.DIALER_STATUS = 0;
                }

                // TODO: Add insert logic here
                User.Office_Id = Session["OFFICE_ID"].ToString();
                if (saveUserAPI(User))
                {
                    int complaintNo = Repository.SaveUser(User, "I");
                    TempData["AlertMessage"] = "User Created Successfully...!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(User);
                }
               
            }
            catch (Exception ex)
            {
                return View(User);
            }
        }

        private bool saveUserAPI(ModelUser User)
        {
            UserAPI userAPI = new UserAPI();

            try
            {
                //return userAPI.CreateAgentUser(User);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}