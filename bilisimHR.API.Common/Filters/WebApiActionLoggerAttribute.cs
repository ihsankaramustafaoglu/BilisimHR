using bilisimHR.Common.Helper.ServiceLocator;
using bilisimHR.Common.Helper.ServiceLocator.RevisionInfo;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

namespace bilisimHR.API.Common.Filters
{
    /// <summary>
    /// WebApiActionLoggerAttribute
    /// </summary>
    public class WebApiActionLoggerAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //string userName = string.Empty;
            //string userID = string.Empty;

            ////var principal = (ClaimsPrincipal)actionContext.RequestContext.Principal;

            //if (actionContext.Request.Properties.ContainsKey("MS_OwinContext"))
            //{
            //    var context = (OwinContext)actionContext.Request.Properties["MS_OwinContext"];
            //    var claimsIdentity = context.Authentication.User.Identity as ClaimsIdentity;

            //    if (claimsIdentity.Claims.ToList().Count > 0)
            //    {
            //        Claim claimUserID = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.PrimarySid).FirstOrDefault();
            //        Claim claimUserName = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.Name).FirstOrDefault();

            //        userID = claimUserID != null ? claimUserID.Value : string.Empty;
            //        userName = claimUserName != null ? claimUserName.Value : string.Empty;
            //    }
            //}

            //var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();

            //if (trace == null)
            //    return;

            //Dictionary<string, string> dictParams = new Dictionary<string, string>();
            //dictParams.Add("IP_ADDRESS", getClientIpAddress(actionContext.Request));
            //dictParams.Add("CONTROLLER", actionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName);
            //dictParams.Add("ACTION", actionContext.ActionDescriptor.ActionName);
            //dictParams.Add("USER_ID", userID);

            //var dictString = string.Join(",", dictParams.Select(m => m.Key + "@" + m.Value).ToArray());

            //if (!string.IsNullOrEmpty(userID))
            //    ServiceLocator.GetService<IRevisionInfoService>().UserId = int.Parse(userID);

            //trace.Info(actionContext.Request, dictString, "JSON", actionContext.ActionArguments);

            ////GethostName
            ////System.Net.Dns.Resolve((actionContext.Request.Properties["MS_HttpContext"] as System.Web.HttpContextWrapper).Request.ServerVariables["remote_addr"]).HostName
            ////Trace.WriteLine(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");
        }

        /// <summary>
        /// OnActionExecuted
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //Trace.WriteLine(string.Format("Action Method {0} executed at {1}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");
        }

        private string getClientIpAddress(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
                return IPAddress.Parse(((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();

            if (request.Properties.ContainsKey("MS_OwinContext"))
                return IPAddress.Parse(((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress).ToString();

            return string.Empty;
        }
    }
}
