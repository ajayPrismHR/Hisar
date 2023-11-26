using ComplaintTracker.DAL;
using ComplaintTracker.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ComplaintTracker.ExternalAPI
{

    public class UserAPI
    {
        private static string ExternalApiURL = System.Configuration.ConfigurationManager.AppSettings["ExternalApiURL"];
        private static string APIUser = System.Configuration.ConfigurationManager.AppSettings["APIUser"];
        private static string APIPassword = System.Configuration.ConfigurationManager.AppSettings["APIPassword"];

        ModelAPI modelAPI = null;
        private string ApiUrl = string.Empty;


        public bool LoginAgentUser(ModelUser modelUser)
        {

          string DialerApiURL = System.Configuration.ConfigurationManager.AppSettings["DialerApiURL"];
            modelAPI = new ModelAPI();

            modelAPI.user = APIUser;
            modelAPI.pass = APIPassword;


            modelAPI.agent_user = modelUser.User_Name;
            modelAPI.agent_pass = modelUser.Password;

            modelAPI.phone_login = modelUser.PhoneLogin;
            modelAPI.phone_pass = modelUser.PhonePassword;
            modelAPI.agent_type = modelUser.agent_type;
           
            string ApiUrl = string.Format("{0}?phone_login={1}&phone_pass={2}&VD_login={3}&VD_pass={4}&VD_campaign={5}&relogin={6}",
                 DialerApiURL,
                modelAPI.phone_login, 
                modelAPI.phone_pass,
                modelAPI.agent_user,
                modelAPI.agent_pass,
                modelAPI.agent_type, 
               "No");


            var client = new RestClient(String.Format("{0}", ApiUrl));
            var restRequest = new RestRequest("", Method.POST);

            var response = client.Execute(restRequest);
            Repository.SaveLogin(modelUser);
            if (response.Content.Contains("SUCCESS"))
            {
                return true;
            }
            else
                return false;
        }

        public bool LogOutAgentUser(ModelUser modelUser)
        {

            modelAPI = new ModelAPI();

            modelAPI.user = APIUser;
            modelAPI.pass = APIPassword;


            modelAPI.agent_user = modelUser.User_Name;
            modelAPI.function = "agent_logout";

            string ApiUrl = string.Format("{0}?user={1}&pass={2}&function={3}&agent_user={4}",
                ExternalApiURL,
                modelAPI.user, modelAPI.pass, modelAPI.function, modelAPI.agent_user);


            var client = new RestClient(String.Format("{0}", ApiUrl));
            var restRequest = new RestRequest("", Method.POST);

            var response = client.Execute(restRequest);
            if (response.Content.Contains("SUCCESS"))
            {
                return true;
            }
            else
                return false;
        }
        public bool BreakResumeAgentUser(ModelUser modelUser,string Value)
        {

            modelAPI = new ModelAPI();

            modelAPI.user = APIUser;
            modelAPI.pass = APIPassword;


            modelAPI.agent_user = modelUser.User_Name;
            modelAPI.function = "agent_pause";

            string ApiUrl = string.Format("{0}?user={1}&pass={2}&function={3}&agent_user={4}&value={5}",
                ExternalApiURL,
                modelAPI.user, modelAPI.pass, modelAPI.function, modelAPI.agent_user, Value);


            var client = new RestClient(String.Format("{0}", ApiUrl));
            var restRequest = new RestRequest("", Method.POST);

            var response = client.Execute(restRequest);
            if (response.Content.Contains("SUCCESS"))
            {
                return true;
            }
            else
                return false;
        }
        public bool CreateAgentUser(ModelUser modelUser)
        {
            modelAPI = new ModelAPI();

            modelAPI.user = APIUser;
            modelAPI.pass = APIPassword;


            modelAPI.agent_user = modelUser.User_Name;
            modelAPI.agent_pass = modelUser.Password;

            modelAPI.phone_login = modelUser.PhoneLogin;
            modelAPI.phone_pass = modelUser.PhonePassword;
            modelAPI.agent_type = modelUser.agent_type;
            modelAPI.function = "add_user";

            string ApiUrl = string.Format("{0}?user={1}&pass={2}&function={3}&agent_user={4}&agent_pass={5}&phone_login={6}&phone_pass={7}&agent_type={8}",
                ExternalApiURL,
                modelAPI.user, modelAPI.pass, modelAPI.function, modelAPI.agent_user, modelAPI.agent_pass, modelAPI.phone_login, modelAPI.phone_pass, modelAPI.agent_type);


            var client = new RestClient(String.Format("{0}", ApiUrl));
            var restRequest = new RestRequest("", Method.POST);

            var response = client.Execute(restRequest);
            Repository.SaveLogin(modelUser);
            if (response.Content.Contains("SUCCESS"))
            {
                return true;
            }
            else
                return false;
        }
        public bool DeleteAgentUser(ModelUser modelUser)
        {
            modelAPI = new ModelAPI();

            modelAPI.user = APIUser;
            modelAPI.pass = APIPassword;
            modelAPI.agent_user = modelUser.User_Name;
            modelAPI.phone_login = modelUser.PhoneLogin;
            modelAPI.function = "delete_user";

            string ApiUrl = string.Format("{0}?user={1}&pass={2}&function={3}&agent_user={4}&phone_login={5}",
                ExternalApiURL,
                modelAPI.user, modelAPI.pass, modelAPI.function, modelAPI.agent_user, modelAPI.phone_login);


            var client = new RestClient(String.Format("{0}", ApiUrl));
            var restRequest = new RestRequest("", Method.POST);

            var response = client.Execute(restRequest);
            Repository.SaveLogin(modelUser);
            if (response.Content.Contains("SUCCESS"))
            {
                return true;
            }
            else
                return false;
        }

        public bool CallAgentUser(ModelUser modelUser, string Value)
        {

            modelAPI = new ModelAPI();

            modelAPI.user = APIUser;
            modelAPI.pass = APIPassword;


            modelAPI.agent_user = modelUser.User_Name;
            modelAPI.function = "external_dial";

            string ApiUrl = string.Format("{0}?user={1}&pass={2}&function={3}&agent_user={4}&phone_number={5}&phone_code={5}",
                ExternalApiURL,
                modelAPI.user, modelAPI.pass, modelAPI.function, modelAPI.agent_user, Value,"91");


            var client = new RestClient(String.Format("{0}", ApiUrl));
            var restRequest = new RestRequest("", Method.POST);

            var response = client.Execute(restRequest);
            if (response.Content.Contains("SUCCESS"))
            {
                return true;
            }
            else
                return false;
        }

        public bool HoldAgentUser(ModelUser modelUser)
        {

            modelAPI = new ModelAPI();

            modelAPI.user = APIUser;
            modelAPI.pass = APIPassword;


            modelAPI.agent_user = modelUser.User_Name;
            modelAPI.function = "hold_customer";

            string ApiUrl = string.Format("{0}?user={1}&pass={2}&function={3}&agent_user={4}",
                ExternalApiURL,
                modelAPI.user, modelAPI.pass, modelAPI.function, modelAPI.agent_user);


            var client = new RestClient(String.Format("{0}", ApiUrl));
            var restRequest = new RestRequest("", Method.POST);

            var response = client.Execute(restRequest);
            if (response.Content.Contains("SUCCESS"))
            {
                return true;
            }
            else
                return false;
        }
        public bool PickAgentUser(ModelUser modelUser)
        {

            modelAPI = new ModelAPI();

            modelAPI.user = APIUser;
            modelAPI.pass = APIPassword;


            modelAPI.agent_user = modelUser.User_Name;
            modelAPI.function = "pickup_customer";

            string ApiUrl = string.Format("{0}?user={1}&pass={2}&function={3}&agent_user={4}",
                ExternalApiURL,
                modelAPI.user, modelAPI.pass, modelAPI.function, modelAPI.agent_user);


            var client = new RestClient(String.Format("{0}", ApiUrl));
            var restRequest = new RestRequest("", Method.POST);

            var response = client.Execute(restRequest);
            if (response.Content.Contains("SUCCESS"))
            {
                return true;
            }
            else
                return false;
        }
        public bool HangupAgentUser(ModelUser modelUser)
        {

            modelAPI = new ModelAPI();

            modelAPI.user = APIUser;
            modelAPI.pass = APIPassword;


            modelAPI.agent_user = modelUser.User_Name;
            modelAPI.function = "external_hangup";

            string ApiUrl = string.Format("{0}?user={1}&pass={2}&function={3}&agent_user={4}",
                ExternalApiURL,
                modelAPI.user, modelAPI.pass, modelAPI.function, modelAPI.agent_user);


            var client = new RestClient(String.Format("{0}", ApiUrl));
            var restRequest = new RestRequest("", Method.POST);

            var response = client.Execute(restRequest);
            if (response.Content.Contains("SUCCESS"))
            {
                return true;
            }
            else
                return false;
        }
    }
}