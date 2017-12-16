using HtmlAgilityPack;
using MagicMirrorApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MagicMirrorApi.Logic
{
    public class PollenScraper : IPollenScraper
    {
        //private string connectionString = @"Data Source=rad-laptop\LOCALHOST2016;Initial Catalog=MagicMirror;Integrated Security=true";
        private string[] relevantCities;
        private string[] relevantPlants;

        private IPollenDmi PollenDmi { get; set; }

        public string[] RelevantPlants
        {
            get { return this.relevantPlants; }
            set { this.relevantPlants = ToLowerCaseStringArray(value); }
        }

        public string[] RelevantCities
        {
            get { return this.relevantCities; }
            set { this.relevantCities = ToLowerCaseStringArray(value); }
        }

        public PollenScraper(IPollenDmi pollenDmi)
        {
            this.PollenDmi = pollenDmi;
        }

        private static string[] ToLowerCaseStringArray(IEnumerable<string> strings)
        {
            return strings.Select(s => s.ToLower()).ToArray();
        }

        public IEnumerable<IPollenInfo> GetPollenInfo(string sourceUrl)
        {
            //Implement this to allow choosing between different pollen sources
            //For now we will always return the pollen levels from the PollenDMI class
            return PollenDmi.GetDmiAllPollens(RelevantCities, RelevantPlants);
        }

        //Overload the method for now. It should later be deterministic and be called with a source url
        public IEnumerable<IPollenInfo> GetPollenInfo()
        {
            return PollenDmi.GetDmiAllPollens(RelevantCities, RelevantPlants);
        }
    }
}