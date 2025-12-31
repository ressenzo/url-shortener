using UrlShortener.Generator.Domain.Entities;

namespace UrlShortener.Generator.Application.Repositories;

public interface IUrlRepository
{
	public Task<Url> CreateUrl(
		Url url,
		CancellationToken cancellationToken
	);
}
