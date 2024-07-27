using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using FinancialDocument.Domain.Exceptions;

namespace FinancialDocument.Api.Handlers;

public class NotFoundExceptionHandler
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

        return false;
    }
}
