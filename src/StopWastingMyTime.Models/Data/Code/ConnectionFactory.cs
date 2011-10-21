using System;
using System.Configuration;
using System.Data.SqlClient;

namespace StopWastingMyTime.Models.Data
{
    internal class ConnectionFactory
    {
        private static DataConfiguration _config = null;

        private static DataConfiguration Config
        {
            get
            {
                if (_config == null)
                    _config = DataConfiguration.GetConfig();
                return _config;
            }
        }

        public static string TableNamePrefix
        {
            get { return Config.TableNamePrefix; }
        }   

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(Config.ConnectionStrings[Config.DefaultConnection].ConnectionString);
            connection.Open();
            return connection;
        }
    }

    public class DataConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("defaultConnection", IsRequired = false, DefaultValue = "Default")]
        public string DefaultConnection
        {
            get { return this["defaultConnection"].ToString(); }
            set { this["defaultConnection"] = value; }
        }

        [ConfigurationProperty("tableNamePrefix", IsRequired = false, DefaultValue = "")]
        public string TableNamePrefix
        {
            get { return this["tableNamePrefix"].ToString(); }
            set { this["tableNamePrefix"] = value; }
        }

        [ConfigurationProperty("connectionStrings", IsRequired = true)]
        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return (ConnectionStringSettingsCollection)this["connectionStrings"]; }
        }

        public static DataConfiguration GetConfig()
        {
            return (DataConfiguration)ConfigurationManager.GetSection("Data");
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }
}
