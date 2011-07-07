using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace StopWastingMyTime.Models
{
    public class Report
    {
        public DateTime FromDate
        {
            get;
            set;
        }

        public DateTime ToDate
        {
            get;
            set;
        }

        public Report(DateTime from, DateTime to)
        {
            FromDate = from;
            ToDate = to;
        }
        
        public void Write(Stream outStream)
        {
            Workbook workbook = new HSSFWorkbook();

            IEnumerable<User> users = User.SelectAll();
            foreach (User user in users)
            {
                IEnumerable<TimeBlock> timeBlocks = TimeBlock.SelectByUserAndDateRange(user.UserId, FromDate, ToDate);
                if (timeBlocks.Count() > 0)
                {
                    Sheet sheet = workbook.CreateSheet(user.Name);
                    Row header = sheet.CreateRow(0);
                    header.CreateCell(0).SetCellValue("Resource");
                    header.CreateCell(1).SetCellValue("Date");
                    header.CreateCell(2).SetCellValue("Client");
                    header.CreateCell(3).SetCellValue("Work Package");
                    header.CreateCell(4).SetCellValue("Hours");

                    int i = 1;
                    foreach (TimeBlock timeBlock in timeBlocks)
                    {
                        Row row = sheet.CreateRow(i ++);
                        row.CreateCell(0).SetCellValue(timeBlock.User.Name);
                        row.CreateCell(1).SetCellValue(timeBlock.Date.ToString("dd/MM/yy"));
                        row.CreateCell(2).SetCellValue(timeBlock.Job.Client.Name);
                        row.CreateCell(3).SetCellValue(timeBlock.JobId);
                        row.CreateCell(4).SetCellValue(Convert.ToDouble(timeBlock.Time));
                    }
                }
            }

            workbook.Write(outStream);
        }
    }
}
