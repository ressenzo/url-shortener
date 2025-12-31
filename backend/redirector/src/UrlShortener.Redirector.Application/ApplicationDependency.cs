using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Redirector.Application.UseCases.GetUrl;

namespace UrlShortener.Redirector.Application;

public static class ApplicationDependency
{
	public static IServiceCollection AddApplicationLayer(
		this IServiceCollection services
	)
	{
		services.AddScoped<IGetUrlUseCase, GetUrlUseCase>();
		return services;
	}
}
