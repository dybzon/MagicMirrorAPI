using System;
using System.Net.Http;
using System.Web.Http;
using MagicMirrorApi.Logic;
using MagicMirrorApi.Models;

namespace MagicMirrorApi.Controllers
{
    [RoutePrefix("api/weather")]
    public class WeatherController : ApiController
    {
        private const int CITY_ID = 2618425; // Copenhagen
        private const string API_KEY = "a3b92ac62ad43130848bd3417370e86a"; // API key for OpenWeatherMap.Org

        /// <summary>
        /// Get the current weather
        /// </summary>
        /// <returns>A weather object</returns>
        [Route("")]
        public IHttpActionResult GetCurrentWeather()
        {
            var apiParameters = "?id=" + CITY_ID + "&appid=" + API_KEY;
            var client = WeatherHttpConnector.CurrentClient;

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
        [Route("{days}")]
        public IHttpActionResult GetForecastWeather(int days)
        {
            // Weather forecast
            var apiParameters = "?id=" + CITY_ID + "&appid=" + API_KEY;
            var client = WeatherHttpConnector.ForecastClient;

            // Get data response
            var response = client.GetAsync(apiParameters).Result;  // Blocking call
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
    }
}
