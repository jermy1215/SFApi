using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Utilities
{
    public class _ServicesConfig
    {
        public static string Environment { get; set; }
        public static string LogPath { get; set; }
        public static bool LogData { get; set; }
        public static string ErrorEmail { get; set; }

        public static void GetConfig(NameValueCollection configuration)
        {
            Environment = configuration["Environment"];
            LogPath = configuration["LogPath"];
            bool logData;
            bool.TryParse(configuration["LogData"], out logData);
            LogData = logData;
            ErrorEmail = configuration["ErrorEmail"];
        }
    }
}
