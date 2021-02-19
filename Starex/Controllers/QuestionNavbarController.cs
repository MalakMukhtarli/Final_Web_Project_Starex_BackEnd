using Buisness.Abstract;
using Entity.Entities.Questions;
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
    public class QuestionNavbarController : ControllerBase
    {
        private readonly IQuestionNavbarService _context;
        private readonly IQuestionService _contextQuestion;

        public QuestionNavbarController(IQuestionNavbarService context, IQuestionService contextQuestion)
        {
            _context = context;
            _contextQuestion = contextQuestion;

        }
        // GET: api/<QuestionNavbarController>
        [HttpGet]
        public async Task<ActionResult<List<QuestionNavbar>>> Get()
        {
            try
            {
                List<QuestionNavbar> questions = await _context.GetAll();
                return Ok(questions);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET api/<QuestionNavbarController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionNavbar>> Get(int id)
        {
            try
            {
                QuestionNavbar question = await _context.GetWithId(id);
                if (question == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(question);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<QuestionNavbarController>
        [HttpPost]
        public async Task<ActionResult> Post(QuestionNavbar question)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                await _context.Add(question);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<QuestionNavbarController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] QuestionNavbar questionNavbar)
        {
            try
            {
                QuestionNavbar dbQuestion = await _context.GetWithId(id);
                if (dbQuestion == null) return BadRequest();

                dbQuestion.Title = questionNavbar.Title;

                await _context.Update(dbQuestion);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<QuestionNavbarController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                QuestionNavbar dbQuestion = await _context.GetWithId(id);

                if (dbQuestion == null) return BadRequest();
                dbQuestion.IsDeleted = true;

                List<Question> questions = await _contextQuestion.GetAll();
                foreach (Question ques in questions)
                {
                    if (ques.QuestionNavbarId == dbQuestion.Id)
                    {
                        ques.IsDelete = true;
                    }
                    await _contextQuestion.Update(ques);
                }


                await _context.Update(dbQuestion);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
