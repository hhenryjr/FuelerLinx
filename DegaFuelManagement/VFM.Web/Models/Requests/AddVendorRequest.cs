using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddVendorRequest
    {
        [Required]
        public string Name { get; set; }

        public bool IsDisabled { get; set; }
        public double Margin { get; set; }
        public string Note { get; set; }
    }
}