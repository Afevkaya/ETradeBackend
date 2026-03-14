using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ETradeBackend.API.Filters;

public class ValidationFilter(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = serviceProvider.GetService(validatorType) as IValidator;

            if (validator is null) continue;

            var validationContext = new ValidationContext<object>(argument);
            var result = await validator.ValidateAsync(validationContext);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage));

                context.Result = new BadRequestObjectResult(new { Errors = errors });
                return;
            }
        }

        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value!.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage));

            context.Result = new BadRequestObjectResult(new { Errors = errors });
            return;
        }

        await next();
    }
}