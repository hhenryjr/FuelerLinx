using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VFM.Web.Models.ViewModels;
using VFMClasses;

namespace VFM.Web.Controllers
{
    [RoutePrefix("PriceMargins")]
    public class PriceMarginsController : Controller
    {
        // GET: PriceMargins
        [Route]
        public ActionResult GetListOfPriceMargins()
        {
            return View();
        }

        [Route("{id:int}")]
        public ActionResult GetPriceMargin(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            model.CurrentUser = Users.CurrentUser;
            return View(model);
        }
    }
}