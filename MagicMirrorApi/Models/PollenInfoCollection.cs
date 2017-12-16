using System.Collections.Generic;

namespace MagicMirrorApi.Models
{
    public class PollenInfoCollection
    {
        public IEnumerable<IPollenInfo> PollenCollection { get; set; }
    }
}