using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using FluentValidation;

using FinancialDocument.Domain.Exceptions;

namespace FinancialDocument.Api.Handlers;
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, 
                                                Exception exception,
                                                CancellationToken cancellationToken)
    {
        if (exception is ClientDetailsNotFoundException ||
            exception is DocumentNotFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)404,
                Title = "Unavailable resource error occurred",
                Detail = exception.Message,
            }, cancellationToken: cancellationToken);
            return true;
        }

        if (exception is ValidationException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)403,
                Title = "Validation error occurred",
                Detail = exception.Message,
            }, cancellationToken: cancellationToken);
            return true;
        }

        var exceptionMessage = exception.Message;
        logger.LogError(
            "Error Message: {exceptionMessage}, Time of occurrence {time}",
            exceptionMessage, DateTime.UtcNow);
        return true;
    }
}
