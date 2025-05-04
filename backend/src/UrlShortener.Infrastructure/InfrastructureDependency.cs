using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Repositories;
using UrlShortener.Infrastructure.Repositories;

namespace UrlShortener.Infrastructure;

public static class InfrastructureDependency
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services)
    {
        services.AddTransient<IUrlRepository, UrlRepository>();
        return services;
    }
}