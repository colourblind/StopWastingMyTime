using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StopWastingMyTime.Controllers
{
    public class TimeSheetsController : Controller
    {
        public ActionResult Index(string dateFrom, string dateTo)
        {
            DateTime? from = ParseDate(dateFrom);
            DateTime? to = ParseDate(dateTo);
            from = (from == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Day, 1) : from);
            return View(Models.TimeBlock.SelectByUserAndDateRange(User.Identity.Name, from, to));
        }

        public ActionResult TimesheetList(string dateFrom, string dateTo)
        {
            DateTime? from = ParseDate(dateFrom);
            DateTime? to = ParseDate(dateTo);
            from = (from == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Day, 1) : from);
            return PartialView(Models.TimeBlock.SelectByUserAndDateRange(User.Identity.Name, from, to));
        }

        [HttpPost]
        public ActionResult AddLine(string dateFrom, string dateTo, FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock();
            timeBlock.TimeBlockId = Guid.NewGuid();
            timeBlock.UserId = User.Identity.Name;
            timeBlock.JobId = form["workPackage"];
            timeBlock.Date = DateTime.Parse(form["date"]).Date;
            timeBlock.Time = Decimal.Parse(form["hours"]);
            timeBlock.Save();

            return RedirectToAction("TimesheetList", new { dateFrom = dateFrom, dateTo = dateTo });
        }

        [HttpPost]
        public ActionResult EditLine(Guid id, string dateFrom, string dateTo, FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock(id);
            if (timeBlock.UserId != User.Identity.Name)
                return View();

            timeBlock.JobId = form["workPackage"];
            timeBlock.Date = DateTime.Parse(form["date"]).Date;
            timeBlock.Time = Decimal.Parse(form["hours"]);
            timeBlock.Save();

            return RedirectToAction("TimesheetList", new { dateFrom = dateFrom, dateTo = dateTo });
        }

        [HttpPost]
        public ActionResult RemoveLine(Guid id, string dateFrom, string dateTo, FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock(id);
            if (timeBlock.UserId != User.Identity.Name)
                return View();

            timeBlock.Delete();

            return RedirectToAction("TimesheetList", new { dateFrom = dateFrom, dateTo = dateTo });
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
