using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace StopWastingMyTime.Models.Data
{
    class DataUtils
    {
        public static bool IsNullable(object o)
        {
            if (o == null)
                return true;
            Type t = o.GetType();
            return t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        public static object HandleNullables(object o)
        {
            if (o == null)
                return DBNull.Value;
            else
                return o;
        }

        public static object ConvertDbNulls(object o)
        {
            if (o is DBNull)
                return null;
            else
                return o;
        }

        public static DataTable RunAdhocQuery(SqlCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");
        
            DataTable data = new DataTable();
            try
            {
                if (command.Connection == null)
                    command.Connection = ConnectionFactory.GetConnection();
                else
                    command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }
            catch (Exception e)
            {
                DataUtils.AddDataToException(ref e, command);
                throw;
            }
            finally
            {
                if (command.Connection != null)
                    command.Connection.Close();
                command.Dispose();
            }
            return data;
        }

        public static void RunAdhocNonQuery(SqlCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");
                
            try
            {
                if (command.Connection == null)
                    command.Connection = ConnectionFactory.GetConnection();
                else
                    command.Connection.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                DataUtils.AddDataToException(ref e, command);
                throw;
            }
            finally
            {
                if (command.Connection != null)
                    command.Connection.Close();
                command.Dispose();
            }
        }
        
        public static Exception AddDataToException(ref Exception e, SqlCommand command)
        {
            if (command != null)
            {
                e.Data.Add("Command", command.CommandText);
                string paramList = "\r\n";
                foreach (SqlParameter param in command.Parameters)
                    paramList += String.Format("\t{0} = {1}\r\n", param.ParameterName, param.Value);
                e.Data.Add("Parameters", paramList);
            }
            return e;
        }
    }
}
