using Buisness.Abstract;
using Entity.Entities.HomePages;
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
    public class IntroController : ControllerBase
    {
        private readonly IIntroService _context;

        public IntroController(IIntroService introService)
        {
            _context = introService;
        }

        // GET: api/<IntroController>
        [HttpGet]
        public async Task<ActionResult<List<Intro>>> Get()
        {
            try
            {
                List<Intro> intros = await _context.GetAll();
                return Ok(intros);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<IntroController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intro>> Get(int id)
        {
            try
            {
                Intro intro = await _context.GetWithId(id);
                if (intro == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(intro);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<IntroController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Intro intro)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                await _context.Add(intro);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<IntroController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Intro intro)
        {
            try
            {
                Intro dbIntro = await _context.GetWithId(id);
                if (dbIntro == null) return BadRequest();

                dbIntro.Title = intro.Title;
                await _context.Update(dbIntro);
                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<IntroController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Intro dbIntro = await _context.GetWithId(id);
                if (dbIntro == null) return BadRequest();
                await _context.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
