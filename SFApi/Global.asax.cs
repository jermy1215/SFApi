using _Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SFApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (Context.Request.HttpMethod.Equals("OPTIONS"))
            {
                Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, OPTIONS");
                Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            try
            {
                _Logging.Log(string.Format("{0} - {1}: {2} => {3}{4}", ConfigurationManager.AppSettings["Environment"], DateTime.Now, GetIPAddress(), HttpContext.Current.Request.Url, System.Environment.NewLine), ConfigurationManager.AppSettings["LogPath"], string.Format("ApiAccess_{0}.log", DateTime.Today.ToString("yyyyMMM")));
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(ConfigurationManager.AppSettings["LogPath"] + "ApiLoggingErrors.log", ex.Message + "\r\n");
            }
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            string ip = context.Request.ServerVariables["REMOTE_ADDR"];
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
