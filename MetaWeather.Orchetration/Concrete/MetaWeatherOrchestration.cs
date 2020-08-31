using MetaWeather.Orchestration.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using MetaWeather.Entity;
using MetaWeather.Common;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MetaWeather.Orchestration.Concrete
{
    public class MetaWeatherOrchestration : IMetaWeatherOrchestration
    {
        private readonly IRestClient _restClient = null;
        private readonly IConfiguration _configuration = null;
        public MetaWeatherOrchestration(IRestClient restClient,IConfiguration configuration)
        {
            _restClient = restClient;
            _configuration = configuration;
        }

       
        public async Task<List<LocationSearchResponse>> GetLocationByTownOrCountryName(string townName)
        {
            _restClient.BaseUrl = _configuration["MetaWeatherRestPoint"];
            _restClient.urlParameter = $"location/search?query={townName}";
            var getlocationResponse =await _restClient.RestclientCall();
            if(!string.IsNullOrEmpty(getlocationResponse))
            {
               return JsonConvert.DeserializeObject<List<LocationSearchResponse>>(getlocationResponse);
            }
            return null;
        }

        public async Task<WeatherSearchResponse> GetWeatherByWOEID(int WOEID)
        {
            _restClient.BaseUrl = _configuration["MetaWeatherRestPoint"];
            _restClient.urlParameter = $"location/{WOEID}";
            var getWeatherResponse = await _restClient.RestclientCall();
            if (!string.IsNullOrEmpty(getWeatherResponse))
            {
                return JsonConvert.DeserializeObject<WeatherSearchResponse>(getWeatherResponse);
            }
            return null;
        }
    }
}
