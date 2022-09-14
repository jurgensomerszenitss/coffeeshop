using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coffeeshop.Api.Filters;

public class ShopOpenAttribute : ActionFilterAttribute, IAsyncAuthorizationFilter
{    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var mediator = context.HttpContext.RequestServices.GetService<IMediator>();

        var status = await mediator.Send(new ShopStatusGet.Query());
        if (status != ShopStatus.Open)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
