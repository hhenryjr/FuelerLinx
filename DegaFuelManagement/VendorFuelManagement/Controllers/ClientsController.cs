using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendorFuelManagement.Controllers
{
    [RoutePrefix("Clients")]
    public class ClientsController : Controller
    {
        // GET: Clients
        [Route]
        public ActionResult GetListOfClients()
        {
            return View();
        }

        [Route("{id:int}")]
        public ActionResult GetClientInfo()
        {
            return View();
        }
    }
}