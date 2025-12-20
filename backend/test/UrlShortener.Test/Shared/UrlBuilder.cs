using UrlShortener.Domain.Entities;

namespace UrlShortener.Test.Shared;

internal class UrlBuilder : BaseBuilder<Url>
{
	public override Url Build() =>
		Url.Factory(
			id: "12345",
			originalUrl: "http://random.com");
}
