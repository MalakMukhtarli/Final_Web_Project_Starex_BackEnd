using Buisness.Abstract;
using Entity.Entities.Contacts;
using Entity.Entities.Countries;
using Entity.Entities.Stores;
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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _context;
        private readonly ICountryContactService _contextContact;
        private readonly ITariffService _contextTariff;
        private readonly IStoreService _contextStore;
        public CountryController(ICountryService countryService,
                                 ICountryContactService contextContact,
                                 ITariffService contextTariff,
                                 IStoreService contextStore)
        {
            _context = countryService;
            _contextContact = contextContact;
            _contextTariff = contextTariff;
            _contextStore = contextStore;
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

                List<CountryContact> allContacts = await _contextContact.GetAll();
                foreach (CountryContact contact in allContacts)
                {
                    if (contact.CountryId == countryDb.Id)
                    {
                        contact.IsDeleted = true;
                    }
                    await _contextContact.Update(contact);
                }

                List<Store> allStores = await _contextStore.GetAll();
                foreach (Store store in allStores)
                {
                    if (store.CountryId == countryDb.Id)
                    {
                        store.IsDeleted = true;
                    }
                    await _contextStore.Update(store);
                }

                List<Tariff> allTariffs = await _contextTariff.GetAll();
                foreach (Tariff tariff in allTariffs)
                {
                    if (tariff.CountryId == countryDb.Id)
                    {
                        tariff.IsDeleted = true;
                    }
                    await _contextTariff.Update(tariff);
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
