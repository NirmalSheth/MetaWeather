using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using MetaWeather.Common;
using Microsoft.Extensions.Configuration;
using MetaWeather.Orchestration.Abstract;
using MetaWeather.Orchestration.Concrete;
using System.Threading.Tasks;
using MetaWeather.Entity;

namespace MetaWeather.Orchestration.Test
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
        public async Task GetLocationByTownOrCountryName_Should_True()
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
            var getresult = await metaWeatherOrchestration.GetLocationByTownOrCountryName(townName); //metaWeatherOrchestrationMock.Setup(x => x.GetLocationByTownOrCountryName(It.IsAny<string>())).ReturnsAsync(listLocationSearchResponse);
            //Assert
            Assert.Equal(townName.ToLower(), getresult[0].title);
        }
    }
}
