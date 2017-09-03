using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddPriceMarginTierRequest
    {
        [Required]
        public int PriceMarginID { get; set; }

        [Required]
        public short MinGallon { get; set; }

        [Required]
        public double MaxGallon { get; set; }

        //[Required]
        public decimal Margin { get; set; }
    }
}