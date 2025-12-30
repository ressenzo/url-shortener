using UrlShortener.Generator.Domain.Entities;

namespace UrlShortener.Generator.Infrastructure.Models;

internal class UrlModel(
	string id,
	string originalUrl,
	DateTime createdAt
)
{
	public string Id { get; init; } = id;

	public string OriginalUrl { get; init; } = originalUrl;

	public DateTime CreatedAt { get; init; } = createdAt;

	public Url ToEntity() =>
		new(Id, OriginalUrl);

	public static UrlModel FromEntity(Url url) =>
		new(
			url.Id,
			url.OriginalUrl,
			url.CreatedAt
		);
}
