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
    public partial class Client
    {
        #region Fields

        public Guid ClientId;
        public string Name;
        public decimal MaintenancePerMonth;

        #endregion

        #region Constructors

        public Client()
        {

        }

        private Client(DataRow data)
        {
            ClientId = (Guid)data["ClientId"];
            Name = (string)data["Name"];
            MaintenancePerMonth = (decimal)data["MaintenancePerMonth"];
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
                command = new SqlCommand("INSERT INTO [" + ConnectionFactory.TableNamePrefix + "Client] ([ClientId], [Name], [MaintenancePerMonth]) VALUES (@ClientId, @Name, @MaintenancePerMonth)", connection);
                command.Parameters.Add(new SqlParameter("ClientId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(ClientId)));
                command.Parameters.Add(new SqlParameter("Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Name)));
                command.Parameters.Add(new SqlParameter("MaintenancePerMonth", SqlDbType.Decimal, 5, ParameterDirection.Input, false, 6, 2, null, DataRowVersion.Current, DataUtils.HandleNullables(MaintenancePerMonth)));
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
                command = new SqlCommand("UPDATE [" + ConnectionFactory.TableNamePrefix + "Client] SET [ClientId] = @ClientId, [Name] = @Name, [MaintenancePerMonth] = @MaintenancePerMonth WHERE [ClientId] = @ClientId", connection);
                command.Parameters.Add(new SqlParameter("Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(Name)));
                command.Parameters.Add(new SqlParameter("MaintenancePerMonth", SqlDbType.Decimal, 5, ParameterDirection.Input, false, 6, 2, null, DataRowVersion.Current, DataUtils.HandleNullables(MaintenancePerMonth)));
                command.Parameters.Add(new SqlParameter("ClientId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(ClientId)));
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

        public static List<Client> Select()
        {
            List<Client> result = new List<Client>();
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "Client]", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    foreach (DataRow dataRow in data.Rows)
                        result.Add(new Client(dataRow));
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

        public static Client SelectById(Guid clientId)
        {
            Client result = null;
            using (DataTable data = new DataTable())
            {
                SqlConnection connection = null;
                SqlCommand command = null;
                
                try
                {
                    connection = ConnectionFactory.GetConnection();
                    command = new SqlCommand("SELECT * FROM [" + ConnectionFactory.TableNamePrefix + "Client] WHERE [ClientId] = @ClientId", connection);
                    command.Parameters.Add(new SqlParameter("ClientId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(clientId)));
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);

                    if (data.Rows.Count > 0)
                        result = new Client(data.Rows[0]);
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

        public static void Delete(Guid clientId)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            
            try
            {
                connection = ConnectionFactory.GetConnection();
                command = new SqlCommand("DELETE FROM [" + ConnectionFactory.TableNamePrefix + "Client] WHERE [ClientId] = @ClientId", connection);
                command.Parameters.Add(new SqlParameter("ClientId", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DataUtils.HandleNullables(clientId)));
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
