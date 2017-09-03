using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddFBOPriceMarginRequest : GetFBODetailsRequest
    {
        //[Required]
        public int Id { get; set; }

        //[Required]
        //public string FBO { get; set; } = String.Empty;

        //[Required]
        public double Margin { get; set; }

        public bool IsOmitted { get; set; }
         
        public bool IsEditable { get; set; }

        public string Note { get; set; }
    }
}