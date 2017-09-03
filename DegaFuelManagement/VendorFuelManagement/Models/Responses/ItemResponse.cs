using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorFuelManagement.Models.Responses
{
    public class ItemResponse<Type> : SuccessResponse
    {
        public Type Item { get; set; }

        public string Message { get; set; }
    }
}