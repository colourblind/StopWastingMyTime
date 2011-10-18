using System;
using System.Linq;
using System.Web.Mvc;
using Colourblind.Core;

namespace StopWastingMyTime.Controllers
{
    public class UsersController : Controller
    {
        [PermissionsRequired("USER_ADMIN")]
        public ActionResult Index()
        {
            return View(Models.User.SelectAll());
        }

        [PermissionsRequired("USER_ADMIN")]
        public ActionResult Details(string id)
        {
            return View(new Models.User(id));
        }

        [PermissionsRequired("USER_ADMIN")]
        public ActionResult Create()
        {
            ViewData["PermissionList"] = Models.Permission.SelectAll().Select(o => o.PermissionId);
            return View();
        } 

        [HttpPost]
        [PermissionsRequired("USER_ADMIN")]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                if (form["Password"] != form["ConfirmPassword"])
                    ModelState.AddModelError("", "The supplied passwords do not match");
                if (form["Password"].Length < 6)
                    ModelState.AddModelError("Password", "Your password must be at least 6 characters");

                Models.User user = new Models.User();
                UpdateModel(user);

                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Input failed model validation");

                user.Save();

                foreach (string permission in form["permission"].Split(','))
                    user.AddPermission(permission);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["PermissionList"] = Models.Permission.SelectAll().Select(o => o.PermissionId);
                return View();
            }
        }

        [PermissionsRequired("USER_ADMIN")]
        public ActionResult Edit(string id)
        {
            ViewData["PermissionList"] = Models.Permission.SelectAll().Select(o => o.PermissionId);
            return View(new Models.User(id));
        }

        [HttpPost]
        [PermissionsRequired("USER_ADMIN")]
        public ActionResult Edit(string id, FormCollection form)
        {
            try
            {
                // If the user leaves the password field blank, turn off the validation
                // and let the model ignore the change
                if (form["Password"].Length > 0)
                {
                    if (form["Password"] != form["ConfirmPassword"])
                        ModelState.AddModelError("", "The supplied passwords do not match");
                    if (form["Password"].Length < 6)
                        ModelState.AddModelError("Password", "Your password must be at least 6 characters");
                }

                Models.User user = new Models.User(id);
                UpdateModel(user);

                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Input failed model validation");

                user.Save();

                user.ClearPermissions();
                foreach (string permission in form["permission"].Split(','))
                    user.AddPermission(permission);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["PermissionList"] = Models.Permission.SelectAll().Select(o => o.PermissionId);
                return View(new Models.User(id));
            }
        }

        [PermissionsRequired("USER_ADMIN")]
        public ActionResult Delete(string id)
        {
            return View(new Models.User(id));
        }

        [HttpPost]
        [PermissionsRequired("USER_ADMIN")]
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
