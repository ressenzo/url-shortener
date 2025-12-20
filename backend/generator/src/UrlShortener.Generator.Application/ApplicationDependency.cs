using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Generator.Application.UseCases.GetUrl;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;

namespace UrlShortener.Generator.Application;

public static class ApplicationDependency
{
	public static IServiceCollection AddApplicationLayer(
		this IServiceCollection services)
	{
		services.AddScoped<IShortenUrlUseCase, ShortenUrlUseCase>();
		services.AddScoped<IGetUrlUseCase, GetUrlUseCase>();
		return services;
	}
}
