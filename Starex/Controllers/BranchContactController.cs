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
        private readonly IBranchContactService _contactContext;
        public BranchContactController(IBranchContactService contactContext)
        {
            _contactContext = contactContext;
        }
        // GET: api/<BranchContactController>
        [HttpGet]
        public IActionResult Get()
        {
            List<BranchContact> contacts = _contactContext.GetAllContact();
            return Ok(contacts);
        }

        // GET api/<BranchContactController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            BranchContact contact = _contactContext.GetContactWithId(id);
            if (contact == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(contact);
        }

        // POST api/<BranchContactController>
        [HttpPost]
        public IActionResult Create([FromBody] BranchContact contact)
        {
            if (!ModelState.IsValid) return BadRequest();
            _contactContext.Add(contact);
            return Ok();
        }

        // PUT api/<BranchContactController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BranchContact contact)
        {
            BranchContact contactDb = _contactContext.GetContactWithId(id);
            if (contactDb == null) return StatusCode(StatusCodes.Status404NotFound);
            contactDb.Address = contact.Address;
            contactDb.Phone = contact.Phone;
            contactDb.Time = contact.Time;
            contactDb.Map = contact.Map;
            _contactContext.Update(contactDb);
            return Ok();
        }

        // DELETE api/<BranchContactController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            BranchContact contactDb = _contactContext.GetContactWithId(id);
            if (contactDb == null) return StatusCode(StatusCodes.Status404NotFound);
            contactDb.IsDeleted = true;
            _contactContext.Delete(id);
            return Ok();
        }
    }
}
