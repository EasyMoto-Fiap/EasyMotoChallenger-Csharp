using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyMoto.Api.Filters
{
    public sealed class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            var problem = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Erro de validação"
            };
            context.Result = new BadRequestObjectResult(problem);
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}