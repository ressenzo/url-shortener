using UrlShortener.Redirector.Application.Models;

namespace UrlShortener.Redirector.Infrastructure.Models;

internal class UrlStatModel(
	string id,
	string originalUrl,
	DateTime lastAccessAt
)
{
	public string Id { get; private set; } = id;

	public string OriginalUrl { get; private set; } = originalUrl;

	public DateTime LastAccessAt { get; private set; } = lastAccessAt;

	public static UrlStatModel FromEntity(UrlModel url, DateTime lastAccessAt) =>
		new(
			url.Id,
			url.OriginalUrl,
			lastAccessAt
		);
}
