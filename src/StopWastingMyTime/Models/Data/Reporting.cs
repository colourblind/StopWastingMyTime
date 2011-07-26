﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace StopWastingMyTime.Models.Data
{
    public class Reporting
    {
        #region Methods

        public static DataTable MaintenanceReport(DateTime date)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataTable result = new DataTable();

            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand(MAINTENANCE_SQL, connection);
                command.Parameters.Add(new SqlParameter("Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 23, 3, null, DataRowVersion.Current, DataUtils.HandleNullables(date)));
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

        private const string MAINTENANCE_SQL =
@"
SELECT
	c.[Name] AS [Client],
	c.[MaintenancePerMonth] AS [Maintenance Per Month],
	COALESCE(SUM(t.[Time]), 0) AS [Hours Used],
	CONVERT(bit, CASE WHEN SUM(t.[Time]) > c.MaintenancePerMonth THEN 1 ELSE 0 END) AS [Overrun]
FROM
	Client c
	LEFT OUTER JOIN Job j ON c.ClientId = j.ClientId
	LEFT OUTER JOIN TimeBlock t ON j.JobId = t.JobId
WHERE
	t.Date IS NULL
	OR
		j.Billable = 0
		AND	t.[Date] >= DATEADD(dd, DATEDIFF(dd, 0, DATEADD(mm, 0, DATEADD(dd, -DAY(getDate()) + 1, getDate()))), 0)
		AND t.[Date] < DATEADD(dd, DATEDIFF(dd, 0, DATEADD(mm, 1, DATEADD(dd, -DAY(getDate()) + 1, getDate()))), 0)
GROUP BY
	c.[Name], c.[MaintenancePerMonth]
ORDER BY
	[Client]
";

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
