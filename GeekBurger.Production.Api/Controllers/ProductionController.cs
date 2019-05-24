using Microsoft.AspNetCore.Mvc;

using GeekBurger.Production.Contract;

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