using UrlShortener.Stats.Application.UseCases.SaveUrl;

namespace UrlShortener.Stats.Application.Repositories;

public interface IUrlRepository
{
	Task SaveUrl(
		SaveUrlRequest url,
		CancellationToken cancellationToken
	);
}
