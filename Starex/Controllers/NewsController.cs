using Buisness.Abstract;
using Entity.Entities.Newss;
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
    public class NewsController : ControllerBase
    {
        private readonly INewsService _context;

        public NewsController(INewsService context)
        {
            _context = context;
        }
        // GET: api/<NewsController>
        [HttpGet]
        public async Task<ActionResult<List<News>>> Get()
        {
            try
            {
                List<News> news = await _context.GetAll();
                return Ok(news);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> Get(int id)
        {
            try
            {
                News news = await _context.GetWithId(id);
                if (news == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<NewsController>
        [HttpPost]
        public async Task<ActionResult> Post(News news)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                await _context.Add(news);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<NewsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] News news)
        {
            try
            {
                News dbNews = await _context.GetWithId(id);
                if (dbNews == null) return BadRequest();

                dbNews.Title = news.Title;
                dbNews.Date = news.Date;
                news.CreatedTime = DateTime.Now;
                dbNews.CreatedTime = news.CreatedTime;
                //will be image extention
                await _context.Update(dbNews);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                News dbNews = await _context.GetWithId(id);
                if (dbNews == null) return BadRequest();
                dbNews.IsDeleted = true;
                dbNews.NewsDetail.IsDeleted = true;
                await _context.Update(dbNews);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
