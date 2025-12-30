using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Redirector.Api.Endpoints;

internal static class RedirectEndpoint
{
	public static RouteHandlerBuilder AddRedirectEndpoint(
		this WebApplication app
	)
	{
		return app
			.MapGet("{id}", HandleRedirection)
			.WithName("Redirect");
	}

	private static IResult HandleRedirection(
		[FromRoute] string id
	)
	{
		Console.WriteLine(id);
		return Results.Redirect("http://google.com");
	}
}
