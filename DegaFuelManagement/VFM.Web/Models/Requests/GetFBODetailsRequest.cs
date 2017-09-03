using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class GetFBODetailsRequest
    {
        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public string ICAO { get; set; }

        [Required]
        public string FBO { get; set; }
    }
}