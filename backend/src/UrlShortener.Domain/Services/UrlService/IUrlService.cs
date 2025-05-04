namespace UrlShortener.Domain.Services.UrlService;

public interface IUrlService
{
    string GetUrl(string server, string id);
}