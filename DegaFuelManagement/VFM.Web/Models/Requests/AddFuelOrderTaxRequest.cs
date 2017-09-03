using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddFuelOrderTaxRequest
    {
        [Required]
        public int FuelOrderID { get; set; }
        
        public string TaxDesc { get; set; }
        public double TaxGal { get; set; }
        public double TaxAmt { get; set; }
        public bool OmitPPG { get; set; }
        public bool IsCustomizable { get; set; }
    }
}