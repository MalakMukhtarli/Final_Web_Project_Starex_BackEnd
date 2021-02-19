using Buisness.Abstract;
using Entity.Entities.Branches;
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
        public BranchController(IBranchService context, 
                                IBranchContactService contextContact, 
                                IDistrictTariffService contextTariff)
        {
            _context = context;
            _contextContact = contextContact;
            _contextTariff = contextTariff;
        }
        // GET: api/<BranchController> 

        [HttpGet]
        public async Task<ActionResult<List<Branch>>> Get()
        {
            List<Branch> branchs = await _context.GetAll();
            return Ok(branchs);
        }

        // GET api/<BranchController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Branch branch = await _context.GetWithId(id);
            if (branch == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(branch);
        }

        // POST api/<BranchController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Branch branch)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _context.Add(branch);
            return Ok();
        }

        // PUT api/<BranchController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Branch branch)
        {
            Branch branchDb = await _context.GetWithId(id);
            if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);
            branchDb.Name = branch.Name;
            branchDb.CityId = branch.CityId;
            await _context.Update(branchDb);
            return Ok();
        }

        // DELETE api/<BranchController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
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

            await _context.Update(branchDb);
            return Ok();
        }
    }
}
