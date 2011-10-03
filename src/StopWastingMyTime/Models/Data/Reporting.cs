using System;
using System.Data;
using System.Data.SqlClient;

namespace StopWastingMyTime.Models.Data
{
    public class Reporting
    {
        #region Methods

        public static DataTable MonthlyReport()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataTable result = new DataTable();

            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand(MONTHLY_SQL, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(result);
            }
            catch (Exception e)
            {
                DataUtils.AddDataToException(ref e, command);
                throw;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return result;
        }

        #endregion

        #region Sql

        private const string MONTHLY_SQL =
@"
SELECT
    j.JobId AS [Job], 
    j.QuotedHours AS [Quoted Hours], 
    j.Billable AS [Billable], SUM(t.[Time]) AS [Total Hours], 
    CONVERT(bit, CASE WHEN j.QuotedHours IS NOT NULL AND SUM(t.[Time]) > j.QuotedHours THEN 1 ELSE 0 END) AS [Overrun]
FROM
    [TimeBlock] t
    INNER JOIN [Job] j ON t.JobId = j.JobId
WHERE
    j.QuotedHours IS NOT NULL
    AND j.QuotedHours > 0
GROUP BY
    j.JobId, j.QuotedHours, j.Billable
";

        #endregion
    }
}
