using UrlShortener.Generator.Domain.Entities;

namespace UrlShortener.Generator.Application.Repositories;

public interface IUrlStatRepository
{
	Task NotifyAccess(
		Url url,
		DateTime lastAccessAt,
		CancellationToken cancellationToken
	);
}
