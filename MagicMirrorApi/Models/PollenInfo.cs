using System;

namespace MagicMirrorApi.Models
{
    public class PollenInfo : IPollenInfo
    {
        public string PlantName { get; set; }
        public string SourceUrl { get; set; }
        public DateTime ObservationTime { get; set; }
        public Int16 PollenLevel { get; set; }
        public string City { get; set; }
        public Int32 Id { get; set; }

        //Simple constructor
        public PollenInfo()
        {
            PollenLevel = 0;
        }

        //Overload the constructor
        public PollenInfo(string plantName, string sourceUrl, DateTime observationTime, Int16 pollenLevel, string city)
        {
            this.PlantName = plantName;
            this.SourceUrl = sourceUrl;
            this.ObservationTime = observationTime;
            this.PollenLevel = pollenLevel;
            this.City = city;
        }
    }
}