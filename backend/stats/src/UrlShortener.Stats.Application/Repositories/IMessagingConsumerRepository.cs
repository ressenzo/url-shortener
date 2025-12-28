namespace UrlShortener.Stats.Application.Repositories;

public interface IMessagingConsumerRepository
{
	Task Start(CancellationToken cancellationToken);
}
