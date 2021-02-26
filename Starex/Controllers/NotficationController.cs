using Buisness.Abstract;
using Entity.Entities.Notfications;
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
    public class NotficationController : ControllerBase
    {
        private readonly INotficationService _context;

        public NotficationController(INotficationService notficationService)
        {
            _context = notficationService;
        }

        // GET: api/<NotficationController>
        [HttpGet]
        public async Task<ActionResult<List<Notfication>>> Get()
        {
            try
            {
                List<Notfication> notfications = await _context.GetAll();
                return Ok(notfications);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<NotficationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notfication>> Get(int id)
        {
            try
            {
                Notfication notfication = await _context.GetWithId(id);
                if (notfication == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(notfication);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<NotficationController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Notfication notfication)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                notfication.Date = DateTime.Now;
                await _context.Add(notfication);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<NotficationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Notfication notfication)
        {
            try
            {
                Notfication dbNotfication = await _context.GetWithId(id);
                if (dbNotfication == null) return BadRequest();
                
                dbNotfication.IsDeliver = notfication.IsDeliver;
                dbNotfication.IsStartDeliver = notfication.IsStartDeliver;
                dbNotfication.IsStored = notfication.IsStored;
                
                if (!dbNotfication.IsDeliver && !dbNotfication.IsStartDeliver && !dbNotfication.IsStored)
                    dbNotfication.IsStartDeliver = true;
 
                await _context.Update(dbNotfication);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<NotficationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Notfication dbNotfication = await _context.GetWithId(id);
                if (dbNotfication == null) return BadRequest();
                dbNotfication.IsDeleted = true;
                await _context.Update(dbNotfication);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
