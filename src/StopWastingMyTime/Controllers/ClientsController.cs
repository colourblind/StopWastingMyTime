﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StopWastingMyTime.Controllers
{
    public class ClientsController : Controller
    {
        public ActionResult Index()
        {
            return View(Models.Client.SelectAll().OrderBy(o => o.Name));
        }

        public ActionResult Details(string id)
        {
            return View(new Models.Client(new Guid(id)));
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                if (String.IsNullOrEmpty(form["Name"]))
                    ModelState.AddModelError("Name", "The Name field is required");

                Models.Client client = new Models.Client();
                UpdateModel(client);

                // Test this here rather than further up so we get the errors from update model even 
                // if the manual validation fails
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Input failed model validation");

                client.ClientId = Guid.NewGuid();
                client.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(string id)
        {
            return View(new Models.Client(new Guid(id)));
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            try
            {
                Models.Client client = new Models.Client(new Guid(id));
                UpdateModel(client);
                client.Save();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            return View(new Models.Client(new Guid(id)));
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                Models.Client client = new Models.Client(new Guid(id));
                client.Delete();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
