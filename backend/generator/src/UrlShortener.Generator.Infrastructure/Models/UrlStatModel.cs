using UrlShortener.Generator.Domain.Entities;

namespace UrlShortener.Generator.Infrastructure.Models;

internal class UrlStatModel(
	string id,
	string originalUrl,
	DateTime lastAccessAt
)
{
	public string Id { get; private set; } = id;

	public string OriginalUrl { get; private set; } = originalUrl;

	public DateTime LastAccessAt { get; private set; } = lastAccessAt;

	public static UrlStatModel FromEntity(Url url, DateTime lastAccessAt) =>
		new(
			url.Id,
			url.OriginalUrl,
			lastAccessAt
		);
}
