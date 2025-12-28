using UrlShortener.Stats.Domain.Entities.Interfaces;

namespace UrlShortener.Stats.Domain.Entities;

public class UrlStat(
	string id,
	string originalUrl,
	int accessesQuantity,
	DateTime lastAccess
) : IUrlStat
{
	public string Id { get; private set; } = id;

	public string OriginalUrl { get; private set; } = originalUrl;

	public int AccessesQuantity { get; private set; } = accessesQuantity;

	public DateTime LastAccess { get; private set; } = lastAccess;

	public void AddAccess()
	{
		AccessesQuantity += 1;
		LastAccess = DateTime.Now;
	}
}
