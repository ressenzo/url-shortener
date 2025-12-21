using UrlShortener.Consolidator.Application.UseCases.SaveUrl;

namespace UrlShortener.Consolidator.Application.Repositories;

public interface IUrlRepository
{
	Task SaveUrl(
		SaveUrlRequest url,
		CancellationToken cancellationToken
	);
}
