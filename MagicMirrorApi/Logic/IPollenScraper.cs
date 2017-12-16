using MagicMirrorApi.Models;
using System.Collections.Generic;

namespace MagicMirrorApi.Logic
{
    interface IPollenScraper
    {
        string[] RelevantPlants { get; set; }
        string[] RelevantCities { get; set; }
        IEnumerable<IPollenInfo> GetPollenInfo(string sourceUrl);
        IEnumerable<IPollenInfo> GetPollenInfo();
    }
}
