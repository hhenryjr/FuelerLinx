using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VFM.Web.Models.ViewModels;
using VFMClasses;

namespace VFM.Web.Controllers
{
    [RoutePrefix("FBODetails")]
    public class FBODetailsController : Controller
    {
        [Route("{icao:alpha}/{fbo:alpha}")]
        public ActionResult GetFBODetails(string icao, string fbo)
        {
            ItemViewModel<FBOPriceMargins> model = new ItemViewModel<FBOPriceMargins>();
            model.Item.ICAO = icao;
            model.Item.FBO = fbo;
            model.CurrentUser = Users.CurrentUser;
            return View(model);
        }
    }
}