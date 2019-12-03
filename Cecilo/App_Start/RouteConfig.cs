using Cecilo;
using System.Web.Mvc;
using System.Web.Routing;

namespace IdentitySample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapMvcAttributeRoutes();

           // routes.MapRoute(
           //   name: "Cecilo",
           //   url: "{culture}/{title}",
           //   defaults: new { culture = CultureHelper.GetDefaultCulture(), controller = "Cecilo", action = "Index", title = "" }

           //);


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { culture = CultureHelper.GetDefaultCulture(), controller = "Cecilo", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Cecilo.Controllers" }
            );
        }
    }
}