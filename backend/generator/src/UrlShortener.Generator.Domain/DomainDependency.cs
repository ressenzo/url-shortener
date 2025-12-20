using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Generator.Domain.Services.UrlService;

namespace UrlShortener.Generator.Domain;

public static class DomainDependency
{
	public static IServiceCollection AddDomainLayer(
		this IServiceCollection services)
	{
		services.AddScoped<IUrlService, UrlService>();
		return services;
	}
}
