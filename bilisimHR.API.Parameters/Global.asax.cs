using bilisimHR.Infrastructure.Dependency.CastleWindsor;
using System.Web;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace bilisimHR.API.Parameters
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //Trace Logger Installer
            var traceLoggerService = WebApiInstaller.Resolve<ITraceWriter>();
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), traceLoggerService);
        }
    }
}
