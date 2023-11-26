using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelAPI
    {
       

        public string user { get; set; }
        public string pass { get; set; }

        public string agent_user { get; set; }
        public string agent_pass { get; set; }
        public string phone_login { get; set; }

        public string phone_pass { get; set; }
        public string agent_type { get; set; }

        public string function { get; set; }
    }
}