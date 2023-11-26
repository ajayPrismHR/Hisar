using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintTracker.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewProfile()
        {
            return View();
        }

        public ActionResult EncoderDecoder()
        {
            return View();
        }

        public JsonResult Encode(string passwordText )
        {
            string newText = Utility.EncryptText(passwordText.Trim());


            return Json(newText, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Decode(string passwordText)
        {
            string newText = Utility.DecryptText(passwordText.Trim());
            return Json(newText, JsonRequestBehavior.AllowGet);
        }
    }
}