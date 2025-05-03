namespace UrlShortener.Application.UseCases.ShortenUrl;

public interface IShortenUrlUseCase
{
    Task<ShortenUrlResponse> ShortenUrlAsync(string originalUrl);
}