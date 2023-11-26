using System;
using System.Collections.Generic;
using System.Linq;
using ComplaintTracker.Models;
using System.Web.Mvc;
using ComplaintTracker.DAL;

namespace ComplaintTracker.Controllers
{
    public class ExceptionController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.fromDate = DateTime.Now;
            ViewBag.toDate = DateTime.Now.AddDays(1);
            return View();
        }
        public JsonResult GetExceptions(ModelReport dataObject) //It will be fired from Jquery ajax call
        {
            List<ModelExceptionLogger> data = new List<ModelExceptionLogger>();
            data = Repository.GetExceptions(dataObject);
            var jsonData = data;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

      

        public ActionResult Settings()
        {
            return View();
        }


    }
}