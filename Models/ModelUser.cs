using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelUser
    {

        [Required]
        
        public string LoginId { get; set; } 
        public int User_id { get; set; }
        public string User_Name { get; set; }

        [Required]
        public string Password { get; set; }
        public string Confirm_Password { get; set; }

        public string PhoneLogin { get; set; }
        public string PhonePassword { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }
        public Int64 Mobile_NO { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_deleted { get; set; }
        public List<ModelRoles> RolesCollection { get; set; }
        public int RoleID { get; set; }

        public bool  RememberMe { get; set; }

        public int DIALER_STATUS { get; set; }

        public string agent_type { get; set; }
        public string Office_Id { get; set; }

        public List<ModelOfficeCode> OfficeCodeCollection { get; set; }
    }
}