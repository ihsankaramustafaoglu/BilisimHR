using bilisimHR.Common.Helper;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace bilisimHR.API.Common.Handler
{
    public class ApiLogHandler : DelegatingHandler
    {
        private string _module = string.Empty;
        private APILogHelper _apiLogHelper;

        public ApiLogHandler(string module)
        {
            _module = module;
            _apiLogHelper = new APILogHelper();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiLogEntry = _apiLogHelper.CreateApiLogEntryWithRequestData(request);

            if (apiLogEntry == null)
                return await base.SendAsync(request, cancellationToken);

            if (request.Content != null)
            {
                
                await request.Content.ReadAsStringAsync().ContinueWith(task => {
                    apiLogEntry.RequestContentBody = task.Result;
                }, cancellationToken);
            }

            return await base.SendAsync(request, cancellationToken).ContinueWith(task => {
                var response = task.Result;

                // Update the API log entry with response info
                apiLogEntry.ResponseStatusCode = (int)response.StatusCode;
                apiLogEntry.ResponseTimestamp = DateTime.UtcNow;
                apiLogEntry.TotalExecutionSeconds = (apiLogEntry.ResponseTimestamp.Value - apiLogEntry.RequestTimestamp.Value).TotalSeconds;

                if (response.Content != null)
                {
                    apiLogEntry.ResponseContentBody = response.Content.ReadAsStringAsync().Result;
                    apiLogEntry.ResponseContentType = response.Content.Headers.ContentType.MediaType;
                    apiLogEntry.ResponseHeaders = _apiLogHelper.SerializeHeaders(response.Content.Headers);
                }

                // TODO: Save the API log entry to the database
                var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                
                if (trace == null)
                    return response;

                trace.Info(request, _module, "JSON", apiLogEntry);

                return response;
            }, cancellationToken);
        }
    }
}
