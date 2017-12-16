using HtmlAgilityPack;
using MagicMirrorApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace MagicMirrorApi.Logic
{
    public class PollenDmi : IPollenDmi
    {
        private string SourceUrl { get; set; }
        //private string connectionString = @"Data Source=rad-laptop\LOCALHOST2016;Initial Catalog=MagicMirror;Integrated Security=true";
        public PollenDmi(string sourceUrl)
        {
            this.SourceUrl = sourceUrl;
        }

        //Check whether a table row (HtmlNode) contains relevant pollen information
        private bool IncludesRelevantPollenInfo(HtmlNode trNode, string[] relevantPlants)
        {
            bool include = false;
            foreach (HtmlNode tdNode in trNode.SelectNodes("./td"))
            {
                //Check whether any of the relevant pollens match the value in the cell
                if (relevantPlants.Any(tdNode.InnerHtml.ToLower().Equals))
                {
                    include = true;
                }
            }
            return include;
        }

        //Get pollen info from a table row
        private PollenInfo GetPollenInfoFromNode(HtmlNode trNode, string city, int id, DateTime observationTime, string[] relevantPlants)
        {
            PollenInfo pol = new PollenInfo();
            pol.City = city;
            pol.SourceUrl = this.SourceUrl;
            pol.ObservationTime = observationTime;

            //Loop through the cells in the row to get the name and count of the pollen
            foreach (HtmlNode tdNode in trNode.SelectNodes("./td"))
            {
                if (relevantPlants.Any(tdNode.InnerHtml.ToLower().Equals))
                {
                    pol.PlantName = tdNode.InnerHtml;
                    pol.Id = id;
                }
                else
                {
                    Int16 pollenCount;
                    if (Int16.TryParse(tdNode.InnerHtml, out pollenCount))
                    {
                        pol.PollenLevel = pollenCount;
                    }
                }
            }
            return pol;
        }

        private List<PollenInfo> GetInnerPollenInfo(string city, HtmlNode bodyNode, DateTime observationTime, string[] relevantPlants)
        {
            var scrapedPollens = new List<PollenInfo>();
            var trNodes = bodyNode.SelectNodes("./tr");
            int counter = 0;

            //Loop through each row inside the table node
            foreach (HtmlNode trNode in trNodes)
            {
                if (trNode.SelectNodes("./td") != null)
                {
                    //If the row contains relevant pollen information, then we will include it in the list of Pollen objects
                    if (IncludesRelevantPollenInfo(trNode, relevantPlants))
                    {
                        scrapedPollens.Add(GetPollenInfoFromNode(trNode, city, counter, observationTime, relevantPlants));
                    }
                    counter++;
                }
            }
            return scrapedPollens;
        }

        public List<PollenInfo> GetDmiAllPollens(string[] relevantCities, string[] relevantPlants)
        {
            var scrapedPollens = new List<PollenInfo>();
            var getHtmlWeb = new HtmlWeb();
            var document = getHtmlWeb.Load(this.SourceUrl);
            DateTime observationTime = DateTime.Now;
            var tableNodes = document.DocumentNode.SelectNodes("//table");

            //We want to find the table nodes that contain pollen info, and scrape the info from there
            tableNodes.Where(e => e.SelectNodes("./tr/th") != null).ToList()
                .ForEach(tableNode =>
                {
                    HtmlNode thNode = tableNode.SelectNodes("./tr/th").FirstOrDefault();
                    if (relevantCities.Any(thNode.InnerHtml.ToLower().Equals)) //Check whether the table contains values for one of the relevant cities
                    {
                        scrapedPollens.AddRange(GetInnerPollenInfo(thNode.InnerHtml, tableNode, observationTime, relevantPlants));
                    }
                });

            ////We want to find the two tables that contain København and Viborg, and add these table elements to a list
            //foreach (HtmlNode tableNode in tableNodes)
            //{
            //    if (tableNode.SelectNodes("./tr/th") != null)
            //    {
            //        HtmlNode thNode = tableNode.SelectNodes("./tr/th")[0];
            //        if (relevantCities.Any(thNode.InnerHtml.ToLower().Equals)) //Check whether the table contains values for one of the relevant cities
            //        {
            //            scrapedPollens.AddRange(GetInnerPollenInfo(thNode.InnerHtml, tableNode, observationTime, relevantPlants));
            //        }
            //    }
            //}

            //Return
            return scrapedPollens;
        }
    }
}