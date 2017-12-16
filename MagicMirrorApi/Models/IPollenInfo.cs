using System;

namespace MagicMirrorApi.Models
{
    public interface IPollenInfo
    {
        string PlantName { get; set; }
        string SourceUrl { get; set; }
        DateTime ObservationTime { get; set; }
        Int16 PollenLevel { get; set; }
        string City { get; set; }
        Int32 Id { get; set; }
    }
}
