using System;
using System.Collections.Generic;

namespace MagicMirrorApi.Models
{
    public interface IWeather
    {
        string cod { get; set; }
        string message { get; set; }
        string cnt { get; set; }
        IEnumerable<Object> list { get; set; }
        IEnumerable<Object> city { get; set; }
    }
}
