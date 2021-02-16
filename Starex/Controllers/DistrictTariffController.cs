using Buisness.Abstract;
using Entity.Entities.Tariffs;
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
    public class DistrictTariffController : ControllerBase
    {
        private readonly IDistrictTariffService _tariffContext;
        public DistrictTariffController(IDistrictTariffService tariffContext)
        {
            _tariffContext = tariffContext;
        }
        // GET: api/<DistrictTariffController>
        [HttpGet]
        public IActionResult Get()
        {
            List<DistrictTariff> tariffs = _tariffContext.GetAllTariff();
            return Ok(tariffs);
        }

        // GET api/<DistrictTariffController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DistrictTariff tariff = _tariffContext.GetTariffWithId(id);
            if (tariff == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(tariff);
        }

        // POST api/<DistrictTariffController>
        [HttpPost]
        public IActionResult Create([FromBody] DistrictTariff tariff)
        {
            if (!ModelState.IsValid) return BadRequest();
            _tariffContext.Add(tariff);
            return Ok();

        }

        // PUT api/<DistrictTariffController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DistrictTariff tariff)
        {
            DistrictTariff tariffDb = _tariffContext.GetTariffWithId(id);
            if (tariffDb == null) return StatusCode(StatusCodes.Status404NotFound);
            tariffDb.District = tariff.District;
            tariffDb.Price = tariff.Price;
            _tariffContext.Update(tariffDb);
            return Ok();
        }

        // DELETE api/<DistrictTariffController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DistrictTariff tariffDb = _tariffContext.GetTariffWithId(id);
            if (tariffDb == null) return StatusCode(StatusCodes.Status404NotFound);
            tariffDb.IsDeleted = true;
            _tariffContext.Delete(id);
            return Ok();
        }
    }
}
