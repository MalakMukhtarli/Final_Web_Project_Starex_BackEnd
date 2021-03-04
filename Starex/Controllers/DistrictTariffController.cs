using Buisness.Abstract;
using Entity.Entities.Branches;
using Entity.Entities.Tariffs;
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
    public class DistrictTariffController : ControllerBase
    {
        private readonly IDistrictTariffService _context;
        private readonly IBranchService _contextBranch;
        public DistrictTariffController(IDistrictTariffService context,
                                        IBranchService contextBranch)
        {
            _context = context;
            _contextBranch = contextBranch;
        }
        // GET: api/<DistrictTariffController>
        [HttpGet]
        public async Task<ActionResult<List<DistrictTariff>>> Get()
        {
            try
            {
                List<DistrictTariff> tariffs = await _context.GetAll();
                return Ok(tariffs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<DistrictTariffController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictTariff>> Get(int id)
        {
            try
            {
                DistrictTariff tariff = await _context.GetWithId(id);
                if (tariff == null) return StatusCode(StatusCodes.Status404NotFound);
                Branch branchDb = await _contextBranch.GetWithId(tariff.BranchId);
                if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);
                tariff.Branch = branchDb;
                return Ok(tariff);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<DistrictTariffController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DistrictTariff tariff)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                Branch branchDb = await _contextBranch.GetWithId(tariff.BranchId);
                if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);

                await _context.Add(tariff);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<DistrictTariffController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] DistrictTariff tariff)
        {
            try
            {
                DistrictTariff tariffDb =await _context.GetWithId(id);
                if (tariffDb == null) return StatusCode(StatusCodes.Status404NotFound);
                Branch branchDb = await _contextBranch.GetWithId(tariff.BranchId);
                if (branchDb == null) return StatusCode(StatusCodes.Status404NotFound);
                tariffDb.District = tariff.District;
                tariffDb.Price = tariff.Price;
                tariffDb.BranchId = tariff.BranchId;
                await _context.Update(tariffDb);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<DistrictTariffController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                DistrictTariff tariffDb = await _context.GetWithId(id);
                if (tariffDb == null) return StatusCode(StatusCodes.Status404NotFound);
                tariffDb.IsDeleted = true;
                await _context.Update(tariffDb);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
