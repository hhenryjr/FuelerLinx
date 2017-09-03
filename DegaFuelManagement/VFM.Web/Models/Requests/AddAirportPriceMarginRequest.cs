using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddAirportPriceMarginRequest
    {
        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public string ICAO { get; set; } = String.Empty;
        
        //public float? Margin { get; set; }
        
        public bool IsInactive { get; set; }
        
        //public bool IsDisabled { get; set; }
    }
}