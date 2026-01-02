using UrlShortener.Generator.Api.Commons;
using UrlShortener.Generator.Api.Filters;
using UrlShortener.Generator.Application;
using UrlShortener.Generator.Domain;
using UrlShortener.Generator.Infrastructure;

namespace UrlShortener.Generator.Api.Configurations;

internal static class ServicesConfigurations
{
	public static IServiceCollection AddConfigurations(
		this IServiceCollection services,
		ConfigurationManager configuration
	)
	{
		services
			.AddLayers(configuration)
			.AddHealthCheck()
			.AddFilters()
			.AddCors()
			.AddEndpointsApiExplorer()
			.AddSwaggerGen();

		return services;
	}

	private static IServiceCollection AddLayers(
		this IServiceCollection services,
		ConfigurationManager configuration
	)
	{
		services.AddApplicationLayer();
		services.AddDomainLayer();
		services.AddInfrastructureLayer(configuration);

		return services;
	}

	private static IServiceCollection AddHealthCheck(
		this IServiceCollection services
	)
	{
		services.AddHealthChecks();

		return services;
	}

	private static IServiceCollection AddFilters(
		this IServiceCollection services
	)
	{
		services.AddControllers(x =>
		{
			x.Filters.Add<ExceptionFilter>();
		});

		return services;
	}

	private static IServiceCollection AddCors(
		this IServiceCollection services
	)
	{
		services.AddCors(x =>
		{
			x.AddPolicy(
				Constants._CORS_NAME,
				builder =>
				{
					builder
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				}
			);
		});

		return services;
	}
}
