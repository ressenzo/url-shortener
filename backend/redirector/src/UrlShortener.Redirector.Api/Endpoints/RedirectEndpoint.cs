using Microsoft.AspNetCore.Mvc;
using ResultPattern;
using UrlShortener.Redirector.Api.Extensions;
using UrlShortener.Redirector.Application.UseCases.GetUrl;

namespace UrlShortener.Redirector.Api.Endpoints;

internal static class RedirectEndpoint
{
	public static RouteHandlerBuilder AddRedirectEndpoint(
		this WebApplication app
	) => app.MapGet("{id}", HandleRedirect);

	private static async Task<IResult> HandleRedirect(
		[FromRoute] string id,
		[FromServices] IGetUrlUseCase getUrlUseCase,
		CancellationToken cancellationToken
	)
	{
		var result = await getUrlUseCase.GetUrl(
			id,
			cancellationToken
		);
		return result.Status switch
		{
			ResultStatus.Success => Results.Redirect(
				result.Content!.OriginalUrl,
				permanent: true,
				preserveMethod: true
			),
			ResultStatus.NotFound => Results.NotFound(result),
			ResultStatus.ValidationError => Results.BadRequest(result),
			_ => result.ToInternalServerError()
		};
	}
}
