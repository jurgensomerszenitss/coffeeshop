using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coffeeshop.Api.Controllers
{
    [ApiController]
    public class ProcessController : ControllerBase
    {        
        public ProcessController(
            IMediator mediator, 
            ILogger<ProcessController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        private readonly IMediator _mediator;
        private readonly ILogger _logger; 

        

        /// <summary>
        /// Gets the next order to prepare
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok when found, NotFound when not found</returns>
        [HttpGet("order/next")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _mediator.Send(new ProcessGet.Query());
                if (result != null)
                {
                    return Ok(result.Adapt<OrderGetDto>());
                }
                return NotFound();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        } 
    }
}