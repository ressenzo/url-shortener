namespace UrlShortener.Domain.Services.UrlService;

public interface IUrlService
{
	string GetUrl(string host, string id);
}
