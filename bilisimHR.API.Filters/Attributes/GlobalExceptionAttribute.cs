using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;
using bilisimHR;

namespace bilisimHR.API.Filters.Attributes
{
    /// <summary>
    /// GlobalExceptionAttribute
    /// </summary>
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();

            if (trace == null)
                return;

            trace.Error(context.Request, "Controller : " + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception);

            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ValidationException))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(context.Exception.Message), ReasonPhrase = "ValidationException", };
                throw new HttpResponseException(resp);

            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new Common.Core.ErrorHandling.WebException()
                    {
                        HttpStatusCode = HttpStatusCode.Unauthorized,
                        HataAciklama = "UnAuthorized",
                        HataDetayAciklama = "UnAuthorized Access",
                    }));
            }
            else if (exceptionType == typeof(Common.Core.ErrorHandling.WebException))
            {
                var webApiException = context.Exception as Common.Core.ErrorHandling.WebException;
                if (webApiException != null)
                    throw new HttpResponseException(context.Request.CreateResponse(webApiException.HttpStatusCode, webApiException));
            }
            //else if (exceptionType == typeof(ApiBusinessException))
            //{
            //    var businessException = context.Exception as ApiBusinessException;
            //    if (businessException != null)
            //        throw new HttpResponseException(context.Request.CreateResponse(businessException.HttpStatus, new ServiceStatus() { StatusCode = businessException.ErrorCode, StatusMessage = businessException.ErrorDescription, ReasonPhrase = businessException.ReasonPhrase }));
            //}
            //else if (exceptionType == typeof(ApiDataException))
            //{
            //    var dataException = context.Exception as ApiDataException;
            //    if (dataException != null)
            //        throw new HttpResponseException(context.Request.CreateResponse(dataException.HttpStatus, new ServiceStatus() { StatusCode = dataException.ErrorCode, StatusMessage = dataException.ErrorDescription, ReasonPhrase = dataException.ReasonPhrase }));
            //}
            else
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }
    }
}
