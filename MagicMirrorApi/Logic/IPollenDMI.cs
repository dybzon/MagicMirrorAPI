using System.Collections.Generic;
using MagicMirrorApi.Models;

namespace MagicMirrorApi.Logic
{
    public interface IPollenDmi
    {
        List<PollenInfo> GetDmiAllPollens(string[] relevantCities, string[] relevantPlants);
    }
}
