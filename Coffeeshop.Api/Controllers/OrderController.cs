using Coffeeshop.Api.Dto;
using Coffeeshop.Api.Filters;
using Coffeeshop.Api.Validations.Extensions;
using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Queries;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Coffeeshop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {        
        public OrderController(
            IMediator mediator, 
            ILogger<OrderController> logger, 
            IValidator<OrderCreateDto> orderCreateValidator,
            IValidator<OrderUpdateDto> orderUpdateValidator)
        {
            _mediator = mediator;
            _logger = logger;
            _orderCreateValidator = orderCreateValidator;
            _orderUpdateValidator = orderUpdateValidator; 
        }

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        private readonly IValidator<OrderCreateDto> _orderCreateValidator;
        private readonly IValidator<OrderUpdateDto> _orderUpdateValidator; 

        /// <summary>
        /// Queries for a list of orders
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns>Ok</returns>
        [HttpGet]
        public async Task<IActionResult> QueryAsync([FromQuery] OrderQueryDto queryDto)
        {
            try
            {
                var query = queryDto.Adapt<OrderSearch.Query>();
                var result = await _mediator.Send(query);
                return Ok(result.Select(x => x.Adapt<OrderSearchDto>()));
            }
            catch ( Exception exc)
            {
                _logger.LogError(exc,exc.Message);
                return Problem(exc.Message);
            }
        }

        /// <summary>
        /// Gets single order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok when found, NotFound when not found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] long id)
        {
            try
            {
                var query = new OrderGet.Query(id);
                var result = await _mediator.Send(query);
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


        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Created when success, BadRequest when not valid</returns>
        [HttpPost, ShopOpen]
        public async Task<IActionResult> CreateAsync([FromBody] OrderCreateDto? dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var validationResult = await _orderCreateValidator.ValidateAsync(dto);
                if (validationResult.IsValid)
                {
                    var command = dto.Adapt<OrderCreate.Command>();
                    var result = await _mediator.Send(command);
                    return Created(new Uri($"/Order/{result}", UriKind.Relative), result);
                }
                else
                {
                    validationResult.AddToModelState(this.ModelState);
                    return BadRequest(this.ModelState);
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }


        /// <summary>
        /// Updates an existing coffee
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Accepted when success, BadRequest when Invalid, NotFound when not found</returns>
        [HttpPut, ShopOpen]
        public async Task<IActionResult> UpdateAsync([FromBody] OrderUpdateDto? dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var validationResult = await _orderUpdateValidator.ValidateAsync(dto);
                if (validationResult.IsValid)
                {
                    var command = dto.Adapt<OrderUpdate.Command>();
                    var result = await _mediator.Send(command);
                    return result switch
                    {
                        UpdateResult.NotFound => NotFound(),
                        _ => Accepted(new Uri($"/Order/{dto.Id}", UriKind.Relative), result)
                    };
                }
                else
                {
                    validationResult.AddToModelState(this.ModelState);
                    return BadRequest(this.ModelState);
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }

        /// <summary>
        /// Updates an existing coffee
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Accepted when success, BadRequest when Invalid, NotFound when not found</returns>
        [HttpPatch("{id:long}"), ShopOpen]
        public async Task<IActionResult> PatchAsync([FromRoute] long id, [FromBody] OrderPatchDto? dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var command = dto.Adapt<OrderPatch.Command>() with { Id = id };
                var result = await _mediator.Send(command);
                return result switch
                {
                    UpdateResult.NotFound => NotFound(),
                    _ => Accepted(new Uri($"/Order/{id}", UriKind.Relative), result)
                };
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }

        /// <summary>
        /// Removes a coffee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent when success, Forbidden when not allowed</returns>
        [HttpDelete("{id}"), ShopOpen]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            try
            {
                var result = await _mediator.Send(new OrderDelete.Command(id));
                /// Example of return switch statement .NET 6
                return result switch
                {
                    DeleteResult.Forbidden => StatusCode((int)HttpStatusCode.Forbidden),
                    DeleteResult.NotFound => NotFound(),
                    _ => NoContent()
                };

            }
            catch (Exception exc)
            {
                _logger.LogError(exc, exc.Message);
                return Problem(exc.Message);
            }
        }
        
    }
}