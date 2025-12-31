using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UrlShortener.Redirector.Infrastructure.Factories;
using UrlShortener.Redirector.Application.Repositories;
using UrlShortener.Redirector.Infrastructure.Repositories;
using UrlShortener.Redirector.Infrastructure.Settings;

namespace UrlShortener.Redirector.Infrastructure;

public static class InfrastructureDependency
{
	public static IServiceCollection AddInfrastructureLayer(
		this IServiceCollection services,
		ConfigurationManager configuration) =>
		services
			.AddDatabase(configuration)
			.AddSettings(configuration)
			.AddTransient<IUrlRepository, UrlRepository>()
			.AddTransient<IUrlStatRepository, UrlStatRepository>()
			.AddTransient<IPublisherFactory, PublisherFactory>();

	private static IServiceCollection AddDatabase(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var mongoConnectionString = configuration
			.GetConnectionString("Mongo");
		ArgumentException.ThrowIfNullOrWhiteSpace(mongoConnectionString);
		var mongoClient = new MongoClient(mongoConnectionString);
		var database = mongoClient.GetDatabase(name: "url-shortener");
		services.AddSingleton(database);

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
}
