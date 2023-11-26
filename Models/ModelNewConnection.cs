using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintTracker.Models
{
    public class ModelNewConnection
    {
        [Key]
        public long ComplaintNo { get; set; }
        public string rdoDsOrNds { get; set; }
        public int NewConnectionStepId { get; set; }
        public List<SelectListItem> NewConnectionStepList { get; set; }
        public int NewConnectionStepDetailId { get; set; }
        public List<ModelComplaintStepsGrid> NewConnectionStepDetailList { get; set; }
        public string userId { get; set; }
    }
}