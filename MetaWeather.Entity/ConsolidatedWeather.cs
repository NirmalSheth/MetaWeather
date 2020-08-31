using System;

namespace MetaWeather.Entity
{
    public class ConsolidatedWeather
    {
        public DateTime created { get; set; }
        public float min_temp { get; set; }
        public int predictability { get; set; }
    }
}