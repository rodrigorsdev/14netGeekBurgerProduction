using Microsoft.AspNetCore.Mvc;

using GeekBurger.Production.Contract;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;
using GeekBurger.Production.Interface;
using System;
using Microsoft.Azure.Documents;
using System.Net;
using System.Collections.Generic;

namespace GeekBurger.Production.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IAreaRepository _areaRepository;

        public ProductionController(
            IAreaRepository areaRepository)
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
                throw;
            }
            
        }

        [HttpPost("addArea")]
        public async Task<IActionResult> AddArea(Area request)
        {
            try
            {
                await _areaRepository.Add(request);
            }
            catch (Exception e)
            {
                throw;
            }

            return Ok();
        }

        [HttpPost("newOrder")]
        public IActionResult NewOrder()
        {
            return Ok();
        }
    }
}