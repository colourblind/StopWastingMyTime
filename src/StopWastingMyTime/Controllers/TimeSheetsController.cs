using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StopWastingMyTime.Controllers
{
    public class TimeSheetsController : Controller
    {
        public ActionResult Index(string date)
        {
            if (String.IsNullOrEmpty(date))
                return RedirectToAction("Index", new { date = DateTime.Now.ToString("dd/MM/yy") });
            DateTime d = ParseDate(date) ?? DateTime.Now;
            d = new DateTime(d.Year, d.Month, d.Day);
            return View(Models.TimeBlock.SelectByUserAndDateRange(User.Identity.Name, d, d.AddDays(1)));
        }

        public ActionResult TimesheetList(string date)
        {
            DateTime d = ParseDate(date) ?? DateTime.Now;
            d = new DateTime(d.Year, d.Month, d.Day);
            return View(Models.TimeBlock.SelectByUserAndDateRange(User.Identity.Name, d, d.AddDays(1)));
        }

        [HttpPost]
        public ActionResult AddLine(string date, FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock();
            timeBlock.TimeBlockId = Guid.NewGuid();
            timeBlock.UserId = User.Identity.Name;
            timeBlock.JobId = form["workPackage"];
            timeBlock.Date = ParseDate(date) ?? DateTime.Now.Date;
            timeBlock.Time = Decimal.Parse(form["hours"]);
            timeBlock.Save();

            return RedirectToAction("TimesheetList", new { date = date });
        }

        [HttpPost]
        public ActionResult EditLine(Guid id, string date, FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock(id);
            if (timeBlock.UserId != User.Identity.Name)
                return View();

            timeBlock.JobId = form["workPackage"];
            timeBlock.Date = ParseDate(date) ?? DateTime.Now.Date;
            timeBlock.Time = Decimal.Parse(form["hours"]);
            timeBlock.Save();

            return RedirectToAction("TimesheetList", new { date = date });
        }

        [HttpPost]
        public ActionResult RemoveLine(Guid id, string date, FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock(id);
            if (timeBlock.UserId != User.Identity.Name)
                return View();

            timeBlock.Delete();

            return RedirectToAction("TimesheetList", new { date = date });
        }

        private DateTime? ParseDate(string date)
        {
            DateTime result;
            if (!DateTime.TryParse(date, out result))
                return null;
            return result;
        }
    }
}
