using System.Web.Http;

namespace MagicMirrorApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS. Might not be necessary after all?
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Error policy
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}
