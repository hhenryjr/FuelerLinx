using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using VFMClasses;

namespace VFM.Web.Models.ViewModels
{
    public class BaseViewModel
    {
        public Users CurrentUser { get; set; }
        public string DateReference { get { return DateTime.Now.ToShortDateString(); } }
    }
}