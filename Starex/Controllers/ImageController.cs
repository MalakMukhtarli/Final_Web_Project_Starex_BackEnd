using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starex.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Starex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public ImageController(IWebHostEnvironment env)
        {
            _env = env;
        }
        // GET api/<ImageController>/5
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                if (name == null) return StatusCode(StatusCodes.Status404NotFound);
                string path = Path.Combine(_env.WebRootPath, "img", name);
                Byte[] b = System.IO.File.ReadAllBytes(path);
                return File(b, "image/jpeg");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<ImageController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile photo)
        {
            try
            {
                photo.OpenReadStream();
                string photoName = await photo.AddImageAsync(_env.WebRootPath, "img");
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
