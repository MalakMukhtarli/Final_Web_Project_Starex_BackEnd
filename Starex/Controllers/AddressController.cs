using Buisness.Abstract;
using Entity.Entities.Addresses;
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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _context;
        private readonly ICountryService _contextCountry;

        public AddressController(IAddressService addressService,
                                 ICountryService contextCountry)
        {
            _context = addressService;
            _contextCountry = contextCountry;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public async Task<ActionResult<List<Address>>> Get()
        {
            try
            {
                List<Address> addresses = await _context.GetAll();
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Get(int id)
        {
            try
            {
                Address address = await _context.GetWithId(id);
                if (address == null) return StatusCode(StatusCodes.Status404NotFound);
                Country countryDb = await _contextCountry.GetWithId(address.CountryId);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                address.Country = countryDb;
                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<AddressController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Address address)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                Country countryDb = await _contextCountry.GetWithId(address.CountryId);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                await _context.Add(address);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Address address)
        {
            try
            {
                Address dbAddress = await _context.GetWithId(id);
                if (dbAddress == null) return BadRequest();
                Country countryDb = await _contextCountry.GetWithId(address.CountryId);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);

                dbAddress.AddressFirst = address.AddressFirst;
                dbAddress.AddressSecond = address.AddressSecond;
                dbAddress.City = address.City;
                dbAddress.CountryName = address.CountryName;
                dbAddress.Phone = address.Phone;
                dbAddress.Region = address.Region;
                dbAddress.Zip = address.Zip;
                dbAddress.CountryId = address.CountryId;

                await _context.Update(dbAddress);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Address address = await _context.GetWithId(id);
                if (address == null) return StatusCode(StatusCodes.Status404NotFound);
                await _context.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
