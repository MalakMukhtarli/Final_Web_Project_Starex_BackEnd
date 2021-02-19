using Buisness.Abstract;
using Entity.Entities.Branches;
using Entity.Entities.Cities;
using Entity.Entities.Contacts;
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
    public class CityController : ControllerBase
    {
        private readonly ICityService _context;
        private readonly IBranchService _contextBranch;
        private readonly IBranchContactService _contextContact;
        private readonly IDistrictTariffService _contextTariff;
        public CityController(ICityService context,
                              IBranchService contextBranch,
                              IBranchContactService contextContact,
                              IDistrictTariffService contextTariff)
        {
            _context = context;
            _contextBranch = contextBranch;
            _contextContact = contextContact;
            _contextTariff = contextTariff;
        }
        // GET: api/<CityController>
        [HttpGet]
        public async Task<ActionResult<List<City>>> Get()
        {
            List<City> cities = await _context.GetAll();
            return Ok(cities);
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> Get(int id)
        {
            City city = await _context.GetWithId(id);
            if (city == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(city);
        }

        // POST api/<CityController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] City city)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _context.Add(city);
            return Ok();
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] City city)
        {
            City cityDb = await _context.GetWithId(id);
            if (cityDb == null) return StatusCode(StatusCodes.Status404NotFound);
            cityDb.Name = city.Name;
            await _context.Update(cityDb);
            return Ok();
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            City cityDb = await _context.GetWithId(id);
            if (cityDb == null) return StatusCode(StatusCodes.Status404NotFound);
            cityDb.IsDeleted = true;

            List<Branch> allBranch = await _contextBranch.GetAll();
            Branch branchDb = null;
            foreach (Branch branch in allBranch)
            {
                if (branch.CityId == cityDb.Id)
                {
                    branch.IsDeleted = true;
                    branchDb = branch;
                }
                await _contextBranch.Update(branch);

                List<BranchContact> allContacts = await _contextContact.GetAll();
                foreach (BranchContact contact in allContacts)
                {
                    if (contact.BranchId == branchDb.Id)
                    {
                        contact.IsDeleted = true;
                    }
                    await _contextContact.Update(contact);
                }

                List<DistrictTariff> allTariffs = await _contextTariff.GetAll();
                foreach (DistrictTariff tariff in allTariffs)
                {
                    if (tariff.BranchId == branchDb.Id)
                    {
                        tariff.IsDeleted = true;
                    }
                    await _contextTariff.Update(tariff);
                }
            }

            await _context.Update(cityDb);
            return Ok();
        }
    }
}
