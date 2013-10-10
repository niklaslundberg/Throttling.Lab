using System.Web.Http;
using System.Web.Http.ValueProviders;
using Jayway.Throttling.Web.ValueProviders;

namespace Jayway.Throttling.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Services.Add(typeof(ValueProviderFactory), new BodyValueProviderFactory());
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

        }
    }
}
