namespace UrlShortener.Generator.Domain.Services.UrlService;

public interface IUrlService
{
	string GetUrl(string host, string id);
}
