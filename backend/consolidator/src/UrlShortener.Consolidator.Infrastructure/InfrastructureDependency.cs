using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UrlShortener.Consolidator.Application.Repositories;
using UrlShortener.Consolidator.Infrastructure.Repositories;
using UrlShortener.Consolidator.Infrastructure.Settings;

namespace UrlShortener.Consolidator.Infrastructure;

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
