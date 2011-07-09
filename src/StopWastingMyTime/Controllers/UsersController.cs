using System;
using System.Web.Mvc;
using Colourblind.Core;

namespace StopWastingMyTime.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View(Models.User.SelectAll());
        }

        public ActionResult Details(string id)
        {
            return View(new Models.User(id));
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
                if (form["Password"] != form["ConfirmPassword"])
                    ModelState.AddModelError("Password", "The supplied passwords do not match");
                if (form["Password"].Length < 6)
                    ModelState.AddModelError("Password", "Your password must be at least 6 characters");

                Models.User user = new Models.User();
                UpdateModel(user);

                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Input failed model validation");

                user.Password = Security.GenerateHash(form["Password"]);
                user.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(string id)
        {
            return View(new Models.User(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            try
            {
                if (form["Password"] != form["ConfirmPassword"])
                    ModelState.AddModelError("Password", "The supplied passwords do not match");
                if (form["Password"].Length < 6)
                    ModelState.AddModelError("Password", "Your password must be at least 6 characters");

                Models.User user = new Models.User(id);
                UpdateModel(user);

                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Input failed model validation");

                user.Password = Security.GenerateHash(form["Password"]);
                user.Save();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Models.User(id));
            }
        }

        public ActionResult Delete(string id)
        {
            return View(new Models.User(id));
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                Models.User user = new Models.User(id);
                user.Delete();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Models.User(id));
            }
        }
    }
}
