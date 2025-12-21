namespace UrlShortener.Consolidator.Application.UseCases.SaveUrl;

public record SaveUrlRequest(
	string Id,
	string OriginalUrl,
	DateTime CreatedAt
);
