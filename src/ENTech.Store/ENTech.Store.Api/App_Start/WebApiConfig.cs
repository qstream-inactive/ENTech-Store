using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http.Formatting;
using System.Web.Http;
using ENTech.Store.Api.App_Start;

namespace ENTech.Store.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //                // Create Json.Net formatter serializing DateTime using the ISO 8601 format
            //    JsonSerializerSettings serializerSettings = new JsonSerializerSettings();            
            //    serializerSettings.Converters.Add(new IsoDateTimeConverter());

            //    GlobalConfiguration.Configuration.Formatters.Add(new System.Net.Http.Formatting.MediaTypeFormatter) = new JsonNetFormatter(serializerSettings
            //    config.Formatters.


            JsonMediaTypeFormatter jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
               // DateTimeZoneHandling = DateTimeZoneHandling.Utc,
               // DateFormatString = "yyyy-MM-dd'T'HH:mm:ss'Z"
            };

            //jSettings.DateTimeZoneHandling = DateTimeZoneHandling.;
            //jSettings.DateParseHandling = DateParseHandling.
            jSettings.Converters.Add(new CustomDateTimeConverter());
            jsonFormatter.SerializerSettings = jSettings;

        }
    }
}