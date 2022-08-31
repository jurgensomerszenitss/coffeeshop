using Coffeeshop.Api.Dto;
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
    public class CoffeeController : ControllerBase
    {        
        public CoffeeController(
            IMediator mediator, 
            ILogger<CoffeeController> logger, 
            IValidator<CoffeeCreateDto> coffeeCreateValidator,
            IValidator<CoffeeUpdateDto> coffeeUpdateValidator)
        {
            _mediator = mediator;
            _logger = logger;
            _coffeeCreateValidator = coffeeCreateValidator;
            _coffeeUpdateValidator = coffeeUpdateValidator;
        }

        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        private readonly IValidator<CoffeeCreateDto> _coffeeCreateValidator;
        private readonly IValidator<CoffeeUpdateDto> _coffeeUpdateValidator;

        /// <summary>
        /// Queries for a list of coffees
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns>Ok</returns>
        [HttpGet]
        public async Task<IActionResult> QueryAsync([FromQuery] CoffeeQueryDto queryDto)
        {
            try
            {
                var query = queryDto.Adapt<CoffeeSearch.Query>();
                var result = await _mediator.Send(query);
                return Ok(result.Select(x => x.Adapt<CoffeeSearchDto>()));
            }
            catch ( Exception exc)
            {
                _logger.LogError(exc,exc.Message);
                return Problem(exc.Message);
            }
        }

        /// <summary>
        /// Gets single coffee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok when found, NotFound when not found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] long id)
        {
            try
            {
                var query = new CoffeeGet.Query(id);
                var result = await _mediator.Send(query);
                if (result != null)
                {
                    return Ok(result.Adapt<CoffeeGetDto>());
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
        /// Creates a new coffee
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Created when success, BadRequest when not valid</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CoffeeCreateDto? dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var validationResult = await _coffeeCreateValidator.ValidateAsync(dto);
                if (validationResult.IsValid)
                {
                    var command = dto.Adapt<CoffeeCreate.Command>();
                    var result = await _mediator.Send(command);
                    return Created(new Uri($"/coffee/{result}", UriKind.Relative), result);
                }
                else
                {
                    validationResult.AddToModelState(this.ModelState);
                    return BadRequest(ModelState);
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
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CoffeeUpdateDto? dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var validationResult = await _coffeeUpdateValidator.ValidateAsync(dto);
                if (validationResult.IsValid)
                {
                    var command = dto.Adapt<CoffeeUpdate.Command>();
                    var result = await _mediator.Send(command);
                    return result switch
                    {
                        UpdateResult.NotFound => NotFound(),
                        _ => Accepted(new Uri($"/Coffee/{dto.Id}", UriKind.Relative), result)
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
        /// Removes a coffee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent when success, Forbidden when not allowed</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            try
            {                
                var result = await _mediator.Send(new CoffeeDelete.Command(id));
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