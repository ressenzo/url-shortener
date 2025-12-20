using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Repositories;

public interface IUrlRepository
{
	public Task<Url> CreateUrl(
		Url url,
		CancellationToken cancellationToken);

	public Task<Url?> GetUrl(
		string id,
		CancellationToken cancellationToken);
}
