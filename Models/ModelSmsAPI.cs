using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelSmsAPI
    {

        private string _smsApiURL = System.Configuration.ConfigurationManager.AppSettings["smsApiURL"];
        private string _appid = System.Configuration.ConfigurationManager.AppSettings["appid"];
        private string _userid = System.Configuration.ConfigurationManager.AppSettings["userid"];
        private string _pass = System.Configuration.ConfigurationManager.AppSettings["pass"];
        private string _contenttype = System.Configuration.ConfigurationManager.AppSettings["contenttype"];
        private string _from = System.Configuration.ConfigurationManager.AppSettings["from"];
        private string _alert = System.Configuration.ConfigurationManager.AppSettings["alert"];
        private string _selfid = System.Configuration.ConfigurationManager.AppSettings["selfid"];

        private string _consumer = System.Configuration.ConfigurationManager.AppSettings["Consumer"];
        private string _cC = System.Configuration.ConfigurationManager.AppSettings["CC"];
        private string _NCMS_Consumer = System.Configuration.ConfigurationManager.AppSettings["NCMS_ConsumerSMS"];
        private string _NCMS_CC = System.Configuration.ConfigurationManager.AppSettings["NCMS_CCSMS"];



        private string _to;
        private string _smstext;
        public string SmsApiURL { get { return _smsApiURL; } }

        public string Appid { get { return _appid; } }
        public string UserId { get { return _userid; } }
        public string Pass { get { return _pass; } }
        public string Contenttype { get { return _contenttype; } }
        public string From { get { return _from; } }
        public string Alert { get { return _alert; } }
        public string Selfid { get { return _selfid; } }
        public string To { get { return _to; } set { _to = value; } }
        public string Smstext { get { return _smstext; } set { _smstext = value; } }



        public string ConsumerSMS  { get {return _consumer; } }
        public string CCSMS {  get { return _cC; } }
        public string NCMS_ConsumerSMS { get { return _NCMS_Consumer; } }
        public string NCMS_CCSMS { get { return _NCMS_CC; } }

    }


}
