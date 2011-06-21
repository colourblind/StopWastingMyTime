using System;
using System.Text.RegularExpressions;

namespace Colourblind.Core
{
    public class Utils
    {
        public static string CleanString(string str)
        {
            string result = String.Empty;
            result = Regex.Replace(str, "[^a-zA-Z0-9]", "");
            return result;
        }
    }
}
