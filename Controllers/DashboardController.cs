using ComplaintTracker.Models;
using System;
using System.Collections.Generic;


using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplaintTracker.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Description;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

using System.Threading;
using static ComplaintTracker.Models.ModelDashboard;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks;
using ComplaintTracker.Handler;
using System.Web.Security;

namespace ComplaintTracker.Controllers
{
    //[ExceptionHandle]
    [Authorize]
    //[Log]
    public class DashboardController : Controller
    {
        static readonly Serilog.ILogger log = EventLogger._log;
        public ActionResult Index()
        {
            //log.Information("In Index");
            //log.Warning("This is serialog using app.config");
            //log.Debug("This is serialog using app.config");
            //log.Verbose("This is serialog using app.config");
            //log.Error("This is serialog using app.config");
            //log.Fatal("This is serialog using app.config");
            if (TempData["loginmsg"] != null)
            {
                
            }
            
            return View();
        }

        public JsonResult ComplaintSourceJson() //It will be fired from Jquery ajax call
        {
           
            ModelDashboard modelDashboard = new ModelDashboard();
            modelDashboard = Repository.GetDashBoardData(Session["OFFICE_ID"].ToString());
            var jsonData = modelDashboard;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ddlMY() //It will be fired from Jquery ajax call
        {
            List<SelectListItem> data = new List<SelectListItem>();
            data = Repository.FillBillYear();

            var jsonData = data;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDivision() //It will be fired from Jquery ajax call
        {
            List<SelectListItem> data = new List<SelectListItem>();
            data = Repository.FillGetDivision();

            var jsonData = data;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSubDivision(string divisionId) //It will be fired from Jquery ajax call
        {
            List<SelectListItem> data = new List<SelectListItem>();
            data = Repository.FillGetSubDivision(divisionId);

            var jsonData = data;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AgentLiveStatus()
        {
            return View();
        }

        public async Task<JsonResult> AgentLiveStatusJson() //It will be fired from Jquery ajax call
        {
            Random random = new Random();

            ModelAgentLiveStatus modelAgentLiveStatus = new ModelAgentLiveStatus();
            modelAgentLiveStatus.Login = random.Next(150, 235).ToString();
            modelAgentLiveStatus.Free = random.Next(100, 120).ToString();
            modelAgentLiveStatus.Ringing = random.Next(400, 500).ToString();
            modelAgentLiveStatus.Busy = random.Next(12, 50).ToString();
            modelAgentLiveStatus.WrapUp = random.Next(5, 10).ToString();
            modelAgentLiveStatus.Break= random.Next(18, 80).ToString();
            modelAgentLiveStatus.Queue = random.Next(30, 60).ToString();
            modelAgentLiveStatus.Hold = random.Next(30, 70).ToString();

            List<LiveGridData> liveGridData = new List<LiveGridData> {
                new LiveGridData { AgentId =random.Next(0,5).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,10).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,15).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,20).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,25).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,30).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,35).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,40).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,45).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,50).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,55).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,60).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,65).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,70).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,75).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,80).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,85).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,90).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                 new LiveGridData { AgentId =random.Next(0,95).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
                new LiveGridData { AgentId =random.Next(0,100).ToString(),AgentName ="Saurav",Phone="5689",Campaign="outBound",ACDGroup="InComing"},
            };
            modelAgentLiveStatus.ListLiveGridData = liveGridData;
            var jsonData = modelAgentLiveStatus;
            return  Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult HarasmentcomplaintShow(string compstatus,string cmonth) //It will be fired from Jquery ajax call
        {
            List<ModelDashboardHaresment>   modelDashboardHaresments      = new List<ModelDashboardHaresment>();
            modelDashboardHaresments = Repository.ShowHaresmentComplaintDetails(compstatus, cmonth);
            return PartialView("_ComplaintDetails", modelDashboardHaresments);
        }
    }
}