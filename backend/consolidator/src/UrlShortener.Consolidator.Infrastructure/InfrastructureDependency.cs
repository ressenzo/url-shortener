using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UrlShortener.Consolidator.Application.Repositories;
using UrlShortener.Consolidator.Infrastructure.Repositories;

namespace UrlShortener.Consolidator.Infrastructure;

public static class InfrastructureDependency
{
	public static IServiceCollection AddInfrastructureLayer(
		this IServiceCollection services,
		IConfiguration configuration) =>
		services
			.AddDatabase(configuration)
			.AddTransient<IUrlRepository, UrlRepository>();

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
}
