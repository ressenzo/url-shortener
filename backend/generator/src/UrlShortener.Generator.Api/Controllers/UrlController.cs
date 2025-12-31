using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UrlShortener.Generator.Api.Extensions;
using UrlShortener.Generator.Api.Requests;
using UrlShortener.Generator.Application.Shared;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;

namespace UrlShortener.Generator.Api.Controllers;

[ApiController]
public class UrlController(
	IShortenUrlUseCase shortenUrlUseCase
) : ControllerBase
{
	[HttpPost("api/urls")]
	[SwaggerResponse(statusCode: (int)HttpStatusCode.Created, type: typeof(ShortenUrlResponse))]
	[SwaggerResponse(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(BaseResult))]
	[SwaggerResponse(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(Result))]
	public async Task<IActionResult> ShortenUrl(
		[FromBody] ShortenUrlRequest request,
		CancellationToken cancellationToken
	)
	{
		var result = await shortenUrlUseCase.ShortenUrl(
			request.OriginalUrl,
			cancellationToken
		);

		return result.Status switch
		{
			ResultStatus.Success => Created("", result.Content),
			ResultStatus.ValidationError => BadRequest(result),
			_ => result.ToInternalServerError()
		};
	}
}
