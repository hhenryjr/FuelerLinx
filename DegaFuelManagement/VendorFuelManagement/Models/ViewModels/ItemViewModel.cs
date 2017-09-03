using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendorFuelManagement.Models.ViewModels
{
    public class ItemViewModel<Type> : BaseViewModel
    {
        public Type Item { get; set; }
    }
}