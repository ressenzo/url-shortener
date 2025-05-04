using UrlShortener.Application.Shared;

namespace UrlShortener.Application.UseCases.ShortenUrl;

public interface IShortenUrlUseCase
{
    Task<Result<ShortenUrlResponse>> ShortenUrl(
        string host,
        string originalUrl,
        CancellationToken cancellationToken);
}