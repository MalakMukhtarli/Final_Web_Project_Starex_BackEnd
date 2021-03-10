using Buisness.Abstract;
using Entity.Entities.Countries;
using Entity.Entities.Declarations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starex.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Starex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeclarationController : ControllerBase
    {
        private readonly IDeclarationService _context;
        private readonly ICountryService _contextCountry;
        private readonly IWebHostEnvironment _env;
        public DeclarationController(IDeclarationService context,
                                     ICountryService contextCountry,
                                     IWebHostEnvironment env)
        {
            _context = context;
            _contextCountry = contextCountry;
            _env = env;
        }
        // GET: api/<DeclarationController>
        [HttpGet]
        public async Task<ActionResult<List<Declaration>>> Get()
        {
            try
            {
                List<Declaration> declarations = await _context.GetAll();
                foreach (Declaration dec in declarations)
                {
                    Country countryDb = await _contextCountry.GetWithId(dec.CountryId);
                    if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                    dec.Country = countryDb;
                }
                return Ok(declarations);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<DeclarationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Declaration>> Get(int id)
        {
            try
            {
                Declaration declaration = await _context.GetWithId(id);
                if (declaration == null) return StatusCode(StatusCodes.Status404NotFound);
                Country countryDb = await _contextCountry.GetWithId(declaration.CountryId);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                declaration.Country = countryDb;
                return Ok(declaration);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<DeclarationController>
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] Declaration declaration)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                Country countryDb = await _contextCountry.GetWithId(declaration.CountryId);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                if (!declaration.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                if (!declaration.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                declaration.Image = await declaration.Photo.AddImageAsync(_env.WebRootPath, "img");
                await _context.Add(declaration);    
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<DeclarationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] Declaration declaration)
        {
            try
            {
                Declaration declarationDb = await _context.GetWithId(id);
                if (declarationDb == null) return StatusCode(StatusCodes.Status404NotFound);
                Country countryDb = await _contextCountry.GetWithId(declaration.CountryId);
                if (countryDb == null) return StatusCode(StatusCodes.Status404NotFound);
                declarationDb.Count = declaration.Count;
                declarationDb.Note = declaration.Note;
                declarationDb.OrderNumber = declaration.OrderNumber;
                declarationDb.Price = declaration.Price;
                declarationDb.Shop = declaration.Shop;
                declarationDb.TrackingNumber = declaration.TrackingNumber;
                declarationDb.CountryId = declaration.CountryId;

                if (declaration.Photo != null)
                {
                    if (!declaration.Photo.IsImage()) return StatusCode(StatusCodes.Status415UnsupportedMediaType);
                    if (!declaration.Photo.PhotoLength(200)) return StatusCode(StatusCodes.Status411LengthRequired);
                    declarationDb.Image = await declaration.Photo.AddImageAsync(_env.WebRootPath, "img");
                }
                await _context.Update(declarationDb);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<DeclarationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Declaration declarationDb = await _context.GetWithId(id);
                if (declarationDb == null) return StatusCode(StatusCodes.Status404NotFound);
                declarationDb.IsDeleted = true;

                await _context.Update(declarationDb);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
