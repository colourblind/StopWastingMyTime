﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StopWastingMyTime.Controllers
{
    public class JobsController : Controller
    {
        public ActionResult Index()
        {
            return View(Models.Job.SelectAll());
        }

        public ActionResult List(string term)
        {
            term = term.ToLower();
            return PartialView(Models.Job.SelectAll().Where(x => x.JobId.ToLower().StartsWith(term)));
        }

        public ActionResult Details(string id)
        {
            Models.Job job = new Models.Job(id);
            return View(job);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                Models.Job job = new Models.Job();
                job.JobId = form["jobId"];
                job.Billable = Boolean.Parse(form["billable"]); // TODO: check checkbox behaviour
                job.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(string id)
        {
            return View(new Models.Job(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            try
            {
                Models.Job job = new Models.Job(id);

                if (id != form["JobId"]) // Primary key updated
                {
                    // Cheesey cascade update
                    Models.Job newJob = new Models.Job();
                    UpdateModel(newJob);
                    newJob.Save();

                    foreach (Models.TimeBlock timeBlock in Models.TimeBlock.SelectByJobId(id))
                    {
                        timeBlock.JobId = newJob.JobId;
                        timeBlock.Save();
                    }

                    job.Delete();
                }
                else
                {
                    UpdateModel<Models.Job>(job);
                    job.Save();
                }
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            return View(new Models.Job(id));
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection form)
        {
            try
            {
                Models.Job job = new Models.Job(id);
                job.Delete();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
