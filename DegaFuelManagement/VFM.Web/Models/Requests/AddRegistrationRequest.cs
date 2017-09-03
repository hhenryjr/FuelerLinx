using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddRegistrationRequest
    {
        //[Required]
        public string FirstName { get; set; }

        //[Required]
        public string LastName { get; set; }

        public string Company { get; set; }

        public string Username { get; set; }

        //[Required]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}