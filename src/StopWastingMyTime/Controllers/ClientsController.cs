using System;
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
            return View(Models.Client.SelectAll());
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
                Models.Client client = new Models.Client();
                client.ClientId = Guid.NewGuid();
                client.Name = form["Name"];
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
