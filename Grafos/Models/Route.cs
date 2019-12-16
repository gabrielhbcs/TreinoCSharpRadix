using System;

namespace Grafos.Models
{
    public class Route
    {
        public int ID { get; set; }
        public string town1 { get; set; }
        public string town2 { get; set; }
        public int maxStops { get; set; }
    }
}
