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
        public static HttpClient CurrentClient { get; private set; }
        public static HttpClient ForecastClient { get; private set; }

        public static void InitializeCurrentClient()
        {
            if (CurrentClient == null)
            {
                CurrentClient = new HttpClient();

                // We always want to receive json
                CurrentClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public static void InitializeForecastClient()
        {
            if (ForecastClient == null)
            {
                ForecastClient = new HttpClient();

                // We always want to receive json
                ForecastClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        //public static Object GetResult(HttpClient client, string baseUri, string apiParameters)
        //{
        //    client.BaseAddress = new Uri(baseUri);

        //    // Get data response
        //    HttpResponseMessage response = client.GetAsync(apiParameters).Result;  // Blocking call
        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Parse the response body to an object. We will simply expose everything, and therefore use Object rather than an instance of an inherited class.
        //        var dataObject = response.Content.ReadAsAsync<Object>().Result;
        //        return dataObject;
        //    }
        //    else
        //    {
        //        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        //        return null;
        //    }

        //}
    }
}