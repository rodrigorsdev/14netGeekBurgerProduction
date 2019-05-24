using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Production.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAreas()
        {
            return Ok();
        }
        
        [HttpPost]
        public IActionResult NewOrder()
        {
            return Ok();
        }
    }
}