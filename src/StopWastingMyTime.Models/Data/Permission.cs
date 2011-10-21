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
    public partial class Permission
    {
        #region Fields

        public string PermissionId;
        public string Description;

        #endregion

        #region Constructors

        public Permission()
        {

        }

        private Permission(DataRow data)
        {
            PermissionId = (string)data["PermissionId"];
            Description = (string)DataUtils.ConvertDbNulls(data["Description"]);
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
                command = new SqlCommand("INSERT INTO [" + ConnectionFactory.TableNamePrefix + "Permission] ([PermissionId], [Description]) VALUES (@PermissionId, @Description)", connection);
                command.Parameters.Add(new SqlParameter("PermissionId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(PermissionId)));
                command.Parameters.Add(new SqlParameter("Description", SqlDbType.NVarChar, 1000, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Description)));
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
                command = new SqlCommand("UPDATE [" + ConnectionFactory.TableNamePrefix + "Permission] SET [PermissionId] = @PermissionId, [Description] = @Description WHERE [PermissionId] = @PermissionId", connection);
                command.Parameters.Add(new SqlParameter("Description", SqlDbType.NVarChar, 1000, ParameterDirection.Input, true, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Description)));
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

        public static List<Permission> Select()
        {
            List<Permission> result = new List<Permission>();
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "Permission]", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new Permission(dataRow));
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

        public static Permission SelectById(string permissionId)
        {
            Permission result = null;
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "Permission] WHERE [PermissionId] = @PermissionId", connection);
                    command.Parameters.Add(new SqlParameter("PermissionId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(permissionId)));
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    if (data.Rows.Count > 0)
                        result = new Permission(data.Rows[0]);
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

        public static void Delete(string permissionId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            
            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand("DELETE FROM [" + ConnectionFactory.TableNamePrefix + "Permission] WHERE [PermissionId] = @PermissionId", connection);
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
    }
}
