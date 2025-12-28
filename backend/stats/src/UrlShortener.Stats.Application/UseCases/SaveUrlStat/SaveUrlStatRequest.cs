namespace UrlShortener.Stats.Application.UseCases.SaveUrlStat;

public record SaveUrlStatRequest(
	string Id,
	string OriginalUrl,
	DateTime CreatedAt
);
