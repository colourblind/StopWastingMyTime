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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            Models.User user = Models.User.Validate(username, password);
            if (user != null)
            {
                if (String.IsNullOrEmpty(returnUrl) || returnUrl == "/")
                {
                    FormsAuthentication.SetAuthCookie(user.UserId, false);
                    return RedirectToAction("Index", "Timesheets");
                }
                else
                    FormsAuthentication.RedirectFromLoginPage(user.UserId, false);
            }
            
            return View();
        }
    }
}
