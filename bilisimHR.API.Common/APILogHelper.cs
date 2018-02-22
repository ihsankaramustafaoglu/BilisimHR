using bilisimHR.Common.Helper;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Routing;

namespace bilisimHR.API.Common
{
    public class APILogHelper
    {
        public ApiLog CreateApiLogEntryWithRequestData(HttpRequestMessage request)
        {
            OwinContext owinContext = null;
            var routeData = request.GetRouteData();

            string userName = string.Empty;
            string userID = string.Empty;

            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                owinContext = (OwinContext)request.Properties["MS_OwinContext"];
                var claimsIdentity = owinContext.Authentication.User.Identity as ClaimsIdentity;

                if (claimsIdentity.Claims.ToList().Count > 0)
                {
                    Claim claimUserID = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                    Claim claimUserName = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.Name).FirstOrDefault();

                    userID = claimUserID != null ? claimUserID.Value : string.Empty;
                    userName = claimUserName != null ? claimUserName.Value : string.Empty;
                }
            }

            if (owinContext == null)
                return null;

            return new ApiLog
            {
                Application = "[insert-calling-app-here]",
                Username = userName,
                UserId = userID,
                Machine = getClientHostName(request),
                Controller = request.GetActionDescriptor() == null ? string.Empty : request.GetActionDescriptor().ControllerDescriptor.ControllerName,
                Action = request.GetActionDescriptor() == null ? string.Empty : request.GetActionDescriptor().ActionName,
                RequestContentType = owinContext.Request.ContentType,
                RequestRouteTemplate = routeData == null ? string.Empty : routeData.Route.RouteTemplate,
                // Tüm Controller ve içlerindeki Action değerlerine kadar yazdığından kapatılmıştır.
                // RequestRouteData = routeData == null ? string.Empty : SerializeRouteData(routeData),
                RequestIpAddress = getClientIpAddress(request),
                RequestMethod = request.Method.Method,
                RequestHeaders = SerializeHeaders(request.Headers),
                RequestTimestamp = DateTime.UtcNow,
                RequestUri = request.RequestUri.ToString()
            };
        }
        
        public string SerializeRouteData(IHttpRouteData routeData)
        {
            // return JsonConvert.SerializeObject(routeData, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return JsonConvert.SerializeObject(routeData, Newtonsoft.Json.Formatting.Indented);
        }

        public string SerializeHeaders(HttpHeaders headers)
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in headers.ToList())
            {
                if (item.Value != null)
                {
                    var header = String.Empty;
                    foreach (var value in item.Value)
                    {
                        header += value + " ";
                    }

                    // Trim the trailing space and add item to the dictionary
                    header = header.TrimEnd(" ".ToCharArray());
                    dict.Add(item.Key, header);
                }
            }

            return JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.Indented);
        }
        
        private string getClientIpAddress(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
                return IPAddress.Parse(((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();

            if (request.Properties.ContainsKey("MS_OwinContext"))
                return IPAddress.Parse(((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress).ToString();

            return string.Empty;
        }

        private string getClientHostName(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
                return ((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostName.ToString();

            if (request.Properties.ContainsKey("MS_OwinContext"))
                return ((OwinContext)request.Properties["MS_OwinContext"]).Request.Host.Value.ToString();

            return string.Empty;
        }
    }
}
