using bilisimHR.Common.Helper.ServiceLocator;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace bilisimHR.API.Common.Handler
{
    public class GlobalExceptionMiddleware : OwinMiddleware
    {
        public GlobalExceptionMiddleware(OwinMiddleware next) 
            : base(next)
        {
            // ...
        }

        public override async Task Invoke(IOwinContext owinContext)
        {
            try
            {
                await Next.Invoke(owinContext);
            }
            catch (Exception ex)
            {
                try
                {
                    handleException(ex, owinContext);
                    var exceptionGuid = ex.Data["httpRequestMessageGuid"];
                    var module = ex.Data["module"];

                    if (exceptionGuid != null && module != null)
                    {
                        HttpRequestMessage httpRequestMessage;
                        bool result = ServiceLocator.GetService<HttpRequestMessageService>().HttpRequestMessageDict.TryGetValue(exceptionGuid.ToString(), out httpRequestMessage);

                        var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();

                        if (trace == null)
                            return;

                        APILogHelper apiLogHelper = new APILogHelper();
                        var apiLogEntry = apiLogHelper.CreateApiLogEntryWithRequestData(httpRequestMessage);


                        apiLogEntry.ResponseContentBody = string.Empty; // owinContext.Response.Body.Read
                        apiLogEntry.ResponseContentType = owinContext.Response.ContentType;
                        apiLogEntry.ResponseHeaders = JsonConvert.SerializeObject(owinContext.Response.Headers, Formatting.Indented);
                        apiLogEntry.ResponseStatusCode = owinContext.Response.StatusCode;
                        apiLogEntry.ResponseTimestamp = DateTime.UtcNow;
                        apiLogEntry.TotalExecutionSeconds = (apiLogEntry.ResponseTimestamp - apiLogEntry.RequestTimestamp).Value.TotalSeconds;

                        trace.Error(httpRequestMessage, module.ToString(), ex, "JSON", apiLogEntry);
                    }
                    
                    return;
                    
                }
                catch (Exception unhandledException)
                {
                    handleException(unhandledException, owinContext);
                    // If there's a Exception while generating the error page, re-throw the original exception.
                }
                throw;
            }
        }

        private void handleException(Exception exception, IOwinContext owinContext)
        {
            var request = owinContext.Request;

            owinContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            owinContext.Response.ReasonPhrase = "Internal Server Error";
            owinContext.Response.ContentType = "application/json";
            owinContext.Response.Write(JsonConvert.SerializeObject(exception.Message));

        }
    }
}
