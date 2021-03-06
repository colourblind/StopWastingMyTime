/*
 * Business Object to ADO
 * Data.cst
 * Template version: 0.5.0.4
 */
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StopWastingMyTime.Models.Data
{
    public partial class TimeBlock
    {
        #region Fields

        public Guid TimeBlockId;
        public string UserId;
        public string JobId;
        public DateTime Date;
        public decimal Time;
        public string Comment;

        #endregion

        #region Constructors

        public TimeBlock()
        {

        }

        private TimeBlock(DataRow data)
        {
            TimeBlockId = (Guid)data["TimeBlockId"];
            UserId = (string)data["UserId"];
            JobId = (string)data["JobId"];
            Date = (DateTime)data["Date"];
            Time = (decimal)data["Time"];
            Comment = (string)DataUtils.ConvertDbNulls(data["Comment"]);
        }

        #endregion

        #region Insert

        public void Insert()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            
            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand("INSERT INTO [" + ConnectionFactory.TableNamePrefix + "TimeBlock] ([TimeBlockId], [UserId], [JobId], [Date], [Time], [Comment]) VALUES (@TimeBlockId, @UserId, @JobId, @Date, @Time, @Comment)", connection);
                command.Parameters.Add(new SqlParameter("TimeBlockId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(TimeBlockId)));
                command.Parameters.Add(new SqlParameter("UserId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(UserId)));
                command.Parameters.Add(new SqlParameter("JobId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(JobId)));
                command.Parameters.Add(new SqlParameter("Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 23, 3, null, DataRowVersion.Current, DataUtils.HandleNullables(Date)));
                command.Parameters.Add(new SqlParameter("Time", SqlDbType.Decimal, 5, ParameterDirection.Input, false, 6, 2, null, DataRowVersion.Current, DataUtils.HandleNullables(Time)));
                command.Parameters.Add(new SqlParameter("Comment", SqlDbType.NVarChar, 2000, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Comment)));
                command.ExecuteNonQuery();
                
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
        }

        #endregion

        #region Update

        public void Update()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            
            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand("UPDATE [" + ConnectionFactory.TableNamePrefix + "TimeBlock] SET [TimeBlockId] = @TimeBlockId, [UserId] = @UserId, [JobId] = @JobId, [Date] = @Date, [Time] = @Time, [Comment] = @Comment WHERE [TimeBlockId] = @TimeBlockId", connection);
                command.Parameters.Add(new SqlParameter("UserId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(UserId)));
                command.Parameters.Add(new SqlParameter("JobId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(JobId)));
                command.Parameters.Add(new SqlParameter("Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 23, 3, null, DataRowVersion.Current, DataUtils.HandleNullables(Date)));
                command.Parameters.Add(new SqlParameter("Time", SqlDbType.Decimal, 5, ParameterDirection.Input, false, 6, 2, null, DataRowVersion.Current, DataUtils.HandleNullables(Time)));
                command.Parameters.Add(new SqlParameter("Comment", SqlDbType.NVarChar, 2000, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Comment)));
                command.Parameters.Add(new SqlParameter("TimeBlockId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(TimeBlockId)));
                command.ExecuteNonQuery();
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
        }

        #endregion

        #region Select

        public static List<TimeBlock> Select()
        {
            List<TimeBlock> result = new List<TimeBlock>();
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "TimeBlock]", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new TimeBlock(dataRow));
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
            }
            return result;
        }

        public static TimeBlock SelectById(Guid timeBlockId)
        {
            TimeBlock result = null;
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "TimeBlock] WHERE [TimeBlockId] = @TimeBlockId", connection);
                    command.Parameters.Add(new SqlParameter("TimeBlockId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(timeBlockId)));
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    if (data.Rows.Count > 0)
                        result = new TimeBlock(data.Rows[0]);
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
            }
            
            return result;
        }

        #endregion

        #region Delete

        public static void Delete(Guid timeBlockId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            
            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand("DELETE FROM [" + ConnectionFactory.TableNamePrefix + "TimeBlock] WHERE [TimeBlockId] = @TimeBlockId", connection);
                command.Parameters.Add(new SqlParameter("TimeBlockId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(timeBlockId)));
                command.ExecuteNonQuery();
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
        }

        #endregion
        
        #region Foreign Key Selects
        
        public static List<TimeBlock> SelectByJobId(string jobId)
        {
            List<TimeBlock> result = new List<TimeBlock>();
        
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "TimeBlock] WHERE ([JobId] = @jobId)", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    command.Parameters.Add(new SqlParameter("JobId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(jobId)));
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new TimeBlock(dataRow));
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
            }

            return result;
        }

        public static List<TimeBlock> SelectByUserId(string userId)
        {
            List<TimeBlock> result = new List<TimeBlock>();
        
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "TimeBlock] WHERE ([UserId] = @userId)", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    command.Parameters.Add(new SqlParameter("UserId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(userId)));
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new TimeBlock(dataRow));
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
            }

            return result;
        }

        #endregion
    }
}
