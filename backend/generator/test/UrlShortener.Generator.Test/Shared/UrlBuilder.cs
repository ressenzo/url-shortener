using UrlShortener.Generator.Domain.Entities;

namespace UrlShortener.Generator.Test.Shared;

internal class UrlBuilder : BaseBuilder<Url>
{
	public override Url Build() =>
		Url.Factory(
			id: "12345",
			originalUrl: "http://random.com");
}
