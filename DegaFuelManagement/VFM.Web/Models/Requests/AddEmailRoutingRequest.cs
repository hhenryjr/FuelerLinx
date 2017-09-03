using Degatech.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddEmailRoutingRequest
    {
        [Required]
        public string AdminClientID { get; set; }

        [Required]
        public string EmailType { get; set; }

        [Required]
        public string FromEmail { get; set; }

        [Required]
        public string ToEmail { get; set; }

        public string BCCEmail { get; set; }

        public string Subject { get; set; }
    }
}