using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddCustomerPriceMarginRequest
    {
        [Required]
        public int CustClientID { get; set; }

        [Required]
        public int PriceMarginID { get; set; }
    }
}