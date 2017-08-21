using BpmContactManager.Models;
using BpmContactManager.Models.Entities;
using BpmContactManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BpmContactManager.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Contacts(int count, int offset)
        {
            var contactServiceManager = new ContactServiceManager();
            var contactEntityList = contactServiceManager.GetContacts(count, offset);
            var contactViewModelList = new List<ContactViewModel>();

            foreach (var contactEntity in contactEntityList)
            {
                contactViewModelList.Add(contactEntity.ToViewModel());
            }
            return Json(contactViewModelList, JsonRequestBehavior.AllowGet);
        }
    }
}