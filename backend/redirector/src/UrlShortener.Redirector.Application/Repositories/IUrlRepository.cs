using UrlShortener.Redirector.Application.Models;

namespace UrlShortener.Redirector.Application.Repositories;

public interface IUrlRepository
{
	public Task<UrlModel?> GetUrl(
		string id,
		CancellationToken cancellationToken
	);
}
