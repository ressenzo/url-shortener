using UrlShortener.Generator.Domain.Entities;

namespace UrlShortener.Generator.Infrastructure.Mappers;

internal class UrlStatMapper(
	string id,
	string originalUrl,
	DateTime lastAccessAt
)
{
	public string Id { get; private set; } = id;

	public string OriginalUrl { get; private set; } = originalUrl;

	public DateTime LastAccessAt { get; private set; } = lastAccessAt;

	public static UrlStatMapper FromEntity(Url url, DateTime lastAccessAt) =>
		new(
			url.Id,
			url.OriginalUrl,
			lastAccessAt
		);
}
