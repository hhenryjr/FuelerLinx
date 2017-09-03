using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VFM.Web.Models.Requests
{
    public class AddCustomerDetailAircraftExclusionRequest : AddCustomerDetailVendorExclusionRequest
    {
        [Required]
        public int AdminClientID { get; set; }

    }
}