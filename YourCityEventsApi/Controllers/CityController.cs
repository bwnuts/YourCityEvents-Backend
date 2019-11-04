using Microsoft.AspNetCore.Mvc;
using YourCityEventsApi.Services;
using System.Collections.Generic;
using YourCityEventsApi.Model;

namespace YourCityEventsApi.Controllers
{
    [Route("api/[controller]")]
    public class CityController:ControllerBase
    {
        private readonly CityService _cityService;

        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }
        
        [HttpGet]
        public ActionResult<List<CityModel>> Get() =>
            _cityService.Get();

        [HttpGet("{id}")]
        public ActionResult<CityModel> Get(string id)
        {
            var city = _cityService.Get(id);
            if (city == null)
                return NotFound();
            return city;
        }

        [HttpPost]
        public ActionResult<CityModel> Create(CityModel cityModel)
        {
            return _cityService.Create(cityModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id,CityModel cityModel)
        {
            var city = _cityService.Get(id);
            if (city == null)
            {
                return NotFound();
            }
            _cityService.Update(id, cityModel);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var city = _cityService.Get(id);
            if (city == null)
                return NotFound();
            _cityService.Delete(city.Id);
            return Ok();
        }
    }
}