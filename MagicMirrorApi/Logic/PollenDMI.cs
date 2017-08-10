using HtmlAgilityPack;
using MagicMirrorApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MagicMirrorApi.Logic
{
    public class PollenDMI : IPollenDMI
    {
        private string SourceUrl { get; set; }
        private string connectionString = @"Data Source=rad-laptop\LOCALHOST2016;Initial Catalog=MagicMirror;Integrated Security=true";
        public PollenDMI(string sourceUrl)
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
                        //Break the following into a seperate private PollenInfo GetPollenInfoFromNode(HtmlNode trNode){}
                        scrapedPollens.Add(GetPollenInfoFromNode(trNode, city, counter, observationTime, relevantPlants));
                    }
                    counter++;
                }
            }
            return scrapedPollens;
        }

        private bool IsPollenUpdated()
        {
            bool isUpdated = false;
            //Get the maximum timestamp from the database and check whether this is older than 1 hour. If not, return true.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand("MM.GetUpdateStatus", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SourceUrl", this.SourceUrl);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        isUpdated = reader.GetBoolean(0);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return isUpdated;
        }

        private void SavePollenInfoToDatabase(IEnumerable<PollenInfo> pollenInfoList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    foreach (PollenInfo p in pollenInfoList)
                    {
                        // Create the Command and Parameter objects.
                        SqlCommand command = new SqlCommand("MM.SavePollenInfo", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PlantName", p.PlantName);
                        command.Parameters.AddWithValue("@SourceUrl", p.SourceUrl);
                        command.Parameters.AddWithValue("@ObservationTime", p.ObservationTime);
                        command.Parameters.AddWithValue("@PollenLevel", p.PollenLevel);
                        command.Parameters.AddWithValue("@City", p.City);

                        //Attempt to save the PollenInfo to the database
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        public List<PollenInfo> GetDmiAllPollens(string[] relevantCities, string[] relevantPlants)
        {
            //Check whether DmiPollen was already fetched within the last hour
            //Move this to a separate method
            if (IsPollenUpdated())
            {
                //Get data from database rather than scraping it
                //Return data from database
                //Write a function here that gets the latest relevant pollen info from the database (is this even feasible compared to 
                //scraping it from the website again? We will use less bandwidth, but we have to communicate with the database instead.
                //What is the best way to do this?)
                Console.WriteLine("Pollen is already up to date");
            }

            var scrapedPollens = new List<PollenInfo>();
            var getHtmlWeb = new HtmlWeb();
            var document = getHtmlWeb.Load(this.SourceUrl);
            DateTime observationTime = DateTime.Now;
            var tableNodes = document.DocumentNode.SelectNodes("//table");

            //We want to find the two tables that contain København and Viborg, and add these table elements to a list
            foreach (HtmlNode tableNode in tableNodes)
            {
                if (tableNode.SelectNodes("./tr/th") != null)
                {
                    HtmlNode thNode = tableNode.SelectNodes("./tr/th")[0];
                    if (relevantCities.Any(thNode.InnerHtml.ToLower().Equals)) //Check whether the table contains values for one of the relevant cities
                    {
                        scrapedPollens.AddRange(GetInnerPollenInfo(thNode.InnerHtml, tableNode, observationTime, relevantPlants));
                    }
                }
            }

            //Write DMI values to the database
            SavePollenInfoToDatabase(scrapedPollens);

            //Return
            return scrapedPollens;
        }
    }
}