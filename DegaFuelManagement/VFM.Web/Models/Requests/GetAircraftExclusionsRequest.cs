using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class GetAircraftExclusionsRequest
    {
        [Required]
        public string ICAO { get; set; } = String.Empty;

        [Required]
        public string FBO { get; set; } = String.Empty;

        [Required]
        public int AdminClientID { get; set; }
    }
}