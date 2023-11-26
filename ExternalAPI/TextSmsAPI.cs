using ComplaintTracker.Handler;
using ComplaintTracker.Models;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace ComplaintTracker.ExternalAPI
{
    public class TextSmsAPI
    {
        static readonly Serilog.ILogger log = EventLogger._log;
        ModelSmsAPI modelsmsClone = null;
        public async Task<string> RegisterComplaintSMS(ModelSmsAPI modelsms)
        {
            log.Information("IN RegisterComplaintSMS");
            modelsmsClone = modelsms;
            var client = new RestClient(modelsmsClone.SmsApiURL);
            var restRequest = new RestRequest();
            restRequest.Method = Method.POST;
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(new
            {
                appid = modelsmsClone.Appid,
                userId = modelsmsClone.UserId,
                pass = modelsmsClone.Pass,
                contenttype = modelsmsClone.Contenttype,
                from = modelsmsClone.From,
                alert = modelsmsClone.Alert,
                selfid = modelsmsClone.Selfid,
                to = modelsmsClone.To,
                text = modelsmsClone.Smstext,

            });
            
            log.Information("SmsApiURL" + modelsmsClone.SmsApiURL);
            log.Information("smstext" + modelsmsClone.Smstext);

            var response = await client.ExecuteAsync(restRequest);

            log.Information(response.Content);
            log.Information(response.ResponseStatus.ToString());

            //response.Content
            if (response.StatusCode ==  System.Net.HttpStatusCode.OK)
            {
                
                return response.Content;
            }
            else
                return string.Empty;
        }
      

    }
}