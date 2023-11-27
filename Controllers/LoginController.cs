using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using ComplaintTracker;
using ComplaintTracker.DAL;
using ComplaintTracker.ExternalAPI;
using ComplaintTracker.Models;

namespace ComplaintTracker.Controllers
{
    public class LoginController : Controller
    {
        #region Data
        string message = string.Empty;
        #endregion

        #region AccountLogin
        [AllowAnonymous]
        public ActionResult AccountLogin()
        {
           
            ViewBag.Title = "AVVNL Account login";
            if (TempData["loginmsg"] != null)
            {
                Session.RemoveAll();
                FormsAuthentication.SignOut();
            }
            return View();
        }
        #endregion

        #region AccountLogin
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AccountLogin(ModelUser user)
        {

            SqlParameter[] param ={
                    new SqlParameter("@Username",user.LoginId.Trim()),
                    new SqlParameter("@Password",Utility.EncryptText(user.Password.Trim()) )
                                       };

            DataSet ds = SqlHelper.ExecuteDataset(HelperClass.Connection, CommandType.StoredProcedure, "Validate_User", param);

            if (ds.Tables.Count == 1)

            {
                message = "Username and/or password is incorrect.";
                ViewBag.Message = message;
                return View(user);
            }
            else
            {
                Session["UserName"] = ds.Tables[0].Rows[0]["name"].ToString();
                Session["UserID"] = ds.Tables[0].Rows[0]["USER_ID"].ToString();
                Session["User_Name"] = ds.Tables[0].Rows[0]["USER_Name"].ToString();
                Session["OFFICE_ID"] = ds.Tables[0].Rows[0]["OFFICE_ID"].ToString();
                Session["Roll_ID"] = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                Session["Roll_Name"] = ds.Tables[0].Rows[0]["ROLE_NAME"].ToString();
                Session["LoginType"] = "Active";

                FormsAuthentication.SetAuthCookie(user.LoginId, false);
                if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                {

                    return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2]);
                }
                else
                {
                    UserAPI userAPI = new UserAPI();
                    ModelUser modelUser = new ModelUser();
                    modelUser.PhoneLogin = "0";
                    modelUser.PhonePassword = "0";
                    modelUser.User_Name = ds.Tables[0].Rows[0]["USER_Name"].ToString();
                    modelUser.Password = user.Password.Trim();
                    modelUser.agent_type = ds.Tables[0].Rows[0]["ROLE_NAME"].ToString();

                    //userAPI.LoginAgentUser(modelUser);
                    //---in sp

                    //Repository.AgentLogin(modelUser.User_Name, modelUser.agent_type, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "IN");
                    List<ModelMenu> lstMenu = new List<ModelMenu>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string submenuId = dr.ItemArray[9].ToString(); //SubMenuId
                        if (string.IsNullOrEmpty(submenuId))
                        {
                            //Main Menu
                            ModelMenu modelMenu = new ModelMenu();
                            modelMenu.MainMenuName = dr.ItemArray[8].ToString(); //MenuName
                            modelMenu.MainMenuViewURL = dr.ItemArray[11].ToString(); //Url
                            lstMenu.Add(modelMenu);
                        }
                        else
                        {
                            List<ModelSubMenu> lstsubMenu = new List<ModelSubMenu>();

                            if (lstMenu.Where(x => x.MainMenuName == dr.ItemArray[8].ToString()).Count() <= 0)
                            {

                                ModelMenu modelMenu = new ModelMenu();
                                modelMenu.MainMenuName = dr.ItemArray[8].ToString();

                                foreach (DataRow drsubMenu in ds.Tables[0].Rows)
                                {

                                    if (!string.IsNullOrEmpty(submenuId) && modelMenu.MainMenuName == drsubMenu.ItemArray[8].ToString())
                                    {
                                        ModelSubMenu modelSubMenu = new ModelSubMenu();
                                        modelSubMenu.SubMenuName = drsubMenu.ItemArray[10].ToString(); //SubMenuName
                                        modelSubMenu.SubMenuViewURL = drsubMenu.ItemArray[11].ToString(); //Url

                                        lstsubMenu.Add(modelSubMenu);
                                        modelMenu.ListSubMenu = lstsubMenu;

                                    }

                                }
                                lstMenu.Add(modelMenu);

                            }
                        }
                    }
                    TempData["loginmsg"] = "Login Successfull.";
                    Session["ModelMenu"] = lstMenu;
                    return RedirectToAction("Index", "Dashboard");
                }
            }


        }
        #endregion

        #region ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            ModelUser obj = new ModelUser();
            obj = Repository.EditUser(Convert.ToInt32(Session["UserID"].ToString()));

            return View(obj);
        }
        #endregion

        #region ChangePassword
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ModelUser User)
        {
            try
            {
                // TODO: Add insert logic here
                User.User_id = Convert.ToInt32(Session["UserID"].ToString());
                String Status = Repository.ChangePassword(User);

                TempData["AlertMessage"] = Status;
                return RedirectToAction("ChangePassword", "Login");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Logout
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {

            //out sp
            UserAPI userAPI = new UserAPI();
            ModelUser modelUser = new ModelUser();
            modelUser.User_Name = Session["User_Name"].ToString();

            //userAPI.LoginAgentUser(modelUser);
            //if (userAPI.LogOutAgentUser(modelUser))
            //{
            //    Repository.AgentLogin(Session["User_Name"].ToString(), Session["Roll_Name"].ToString(), DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "OUT");
            //}
 
            message = "Logout Successfully! " + modelUser.User_Name;
            TempData["loginmsg"] = message;
            return RedirectToAction("AccountLogin");
        }
        #endregion

        #region Break
        [HttpGet]
        public ActionResult Break()
        {
            UserAPI userAPI = new UserAPI();
            ModelUser modelUser = new ModelUser();
            modelUser.User_Name = Session["User_Name"].ToString();

            //userAPI.LoginAgentUser(modelUser);
            if (userAPI.BreakResumeAgentUser(modelUser, "PAUSE"))
            {
                Repository.AgentLogin(Session["User_Name"].ToString(), Session["Roll_Name"].ToString(), DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "BR");
            }

            Session["LoginType"] = "Resume";
            return RedirectToAction("Index", "Dashboard");
        }
        #endregion

        #region Resume
        [HttpGet]
        public ActionResult Resume()
        {
            UserAPI userAPI = new UserAPI();
            ModelUser modelUser = new ModelUser();
            modelUser.User_Name = Session["User_Name"].ToString();

            //userAPI.LoginAgentUser(modelUser);
            if (userAPI.BreakResumeAgentUser(modelUser, "RESUME"))
            {
                Repository.AgentLogin(Session["User_Name"].ToString(), Session["Roll_Name"].ToString(), DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "BRR");
            }
            Session["LoginType"] = "Active";
            return RedirectToAction("Index", "Dashboard");
        }
        #endregion
    }
}