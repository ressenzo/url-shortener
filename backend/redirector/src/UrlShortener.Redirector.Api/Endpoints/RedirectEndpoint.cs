using Microsoft.AspNetCore.Mvc;
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
		if (result.IsSuccess)
		{
			return Results.Redirect(
				"http://google.com",
				permanent: true,
				preserveMethod: true
			);
		}
		else
		{
			return Results.NotFound(new
			{
				result = "Not Found"
			});
		}
	}
}
