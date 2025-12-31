using RabbitMQ.Client;

namespace UrlShortener.Redirector.Infrastructure.Factories;

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
