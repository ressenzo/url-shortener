using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Consolidator.Application.UseCases.SaveUrl;

namespace UrlShortener.Consolidator.Application;

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
