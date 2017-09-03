using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VFMClasses;

namespace VFM.Web.Models.Requests
{
    public class AddFuelOrderPricingRequest : FuelOrderPricings
    {
        [Required]
        public int AdminClientID { get; set; }

        [Required]
        public int SupplierID { get; set; }

    }
}