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
        private readonly INewsDetailService _contextDetail;
        private readonly IWebHostEnvironment _env;

        public NewsController(INewsService context,
                              INewsDetailService contextDetail,
                              IWebHostEnvironment env)
        {
            _context = context;
            _contextDetail = contextDetail;
            _env = env;
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
                List<NewsDetail> newsDetails = await _contextDetail.GetAll();
                foreach (NewsDetail detail in newsDetails)
                {
                    if (news.Id == detail.NewsId)
                    {
                        news.NewsDetail = detail;
                    }
                }
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
                if (!news.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                if (!news.NewsDetail.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                if (!news.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                if (!news.NewsDetail.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                news.Image = await news.Photo.AddImageAsync(_env.WebRootPath, "img");
                news.NewsDetail.ImageBig = await news.NewsDetail.Photo.AddImageAsync(_env.WebRootPath, "img");
                await _context.Add(news);
                NewsDetail detail = new NewsDetail
                {
                    Content=news.NewsDetail.Content,
                    ImageBig=news.NewsDetail.ImageBig                    
                };
                await _contextDetail.Add(detail);
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
                if (news.Photo!=null)
                {
                    if (!news.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                    if (!news.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                    dbNews.Image = await news.Photo.AddImageAsync(_env.WebRootPath, "img");
                }
                if (news.NewsDetail.Photo != null)
                {
                    if (!news.NewsDetail.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                    if (!news.NewsDetail.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                    dbNews.NewsDetail.ImageBig = await news.NewsDetail.Photo.AddImageAsync(_env.WebRootPath, "img");
                }
                List<NewsDetail> allDetail = await _contextDetail.GetAll();
                foreach (NewsDetail detail in allDetail)
                {
                    if (detail.NewsId == dbNews.Id)
                    {
                        detail.Content = dbNews.NewsDetail.Content;
                        detail.ImageBig = dbNews.NewsDetail.ImageBig;
                        await _contextDetail.Update(detail);
                    }
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
                await _contextDetail.Update(dbNews.NewsDetail);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
