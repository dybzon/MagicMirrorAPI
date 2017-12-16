using MagicMirrorApi.Logic;
using MagicMirrorApi.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace MagicMirrorApi.Controllers
{
    [RoutePrefix("api/pollen")]
    public class PollenController : ApiController
    {
        /// <summary>
        /// Get pollen numbers for all available plants and locations
        /// GET api/pollen
        /// </summary>
        /// <returns>A list of pollen numbers</returns>
        [HttpGet]
        [Route("")]
        public IEnumerable<IPollenInfo> GetAllPollen()
        {
            var pollenSourceUrl = @"http://www.dmi.dk/vejr/sundhedsvejr/pollen/";
            var pd = new PollenDmi(pollenSourceUrl);
            var relevantCities = new string[]{"København", "Viborg"};
            var relevantPollens = new string[]{ "Bynke", "El", "Elm", "Græs", "Birk", "Hassel" };
            var p = new PollenScraper(pd)
            {
                RelevantCities = relevantCities,
                RelevantPlants = relevantPollens
            };
            return p.GetPollenInfo();
        }

        /// <summary>
        /// Get pollen numbers for all available plants for the specified location
        /// GET api/pollen/københavn
        /// </summary>
        /// <param name="city"></param>
        /// <returns>A list of pollen numbers</returns>
        [HttpGet]
        [Route("{city}")]
        public IEnumerable<IPollenInfo> GetCityPollen(string city)
        {
            var pollenSourceUrl = @"http://www.dmi.dk/vejr/sundhedsvejr/pollen/";
            var pd = new PollenDmi(pollenSourceUrl);
            var relevantCities = new string[]{ city };
            var relevantPollens = new string[] { "Bynke", "El", "Elm", "Græs", "Birk", "Hassel" };
            var p = new PollenScraper(pd)
            {
                RelevantCities = relevantCities,
                RelevantPlants = relevantPollens
            };
            return p.GetPollenInfo();
        }

        /// <summary>
        /// Get pollen numbers for the specified plant and location
        /// GET api/pollen/københavn/birk
        /// </summary>
        /// <param name="city"></param>
        /// <param name="plant"></param>
        /// <returns>A list of pollen numbers</returns>
        [HttpGet]
        [Route("{city}/{plant}")]
        public IEnumerable<IPollenInfo> GetCityPlantPollen(string city, string plant)
        {
            var pollenSourceUrl = @"http://www.dmi.dk/vejr/sundhedsvejr/pollen/";
            var pd = new PollenDmi(pollenSourceUrl);
            var relevantCities = new string[]{ city };
            var relevantPollens = new string[]{ plant };
            var p = new PollenScraper(pd)
            {
                RelevantCities = relevantCities,
                RelevantPlants = relevantPollens
            };
            return p.GetPollenInfo();
        }
    }
}
