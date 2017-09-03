using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddFBODetailVendorExclusionRequest
    {
        [Required]
        public int VendorID { get; set; }

        [Required]
        public int FBOID { get; set; }
    }
}