using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VFM.Web.Models.ViewModels;
using VFMClasses;

namespace VFM.Web.Controllers
{
    [RoutePrefix("Contacts")]
    public class ContactsController : Controller
    {
        // GET: Contacts
        [Route]
        public ActionResult GetListOfContacts()
        {
            return View();
        }

        [Route("{id:int}")]
        public ActionResult GetContactInfo(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            model.CurrentUser = Users.CurrentUser;
            return View(model);
        }
    }
}