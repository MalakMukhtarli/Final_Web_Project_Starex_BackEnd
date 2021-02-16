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
        private readonly IBranchService _branchContext;
        public BranchController(IBranchService branchContext)
        {
            _branchContext = branchContext;
        }
        // GET: api/<BranchController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Branch> branch = _branchContext.GetAllBranch();
            return Ok(branch);
        }

        // GET api/<BranchController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Branch branch = _branchContext.GetBranchWithId(id);
            if (branch == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(branch);
        }

        // POST api/<BranchController>
        [HttpPost]
        public IActionResult Create([FromBody] Branch branch)
        {
            if (!ModelState.IsValid) return BadRequest();
            _branchContext.Add(branch);
            return Ok();
        }

        // PUT api/<BranchController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Branch branch)
        {
            Branch branchDb = _branchContext.GetBranchWithId(id);
            if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);
            branchDb.Name = branch.Name;
            _branchContext.Update(branchDb);
            return Ok();
        }

        // DELETE api/<BranchController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Branch branchDb = _branchContext.GetBranchWithId(id);
            if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);
            branchDb.IsDeleted = true;
            _branchContext.Delete(id);
            return Ok();
        }
    }
}
