using HtmlAgilityPack;
using MagicMirrorApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MagicMirrorApi.Logic;

namespace MagicMirrorApi.Logic
{
    public class PollenScraper : IPollenScraper
    {
        private string[] RelevantPlants { get; set; }
        private string[] RelevantCities { get; set; }
        private string connectionString = @"Data Source=rad-laptop\LOCALHOST2016;Initial Catalog=MagicMirror;Integrated Security=true";
        private IPollenDMI PollenDMI { get; set; }

        public PollenScraper(IPollenDMI pollenDMI)
        {
            this.PollenDMI = pollenDMI;
        }

        public void SetRelevantPlants(string[] relevantPlants)
        {
            this.RelevantPlants = ToLowerCaseStringArray(relevantPlants);
        }
        public void SetRelevantCities(string[] relevantCities)
        {
            this.RelevantCities = ToLowerCaseStringArray(relevantCities);
        }

        //Lower case all strings in a string array
        private string[] ToLowerCaseStringArray(string[] strings)
        {
            List<string> lowerCasedStringList = new List<string>();
            foreach (string p in strings)
            {
                lowerCasedStringList.Add(p.ToLower());
            }
            return lowerCasedStringList.ToArray();
        }

        //Get PollenInfo from the requested source site
        public IEnumerable<PollenInfo> GetPollenInfo(string sourceUrl)
        {
            //Implement this to allow choosing between different pollen sources
            //For now we will always return the pollen levels from the PollenDMI class
            return PollenDMI.GetDmiAllPollens(RelevantCities, RelevantPlants);
        }

        //Overload the method for now. It should later be deterministic and be called with a source url
        public IEnumerable<PollenInfo> GetPollenInfo()
        {
            return PollenDMI.GetDmiAllPollens(RelevantCities, RelevantPlants);
        }
    }
}