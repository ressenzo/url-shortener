namespace UrlShortener.Stats.Application.UseCases.SaveUrl;

public record SaveUrlRequest(
	string Id,
	string OriginalUrl,
	DateTime CreatedAt
);
