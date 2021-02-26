using Buisness.Abstract;
using Entity.Entities.Balancess;
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
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _context;

        public BalanceController(IBalanceService balanceService)
        {
            _context = balanceService;
        }

        // GET: api/<BalanceController>
        [HttpGet]
        public async Task<ActionResult<List<Balance>>> Get()
        {
            try
            {
                List<Balance> balances = await _context.GetAll();
                return Ok(balances);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<BalanceController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Balance>> Get(int id)
        {
            try
            {
                Balance balance = await _context.GetWithId(id);
                if (balance == null) return StatusCode(StatusCodes.Status404NotFound);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<BalanceController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Balance balance)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                if (balance.Price == null) balance.Price = 0;
                if (balance.MyBalance == null) balance.MyBalance = 0;

                await _context.Add(balance);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT api/<BalanceController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Balance balance)
        {
            try
            {
                Balance dbBalance = await _context.GetWithId(id);
                if (dbBalance == null) return BadRequest();

                dbBalance.Currency = balance.Currency;
                dbBalance.Price = balance.Price;
                dbBalance.MyBalance = balance.MyBalance;

                if (balance.MyBalance == null) balance.MyBalance = balance.MyBalance;
                if (balance.Price == null) balance.Price = balance.Price;


                dbBalance.MyBalance += balance.Price;

                await _context.Update(dbBalance);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // DELETE api/<BalanceController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Balance balance = await _context.GetWithId(id);
                if (balance == null) return StatusCode(StatusCodes.Status404NotFound);
                await _context.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
