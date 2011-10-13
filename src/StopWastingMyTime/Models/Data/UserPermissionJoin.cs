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
    public partial class UserPermissionJoin
    {
        #region Fields

        public string UserId;
        public string PermissionId;

        #endregion

        #region Constructors

        public UserPermissionJoin()
        {

        }

        private UserPermissionJoin(DataRow data)
        {
            UserId = (string)data["UserId"];
            PermissionId = (string)data["PermissionId"];
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
                command = new SqlCommand("INSERT INTO [" + ConnectionFactory.TableNamePrefix + "UserPermissionJoin] ([UserId], [PermissionId]) VALUES (@UserId, @PermissionId)", connection);
                command.Parameters.Add(new SqlParameter("UserId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(UserId)));
                command.Parameters.Add(new SqlParameter("PermissionId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(PermissionId)));
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
                command = new SqlCommand("UPDATE [" + ConnectionFactory.TableNamePrefix + "UserPermissionJoin] SET [UserId] = @UserId, [PermissionId] = @PermissionId WHERE [UserId] = @UserId, [PermissionId] = @PermissionId", connection);
                command.Parameters.Add(new SqlParameter("UserId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(UserId)));
                command.Parameters.Add(new SqlParameter("PermissionId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(PermissionId)));
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

        public static List<UserPermissionJoin> Select()
        {
            List<UserPermissionJoin> result = new List<UserPermissionJoin>();
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "UserPermissionJoin]", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new UserPermissionJoin(dataRow));
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

        public static UserPermissionJoin SelectById(string userId, string permissionId)
        {
            UserPermissionJoin result = null;
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "UserPermissionJoin] WHERE [UserId] = @UserId, [PermissionId] = @PermissionId", connection);
                    command.Parameters.Add(new SqlParameter("UserId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(userId)));
                    command.Parameters.Add(new SqlParameter("PermissionId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(permissionId)));
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    if (data.Rows.Count > 0)
                        result = new UserPermissionJoin(data.Rows[0]);
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

        public static void Delete(string userId, string permissionId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            
            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand("DELETE FROM [" + ConnectionFactory.TableNamePrefix + "UserPermissionJoin] WHERE [UserId] = @UserId AND [PermissionId] = @PermissionId", connection);
                command.Parameters.Add(new SqlParameter("UserId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(userId)));
                command.Parameters.Add(new SqlParameter("PermissionId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(permissionId)));
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
        
        public static List<UserPermissionJoin> SelectByPermissionId(string permissionId)
        {
            List<UserPermissionJoin> result = new List<UserPermissionJoin>();
        
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "UserPermissionJoin] WHERE ([PermissionId] = @permissionId)", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    command.Parameters.Add(new SqlParameter("PermissionId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(permissionId)));
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new UserPermissionJoin(dataRow));
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

        public static List<UserPermissionJoin> SelectByUserId(string userId)
        {
            List<UserPermissionJoin> result = new List<UserPermissionJoin>();
        
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "UserPermissionJoin] WHERE ([UserId] = @userId)", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    command.Parameters.Add(new SqlParameter("UserId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(userId)));
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new UserPermissionJoin(dataRow));
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
