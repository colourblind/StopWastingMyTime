using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StopWastingMyTime.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            Models.User user = Models.User.Validate(username, password);
            if (user != null)
                FormsAuthentication.RedirectFromLoginPage(user.UserId, false);
            else
                ModelState.AddModelError("Login", "Please check your details and try again");
            
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(string password, string confirmPassword)
        {
            if (password != confirmPassword)
                ModelState.AddModelError("Password", "The supplied passwords do not match");
            if (password.Length < 6)
                ModelState.AddModelError("Password", "Your password must be at least 6 characters");

            if (ModelState.IsValid)
            {
                Models.User user = new Models.User(User.Identity.Name);
                user.Password = Colourblind.Core.Security.GenerateHash(password);
                user.Save();
            }

            return View();
        }
    }
}
