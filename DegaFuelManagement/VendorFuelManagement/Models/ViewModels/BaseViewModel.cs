using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorFuelManagement.Models.ViewModels
{
    public class BaseViewModel
    {
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        //public IdentityUser CurrentUser { get; set; }
        //public UserInfo UserProfile { get; set; }
        public bool HasWikiTreeNavigation { get; set; } // indicates if main nav bar should be by wiki tree
        public string BrandName { get; set; }
        public string BrandTagline { get; set; }
        public string BrandLogo { get; set; }
        public string BrandDescription { get; set; }
    }
}