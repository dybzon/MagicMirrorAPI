using System;

namespace MagicMirrorApi.Models
{
    public interface IEvent
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string EventSummary { get; set; }
        string EventDescription { get; set; }
    }
}
