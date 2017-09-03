using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorFuelManagement.Models.Responses
{
    public class BaseResponse
    {
        public bool IsSuccessFull { get; set; }

        public string TransactionId { get; set; }

        public BaseResponse()
        {
            TransactionId = Guid.NewGuid().ToString();
        }
    }
}