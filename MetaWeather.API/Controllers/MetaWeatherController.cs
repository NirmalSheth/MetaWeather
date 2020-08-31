using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetaWeather.Orchestration.Abstract;

namespace MetaWeather.API.Controllers
{
    [Route("api/[controller]")]
    public class MetaWeatherController : Controller
    {
        private readonly IMetaWeatherOrchestration _metaWeatherOrchestration = null;

        public MetaWeatherController(IMetaWeatherOrchestration metaWeatherOrchestration)
        {
            _metaWeatherOrchestration = metaWeatherOrchestration;
        }
       
        [HttpGet]
        [Route("GetLocationByTownOrCountryName/{townName}")]      
        public async Task<IActionResult> GetLocationByTownOrCountryName(string townName)
        {
            try
            {
                if (string.IsNullOrEmpty(townName))
                    BadRequest("CountryName or TownName Should not be null");

                var getLocationResponse = await _metaWeatherOrchestration.GetLocationByTownOrCountryName(townName);
                if (getLocationResponse?.Count > 0)
                {
                    var result = new { sucess = true, getLocationResponse };
                    return Ok(result);
                }
                return NotFound("No Data found");
            }
            catch (Exception ex)
            {
                var exceptionResponse = new { success = false, Message = ex.Message.ToString() };
                return BadRequest(exceptionResponse);
            }
        }

        [HttpGet]
        [Route("getWeatherByWoeId/{woeId}")]      
        public async Task<IActionResult> GetWeatherByWOEID(int woeId)
        {
            try
            {
                if (woeId == 0)
                    BadRequest("Where On Earth ID(WOEID) Should not be Zero");

                var getLocationResponse = await _metaWeatherOrchestration.GetWeatherByWOEID(woeId);
                if (getLocationResponse?.consolidated_weather.Count > 0)
                {
                    var result = new { sucess = true, getLocationResponse };
                    return Ok(result);
                }
                return NotFound("No Data found");
            }
            catch (Exception ex)
            {
                var exceptionResponse = new { success = false, Message = ex.Message.ToString() };
                return BadRequest(exceptionResponse);
            }
        }

    }
}