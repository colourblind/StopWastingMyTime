using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace StopWastingMyTime.Controllers
{
    public class ReportingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ByUser(string fromDate, string toDate)
        {
            DateTime from = DateTime.Parse(fromDate);
            DateTime to = DateTime.Parse(toDate);

            Stream stream = new MemoryStream();
            Models.Report report = new Models.Report(from, to.AddDays(1));
            report.Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            FileStreamResult result = new FileStreamResult(stream, "application/excel");
            result.FileDownloadName = String.Format("timesheet_{0}_{1}.xls", from.ToString("yyyyMMdd"), to.ToString("yyyyMMdd"));
            return result;
        }
    }
}
