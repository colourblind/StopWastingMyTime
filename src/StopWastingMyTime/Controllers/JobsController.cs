using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            ViewData["ClientList"] = new SelectList(Models.Client.SelectAll().OrderBy(o => o.Name), "ClientId", "Name");
            return View();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ValidateModel(form);

            try
            {
                Models.Job job = new Models.Job();
                UpdateModel(job);

                // Test this here rather than further up so we get the errors from update model even 
                // if the manual validation fails
                if (!ModelState.IsValid)
                    throw new InvalidOperationException("Input failed model validation");

                job.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["ClientList"] = new SelectList(Models.Client.SelectAll(), "ClientId", "Name");
                return View();
            }
        }
        
        public ActionResult Edit(string id)
        {
            Models.Job job = new Models.Job(id);
            ViewData["ClientList"] = new SelectList(Models.Client.SelectAll().OrderBy(o => o.Name), "ClientId", "Name", job.ClientId);
            return View(job);
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            ValidateModel(form);

            try
            {
                Models.Job job = new Models.Job(id);

                if (id != form["JobId"]) // Primary key updated
                {
                    // Cheesey cascade update
                    Models.Job newJob = new Models.Job();
                    UpdateModel(newJob);

                    if (!ModelState.IsValid)
                        throw new InvalidOperationException("Input failed model validation");

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
                    if (!ModelState.IsValid)
                        throw new InvalidOperationException("Input failed model validation");
                    job.Save();
                }
 
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["ClientList"] = new SelectList(Models.Client.SelectAll(), "ClientId", "Name");
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

        private bool ValidateModel(FormCollection form)
        {
            if (String.IsNullOrEmpty(form["JobId"]))
                ModelState.AddModelError("JobId", "Job ID is missing");
            if (String.IsNullOrEmpty(form["ClientId"]))
                ModelState.AddModelError("ClientId", "Client is missing");
            if (!Regex.IsMatch(form["JobId"], "^[0-9A-Za-z_-]+$"))
                ModelState.AddModelError("JobId", "Job ID can only contain letters, numbers, underscores and dashes");

            return ModelState.IsValid;
        }
    }
}
