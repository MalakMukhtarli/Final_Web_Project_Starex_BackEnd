﻿using Buisness.Abstract;
using Entity.Entities.Countries;
using Entity.Entities.Stores;
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
    public class StoreController : ControllerBase
    {

        private readonly IStoreService _context;
        private readonly ICountryService _contextCountry;

        public StoreController(IStoreService storeService,
                               ICountryService contextCountry)
        {
            _context = storeService;
            _contextCountry = contextCountry;
        }

        // GET: api/<StoreController>
        [HttpGet]
        public async Task<ActionResult<List<Store>>> Get()
        {
            try
            {
                List<Store> stores = await _context.GetAll();
                return Ok(stores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            };
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> Get(int id)
        {
            try
            {
                Store store = await _context.GetWithId(id);
                if (store == null) return StatusCode(StatusCodes.Status404NotFound);
                Country country = await _contextCountry.GetWithId(store.CountryId);
                if (country == null) return StatusCode(StatusCodes.Status404NotFound);
                store.Country = country;
                return Ok(store);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<StoreController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Store store)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                Country countryDb = await _contextCountry.GetWithId(store.CountryId);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                await _context.Add(store);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Store store)
        {
            try
            {
                Store dbStore = await _context.GetWithId(id);
                if (dbStore == null) return BadRequest();
                Country countryDb = await _contextCountry.GetWithId(store.CountryId);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);

                dbStore.Link = store.Link;
                dbStore.Name = store.Name;
                dbStore.CountryId = store.CountryId;

                //will be image

                await _context.Update(dbStore);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Store dbStore = await _context.GetWithId(id);
                if (dbStore == null) return BadRequest();

                dbStore.IsDeleted = true;
                await _context.Update(dbStore);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
