using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UrlShortener.Application.Shared;

namespace UrlShortener.Api.Filters;

public class ExceptionFilter(
    ILogger<ExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not null)
        {
            var exception = context.Exception;
            logger.LogError(
                exception,
                message: "The following error occurred: {Message}",
                exception.Message);
        }

        var internalServerErrorResult = Result.InternalServerError();
        context.Result = new ObjectResult(internalServerErrorResult)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
        context.ExceptionHandled = true;
    }
}