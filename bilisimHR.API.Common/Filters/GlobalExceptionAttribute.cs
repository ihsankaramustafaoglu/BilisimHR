using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;
using ErrorHandling = bilisimHR.Common.Core.ErrorHandling;

namespace bilisimHR.API.Common.Filters
{
    /// <summary>
    /// GlobalExceptionAttribute
    /// </summary>
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        private string _module;

        public GlobalExceptionAttribute(string module)
        {
            _module = module;
        }

        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();

            if (trace == null)
                return;

            APILogHelper apiLogHelper = new APILogHelper();
            var apiLogEntry = apiLogHelper.CreateApiLogEntryWithRequestData(context.Request);

            trace.Error(context.Request, _module, "JSON", apiLogEntry);

            //trace.Error(context.Request,
            //    "Controller : " + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
            //    Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception);

            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ValidationException))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(context.Exception.Message), ReasonPhrase = "ValidationException", };
                throw new HttpResponseException(resp);

            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new ErrorHandling.WebException()
                    {
                        HttpStatusCode = HttpStatusCode.Unauthorized,
                        HataAciklama = "UnAuthorized",
                        HataDetayAciklama = "UnAuthorized Access",
                    }));
            }
            else if (exceptionType == typeof(ErrorHandling.WebException))
            {
                var webApiException = context.Exception as ErrorHandling.WebException;

                if (webApiException != null)
                    throw new HttpResponseException(context.Request.CreateResponse(webApiException.HttpStatusCode, webApiException));
            }
            else
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }
    }
}
