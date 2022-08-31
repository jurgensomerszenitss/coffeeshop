using Coffeeshop.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coffeeshop.Api.Controllers
{
    [ApiController]
    [Route("coffee/types")]
    public class TypeController : ControllerBase
    {
        public TypeController(
            IMediator mediator,
            ILogger<TypeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        /// <summary>
        /// Queries for a list of coffees
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns>Ok</returns>
        [HttpGet]
        public async Task<IActionResult> QueryAsync()
        {
            try
            {
                var result = await _mediator.Send(new TypeSearch.Query());
                return Ok(result);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }

        /// <summary>
        /// Show the list of sales per type
        /// </summary>
        /// <returns>Ok</returns>
        [HttpGet("{type}/sales")]
        public async Task<IActionResult> QuerySalesAsync([FromRoute] string type)
        {
            try
            {
                var result = await _mediator.Send(new SalesSearch.Query(type));
                return Ok(result);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }
    }
}