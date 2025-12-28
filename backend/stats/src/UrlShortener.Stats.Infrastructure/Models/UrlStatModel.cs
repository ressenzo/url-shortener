using UrlShortener.Stats.Domain.Entities;
using UrlShortener.Stats.Domain.Entities.Interfaces;

namespace UrlShortener.Stats.Infrastructure.Models;

public class UrlStatModel(
	string id,
	string originalUrl,
	int accessesQuantity,
	DateTime lastAccess
)
{
	public string Id { get; private set; } = id;

	public string OriginalUrl { get; private set; } = originalUrl;

	public int AccessesQuantity { get; private set; } = accessesQuantity;

	public DateTime LastAccessAt { get; private set; } = lastAccess;

	public static UrlStatModel FromEntity(IUrlStat urlStat) =>
		new(
			urlStat.Id,
			urlStat.OriginalUrl,
			urlStat.AccessesQuantity,
			urlStat.LastAccessAt
		);

	public IUrlStat ToEntity() =>
		new UrlStat(
			Id,
			OriginalUrl,
			AccessesQuantity,
			LastAccessAt
		);
}
