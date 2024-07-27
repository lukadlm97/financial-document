using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using FluentValidation;

namespace FinancialDocument.Api.Handlers;
public class ValidationExceptionHandler
: IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context,
                                                Exception exception,
                                                CancellationToken cancellationToken)
    {
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

        return false;
    }
}
