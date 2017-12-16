using System;

namespace MagicMirrorApi.Models
{
    public class Event : IEvent
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventSummary { get; set; }
        public string EventDescription { get; set; }
    }
}