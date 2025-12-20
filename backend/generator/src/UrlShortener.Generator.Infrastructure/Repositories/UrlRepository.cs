using System.Text;
using System.Text.Json;
using MongoDB.Driver;
using RabbitMQ.Client;
using UrlShortener.Generator.Application.Repositories;
using UrlShortener.Generator.Domain.Entities;
using UrlShortener.Generator.Infrastructure.Factories;
using UrlShortener.Generator.Infrastructure.Mappers;

namespace UrlShortener.Generator.Infrastructure.Repositories;

internal sealed class UrlRepository(
	IMongoDatabase mongoDatabase,
	IPublisherFactory rabbitMqFactory
) : IUrlRepository
{
	private readonly IMongoCollection<UrlMapper> _urlCollection =
		mongoDatabase.GetCollection<UrlMapper>(name: "urls");

	public async Task CreateUrl(
		Url url,
		CancellationToken cancellationToken)
	{
		await using var connection = await rabbitMqFactory.CreateConnection(
			cancellationToken
		);
		await using var channel = await rabbitMqFactory.CreateChannel(
			connection,
			cancellationToken
		);

		var urlMapper = UrlMapper.FromEntity(
			url
		);
		var body = Encoding.UTF8.GetBytes(
			JsonSerializer.Serialize(urlMapper)
		);

		await channel.BasicPublishAsync(
			exchange: string.Empty,
			routingKey: "url-shortener.creation",
			body: body,
			mandatory: true,
			basicProperties: new BasicProperties(),
			cancellationToken: cancellationToken
		);
	}

	public async Task<Url?> GetUrl(
		string id,
		CancellationToken cancellationToken)
	{
		var urlMapper = await _urlCollection
			.Find(x => x.Id.Equals(id))
			.FirstOrDefaultAsync(cancellationToken);
		return urlMapper?.ToEntity();
	}
}
