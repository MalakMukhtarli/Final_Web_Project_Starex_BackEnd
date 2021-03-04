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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _context;
        private readonly IQuestionNavbarService _contextNavbar;

        public QuestionController(IQuestionService questionService,
                                  IQuestionNavbarService contextNavbar)
        {
            _context = questionService;
            _contextNavbar = contextNavbar;
        }

        // GET: api/<QuestionController>
        [HttpGet]
        public async Task<ActionResult<List<Question>>> Get()
        {
            try
            {
                List<Question> questions = await _context.GetAll();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> Get(int id)
        {
            try
            {
                Question question = await _context.GetWithId(id);
                if (question == null) return StatusCode(StatusCodes.Status404NotFound);
                QuestionNavbar navbars = await _contextNavbar.GetWithId(question.QuestionNavbarId);
                if(navbars==null)return StatusCode(StatusCodes.Status404NotFound);
                question.QuestionNavbar = navbars;
                return Ok(question);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<QuestionController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Question question)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                QuestionNavbar navbaDb = await _contextNavbar.GetWithId(question.QuestionNavbarId);
                if (navbaDb == null) return StatusCode(StatusCodes.Status404NotFound);

                await _context.Add(question);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<QuestionController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Question question)
        {
            try
            {
                Question dbQuestion = await _context.GetWithId(id);
                if (dbQuestion == null) return BadRequest();
                QuestionNavbar navbaDb = await _contextNavbar.GetWithId(question.QuestionNavbarId);
                if (navbaDb == null) return StatusCode(StatusCodes.Status404NotFound);
                dbQuestion.AskedQuestion = question.AskedQuestion;
                dbQuestion.ResponseQuestion = question.ResponseQuestion;
                dbQuestion.QuestionNavbarId = question.QuestionNavbarId;

                await _context.Update(dbQuestion);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<QuestionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Question dbQuestion = await _context.GetWithId(id);
                if (dbQuestion == null) return BadRequest();
                dbQuestion.IsDelete = true;

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
