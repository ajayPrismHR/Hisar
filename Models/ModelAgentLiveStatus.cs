using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelAgentLiveStatus
    {
        public string Login { get; set; }
        public string Free { get; set; }
        public string Ringing { get; set; }
        public string Busy { get; set; }

        public string WrapUp { get; set; }
        public string Break { get; set; }
        public string Queue { get; set; }
        public string Hold { get; set; }

        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }

        public List<LiveGridData> ListLiveGridData { get; set; }
        


    }

    public class LiveGridData
    {
        public string AgentId { get; set; }
        public string AgentName { get; set; }

        public string Phone { get; set; }
        public string Campaign { get; set; }

        public string ACDGroup { get; set; }

    }
}