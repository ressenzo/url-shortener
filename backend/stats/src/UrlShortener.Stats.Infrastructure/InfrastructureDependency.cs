using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UrlShortener.Stats.Application.Repositories;
using UrlShortener.Stats.Infrastructure.Repositories;
using UrlShortener.Stats.Infrastructure.Settings;

namespace UrlShortener.Stats.Infrastructure;

public static class InfrastructureDependency
{
	public static IServiceCollection AddInfrastructureLayer(
		this IServiceCollection services,
		ConfigurationManager configuration) =>
		services
			.AddDatabase(configuration)
			.AddTransient<IUrlRepository, UrlRepository>()
			.AddSingleton<IMessagingConsumerRepository, MessagingConsumerRepository>()
			.AddSettings(configuration);

	private static IServiceCollection AddDatabase(
		this IServiceCollection services,
		ConfigurationManager configuration)
	{
		var mongoConnectionString = configuration
			.GetConnectionString("Mongo");
		ArgumentException.ThrowIfNullOrWhiteSpace(mongoConnectionString);
		var mongoClient = new MongoClient(mongoConnectionString);
		var database = mongoClient.GetDatabase(name: "url-shortener-stats");
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
