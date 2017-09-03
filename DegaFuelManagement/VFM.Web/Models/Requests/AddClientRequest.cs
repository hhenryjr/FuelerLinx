using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VFMClasses;
using Degatech.Common;

namespace VFM.Web.Models.Requests
{
    public class AddClientRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public BaseClass.ClientTypes ClientType { get; set; }
    }
}