using Microsoft.AspNetCore.Mvc;

using GeekBurger.Production.Contract;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace GeekBurger.Production.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        [HttpGet("areas")]
        public async Task<IActionResult> GetAreas()
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