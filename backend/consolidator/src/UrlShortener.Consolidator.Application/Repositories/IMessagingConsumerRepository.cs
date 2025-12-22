namespace UrlShortener.Consolidator.Application.Repositories;

public interface IMessagingConsumerRepository
{
	Task Start(CancellationToken cancellationToken);
}
