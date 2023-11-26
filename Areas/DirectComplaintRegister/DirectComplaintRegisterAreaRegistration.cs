using System.Web.Mvc;

namespace ComplaintTracker.Areas.DirectComplaintRegister
{
    public class DirectComplaintRegisterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DirectComplaintRegister";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DirectComplaintRegister_default",
                "DirectComplaint/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}