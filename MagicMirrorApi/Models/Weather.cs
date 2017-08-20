using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicMirrorApi.Models
{
    public class Weather : IWeather
    {
        public string cod { get; set; }
        public string message { get; set; }
        public string cnt { get; set; }
        public IEnumerable<Object> list { get; set; }
        public IEnumerable<Object> city { get; set; }
    }
}