using Buisness.Abstract;
using Entity.Entities.Declarations;
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
    public class DeclarationController : ControllerBase
    {
        private readonly IDeclarationService _context;
        public DeclarationController(IDeclarationService context)
        {
            _context = context;
        }
        // GET: api/<DeclarationController>
        [HttpGet]
        public async Task<ActionResult<List<Declaration>>> Get()
        {
            List<Declaration> declarations = await _context.GetAll();
            return Ok(declarations);
        }

        // GET api/<DeclarationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Declaration>> Get(int id)
        {
            Declaration declaration = await _context.GetWithId(id);
            if (declaration == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(declaration);
        }

        // POST api/<DeclarationController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Declaration declaration)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _context.Add(declaration);    
            return Ok();
        }

        // PUT api/<DeclarationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Declaration declaration)
        {
            Declaration declarationDb = await _context.GetWithId(id);
            if (declarationDb == null) return StatusCode(StatusCodes.Status404NotFound);
            declarationDb.Count = declaration.Count;
            declarationDb.Note = declaration.Note;
            declarationDb.OrderNumber = declaration.OrderNumber;
            declarationDb.Price = declaration.Price;
            declarationDb.Shop = declaration.Shop;
            declarationDb.TrackingNumber = declaration.TrackingNumber;

            
            // PHOTO  yazilacaq
            await _context.Update(declarationDb);
            return Ok();
        }

        // DELETE api/<DeclarationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Declaration declarationDb = await _context.GetWithId(id);
            if (declarationDb == null) return StatusCode(StatusCodes.Status404NotFound);
            declarationDb.IsDeleted = true;
            // PHOTO  yazilacaq

            await _context.Update(declarationDb);
            return Ok();

        }
    }
}
