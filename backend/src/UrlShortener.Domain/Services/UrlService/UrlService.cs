namespace UrlShortener.Domain.Services.UrlService;

internal sealed class UrlService : IUrlService
{
    public string GetUrl(string server, string id) =>
        $"{server}/{id}";
}