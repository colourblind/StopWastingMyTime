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
            DateTime now = DateTime.Now;
            DateTime from = new DateTime(now.Year, now.Month, 1);
            DateTime to = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            return Index(from.ToString(), to.ToString());
        }

        [HttpPost]
        public ActionResult Index(string fromDate, string toDate)
        {
            DateTime from = DateTime.Parse(fromDate);
            DateTime to = DateTime.Parse(toDate).AddDays(1);

            ViewData["MonthlyReport"] = Models.Data.Reporting.MonthlyReport(from, to);
            ViewData["ProblemReport"] = Models.Data.Reporting.ProblemReport();
            return View();
        }
    }
}
