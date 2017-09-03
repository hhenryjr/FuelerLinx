using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VFM.Web.Models.ViewModels;
using VFMClasses;

namespace VFM.Web.Controllers
{
    [RoutePrefix("Login")]
    public class LoginController : Controller
    {
        // GET: Login
        [Route]
        public ActionResult Login()
        {
            if (Users.CurrentUser.IsActive)
            {
                return RedirectToAction(Users.CurrentUser.Id.ToString(), "Home");
            }
            ItemViewModel<int> model = new ItemViewModel<int>();
            return View("/Views/Login/Login.cshtml", model);
        }
    }
}