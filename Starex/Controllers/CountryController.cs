using Buisness.Abstract;
using Entity.Entities.Countries;
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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _context;
        public CountryController(ICountryService countryService)
        {
            _context = countryService;
        }
        // GET: api/<CountryController>
        [HttpGet]
        public async Task<ActionResult<List<Country>>> Get()
        {
            try
            {
                List<Country> country = await _context.GetAll();
                return Ok(country);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            try
            {
                Country country = await _context.GetWithId(id);
                if (country == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(country);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<CountryController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Country country)
        {
            try
            {
                // SHEKIL UCUN EXTANSION ELAVE OLUNACAQ
                if (!ModelState.IsValid) return BadRequest();
                await _context.Add(country);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Country country)
        {
            try
            {
                Country countryDb = await _context.GetWithId(id);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                //countryDb.Image = country.Image;
                countryDb.Name = country.Name;
                countryDb.HasLiquid = country.HasLiquid;
                await _context.Update(countryDb);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Country countryDb = await _context.GetWithId(id);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                countryDb.IsDeleted = true;
                foreach (var item in countryDb.Tariffs)
                {
                    item.IsDeleted = true;
                }
                foreach (var item in countryDb.CountryContacts)
                {
                    item.IsDeleted = true;
                }
                // SHEKIL SILMEK YAZILACAQ
                await _context.Update(countryDb);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
