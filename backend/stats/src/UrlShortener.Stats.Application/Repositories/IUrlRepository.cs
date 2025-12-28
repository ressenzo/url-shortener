using UrlShortener.Stats.Domain.Entities.Interfaces;

namespace UrlShortener.Stats.Application.Repositories;

public interface IUrlRepository
{
	Task SaveUrlStat(
		IUrlStat urlStat,
		CancellationToken cancellationToken
	);

	Task<IUrlStat?> GetUrlStat(
		string id,
		CancellationToken cancellationToken
	);
}
