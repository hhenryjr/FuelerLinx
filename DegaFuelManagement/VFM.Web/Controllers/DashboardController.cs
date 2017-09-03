﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VFM.Web.Models.ViewModels;
using VFMClasses;

namespace VFM.Web.Controllers
{
    [RoutePrefix("Home")]
    public class DashboardController : Controller
    {
        // GET: Clients
        //[Route]
        //public ActionResult GetListOfClients()
        //{
        //    return View();
        //}

        [Route("{id:int}")]
        public ActionResult Dashboard(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            model.CurrentUser = Users.CurrentUser;
            return View(model);
        }
    }
}