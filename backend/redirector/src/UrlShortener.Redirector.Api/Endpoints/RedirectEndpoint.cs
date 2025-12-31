using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Redirector.Api.Endpoints;

internal static class RedirectEndpoint
{
	public static RouteHandlerBuilder AddRedirectEndpoint(
		this WebApplication app
	)
	{
		return app
			.MapGet("{id}", HandleRedirect)
			.WithSummary("Route responsible to redirect user according to shortened URL")
			.Produces(StatusCodes.Status308PermanentRedirect)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status500InternalServerError)
			.WithName("Redirect")
			.WithTags("Redirect");
	}

	private static IResult HandleRedirect(
		[FromRoute] string id,
		CancellationToken cancellationToken
	)
	{
		Console.WriteLine(id);
		return Results.Redirect(
			"http://google.com",
			permanent: true,
			preserveMethod: true
		);
	}
}
