using System;
using System.Collections.Generic;
using System.Text;

namespace MetaWeather.Entity
{
    public class WeatherSearchResponse
    {
        public List<ConsolidatedWeather> consolidated_weather { get; set; }
        public string title { get; set; }
    }
}
