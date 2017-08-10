using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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