using System;
using System.Threading.Tasks;

using GeekBurger.Production.Application.Interfaces;
using GeekBurger.Production.Application.ViewModel;
using GeekBurger.Production.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Production.Api.Controllers
{
    /// <summary>
    /// Production controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        #region| Fields |

        private ILogService _logService;
        private readonly IProductionRepository _areaRepository;

        #endregion

        #region| Constructor |

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="areaRepository">IProductionRepository</param>
        /// <param name="logService">ILogService</param>
        public ProductionController(IProductionRepository areaRepository, ILogService logService)
        {
            _areaRepository = areaRepository;
            _logService = logService;

        }

        #endregion

        #region| Methods |

        /// <summary>
        /// Get all areas
        /// </summary>
        [HttpGet("areas")]
        [ProducesResponseType(200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Add a new area
        /// </summary>
        /// <param name="request">Production model</param>
        /// <returns>200</returns>
        [HttpPost("addArea")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Update an existing area
        /// </summary>
        /// <param name="request">Production model</param>
        /// <returns></returns>
        [HttpPut("updateArea")]
        public async Task<IActionResult> UpdateArea(Contract.Production request)
        {
            try
            {
                await _areaRepository.Update(request);
                _logService.SendMessagesAsync("ProductionAreaChanged");
                return Ok(request);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Post a new Order
        /// </summary>
        /// <returns></returns>
        [HttpPost("newOrder")]
        public IActionResult NewOrder(NewOrder request)
        {
            try
            {
                _logService.SendMessagesAsync("NewOrder");
                return Ok(request);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <returns></returns>
        [HttpPut("updateOrder")]
        public IActionResult UpdateOrder(UpdateOrder request)
        {
            try
            {
                _logService.SendMessagesAsync("OrderChanged");
                return Ok(request);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}