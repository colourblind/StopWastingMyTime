using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Colourblind.Core
{
    public class Log : Singleton<Log>
    {
        #region Fields

        private LoggingConfiguration _config = null;

        #endregion

        #region Constructors

        public Log()
        {
            _config = LoggingConfiguration.GetConfig();
        }

        #endregion

        #region Methods

        public void Write(string message)
        {
            Write(_config.DefaultLogName, message);
        }

		public void Write(string logName, string message)
		{
            try
            {
                if (_config.Active)
                {
                    if (_config.Logs[logName] != null && _config.Logs[logName].Active)
                    {
                        if (_config.Logs[logName].EventLog.ElementInformation.IsPresent)
                            WriteEventLog(message);
                        if (_config.Logs[logName].File.ElementInformation.IsPresent)
                            WriteFile(message, _config.Logs[logName].File);
                        if (_config.Logs[logName].Email.ElementInformation.IsPresent)
                            WriteEmail(message, _config.Logs[logName].Email);
                    }
                }
            }
            catch (Exception)
            {
                // OHSHI-
            }
		}

		public void Write(Exception e)
		{
			Write(_config.DefaultLogName, e);
		}

        public void Write(string logName, Exception e)
        {
			Write(logName, FormatException(e));
        }

        private void WriteEventLog(string message)
        {
            // This won't work by default for web apps because the ASPNET user doesn't have 
            // the required rights.

            if (!EventLog.SourceExists(_config.Application))
                EventLog.CreateEventSource(_config.Application, "Application");

            EventLog eventLog = new EventLog();
            eventLog.Log = "Application";
            eventLog.Source = _config.Application;
            eventLog.WriteEntry(message, EventLogEntryType.Warning);
        }

        private void WriteFile(string message, FileLogConfigurationElement config)
        {
            string path;
            string logNum = String.Format("_{0}{1}{2}", DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day.ToString().PadLeft(2, '0'));

            if (config.Path.LastIndexOf(".") == -1)
                path = config.Path + logNum;
            else
                path = config.Path.Insert(config.Path.LastIndexOf("."), logNum);

            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(String.Format("{0} - {1}", DateTime.Now, message));
            writer.Flush();
            writer.Close();
        }

        private void WriteEmail(string message, EmailLogConfigurationElement config)
        {
            Email email = new Email();
            email.From = config.From;
            foreach (string recipient in config.Recipients.Split(new char[] {',', ';'}))
                email.Recipients.Add(recipient);
            email.Subject = String.Format("{0}: {1}", _config.Application, message.Substring(0, message.IndexOfAny(new char[] { ' ', '\r', '\n' })));
            email.Body = String.Format("Error Email generated at {0}\r\n", DateTime.Now);
            email.Body += message;
            email.IsHtml = false;
            email.Send();
        }

		private string FormatException(Exception e)
		{
			string result = String.Empty;

			if (e.InnerException != null)
				result += FormatException(e.InnerException);

			string data = String.Empty;
			if (e.Data.Count > 0)
			{
				foreach (DictionaryEntry entry in e.Data)
					data += String.Format("{0}: {1}\r\n", entry.Key.ToString(), entry.Value.ToString());
			}
			result += String.Format("{0}\r\n{1}\r\n{2}\r\n{3}", e.GetType().ToString(), e.Message, e.StackTrace, data);

			return result;
		}

        #endregion
	}

	#region Config Handler

	public class LoggingConfiguration : ConfigurationSection
	{
		[ConfigurationProperty("active", DefaultValue = true, IsRequired = false)]
		public bool Active
		{
			get { return (bool)this["active"]; }
		}

        [ConfigurationProperty("application", IsRequired = true)]
        public string Application
        {
            get { return this["application"].ToString(); }
        }

		[ConfigurationProperty("defaultLogName", IsRequired = true)]
		public string DefaultLogName
		{
			get { return this["defaultLogName"].ToString(); }
		}

		[ConfigurationProperty("Logs", IsRequired = true)]
		public LogConfigurationElementCollection Logs
		{
			get { return (LogConfigurationElementCollection)this["Logs"]; }
		}

		public static LoggingConfiguration GetConfig()
		{
			return (LoggingConfiguration)ConfigurationManager.GetSection("Logging");
		}
    }

    #region <Logging> <Logs>

    public class LogConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return this["name"].ToString(); }
		}

		[ConfigurationProperty("active", DefaultValue = true, IsRequired = false)]
		public bool Active
		{
			get { return (bool)this["active"]; }
		}

        [ConfigurationProperty("eventLog", IsRequired = false)]
        public EventLogConfigurationElement EventLog
        {
            get { return (EventLogConfigurationElement)this["eventLog"]; }
        }

        [ConfigurationProperty("file", IsRequired = false)]
        public FileLogConfigurationElement File
        {
            get { return (FileLogConfigurationElement)this["file"]; }
        }
        
        [ConfigurationProperty("email", IsRequired = false)]
        public EmailLogConfigurationElement Email
        {
            get { return (EmailLogConfigurationElement)this["email"]; }
        }
	}

	[ConfigurationCollection(typeof(LogConfigurationElement), AddItemName = "Log", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class LogConfigurationElementCollection : ConfigurationElementCollection
	{
		public LogConfigurationElement this[int index]
		{
			get
			{
				return (LogConfigurationElement)BaseGet(index);
			}
			set
			{
				if (BaseGet(index) != null)
					BaseRemoveAt(index);
				BaseAdd(index, value);
			}
		}

        public LogConfigurationElement this[string index]
        {
            get
            {
                return (LogConfigurationElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemove(index);
                BaseAdd(value);
            }
        }

		#region Overrides

		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.BasicMap; }
		}

		protected override string ElementName
		{
			get { return "Log"; }
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new LogConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((LogConfigurationElement)element).Name;
		}

		#endregion
    }

    #endregion

    #region Logging Targets

    public class EventLogConfigurationElement : ConfigurationElement
    {

    }

    public class FileLogConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true)]
        public string Path
        {
            get { return this["path"].ToString(); }
        }
    }

    public class EmailLogConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("from", IsRequired = true)]
        public string From
        {
            get { return this["from"].ToString(); }
        }
        
        [ConfigurationProperty("recipients", IsRequired = true)]
        public string Recipients
        {
            get { return this["recipients"].ToString(); }
        }
    }
    
    #endregion

    #endregion
}
