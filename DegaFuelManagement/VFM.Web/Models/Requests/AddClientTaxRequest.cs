using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddClientTaxRequest
    {
        [Required]
        public int ClientID { get; set; }

        public string TaxDesc { get; set; }
    }
}