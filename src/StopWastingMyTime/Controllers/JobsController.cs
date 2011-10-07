using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;

namespace StopWastingMyTime.Controllers
{
    public class JobsController : Controller
    {
        public ActionResult Index()
        {
            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            List<JobViewData> data = new List<JobViewData>();
            Models.Job.SelectAll().ForEach(o => data.Add(new JobViewData(o, urlHelper)));

            // How very . . . Javaesque . . .
            DataContractJsonSerializer serialiser = new DataContractJsonSerializer(typeof(IEnumerable<JobViewData>));
            MemoryStream stream = new MemoryStream();
            serialiser.WriteObject(stream, data);
            ViewData["JobsJson"] = Encoding.UTF8.GetString(stream.ToArray());

            return View(Models.Job.SelectAll());
        }

        public ActionResult List(string term)
        {
            term = term.ToLower();
            return PartialView(Models.Job.SelectAll().Where(x => x.IsActive && x.JobId.ToLower().StartsWith(term)));
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

        [DataContract]
        private class JobViewData
        {
            [DataMember]
            public string JobId;
            [DataMember]
            public string ClientName;
            [DataMember]
            public bool IsActive;
            [DataMember]
            public bool IsBillable;
            [DataMember]
            public decimal? QuotedHours;
            [DataMember]
            public string Description;
            [DataMember]
            public string EditLink;
            [DataMember]
            public string DetailsLink;
            [DataMember]
            public string DeleteLink;

            public JobViewData(Models.Job job, UrlHelper urlHelper)
            {
                JobId = job.JobId;
                ClientName = job.Client.Name;
                IsActive = job.IsActive;
                IsBillable = job.Billable;
                QuotedHours = job.QuotedHours;
                Description = job.Description;

                EditLink = urlHelper.Action("Edit", new { id = job.JobId });
                DetailsLink = urlHelper.Action("Details", new { id = job.JobId });
                DeleteLink = urlHelper.Action("Delete", new { id = job.JobId });
            }
        }
    }
}
