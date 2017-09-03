using Degatech.Utilities.SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VFM.Web.Models.ViewModels;
using VFMClasses;

namespace VFM.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("/Views/LandingPages/" + Clients.GetClientName().Replace(" ", "") + ".cshtml");
        }

        public ActionResult Login()
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            return View("/Views/Login/Login.cshtml", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}