namespace UrlShortener.Generator.Domain.Services.UrlService;

internal sealed class UrlService : IUrlService
{
	public string GetUrl(string host, string id) =>
		$"{host}/{id}";
}
