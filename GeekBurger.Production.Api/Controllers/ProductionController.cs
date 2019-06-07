using GeekBurger.Production.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GeekBurger.Production.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IProductionRepository _areaRepository;

        public ProductionController(
            IProductionRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        [HttpGet("areas")]
        public async Task<IActionResult> GetAreas()
        {
            try
            {
                var output = await _areaRepository.List();
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("addArea")]
        public async Task<IActionResult> AddArea(Contract.Production request)
        {
            try
            {
                await _areaRepository.Add(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("updateArea")]
        public async Task<IActionResult> UpdateArea(Contract.Production request)
        {
            try
            {
                await _areaRepository.Update(request);
                return Ok(request);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
        [HttpPost("newOrder")]
        public IActionResult NewOrder()
        {
            return Ok();
        }
    }
}