using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicMirrorApi.Models;

namespace MagicMirrorApi.Logic
{
    public interface IPollenDMI
    {
        List<PollenInfo> GetDmiAllPollens(string[] relevantCities, string[] relevantPlants);
    }
}
