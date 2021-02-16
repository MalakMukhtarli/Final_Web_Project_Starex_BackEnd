using Buisness.Abstract;
using Entity.Entities.Service;
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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceContext;
        
        public ServiceController(IServiceService serviceContext)
        {
            _serviceContext = serviceContext;
        }
        // GET: api/<ServiceController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Service> services = _serviceContext.GetAllService();
                return Ok(services);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Service service = _serviceContext.GetServiceWithId(id);
                if (service == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(service);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/<ServiceController>
        [HttpPost]
        public IActionResult Create([FromBody] Service service)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                _serviceContext.Add(service);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Service service)
        {
            try
            {
                Service serviceDb = _serviceContext.GetServiceWithId(id);
                if (serviceDb == null) return StatusCode(StatusCodes.Status404NotFound);

                serviceDb.Description = service.Description;
                serviceDb.Title = service.Title;
                serviceDb.VideoLink = service.VideoLink;
                _serviceContext.Update(serviceDb);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Service serviceDb = _serviceContext.GetServiceWithId(id);
                if (serviceDb == null) return StatusCode(StatusCodes.Status404NotFound);
                serviceDb.IsDeleted = true;
                _serviceContext.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }
    }
}
