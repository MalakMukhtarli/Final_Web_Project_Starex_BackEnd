using Buisness.Abstract;
using Entity.Entities.Bios;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Starex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiosController : ControllerBase
    {

        private readonly IBioService _context;
        private readonly IWebHostEnvironment _env;

        public BiosController(IBioService bioService, IWebHostEnvironment env)
        {
            _context = bioService;
            _env = env;
        }
        // GET: api/<BiosController>
        [HttpGet]
        public async Task<ActionResult<List<Bio>>> Get()
        {
            try
            {
                List<Bio> bio = await _context.GetAll();
                return Ok(bio);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<BiosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bio>> Get(int id)
        {
            try
            {
                Bio bio = await _context.GetWithId(id);
                if (bio == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(bio);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<BiosController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Bio bio)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                await _context.Add(bio);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<BiosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Bio bio)
        {
            try
            {
                Bio dbBio = await _context.GetWithId(id);
                if(dbBio == null) return BadRequest();

                dbBio.Address = bio.Address;
                dbBio.Contact = bio.Contact;
                dbBio.WorkTime = bio.WorkTime;

                //Will be image
                if (bio.PhotoHeader == null) return BadRequest();

                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<BiosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Bio dbBio = await _context.GetWithId(id);
                if (dbBio == null) return BadRequest();
                await _context.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
