using MetaWeather.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MetaWeather.Orchestration.Abstract
{
   public interface IMetaWeatherOrchestration
    {
       Task<List<LocationSearchResponse>> GetLocationByTownOrCountryName(string townName);
        Task<WeatherSearchResponse> GetWeatherByWOEID(int WOEID);
    }
}
