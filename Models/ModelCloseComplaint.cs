using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ComplaintTracker.Models
{

    public class ModelCloseComplaint
    {
        [Key]
        public string ComplaintNo { get; set; }

        
        public int OutageTypeCollectionId1 { get; set; }
        public List<SelectListItem> OutageTypeCollection1 { get; set; }
        public int OutageTypeCollectionId2 { get; set; }
        public List<SelectListItem> OutageTypeCollection2 { get; set; }
        public int CauseCollectionId { get; set; }
        public List<SelectListItem> CauseCollection { get; set; }

        public string TO_DATE { get; set; }



        public string MeterNo{ get; set; }
        public string Reading { get; set; }
        public string Type { get; set; }
        public string StatusAfterRectification{ get; set; }
        public string StatusBeforeRectification  { get; set; }
        public int AbnormalityCollectionId { get; set; }
        public List<SelectListItem>  AbnormalityCollection { get; set; }
        public bool IsConfirmByCustomer { get; set; }
        public string Remarks { get; set; }


    }
}