using bilisimHR.Common.Helper.ServiceLocator;
using System;
using System.Runtime.ExceptionServices;
using System.Web.Http.ExceptionHandling;

namespace bilisimHR.API.Common.Handler
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private string _module;

        public GlobalExceptionHandler(string module)
        {
            _module = module;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            // don't just throw the exception; that will ruin the stack trace
            var info = ExceptionDispatchInfo.Capture(context.Exception);
            
            string guid = Guid.NewGuid().ToString();
            ServiceLocator.GetService<HttpRequestMessageService>().HttpRequestMessageDict.Add(guid, context.Request);
            
            info.SourceException.Data.Add("httpRequestMessageGuid", guid);
            info.SourceException.Data.Add("module", _module);
            info.Throw();
        }
    }
}
