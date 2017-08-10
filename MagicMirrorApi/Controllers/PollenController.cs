using MagicMirrorApi.Logic;
using MagicMirrorApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;using System.Runtime.InteropServices;
using System.Web.Http;

namespace MagicMirrorApi.Controllers
{
    public class PollenController : ApiController
    {
        // GET api/pollen
        [HttpGet]
        public IHttpActionResult GetAllPollen()
        {
            string pollenSourceUrl = @"http://www.dmi.dk/vejr/sundhedsvejr/pollen/";
            IPollenDMI pd = new PollenDMI(pollenSourceUrl);
            IPollenScraper p = new PollenScraper(pd);
            string[] relevantCities = {"København", "Viborg"};
            string[] relevantPollens = { "Bynke", "El", "Elm", "Græs", "Birk", "Hassel" };
            p.SetRelevantPlants(relevantPollens);
            p.SetRelevantCities(relevantCities);
            IEnumerable<IPollenInfo> pollen = p.GetPollenInfo();

            if (pollen == null)
            {
                return NotFound();
            }
            return Ok(pollen);
        }

        // GET api/pollen/københavn
        [HttpGet]
        public IHttpActionResult GetCityPollen([FromUri] string city)
        {
            string pollenSourceUrl = @"http://www.dmi.dk/vejr/sundhedsvejr/pollen/";
            IPollenDMI pd = new PollenDMI(pollenSourceUrl);
            IPollenScraper p = new PollenScraper(pd);
            string[] relevantCities = { city };
            string[] relevantPollens = { "Bynke", "El", "Elm", "Græs", "Birk", "Hassel" };
            p.SetRelevantPlants(relevantPollens);
            p.SetRelevantCities(relevantCities);
            IEnumerable<PollenInfo> pollen = p.GetPollenInfo();

            if (pollen == null)
            {
                return NotFound();
            }
            return Ok(pollen);
        }

        // GET api/pollen/københavn/birk
        [HttpGet]
        public IHttpActionResult GetCityPlantPollen([FromUri] string city, [FromUri] string plant)
        {
            string pollenSourceUrl = @"http://www.dmi.dk/vejr/sundhedsvejr/pollen/";
            IPollenDMI pd = new PollenDMI(pollenSourceUrl);
            IPollenScraper p = new PollenScraper(pd);
            string[] relevantCities = { city };
            string[] relevantPollens = { plant };
            p.SetRelevantPlants(relevantPollens);
            p.SetRelevantCities(relevantCities);
            IEnumerable<PollenInfo> pollen = p.GetPollenInfo();

            if (pollen == null)
            {
                return NotFound();
            }
            return Ok(pollen);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
        }

    }
}
