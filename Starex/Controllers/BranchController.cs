using Buisness.Abstract;
using Entity.Entities.Branches;
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
        public BranchController(IBranchService context)
        {
            _context = context;
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
            foreach (var contact in branchDb.BranchContacts)
            {
                contact.IsDeleted = true;
            }
            foreach (var tariff in branchDb.DistrictTariffs)
            {
                tariff.IsDeleted = true;
            }
            await _context.Update(branchDb);
            return Ok();
        }
    }
}
