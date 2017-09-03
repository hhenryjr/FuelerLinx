using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VFM.Web.Models.ViewModels;
using VFMClasses;

namespace VFM.Web.Controllers
{
    [RoutePrefix("FuelOrders")]
    public class FuelOrdersController : Controller
    {
        // GET: FuelOrder
        [Route]
        public ActionResult GetListOfFuelOrders()
        {
            return View();
        }

        [Route("{id:int}")]
        public ActionResult GetFuelOrder(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            model.CurrentUser = Users.CurrentUser;
            return View(model);
        }

    }
}