using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System;

namespace ComplaintTracker
{
    public class CustomExceptionFilterAPI : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message;
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.InnerException.Message;
            }
            //We can log this exception message to the file or database.
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unhandled exception was thrown by service"),
                ReasonPhrase = "unique Que",

            };


            actionExecutedContext.Response = response;
        }

    }
}