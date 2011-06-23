using System;
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

        public ActionResult Details(string jobId)
        {
            Models.Job job = new Models.Job(jobId);
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
        
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string jobId, FormCollection form)
        {
            try
            {
                Models.Job job = new Models.Job(jobId);
                job.Billable = Boolean.Parse(form["billable"]); // TODO: check checkbox behaviour
                job.Save();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(string jobId, FormCollection form)
        {
            try
            {
                Models.Job job = new Models.Job(jobId);
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
