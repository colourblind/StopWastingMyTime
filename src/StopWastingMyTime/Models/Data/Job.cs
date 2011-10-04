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
    public partial class Job
    {
        #region Fields

        public string JobId;
        public Guid ClientId;
        public bool Billable;
        public decimal? QuotedHours;
        public string Description;
        public bool IsActive;

        #endregion

        #region Constructors

        public Job()
        {

        }

        private Job(DataRow data)
        {
            JobId = (string)data["JobId"];
            ClientId = (Guid)data["ClientId"];
            Billable = (bool)data["Billable"];
            QuotedHours = (decimal?)DataUtils.ConvertDbNulls(data["QuotedHours"]);
            Description = (string)DataUtils.ConvertDbNulls(data["Description"]);
            IsActive = (bool)data["IsActive"];
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
                command = new SqlCommand("INSERT INTO [" + ConnectionFactory.TableNamePrefix + "Job] ([JobId], [ClientId], [Billable], [QuotedHours], [Description], [IsActive]) VALUES (@JobId, @ClientId, @Billable, @QuotedHours, @Description, @IsActive)", connection);
                command.Parameters.Add(new SqlParameter("JobId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(JobId)));
                command.Parameters.Add(new SqlParameter("ClientId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(ClientId)));
                command.Parameters.Add(new SqlParameter("Billable", SqlDbType.Bit, 1, ParameterDirection.Input, false, 1, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Billable)));
                command.Parameters.Add(new SqlParameter("QuotedHours", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 6, 2, null, DataRowVersion.Current, DataUtils.HandleNullables(QuotedHours)));
                command.Parameters.Add(new SqlParameter("Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Description)));
                command.Parameters.Add(new SqlParameter("IsActive", SqlDbType.Bit, 1, ParameterDirection.Input, false, 1, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(IsActive)));
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
                command = new SqlCommand("UPDATE [" + ConnectionFactory.TableNamePrefix + "Job] SET [JobId] = @JobId, [ClientId] = @ClientId, [Billable] = @Billable, [QuotedHours] = @QuotedHours, [Description] = @Description, [IsActive] = @IsActive WHERE [JobId] = @JobId", connection);
                command.Parameters.Add(new SqlParameter("ClientId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(ClientId)));
                command.Parameters.Add(new SqlParameter("Billable", SqlDbType.Bit, 1, ParameterDirection.Input, false, 1, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Billable)));
                command.Parameters.Add(new SqlParameter("QuotedHours", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 6, 2, null, DataRowVersion.Current, DataUtils.HandleNullables(QuotedHours)));
                command.Parameters.Add(new SqlParameter("Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Description)));
                command.Parameters.Add(new SqlParameter("IsActive", SqlDbType.Bit, 1, ParameterDirection.Input, false, 1, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(IsActive)));
                command.Parameters.Add(new SqlParameter("JobId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(JobId)));
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

        public static List<Job> Select()
        {
            List<Job> result = new List<Job>();
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "Job]", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new Job(dataRow));
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

        public static Job SelectById(string jobId)
        {
            Job result = null;
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "Job] WHERE [JobId] = @JobId", connection);
                    command.Parameters.Add(new SqlParameter("JobId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(jobId)));
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    if (data.Rows.Count > 0)
                        result = new Job(data.Rows[0]);
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

        public static void Delete(string jobId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            
            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand("DELETE FROM [" + ConnectionFactory.TableNamePrefix + "Job] WHERE [JobId] = @JobId", connection);
                command.Parameters.Add(new SqlParameter("JobId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(jobId)));
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
        
        public static List<Job> SelectByClientId(Guid clientId)
        {
            List<Job> result = new List<Job>();
        
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "Job] WHERE ([ClientId] = @clientId)", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    command.Parameters.Add(new SqlParameter("ClientId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(clientId)));
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new Job(dataRow));
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
