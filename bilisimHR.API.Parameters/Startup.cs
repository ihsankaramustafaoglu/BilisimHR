using bilisimHR.API.Common;
using bilisimHR.API.Common.Filters;
using bilisimHR.API.Common.Handler;
using bilisimHR.Common.Core;
using bilisimHR.Infrastructure.Dependency.CastleWindsor;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Owin;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

[assembly: OwinStartup(typeof(bilisimHR.API.Parameters.Startup))]
namespace bilisimHR.API.Parameters
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var logger = WebApiInstaller.Resolve<ICoreLogger>();
            logger.Info(GetType(), null, "Startup.Configuration Started.");

            HttpConfiguration config = new HttpConfiguration();

            // Newtonsoft.JSON kütüphanesinde nested nesnelerin birbirlerine dönüşümünde yaşanan StackOverFlow exception için eklenmiştir.
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Custom Filters
            config.Filters.Add(new SessionAuthorizeAttribute());
            config.Filters.Add(new WebApiActionLoggerAttribute());

            // Message Handlers (For Logging)
            config.MessageHandlers.Add(new ApiLogHandler(GetType().Namespace));

            // Controller and Actions persisting
            ControllerPersister persister = new ControllerPersister();
            persister.PersistControllerActions(typeof(Controllers.CodeTableController));

            // Register OWIN Middleware
            app.Use<GlobalExceptionMiddleware>();
            //Register other middlewares
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler(GetType().Namespace));

            // Constructor Injection Installer
            config.Services.Replace(typeof(System.Web.Http.Dispatcher.IHttpControllerActivator), WebApiInstaller.Installer());
            logger.Info(GetType(), null, "Dependency Injection Replaced.");
            
            ConfigureOAuth(app);
            logger.Info(GetType(), null, "OAuth configuration finished.");

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            
            logger.Info(GetType(), null, "Startup.Configuration completed.");
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            //Token Consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                //...
            });
        }
    }
}