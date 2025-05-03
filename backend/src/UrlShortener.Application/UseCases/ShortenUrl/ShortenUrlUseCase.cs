using Microsoft.Extensions.Logging;
using UrlShortener.Application.Repositories;
using UrlShortener.Application.Shared;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.ShortenUrl;

internal sealed class ShortenUrlUseCase(
    ILogger<ShortenUrlUseCase> logger,
    IUrlRepository urlRepository) : IShortenUrlUseCase
{
    // Add properties, methods, and logic here as needed
    public async Task<Result<ShortenUrlResponse>> ShortenUrl(
        string originalUrl,
        CancellationToken cancellationToken)
    {
        var l = logger;
        var u = urlRepository;
        var url = Url.Factory(originalUrl);
        if (!url.IsValid())
        {
            return Result<ShortenUrlResponse>.ValidationError(url.Errors);
        }
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}