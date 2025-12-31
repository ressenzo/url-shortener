using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using UrlShortener.Redirector.Infrastructure.Settings;

namespace UrlShortener.Redirector.Infrastructure.Factories;

internal sealed class PublisherFactory(
	IOptions<RabbitMqSettings> options
) : IPublisherFactory
{
	public async Task<IConnection> CreateConnection(
		CancellationToken cancellationToken
	)
	{
		var settings = options.Value;
		var factory = new ConnectionFactory
		{
			HostName = settings.HostName,
			Port = settings.Port,
			VirtualHost = settings.VirtualHost,
			UserName = settings.UserName,
			Password = settings.Password
		};

		return await factory.CreateConnectionAsync(
			cancellationToken
		);
	}

	public async Task<IChannel> CreateChannel(
		IConnection connection,
		CancellationToken cancellationToken
	) =>
		await connection.CreateChannelAsync(
			cancellationToken: cancellationToken
		);
}
