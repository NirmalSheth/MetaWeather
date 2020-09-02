
using MetaWeather.Common;
using MetaWeather.Entity;
using MetaWeather.Orchestration.Abstract;
using MetaWeather.Orchestration.Concrete;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetaWeather.OrchestrationTest
{
    public class MetaWeatherOrchestrationTest
    {
        private MetaWeatherOrchestration metaWeatherOrchestration;
        private readonly Mock<IRestClient> restClientMock;
        private readonly Mock<IConfiguration> configurationMock;
        private readonly Mock<IMetaWeatherOrchestration> metaWeatherOrchestrationMock;
        public MetaWeatherOrchestrationTest()
        {
            restClientMock = new Mock<IRestClient>();
            configurationMock = new Mock<IConfiguration>();
            metaWeatherOrchestrationMock = new Mock<IMetaWeatherOrchestration>();
            metaWeatherOrchestration = new MetaWeatherOrchestration(restClientMock.Object, configurationMock.Object);
        }

        [Fact]
        public async Task GetLocationByTownOrCountryName_Name_Should_be_Equal()
        {
            //Arrange
            var townName = "atlanta";
            var listLocationSearchResponse = new List<LocationSearchResponse>()
            {
                new LocationSearchResponse()
                {
                    title="Atlanta",
                    location_type="City",
                    woeid=2357024,
                    latt_long="33.748310,-84.391113"
                }
            };
            //Act
            var getResponse = "[{\"title\":\"Atlanta\",\"location_type\":\"City\",\"woeid\":2357024,\"latt_long\":\"33.748310,-84.391113\"}]";

            var getRestResponse = restClientMock.Setup(x => x.RestclientCall()).ReturnsAsync(getResponse);
            var getresult = await metaWeatherOrchestration.GetLocationByTownOrCountryName(townName); 
            //Assert
            Assert.Equal(townName.ToLower(), getresult[0].title.ToLower());
        }
        [Fact]
        public async Task GetLocationByTownOrCountryName_Should_null()
        {
            //Arrange
            var townName = "atlanta";
            var listLocationSearchResponse = new List<LocationSearchResponse>()
            {
                new LocationSearchResponse()
                {
                    title="Atlanta",
                    location_type="City",
                    woeid=2357024,
                    latt_long="33.748310,-84.391113"
                }
            };
            //Act
            var getRestResponse = restClientMock.Setup(x => x.RestclientCall()).ReturnsAsync(string.Empty);
            var getresult = await metaWeatherOrchestration.GetLocationByTownOrCountryName(townName);
            //Assert
            Assert.Null(getresult);
        }
        [Fact]
        public async Task GetWeatherByWOEID_Name_Should_be_Equal()
        {
            //Arrange
            var WOEID = 2357024;
            var WeatherSearchResponse = new WeatherSearchResponse()
            {
                consolidated_weather= new List<ConsolidatedWeather>()
                {
                    new ConsolidatedWeather()
                    {
                        created=DateTime.Now,
                        min_temp=24.77f,
                        predictability=71
                    }
                },
                title="Atlanta"
            };
            
            //Act
            var getResponse = "{\"consolidated_weather\":[{\"id\":6626227894878208,\"weather_state_name\":\"Heavy Cloud\",\"weather_state_abbr\":\"hc\",\"wind_direction_compass\":\"WSW\",\"created\":\"2020-09-02T04:01:43.761599Z\",\"applicable_date\":\"2020-09-02\",\"min_temp\":24.77,\"max_temp\":30.200000000000003,\"the_temp\":30.259999999999998,\"wind_speed\":5.478668238633051,\"wind_direction\":245.03471555900433,\"air_pressure\":1016.0,\"predictability\":71}],\"title\":\"Atlanta\"}";

            var getRestResponse = restClientMock.Setup(x => x.RestclientCall()).ReturnsAsync(getResponse);
            var getresult = await metaWeatherOrchestration.GetWeatherByWOEID(WOEID);
            //Assert
            Assert.Equal(WeatherSearchResponse.title.ToLower(), getresult.title.ToLower());
        }

        [Fact]
        public async Task GetWeatherByWOEID_Should_null()
        {
            var WOEID = 2357024;
            var WeatherSearchResponse = new WeatherSearchResponse()
            {
                consolidated_weather = new List<ConsolidatedWeather>()
                {
                    new ConsolidatedWeather()
                    {
                        created=DateTime.Now,
                        min_temp=24.77f,
                        predictability=71
                    }
                },
                title = "Atlanta"
            };

            //Act
           
            var getRestResponse = restClientMock.Setup(x => x.RestclientCall()).ReturnsAsync(string.Empty);
            var getresult = await metaWeatherOrchestration.GetWeatherByWOEID(WOEID);
           
            //Assert
            Assert.Null(getresult);
        }
    }
}
