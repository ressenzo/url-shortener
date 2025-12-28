using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Stats.Application.UseCases.SaveUrl;

namespace UrlShortener.Stats.Application;

public static class ApplicationDependency
{
	public static IServiceCollection AddApplicationLayer(
		this IServiceCollection services
	)
	{
		services.AddScoped<ISaveUrlUseCase, SaveUrlUseCase>();
		return services;
	}
}
