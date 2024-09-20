using FluentResults;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Core.Common.ActionFilters;

public class ResultTypeFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult { Value: IResultBase result })
            context.Result = ToActionResult(result);

        await next();
    }

    private static ActionResult ToActionResult(IResultBase result)
    {
        //TODO: Optimize this code
        if (result.IsSuccess)
            return new OkObjectResult((result as IResult<object>)?.Value);

        if (result.Reasons.Any())
        {
            if (result.Reasons.Any(x => x.Message.Contains("not found")))
            {
                return new NotFoundObjectResult(new ProblemDetails
                {
                    Title = "Not found",
                    Status = 404,
                    Detail = string.Join(", ", result.Errors?.Select(x => x.Message)),
                });
            }

            var validationDetails = new ValidationProblemDetails();

            foreach (var error in result.Errors)
            {
                var errorMessages = error.Metadata.Select(x => x.Key?.ToString() ?? string.Empty).ToArray();
                validationDetails.Errors.Add(error.Message, errorMessages);
            }

            return new ObjectResult(validationDetails) { StatusCode = validationDetails.Status ?? (int)HttpStatusCode.UnprocessableEntity };
        }

        var problemDetails = new ProblemDetails
        {
            Title = result.Errors?.FirstOrDefault()?.Message ?? "Unknown Error",
            Status = 400,
            Detail = string.Join(", ", result.Errors?.Select(x => x.Message)),
        };

        return new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
    }
}