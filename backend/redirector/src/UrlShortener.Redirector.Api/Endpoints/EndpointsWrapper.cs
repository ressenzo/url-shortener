namespace UrlShortener.Redirector.Api.Endpoints;

internal static class EndpointsWrapper
{
	public static RouteHandlerBuilder AddEndpoints(
		this WebApplication app
	)
	{
		return app.AddRedirectEndpoint();
	}
}
