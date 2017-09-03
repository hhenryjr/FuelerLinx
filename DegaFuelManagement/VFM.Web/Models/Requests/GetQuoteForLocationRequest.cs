using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VFMClasses;

namespace VFM.Web.Models.Requests
{
    public class GetQuoteForLocationRequest
    {
        [Required]
        public virtual int AdminClientID { get; set; }
        
        [Required]
        public virtual int CustClientID { get; set; }

        [Required]
        public string ICAO { get; set; }

        [Required]
        public virtual string TailNumber { get; set; }
    }
}