using System;
using System.Threading.Tasks;

using GeekBurger.Production.Application.Interfaces;
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
        private readonly IOrderRepository _orderRepository;

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
            _orderRepository = orderRepository;
            _logService = logService;

        }

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
        /// Get a order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getOrder")]
        public async Task<IActionResult>GetOrder([FromQuery]Guid id)
        {
            try
            {
                var result = await _orderRepository.GetById(id);
                return Ok(result);
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
        public async Task<IActionResult> NewOrder(GeekBurguer.Orders.Contract.NewOrderMessage request)
        {
            try
            {
                await _orderRepository.Add(request);
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
        public async Task<IActionResult> UpdateOrder(GeekBurguer.Orders.Contract.OrderChangedMessage request)
        {
            try
            {
                await _orderRepository.Update(request);
                _logService.SendMessagesAsync("OrderChanged");
                return Ok(request);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}