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
            DateTime from, to;
            DateTime.TryParse(dateFrom, out from);
            DateTime.TryParse(dateTo, out to);
            IEnumerable<Models.TimeBlock> data = Models.TimeBlock.SelectByUserId(User.Identity.Name);
            data = data.Where(x => x.Date >= from && x.Date <= (to == DateTime.MinValue ? DateTime.MaxValue : to));
            return View(data.OrderBy(x => x.Date));
        }

        public ActionResult TimesheetList()
        {
            return PartialView(Models.TimeBlock.SelectByUserId(User.Identity.Name).OrderBy(x => x.Date));
        }

        [HttpPost]
        public ActionResult AddLine(FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock();
            timeBlock.TimeBlockId = Guid.NewGuid();
            timeBlock.UserId = User.Identity.Name;
            timeBlock.JobId = form["workPackage"];
            timeBlock.Date = DateTime.Parse(form["date"]).Date;
            timeBlock.Time = Decimal.Parse(form["hours"]);
            timeBlock.Save();

            return RedirectToAction("TimesheetList");
        }

        [HttpPost]
        public ActionResult EditLine(Guid id, FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock(id);
            if (timeBlock.UserId != User.Identity.Name)
                return View();

            timeBlock.JobId = form["workPackage"];
            timeBlock.Date = DateTime.Parse(form["date"]).Date;
            timeBlock.Time = Decimal.Parse(form["hours"]);
            timeBlock.Save();

            return RedirectToAction("TimesheetList");
        }

        [HttpPost]
        public ActionResult RemoveLine(Guid id, FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock(id);
            if (timeBlock.UserId != User.Identity.Name)
                return View();

            timeBlock.Delete();

            return RedirectToAction("TimesheetList");
        }
    }
}
