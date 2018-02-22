using System.Web.Mvc;
using System.Web.Routing;

namespace bilisimHR
{
    public class RouteConfig
    {
        public const string LoginRouteName = "Login";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(LoginRouteName, "Account/Login", new { controller = "Account", Action = "Login" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
