using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StopWastingMyTime.Controllers
{
    public class TimeSheetsController : Controller
    {
        public ActionResult Index()
        {
            return View(Models.TimeBlock.SelectAll());
        }

    }
}
