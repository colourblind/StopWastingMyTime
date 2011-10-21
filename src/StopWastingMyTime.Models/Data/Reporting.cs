using System;
using System.Data;
using System.Data.SqlClient;

namespace StopWastingMyTime.Models.Data
{
    public class Reporting
    {
        #region Methods

        public static DataTable MonthlyReport(DateTime from, DateTime to)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataTable result = new DataTable();

            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand(MONTHLY_SQL, connection);
                command.Parameters.Add(new SqlParameter("From", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 23, 3, null, DataRowVersion.Current, DataUtils.HandleNullables(from)));
                command.Parameters.Add(new SqlParameter("To", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 23, 3, null, DataRowVersion.Current, DataUtils.HandleNullables(to)));
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

        public static DataTable ProblemReport()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataTable result = new DataTable();

            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand(PROBLEM_SQL, connection);
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
    c.[Name] AS [Client],
    j.JobId AS [Job], 
    j.QuotedHours AS [Quoted Hours], 
    j.Billable AS [Billable], 
    SUM(t.[Time]) AS [Total Hours]
FROM
    [TimeBlock] t
    INNER JOIN [Job] j ON t.JobId = j.JobId
    INNER JOIN [Client] c ON j.ClientId = c.ClientId
WHERE
    j.IsActive = 1
    AND t.[Date] >= @From
    AND t.[Date] < @To
GROUP BY
    j.JobId, c.Name, j.QuotedHours, j.Billable
";

        private const string PROBLEM_SQL =
@"
SELECT
    c.[Name] AS [Client],
    j.JobId AS [Job], 
    j.QuotedHours AS [Quoted Hours], 
    j.Billable AS [Billable], 
	SUM(t.[Time]) AS [Total Hours]
FROM
    [TimeBlock] t
    INNER JOIN [Job] j ON t.JobId = j.JobId
    INNER JOIN [Client] c ON j.ClientId = c.ClientId
WHERE
    j.IsActive = 1
GROUP BY
    j.JobId, c.[Name], j.QuotedHours, j.Billable
HAVING
    (CASE WHEN j.QuotedHours IS NOT NULL AND j.QuotedHours > 0 AND SUM(t.[Time]) > j.QuotedHours THEN 1 ELSE 0 END) = 1
";

        #endregion
    }
}
