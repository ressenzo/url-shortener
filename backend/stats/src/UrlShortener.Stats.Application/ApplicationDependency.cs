using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Stats.Application.UseCases.SaveUrlStat;

namespace UrlShortener.Stats.Application;

public static class ApplicationDependency
{
	public static IServiceCollection AddApplicationLayer(
		this IServiceCollection services
	)
	{
		services.AddScoped<ISaveUrlStatUseCase, SaveUrlStatUseCase>();
		return services;
	}
}
