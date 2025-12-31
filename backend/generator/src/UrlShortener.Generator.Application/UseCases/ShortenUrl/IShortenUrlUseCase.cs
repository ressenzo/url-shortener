using UrlShortener.Generator.Application.Shared;

namespace UrlShortener.Generator.Application.UseCases.ShortenUrl;

public interface IShortenUrlUseCase
{
	Task<Result<ShortenUrlResponse>> ShortenUrl(
		string originalUrl,
		CancellationToken cancellationToken
	);
}
