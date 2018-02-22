using bilisimHR.Infrastructure.Dependency.CastleWindsor;
using bilisimHR.Services.Auth.Interfaces;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Tracing;
using System.Web.Routing;

namespace bilisimHR.API.Authentication
{
    /// <summary>
    /// WebApiApplication
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Application_Start
        /// </summary>
        protected void Application_Start()
        {
            //Trace Logger Installer
            var traceLoggerService = WebApiInstaller.Resolve<ITraceWriter>();
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), traceLoggerService);
        }
    }
}
