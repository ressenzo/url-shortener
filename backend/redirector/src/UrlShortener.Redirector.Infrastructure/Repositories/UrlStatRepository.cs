using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using UrlShortener.Redirector.Application.Models;
using UrlShortener.Redirector.Application.Repositories;
using UrlShortener.Redirector.Infrastructure.Factories;
using UrlShortener.Redirector.Infrastructure.Models;

namespace UrlShortener.Redirector.Infrastructure.Repositories;

internal class UrlStatRepository(
	IPublisherFactory rabbitMqFactory
) : IUrlStatRepository
{
	public async Task NotifyAccess(
		UrlModel url,
		DateTime lastAccessAt,
		CancellationToken cancellationToken
	)
	{
		await using var connection = await rabbitMqFactory.CreateConnection(
			cancellationToken
		);
		await using var channel = await rabbitMqFactory.CreateChannel(
			connection,
			cancellationToken
		);

		var urlStatModel = UrlStatModel.FromEntity(
			url,
			lastAccessAt
		);
		var body = Encoding.UTF8.GetBytes(
			JsonSerializer.Serialize(urlStatModel)
		);

		await channel.BasicPublishAsync(
			exchange: string.Empty,
			routingKey: "url-shortener.access",
			body: body,
			mandatory: true,
			basicProperties: new BasicProperties(),
			cancellationToken: cancellationToken
		);
	}
}
