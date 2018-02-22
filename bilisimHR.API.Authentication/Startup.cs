using bilisimHR.API.Authentication.Properties;
using bilisimHR.API.Authentication.Providers;
using bilisimHR.API.Common;
using bilisimHR.API.Common.Filters;
using bilisimHR.API.Common.Handler;
using bilisimHR.Common.Helper;
using bilisimHR.Infrastructure.Dependency.CastleWindsor;
using bilisimHR.Services.Auth.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

[assembly: OwinStartup(typeof(bilisimHR.API.Authentication.Startup))]
namespace bilisimHR.API.Authentication
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
            // Web API Configuration
            GlobalConfiguration.Configure(WebApiConfig.Register);

            HttpConfiguration config = new HttpConfiguration();

            // Newtonsoft.JSON kütüphanesinde nested nesnelerin birbirlerine dönüşümünde yaşanan StackOverFlow exception için eklenmiştir.
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Custom Filters
            // config.Filters.Add(new SessionAuthorizeAttribute());
            config.Filters.Add(new WebApiActionLoggerAttribute());

            // Message Handlers (For Logging)
            config.MessageHandlers.Add(new ApiLogHandler(GetType().Namespace));

            // Controller and Actions persisting
            ControllerPersister persister = new ControllerPersister();
            persister.PersistControllerActions(typeof(Controllers.ClientsController));
            
            ConfigureOAuth(app);

            // Register OWIN Middleware
            app.Use<GlobalExceptionMiddleware>();
            //Register other middlewares
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler(GetType().Namespace));

            // Constructor Injection Installer
            config.Services.Replace(typeof(System.Web.Http.Dispatcher.IHttpControllerActivator), WebApiInstaller.Installer());

            // WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            #region v.1.0.0

            //OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            //{
            //    TokenEndpointPath = new PathString(Settings.Default.TokenPath), // token alacağımız path'i belirtiyoruz
            //    Provider = new ApplicationOAuthProvider(),
            //    AccessTokenExpireTimeSpan = Settings.Default.UserSessionTimeout,
            //    AllowInsecureHttp = true
            //};

            //// AppBuilder'a token üretimini gerçekleştirebilmek için ilgili authorization ayarlarımızı veriyoruz.
            //app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);

            //// Authentication type olarak ise Bearer Authentication'ı kullanacağımızı belirtiyoruz.
            //// Bearer token OAuth 2.0 ile gelen standartlaşmış token türüdür.
            //// Herhangi kriptolu bir veriye ihtiyaç duymadan client tarafından token isteğinde bulunulur ve server belirli bir expire date'e sahip bir access_token üretir.
            //// Bearer token üzerinde güvenlik SSL'e dayanır.
            //// Bir diğer tip ise MAC token'dır. OAuth 1.0 versiyonunda kullanılıyor, hem client'a, hemde server tarafına implementasyonlardan dolayı ek maliyet çıkartmaktadır. Bu maliyetin yanı sıra ise Bearer token'a göre kaynak alış verişinin biraz daha güvenli olduğu söyleniyor çünkü client her request'inde veriyi hmac ile imzalayıp verileri kriptolu bir şekilde göndermeleri gerektiği için.
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            #endregion

            #region v.2.0.0

            // Enable Application Sign In Cookie
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "Application",
            //    AuthenticationMode = AuthenticationMode.Passive,
            //    LoginPath = new PathString(Paths.LoginPath),
            //    LogoutPath = new PathString(Paths.LogoutPath),
            //});

            //// Enable External Sign In Cookie
            //app.SetDefaultSignInAsAuthenticationType("External");
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "External",
            //    AuthenticationMode = AuthenticationMode.Passive,
            //    CookieName = CookieAuthenticationDefaults.CookiePrefix + "External",
            //    ExpireTimeSpan = TimeSpan.FromMinutes(5),
            //});

            // We're enabling cookie authentication, but with a specific cookie name.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = Utility.AuthenticationType,
                ExpireTimeSpan = Settings.Default.UserSessionTimeout,
                //#if DEBUG
                CookieHttpOnly = true,
                //#endif
                CookieName = Utility.AuthCookieName
            });

            // Setup Authorization Server
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString(Settings.Default.TokenPath),
                AccessTokenExpireTimeSpan = Settings.Default.UserSessionTimeout,
                ApplicationCanDisplayErrors = true,
                //#if DEBUG
                AllowInsecureHttp = true,
                //#endif
                Provider = new ApplicationOAuthProvider(),
                AuthorizationCodeProvider = new AuthorizationCodeProvider(),
                RefreshTokenProvider = new RefreshTokenProvider()
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            #endregion
        }
    }
}