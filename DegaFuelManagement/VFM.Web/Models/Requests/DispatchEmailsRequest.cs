using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class DispatchEmailsRequest
    {
        [Required]
        public int  fuelOrderId { get; set; }
    }
}