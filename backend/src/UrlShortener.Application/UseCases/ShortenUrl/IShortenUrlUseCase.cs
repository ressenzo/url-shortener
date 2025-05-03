using UrlShortener.Application.Shared;

namespace UrlShortener.Application.UseCases.ShortenUrl;

public interface IShortenUrlUseCase
{
    Task<Result<ShortenUrlResponse>> ShortenUrl(
        string originalUrl,
        CancellationToken cancellationToken);
}