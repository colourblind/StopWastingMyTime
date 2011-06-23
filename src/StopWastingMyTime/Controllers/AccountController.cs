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
        public ActionResult Login(FormCollection form)
        {
            Models.User user = Models.User.Validate(form["username"], form["password"]);
            if (user != null)
                FormsAuthentication.RedirectFromLoginPage(user.UserId, false);
            
            return View();
        }
    }
}
