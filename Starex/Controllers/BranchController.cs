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
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _context;
        private readonly IBranchContactService _contextContact;
        private readonly IDistrictTariffService _contextTariff;
        private readonly ICityService _contextCity;
        public BranchController(IBranchService context, 
                                IBranchContactService contextContact, 
                                IDistrictTariffService contextTariff,
                                ICityService contextCity)
        {
            _context = context;
            _contextContact = contextContact;
            _contextTariff = contextTariff;
            _contextCity = contextCity;
        }
        // GET: api/<BranchController> 

        [HttpGet]
        public async Task<ActionResult<List<Branch>>> Get()
        {
            try
            {
                List<Branch> branchs = await _context.GetAll();
                foreach (var item in branchs)
                {
                    City citydB = await _contextCity.GetWithId(item.CityId);
                    if (citydB == null) return StatusCode(StatusCodes.Status404NotFound);
                    item.City = citydB;
                }
                return Ok(branchs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<BranchController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                Branch branch = await _context.GetWithId(id);
                if (branch == null) return StatusCode(StatusCodes.Status404NotFound);
                City citydB = await _contextCity.GetWithId(branch.CityId);
                branch.City = citydB;
                List<BranchContact> branchContacts = await _contextContact.GetAll();
                foreach (BranchContact contact in branchContacts)
                {
                    if (contact.BranchId == branch.Id)
                    {
                        branch.BranchContacts = contact;
                    }
                }
                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<BranchController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Branch branch)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                City cityDb = await _contextCity.GetWithId(branch.CityId);
                if (cityDb == null) return StatusCode(StatusCodes.Status404NotFound);
                await _context.Add(branch);

                BranchContact contact = new BranchContact
                {
                    Address = branch.BranchContacts.Address,
                    Phone = branch.BranchContacts.Phone,
                    Time = branch.BranchContacts.Time,
                    Map = branch.BranchContacts.Map,
                    IsDeleted = false,
                    BranchId = branch.Id,
                };
                await _contextContact.Add(contact);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<BranchController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Branch branch)
        {
            try
            {
                Branch branchDb = await _context.GetWithId(id);
                if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);
                City cityDb = await _contextCity.GetWithId(branch.CityId);
                if (cityDb == null) return StatusCode(StatusCodes.Status404NotFound);
                branchDb.Name = branch.Name;
                branchDb.CityId = branch.CityId;


                await _context.Update(branchDb);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // DELETE api/<BranchController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Branch branchDb = await _context.GetWithId(id);
                if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);
                branchDb.IsDeleted = true;

                List<BranchContact> allContacts = await _contextContact.GetAll();
                foreach (BranchContact contact in allContacts)
                {
                    if (contact.BranchId == branchDb.Id)
                    {
                        contact.IsDeleted = true;
                        await _contextContact.Update(contact);
                    }
                }

                List<DistrictTariff> allTariffs = await _contextTariff.GetAll();
                foreach (DistrictTariff tariff in allTariffs)
                {
                    if (tariff.BranchId == branchDb.Id)
                    {
                        tariff.IsDeleted = true;
                        await _contextTariff.Update(tariff);
                    }
                }

                await _context.Update(branchDb);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
