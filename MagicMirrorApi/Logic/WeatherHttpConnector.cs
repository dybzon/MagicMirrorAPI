using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MagicMirrorApi.Logic
{
    public class WeatherHttpConnector
    {
        // We want to keep the HttpClients as static attributes on this class to ensure that only one instanse of each
        // HttpClient type is created
        // We need two different HttpClients to ensure that calls to the Forecast method and the Current method 
        // do not interfere with each other
        private static HttpClient currentClient;
        private static HttpClient forecastClient;
        private const string CURRENT_WEATHER_URI = @"http://api.openweathermap.org/data/2.5/weather"; // Base URI for current weather
        private const string FORECAST_WEATHER_URI = @"http://api.openweathermap.org/data/2.5/forecast"; // Base URI for weather forecast

        public static HttpClient CurrentClient
        {
            get
            {
                if (currentClient == null)
                {
                    currentClient = new HttpClient();
                    currentClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    currentClient.BaseAddress = new Uri(CURRENT_WEATHER_URI);
                }
                return currentClient;
            }
        }

        public static HttpClient ForecastClient
        {
            get
            {
                if (forecastClient == null)
                {
                    forecastClient = new HttpClient();
                    forecastClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    forecastClient.BaseAddress = new Uri(FORECAST_WEATHER_URI);
                }
                return forecastClient;
            }
        }
    }
}