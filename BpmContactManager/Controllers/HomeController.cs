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
        ContactServiceManager contactServiceManager;

        public HomeController()
        {
            contactServiceManager = new ContactServiceManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Contacts(int count, int offset)
        {
            try
            {
                var contactEntityList = contactServiceManager.GetContacts(count, offset);


                var contactViewModelList = new List<ContactViewModel>();

                foreach (var contactEntity in contactEntityList)
                {
                    contactViewModelList.Add(contactEntity.ToViewModel());
                }
                return Json(contactViewModelList, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContactViewModel contactViewModel)
        {
            try
            {
                if(contactServiceManager.AddContact(contactViewModel.ToEntity()))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                var contactToDelete = contactServiceManager.CetContactById(id);

                contactServiceManager.RemoveContact(contactToDelete);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                var contactViewModel = contactServiceManager.CetContactById(id).ToViewModel();
                return View(contactViewModel);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(ContactViewModel contactViewModel)
        {
            try
            {
                var entity = contactViewModel.ToEntity();
                contactServiceManager.ModifyContact(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}