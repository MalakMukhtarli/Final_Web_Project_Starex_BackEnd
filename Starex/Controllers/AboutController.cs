using Buisness.Abstract;
using Entity.Entities;
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
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _context;
        public AboutController(IAboutService context)
        {
            _context = context;
        }
        // GET: api/<AboutController>
        [HttpGet]
        public async Task<ActionResult<List<About>>> Get()
        {
            try
            {
                List<About> abouts = await _context.GetAll();
                return Ok(abouts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<About>> Get(int id)
        {
            try
            {
                About about = await _context.GetWithId(id);
                if(about == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(about);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] About about)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                await _context.Add(about);
                return Ok();
            }
            catch (Exception e) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<AboutController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] About about)
        {
            try
            {
                About dbAbout = await _context.GetWithId(id);
                if (dbAbout == null) return BadRequest();

                dbAbout.Description = about.Description;
                dbAbout.Title = about.Title;
                dbAbout.Content = about.Content;

                await _context.Update(dbAbout);
                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<AboutController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                About about = await _context.GetWithId(id);
                if (about == null) return StatusCode(StatusCodes.Status404NotFound);
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
