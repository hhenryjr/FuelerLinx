using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddFuelOrderFeeRequest
    {
        [Required]
        public int FuelOrderID { get; set; } 
        public string FeeDesc { get; set; } 
        public double FeeAmount { get; set; } 
        public string FeeNameAsKey { get; set; }
        public bool IsStored { get; set; }
    }
}