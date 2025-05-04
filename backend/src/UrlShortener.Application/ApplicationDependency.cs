using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.UseCases.ShortenUrl;

namespace UrlShortener.Application;

public static class ApplicationDependency
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IShortenUrlUseCase, ShortenUrlUseCase>();
        return services;
    }
}