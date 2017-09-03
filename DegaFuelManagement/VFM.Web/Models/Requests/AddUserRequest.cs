using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddUserRequest
    {
        [Required]
        public int RegistrationID { get; set; }

        [Required]
        public int ClientID { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}