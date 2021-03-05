using Buisness.Abstract;
using Entity.Entities.Titles;
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
    public class TitleController : ControllerBase
    {
        private readonly ITitleService _context;
        public TitleController(ITitleService context)
        {
            _context = context;
        }

        // GET: api/<TitleController>
        [HttpGet]
        public async Task<ActionResult<List<Title>>> Get()
        {
            try
            {
                List<Title> titles = await _context.GetAll();
                return Ok(titles);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<TitleController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Title>> Get(int id)
        {
            try
            {
                Title title = await _context.GetWithId(id);
                if (title == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(title);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<TitleController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Title title)
        {
            try
            {
                Title titleDb = await _context.GetWithId(id);
                if (titleDb == null) return StatusCode(StatusCodes.Status404NotFound);

                titleDb.Description = title.Description;
                titleDb.Header = title.Header;
                await _context.Update(titleDb);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
