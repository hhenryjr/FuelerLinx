using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class UpdateFuelOrderRequest : AddFuelOrdersRequest
    {
        public int Id { get; set; }

        public Guid AccountId { get; set; }

        public string ProcessName { get; set; }
    }
}