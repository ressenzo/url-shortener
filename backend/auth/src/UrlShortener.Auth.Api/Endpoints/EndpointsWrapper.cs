namespace UrlShortener.Auth.Api.Endpoints;

internal static class EndpointsWrapper
{
	public static RouteHandlerBuilder AddEndpoints(
		this WebApplication app
	)
	{
		var authGroup = app.MapGroup("/api/auth");
		return authGroup
			.AddLoginEndpoint()
			.WithTags("Auth");
	}
}
