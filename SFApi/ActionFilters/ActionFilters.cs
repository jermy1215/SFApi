using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace SFApi.ActionFilters
{
    public class SalesforceDataActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SalesforceData.DynamicConfig.GetConfig(ConfigurationManager.AppSettings);
        }
    }
}