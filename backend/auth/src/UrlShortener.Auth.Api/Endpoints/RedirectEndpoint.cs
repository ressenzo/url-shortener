
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Auth.Api.Requests;

namespace UrlShortener.Auth.Api.Endpoints;

internal static class RedirectEndpoint
{
	public static RouteHandlerBuilder AddLoginEndpoint(
		this IEndpointRouteBuilder app
	) => app.MapPost("login", HandleLogin);

	private static async Task<IResult> HandleLogin(
		[FromBody] LoginRequest request,
		CancellationToken cancellationToken
	)
	{
		return Results.Ok(request);
	}
}
