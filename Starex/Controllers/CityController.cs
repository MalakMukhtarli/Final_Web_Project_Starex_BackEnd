using Buisness.Abstract;
using Entity.Entities.Cities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Starex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityContext;
        public CityController(ICityService cityContext)
        {
            _cityContext = cityContext;
        }
        // GET: api/<CityController>
        [HttpGet]
        public IActionResult Get()
        {
            List<City> cities = _cityContext.GetAllCity();
            return Ok(cities);
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            City city = _cityContext.GetCityWithId(id);
            if (city == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(city);
        }

        // POST api/<CityController>
        [HttpPost]
        public IActionResult Create([FromBody] City city)
        {
            if (!ModelState.IsValid) return BadRequest();
            _cityContext.Add(city);
            return Ok();
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] City city)
        {
            City cityDb = _cityContext.GetCityWithId(id);
            if (cityDb == null) return StatusCode(StatusCodes.Status404NotFound);
            cityDb.Name = city.Name;
            _cityContext.Update(cityDb);
            return Ok();
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            City cityDb = _cityContext.GetCityWithId(id);
            if (cityDb == null) return StatusCode(StatusCodes.Status404NotFound);
            cityDb.IsDeleted = true;
            _cityContext.Delete(id);
            return Ok();
        }
    }
}
