using UrlShortener.Domain.Services.UrlService;

namespace UrlShortener.Test.Domain.Services;

public class UrlServiceTest
{
    private readonly UrlService _service = new();

    [Fact]
    public void GetUrl_ShouldReturnUrl()
    {
        // Arrange
        var server = "http://localhost:3000";
        var id = "1a2b";

        // Act
        var shortenUrl = _service.GetUrl(server, id);

        // Assert
        shortenUrl.ShouldBe($"{server}/{id}");
    }
}