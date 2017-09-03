using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class AddFuelOrderCustomerPricingRequest : AddFuelOrderPricingRequest
    {
        public int PriceMarginID { get; set; }
    }
}