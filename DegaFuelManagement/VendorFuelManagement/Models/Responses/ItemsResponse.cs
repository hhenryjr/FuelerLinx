using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorFuelManagement.Models.Responses
{
    public class ItemsResponse<Type> : SuccessResponse
    {
        public List<Type> Items { get; set; }
    }
}