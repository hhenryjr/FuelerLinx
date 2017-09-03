using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddSupplierFuelsPriceRequest
    {
        [Required]
        public string Vendor { get; set; }

        [Required]
        public string IATA { get; set; }

        [Required]
        public string ICAO { get; set; }

        [Required]
        public string FBOName { get; set; }

        [Required]
        public int Min { get; set; }

        [Required]
        public int Max { get; set; }

        [Required]
        public decimal TotalWithTax { get; set; }

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        public string Product { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public int SupplierID { get; set; }
    }
}