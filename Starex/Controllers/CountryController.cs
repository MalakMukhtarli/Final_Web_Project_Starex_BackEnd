﻿using Buisness.Abstract;
using Entity.Entities.Addresses;
using Entity.Entities.Contacts;
using Entity.Entities.Countries;
using Entity.Entities.Stores;
using Entity.Entities.Tariffs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starex.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        private readonly IAddressService _contextAddress;
        private readonly IWebHostEnvironment _env;
        public CountryController(ICountryService countryService,
                                 ICountryContactService contextContact,
                                 ITariffService contextTariff,
                                 IStoreService contextStore,
                                 IAddressService contextAddress,
                                 IWebHostEnvironment env)
        {
            _context = countryService;
            _contextContact = contextContact;
            _contextTariff = contextTariff;
            _contextStore = contextStore;
            _contextAddress = contextAddress;
            _env = env; 
        }

        // GET: api/<CountryController>
        /// <summary>
        /// where IsDeleted is False, get all Country
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// where IsDeleted is False, Get a Country according to 'Id'
        /// </summary>
        /// <param name="id"> whichever item you want, you should write its 'Id' </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            try
            {
                Country country = await _context.GetWithId(id);
                if (country == null) return StatusCode(StatusCodes.Status404NotFound);
                List<CountryContact> countryContacts = await _contextContact.GetAll();
                foreach (CountryContact contact in countryContacts)
                {
                    if (contact.CountryId == country.Id)
                    {
                        country.CountryContacts = contact;
                    }
                }
                return Ok(country);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<CountryController>
        /// <summary>
        /// For create Country and CountryContact
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] Country country)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                if (!country.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                if (!country.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                country.Image = await country.Photo.AddImageAsync(_env.WebRootPath, "img");

                await _context.Add(country);
                CountryContact contact = new CountryContact
                {
                    Address = country.CountryContacts.Address,
                    Time = country.CountryContacts.Time,
                    IsDeleted = false,
                    CountryId = country.Id
                };
                await _contextContact.Add(contact);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<CountryController>/5
        /// <summary>
        /// For update Country and CountryContact
        /// </summary>
        /// <param name="id"> whichever item you want, you should write its 'Id' </param>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] Country country)
        {
            try
            {
                Country countryDb = await _context.GetWithId(id);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                countryDb.Name = country.Name;
                countryDb.HasLiquid = country.HasLiquid;
                if (country.Photo != null)
                {
                    if (!country.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                    if (!country.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                    countryDb.Image = await country.Photo.AddImageAsync(_env.WebRootPath, "img");
                }

                List<CountryContact> allContacts = await _contextContact.GetAll();
                foreach (CountryContact contact in allContacts)
                {
                    if (contact.CountryId == countryDb.Id)
                    {
                        contact.Time = countryDb.CountryContacts.Time;
                        contact.Address = countryDb.CountryContacts.Address;
                        await _contextContact.Update(contact);
                    }
                }
                await _context.Update(countryDb);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<CountryController>/5
        /// <summary>
        /// Delete for Country and relations (CountryContact, Address, Store and Tariff)
        /// </summary>
        /// <param name="id"> whichever item you want, you should write its 'Id' </param>
        /// <returns></returns>
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
                        await _contextContact.Update(contact);
                    }
                }

                List<Address> allAddress = await _contextAddress.GetAll();
                foreach (Address address in allAddress)
                {
                    if (address.CountryId == countryDb.Id)
                    {
                        await _contextContact.Delete(id);
                    }
                }

                List<Store> allStores = await _contextStore.GetAll();
                foreach (Store store in allStores)
                {
                    if (store.CountryId == countryDb.Id)
                    {
                        store.IsDeleted = true;
                        await _contextStore.Update(store);
                    }
                }

                List<Tariff> allTariffs = await _contextTariff.GetAll();
                foreach (Tariff tariff in allTariffs)
                {
                    if (tariff.CountryId == countryDb.Id)
                    {
                        tariff.IsDeleted = true;
                        await _contextTariff.Update(tariff);
                    }
                }
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
