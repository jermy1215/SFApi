using _Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace _Utilities
{
    public static class _Logging
    {
        //allows the option to statically set log path
        public static string LogPath { get; set; }

        /// <summary>
        /// Unsafe filtered error logging with an email containing request data using Logging.LogPath property.
        /// !!!LogPath property must be set!!!
        /// </summary>
        /// <param name="error"></param>
        /// <param name="errorEmail"></param>
        /// <param name="request"></param>
        public static void LogMinError(string error, string errorEmail, HttpRequestBase request, string action = "")
        {
            LogError(error, LogPath, false, errorEmail, request, action);
        }

        /// <summary>
        /// Filtered error logging with an email containing request data
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logPath"></param>
        /// <param name="errorEmail"></param>
        /// <param name="request"></param>
        public static void LogMinError(string error, string logPath, string errorEmail, HttpRequestBase request, string action = "")
        {
            LogError(error, logPath, false, errorEmail, request, action);
        }

        /// <summary>
        /// Unsafe filtered error logging using Logging.LogPath property.
        /// !!!LogPath property must be set!!!
        /// </summary>
        /// <param name="error"></param>
        public static void LogMinError(string error, string action = "")
        {
            LogError(error, LogPath, false, "", null, action);
        }

        /// <summary>
        /// Filtered error logging
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logPath"></param>
        public static void LogMinError(string error, string logPath, string action = "")
        {
            LogError(error, logPath, false, "", null, action);
        }

        /// <summary>
        /// Unsafe verbose error logging using Logging.LogPath property.
        /// !!!LogPath property must be set!!!
        /// </summary>
        /// <param name="error"></param>
        public static void LogVerboseError(string error, string action = "")
        {
            LogError(error, LogPath, true, "", null, action);
        }

        /// <summary>
        /// Verbose error logging
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logPath"></param>
        public static void LogVerboseError(string error, string logPath, string action = "")
        {
            LogError(error, logPath, true, "", null, action);
        }

        /// <summary>
        /// Unsafe verbose error logging containing request data using Logging.LogPath property.
        /// !!!LogPath property must be set!!!
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logPath"></param>
        public static void LogVerboseError(string error, HttpRequestBase request, string action = "")
        {
            LogError(error, LogPath, true, "", request, action);
        }

        /// <summary>
        /// Verbose error logging containing request data
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logPath"></param>
        public static void LogVerboseError(string error, string logPath, HttpRequestBase request, string action = "")
        {
            LogError(error, logPath, true, "", request, action);
        }

        /// <summary>
        /// Error logging with all options available
        /// </summary>
        /// <param name="error"></param>
        /// <param name="logPath"></param>
        /// <param name="verboseLog"></param>
        /// <param name="errorEmail"></param>
        /// <param name="request"></param>
        public static void LogError(string error, string logPath, bool verboseLog, string errorEmail, HttpRequestBase request = null, string action = "")
        {
            try
            {
                string data = "";
                if (request != null)
                    if (request.InputStream.Length > 0)
                    {
                        request.InputStream.Position = 0;
                        data = new StreamReader(request.InputStream).ReadToEnd();
                    }

                (new FileInfo(logPath)).Directory.Create();
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                File.AppendAllText(logPath + "Errors" + (verboseLog ? "" : "Min") + "_" + DateTime.Today.ToString("yyyyMMM") + ".log", DateTime.Now.ToString()
                    + (string.IsNullOrWhiteSpace(action) ? "" : " - Endpoint/Action = " + action + " ")
                    + ": " + error + (data.Length > 0 ? "\r\nHttpRequest: " + data : "") + "\r\n");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logPath + "EnrollmentsAPILoggingErrors.log", ex.Message + "\r\nOriginal Error: " + error + "\r\n");
            }
        }

        public static void LogData(HttpRequestBase request, string action)
        {
            LogData(request, action, LogPath);
        }

        public static void LogData(HttpRequestBase request, string action, string logPath)
        {
            try
            {
                string data = "";
                if (request.InputStream.Length < 1)
                    data = "No request data found.";
                else
                {
                    request.InputStream.Position = 0;
                    data = new StreamReader(request.InputStream).ReadToEnd();
                }

                (new FileInfo(logPath)).Directory.Create();
                File.AppendAllText(logPath + "EnrollmentsAPIData_" + DateTime.Today.ToString("yyyyMMM") + ".log", DateTime.Now.ToString() + ": " + action + " - " + data + "\r\n");
            }
            catch (Exception ex) { }
        }

        public static void LogObjectData(object obj, string logPath)
        {
            (new FileInfo(logPath)).Directory.Create();
            File.AppendAllText(logPath + "EnrollmentsAPIData_" + DateTime.Today.ToString("yyyyMMM") + ".log", DateTime.Now.ToString() + ": " + JsonHelper.JsonString(obj) + "\r\n");
        }

        public static void Log(string message, string logPath, string file)
        {
            (new FileInfo(logPath)).Directory.Create();
            File.AppendAllText(file, message);
        }
    }
}
