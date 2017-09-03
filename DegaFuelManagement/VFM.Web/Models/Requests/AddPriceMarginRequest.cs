using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddPriceMarginRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int AdminClientID { get; set; }

        public decimal PrimaryMargin { get; set; }

        public string Note { get; set; }
    }
}