using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coffeeshop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {        
        public ShopController(
            IMediator mediator, 
            ILogger<ShopController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        private readonly IMediator _mediator;
        private readonly ILogger _logger; 

        /// <summary>
        /// Gets the shop status
        /// </summary>
        /// <returns>Ok when found, NotFound when not found</returns>
        [HttpGet("status")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _mediator.Send(new ShopStatusGet.Query());                
                return Ok(result.Adapt<ShopGetStatusDto>());
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }

        /// <summary>
        /// Set the shop status
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Ok when found, NotFound when not found</returns>
        [HttpPut("status/{status}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] ShopStatus status)
        {
            try
            {
                _ = await _mediator.Send(new ShopPatch.Command(status));
                return Accepted();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }
    }
}