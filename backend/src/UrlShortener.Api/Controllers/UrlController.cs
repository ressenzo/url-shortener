using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UrlShortener.Api.Requests;
using UrlShortener.Application.Shared;
using UrlShortener.Application.UseCases.GetUrl;
using UrlShortener.Application.UseCases.ShortenUrl;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/urls")]
public class UrlController(
    IShortenUrlUseCase shortenUrlUseCase,
    IGetUrlUseCase getUrlUseCase) : ControllerBase
{
    [HttpPost]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.Created, type: typeof(ShortenUrlResponse))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(BaseResult))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(Result))]
    public async Task<IActionResult> ShortenUrl(
        [FromBody]ShortenUrlRequest request,
        CancellationToken cancellationToken)
    {
        var scheme = HttpContext.Request.Scheme;
        var host = HttpContext.Request.Host.Value;
        var completedHost = $"{scheme}://{host}";
        var result = await shortenUrlUseCase.ShortenUrl(
            completedHost,
            request.OriginalUrl,
            cancellationToken);

        return result.Status switch
        {
            ResultStatus.Success => Created("", result.Content),
            ResultStatus.ValidationError => BadRequest(result),
            _ => throw new NotImplementedException()
        };
    }

    [HttpGet("{id}")]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.OK, type: typeof(ShortenUrlResponse))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(BaseResult))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.NotFound, type: typeof(BaseResult))]
    [SwaggerResponse(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(Result))]
    public async Task<IActionResult> GetUrl(
        [FromRoute][Required] string id,
        CancellationToken cancellationToken)
    {
        var result = await getUrlUseCase.GetUrl(
            id,
            cancellationToken);
        return result.Status switch
        {
            ResultStatus.Success => Redirect(result.Content!.OriginalUrl),
            ResultStatus.ValidationError => BadRequest(result),
            ResultStatus.NotFound => NotFound(result),
            _ => throw new NotImplementedException()
        };
    }
}
