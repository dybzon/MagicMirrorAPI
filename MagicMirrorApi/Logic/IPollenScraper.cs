using MagicMirrorApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMirrorApi.Logic
{
    interface IPollenScraper
    {
        IEnumerable<PollenInfo> GetPollenInfo(string sourceUrl);
        IEnumerable<PollenInfo> GetPollenInfo();
        void SetRelevantPlants(string[] relevantPlants);
        void SetRelevantCities(string[] relevantCities);
    }
}
