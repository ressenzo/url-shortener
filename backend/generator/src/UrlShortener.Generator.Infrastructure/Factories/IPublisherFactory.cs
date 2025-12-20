using RabbitMQ.Client;

namespace UrlShortener.Generator.Infrastructure.Factories;

public interface IPublisherFactory
{
	Task<IConnection> CreateConnection(
		CancellationToken cancellationToken
	);

	Task<IChannel> CreateChannel(
		IConnection connection,
		CancellationToken cancellationToken
	);
}
