using UrlShortener.Redirector.Application.Models;

namespace UrlShortener.Redirector.Application.Repositories;

public interface IUrlStatRepository
{
	Task NotifyAccess(
		UrlModel url,
		DateTime lastAccessAt,
		CancellationToken cancellationToken
	);
}
