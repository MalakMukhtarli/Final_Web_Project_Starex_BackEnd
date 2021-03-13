using Buisness.Abstract;
using Entity.Entities.Newss;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starex.Extension;
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
        private readonly IWebHostEnvironment _env;

        public NewsController(INewsService context,
                              IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET: api/<NewsController>
        /// <summary>
        /// where IsDeleted is False, get all News
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<News>>> Get()
        {
            try
            {
                List<News> news = await _context.GetAllOrder();
                return Ok(news);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<NewsController>/5
        /// <summary>
        /// where IsDeleted is False, Get a News according to 'Id'
        /// </summary>
        /// <param name="id">whichever item you want, you should write its 'Id'</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> Get(int id)
        {
            try
            {
                News news = await _context.GetWithId(id);
                if (news == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(news);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<NewsController>
        /// <summary>
        /// For create News
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] News news)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                if (!news.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                if (!news.PhotoBig.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                if (!news.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                if (!news.PhotoBig.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                news.Image = await news.Photo.AddImageAsync(_env.WebRootPath, "img");
                news.ImageBig = await news.PhotoBig.AddImageAsync(_env.WebRootPath, "img");
                await _context.Add(news);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<NewsController>/5
        /// <summary>
        /// For update News
        /// </summary>
        /// <param name="id">whichever item you want, you should write its 'Id'</param>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] News news)
        {
            try
            {
                News dbNews = await _context.GetWithId(id);
                if (dbNews == null) return BadRequest();

                dbNews.Title = news.Title;
                dbNews.Date = news.Date;
                dbNews.Content = dbNews.Content;
                news.CreatedTime = DateTime.Now;
                dbNews.CreatedTime = news.CreatedTime;
                if (news.Photo!=null)
                {
                    if (!news.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                    if (!news.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                    dbNews.Image = await news.Photo.AddImageAsync(_env.WebRootPath, "img");
                }
                if (news.PhotoBig != null)
                {
                    if (!news.PhotoBig.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                    if (!news.PhotoBig.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                    dbNews.ImageBig = await news.PhotoBig.AddImageAsync(_env.WebRootPath, "img");
                }
                
                await _context.Update(dbNews);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<NewsController>/5
        /// <summary>
        /// For delete News
        /// </summary>
        /// <param name="id">whichever item you want, you should write its 'Id'</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                News dbNews = await _context.GetWithId(id);
                if (dbNews == null) return BadRequest();
                dbNews.IsDeleted = true;
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
