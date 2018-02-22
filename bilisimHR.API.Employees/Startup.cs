using bilisimHR.API.Employees.Properties;
using bilisimHR.API.Employees.Providers;
using bilisimHR.Common.Helper;
using bilisimHR.Infrastructure.Dependency.CastleWindsor;
using bilisimHR.Services.Auth.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Owin;
using Swashbuckle.Application;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(bilisimHR.API.Employees.Startup))]

namespace bilisimHR.API.Employees
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //Newtonsoft.JSON kütüphanesinde nested nesnelerin birbirlerine dönüşümünde yaşanan StackOverFlow exception için eklenmiştir.
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            ConfigureOAuth(app);

            //Dependency Injection Installer
            config.Services.Replace(typeof(System.Web.Http.Dispatcher.IHttpControllerActivator), WebApiInstaller.Installer());

            //WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);

            // Attribute routing
            config.MapHttpAttributeRoutes();

            Assembly asm = Assembly.GetAssembly(typeof(Controllers.EmpEmployeePkController));

            app.UseWebApi(config);

            var controllerActionsService = WebApiInstaller.Resolve<IControllerActionsService>();

            var controlleractionlist = asm.GetTypes()
                .Where(type => typeof(ApiController).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Select(x => new
                {
                    Controller = x.DeclaringType.Name,
                    Action = x.Name,
                    CustomAttributes = x.GetCustomAttributes(false)
                    // Description = x.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().FirstOrDefault() != null ? x.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().FirstOrDefault().Description : string.Empty
                });

            foreach (var controller in controlleractionlist)
            {

                string description = controller.CustomAttributes.OfType<DescriptionAttribute>().FirstOrDefault() != null ? controller.CustomAttributes.OfType<DescriptionAttribute>().FirstOrDefault().Description : string.Empty;

                OperationType type;

                if (controller.CustomAttributes.OfType<HttpGetAttribute>().FirstOrDefault() != null)
                    type = OperationType.Read;
                else if (controller.CustomAttributes.OfType<HttpPostAttribute>().FirstOrDefault() != null)
                    type = OperationType.Create;
                else if (controller.CustomAttributes.OfType<HttpPutAttribute>().FirstOrDefault() != null)
                    type = OperationType.Update;
                else if (controller.CustomAttributes.OfType<HttpDeleteAttribute>().FirstOrDefault() != null)
                    type = OperationType.Delete;
                else
                    type = OperationType.Operation;

                Business.Model.Auth.ControllerActionsModel model = new Business.Model.Auth.ControllerActionsModel()
                {
                    Controller = controller.Controller,
                    Action = controller.Action,
                    Description = description, // controller.Description,
                    InsertedBy = 1,
                    InsertedDate = System.DateTime.Now,
                    UpdatedBy = 1,
                    UpdatedDate = System.DateTime.Now,
                    OperationType = type
                };

                //controllerActionsService.SaveOrUpdateAsync(model);
            }
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