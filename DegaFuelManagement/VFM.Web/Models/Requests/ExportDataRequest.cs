using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Requests
{
    public class ExportDataRequest : GetFuelOrdersRequest
    { 
        [Required]
        public string ClientName { get; set; }

        [Required]
        public string ListOfIDs { get; set; }
    }
}