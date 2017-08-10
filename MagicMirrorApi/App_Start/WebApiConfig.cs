using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MagicMirrorApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Force this route for the Calendar controller
            config.Routes.MapHttpRoute(
                name: "Calendar",
                routeTemplate: "api/calendar/{calendarId}",
                defaults: new { controller = "calendar", calendarId = RouteParameter.Optional }
            );

            //Force this route for the Pollen controller
            config.Routes.MapHttpRoute(
                name: "Pollen",
                routeTemplate: "api/pollen/{city}/{plant}",
                defaults: new { controller = "pollen", city = RouteParameter.Optional, plant = RouteParameter.Optional }
            );

            //Consider reverting to regular routing instead of one route template per controller
            //config.Routes.MapHttpRoute(
            //    name: "PollenApi",
            //    routeTemplate: "api/{controller}/{city}/{plant}",
            //    //The city and plant parameters should be optional
            //    defaults: new { city = RouteParameter.Optional, plant = RouteParameter.Optional }
            //);
        }
    }
}
