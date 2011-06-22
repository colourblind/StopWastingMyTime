﻿using System;
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
            return View(Models.TimeBlock.SelectByUserId(User.Identity.Name));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddLine(FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock();
            timeBlock.TimeBlockId = Guid.NewGuid();
            timeBlock.UserId = User.Identity.Name;
            timeBlock.JobId = form["workPackage"];
            timeBlock.Date = DateTime.Parse(form["date"]).Date;
            timeBlock.Time = Decimal.Parse(form["hours"]);
            timeBlock.Save();

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditLine(FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock(new Guid(form["timeBlockId"]));
            if (timeBlock.UserId != User.Identity.Name)
                return View();

            timeBlock.JobId = form["workPackage"];
            timeBlock.Date = DateTime.Parse(form["date"]).Date;
            timeBlock.Time = Decimal.Parse(form["hours"]);
            timeBlock.Save();

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveLine(FormCollection form)
        {
            Models.TimeBlock timeBlock = new Models.TimeBlock(new Guid(form["timeBlockId"]));
            if (timeBlock.UserId != User.Identity.Name)
                return View();

            timeBlock.Delete();

            return View();
        }
    }
}
