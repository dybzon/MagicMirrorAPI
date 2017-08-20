using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using MagicMirrorApi.Logic;
using MagicMirrorApi.Models;

namespace MagicMirrorApi.Controllers
{
    public class WeatherController : ApiController
    {
        private const int CITY_ID = 2618425; // Copenhagen
        private const string API_KEY = "a3b92ac62ad43130848bd3417370e86a"; // API key for OpenWeatherMap.Org
        private const string CURRENT_WEATHER_URI = @"http://api.openweathermap.org/data/2.5/weather"; // Base URI for current weather
        private const string FORECAST_WEATHER_URI = @"http://api.openweathermap.org/data/2.5/forecast"; // Base URI for weather forecast

        /// <summary>
        /// Get the current weather
        /// </summary>
        /// <returns>A weather object</returns>
        [HttpGet]
        public IHttpActionResult GetCurrentWeather()
        {
            var apiParameters = "?id=" + CITY_ID + "&appid=" + API_KEY;

            WeatherHttpConnector.InitializeCurrentClient();
            HttpClient client = WeatherHttpConnector.CurrentClient;
            client.BaseAddress = new Uri(CURRENT_WEATHER_URI);

            // Get data response
            HttpResponseMessage response = client.GetAsync(apiParameters).Result;  // Blocking call
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body to an object. We will simply expose everything, and therefore use Object rather than an instance of an inherited class.
                var dataObject = response.Content.ReadAsAsync<Object>().Result;
                return Ok(dataObject);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return NotFound();
            }
        }
        
        /// <summary>
        /// Get the weather forecast for the following days
        /// </summary>
        /// <param name="days"></param>
        /// <returns>A weather forecast for the specified number of days</returns>
        [HttpGet]
        public IHttpActionResult GetForecastWeather(int days)
        {
            // Weather forecast
            var apiParameters = "?id=" + CITY_ID + "&appid=" + API_KEY;

            WeatherHttpConnector.InitializeForecastClient();
            HttpClient client = WeatherHttpConnector.ForecastClient;
            client.BaseAddress = new Uri(FORECAST_WEATHER_URI);

            // Get data response
            HttpResponseMessage response = client.GetAsync(apiParameters).Result;  // Blocking call
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body to an object. We will simply expose everything, and therefore use Object rather than an instance of an inherited class.
                var dataObject = response.Content.ReadAsAsync<Weather>().Result;

                return Ok(dataObject);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return NotFound();
            }
        }
    }
}
