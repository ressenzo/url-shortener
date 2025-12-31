using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;

namespace UrlShortener.Generator.Application;

public static class ApplicationDependency
{
	public static IServiceCollection AddApplicationLayer(
		this IServiceCollection services)
	{
		services.AddScoped<IShortenUrlUseCase, ShortenUrlUseCase>();
		return services;
	}
}
