using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Infrastructure;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred");

        IResult problemDetails = Results.Problem(
            detail: exception.Message,
            statusCode: StatusCodes.Status500InternalServerError
        );

        await problemDetails.ExecuteAsync(httpContext);

        return true;
    }
}
