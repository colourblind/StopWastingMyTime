using System;
using System.Configuration;

namespace StopWastingMyTime
{
    public static class Constants
    {
        public static string Name
        {
            get
            {
                string friendlyName = ConfigurationManager.AppSettings["FriendlyName"];
                return String.IsNullOrEmpty(friendlyName) ? "Stop Wasting my Time" : friendlyName;
            }
        }
    }
}
