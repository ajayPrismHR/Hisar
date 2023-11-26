using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using System;
using System.Web.Mvc;

namespace ComplaintTracker
{
    public class ExceptionHandle : FilterAttribute, IExceptionFilter
    {



        void IExceptionFilter.OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            ModelExceptionLogger logger = new ModelExceptionLogger()
            {
                ExceptionMessage = filterContext.Exception.Message,
                ExceptionStackTrack = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                ExceptionLogTime = DateTime.Now.ToString()
            };
            filterContext.ExceptionHandled = true;

            Repository.SaveUnhandeledExceptions(logger);
            //Redirect to action
            
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Exception/Settings.cshtml"
                //ViewName = "error";

            };
        }
    }
}

