using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StopWastingMyTime.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["MaintenanceReport"] = Models.Data.Reporting.MaintenanceReport(DateTime.Now);
            ViewData["MonthlyReport"] = Models.Data.Reporting.MonthlyReport();
            return View();
        }
    }
}
