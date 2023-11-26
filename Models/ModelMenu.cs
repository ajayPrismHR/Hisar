using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelMenu
    {
     
        public string MainMenuName { get; set; }
        public string MainMenuViewURL { get; set; }
        public List<ModelSubMenu> ListSubMenu { get; set; }
    }

    public class ModelSubMenu 
    
    {
        public string SubMenuName { get; set; }
        public string SubMenuViewURL { get; set; }
    }

}