using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{

    public class ModelPowerOutage
    {
        public int IsData { get; set; }
        public List<ModelPowerOutageList> modelPowerOutages { get; set; }

    }

    public class ModelPowerOutageList
    {
        public string OFFICE_CODE { get; set; }
        public string START_TIME { get; set; }
        public string END_TIME { get; set; }
        public string COLONIES { get; set; }
        public string SHUT_DOWN_INFORMATION { get; set; }
        public string INFORMATION_SOURCE { get; set; }

    }
}