using Buisness.Abstract;
using Entity.Entities.Contacts;
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
    public class BranchContactController : ControllerBase
    {
        private readonly IBranchContactService _context;
        public BranchContactController(IBranchContactService context)
        {
            _context = context;
        }
        // GET: api/<BranchContactController>
        [HttpGet]
        public async Task<ActionResult<List<BranchContact>>> Get()
        {
            List<BranchContact> contacts = await _context.GetAll();
            return Ok(contacts);
        }

        // GET api/<BranchContactController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BranchContact>> Get(int id)
        {
            BranchContact contact = await _context.GetWithId(id);
            if (contact == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(contact);
        }

        // POST api/<BranchContactController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BranchContact contact)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _context.Add(contact);
            return Ok();
        }

        // PUT api/<BranchContactController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] BranchContact contact)
        {
            BranchContact contactDb = await _context.GetWithId(id);
            if (contactDb == null) return StatusCode(StatusCodes.Status404NotFound);
            contactDb.Address = contact.Address;
            contactDb.Phone = contact.Phone;
            contactDb.Time = contact.Time;
            contactDb.Map = contact.Map;
            contactDb.BranchId = contact.BranchId;
            await _context.Update(contactDb);
            return Ok();
        }

        // DELETE api/<BranchContactController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            BranchContact contactDb = await _context.GetWithId(id);
            if (contactDb == null) return StatusCode(StatusCodes.Status404NotFound);
            contactDb.IsDeleted = true;
            await _context.Update(contactDb);
            return Ok();
        }
    }
}
