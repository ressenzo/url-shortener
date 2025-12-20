using UrlShortener.Generator.Domain.Services.UrlService;

namespace UrlShortener.Generator.Test.Domain.Services;

public class UrlServiceTest
{
	private readonly UrlService _service = new();

	[Fact]
	public void GetUrl_ShouldReturnUrl()
	{
		// Arrange
		var host = "http://localhost:3000";
		var id = "1a2b";

		// Act
		var shortenUrl = _service.GetUrl(host, id);

		// Assert
		shortenUrl.ShouldBe($"{host}/{id}");
	}
}
