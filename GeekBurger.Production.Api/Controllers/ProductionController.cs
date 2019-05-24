using GeekBurger.Production.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GeekBurger.Production.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        [HttpGet("areas")]
        public IActionResult GetAreas()
        {
            var output = new Restriction();

            return Ok(output);
        }

        [HttpPost]
        public IActionResult NewOrder()
        {
            return Ok();
        }
    }
}