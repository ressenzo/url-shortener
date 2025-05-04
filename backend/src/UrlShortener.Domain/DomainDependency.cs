using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Domain.Services.UrlService;

namespace UrlShortener.Domain;

public static class DomainDependency
{
    public static IServiceCollection AddDomainLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IUrlService, UrlService>();
        return services;
    }
}