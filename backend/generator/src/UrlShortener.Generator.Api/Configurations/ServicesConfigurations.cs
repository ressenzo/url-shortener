using UrlShortener.Generator.Api.Filters;
using UrlShortener.Generator.Application;
using UrlShortener.Generator.Domain;
using UrlShortener.Generator.Infrastructure;
using UrlShortener.Generator.Infrastructure.Settings;

namespace UrlShortener.Generator.Api.Configurations;

internal static class ServicesConfigurations
{
	private const string _CORS_NAME = "All";

	public static IServiceCollection AddConfigurations(
		this IServiceCollection services,
		ConfigurationManager configuration
	)
	{
		services
			.AddLayers(configuration)
			.AddHealthCheck()
			.AddSettings(configuration)
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

	private static IServiceCollection AddSettings(
		this IServiceCollection services,
		ConfigurationManager configuration
	)
	{
		services.Configure<RabbitMqSettings>(
			configuration.GetSection(nameof(RabbitMqSettings))
		);

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
				_CORS_NAME,
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
