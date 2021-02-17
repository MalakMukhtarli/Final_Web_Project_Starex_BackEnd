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
        private readonly IDistrictTariffService _context;
        public DistrictTariffController(IDistrictTariffService context)
        {
            _context = context;
        }
        // GET: api/<DistrictTariffController>
        [HttpGet]
        public async Task<ActionResult<List<DistrictTariff>>> Get()
        {
            List<DistrictTariff> tariffs = await _context.GetAll();
            return Ok(tariffs);
        }

        // GET api/<DistrictTariffController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictTariff>> Get(int id)
        {
            DistrictTariff tariff = await _context.GetWithId(id);
            if (tariff == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(tariff);
        }

        // POST api/<DistrictTariffController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DistrictTariff tariff)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _context.Add(tariff);
            return Ok();

        }

        // PUT api/<DistrictTariffController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] DistrictTariff tariff)
        {
            DistrictTariff tariffDb =await _context.GetWithId(id);
            if (tariffDb == null) return StatusCode(StatusCodes.Status404NotFound);
            tariffDb.District = tariff.District;
            tariffDb.Price = tariff.Price;
            await _context.Update(tariffDb);
            return Ok();
        }

        // DELETE api/<DistrictTariffController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            DistrictTariff tariffDb = await _context.GetWithId(id);
            if (tariffDb == null) return StatusCode(StatusCodes.Status404NotFound);
            tariffDb.IsDeleted = true;
            await _context.Update(tariffDb);
            return Ok();
        }
    }
}
