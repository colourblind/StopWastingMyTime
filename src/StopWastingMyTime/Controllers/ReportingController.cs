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
        public ActionResult Index(string fromDate, string toDate)
        {
            DateTime from = DateTime.Parse(fromDate);
            DateTime to = DateTime.Parse(toDate).AddDays(1);

            Stream stream = new MemoryStream();
            Models.Report report = new Models.Report(from, to);
            report.Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            FileStreamResult result = new FileStreamResult(stream, "application/excel");
            result.FileDownloadName = "timesheet.xls";
            return result;
        }
    }
}
