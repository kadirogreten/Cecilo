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

            routes.MapRoute(
              name: "Menu",
              url: "{controller}/{action}/{title}",
              defaults: null,
              constraints: new { title = @"[a-zA-Z]+".Replace(" ", "-") } //Burada yönlendirmenin yapılabilmesi için
                                                                          //title verisinin yalnızca metinsel olmasını şart koştuk.

           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Cecilo", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Cecilo.Controllers" }
            );
        }
    }
}