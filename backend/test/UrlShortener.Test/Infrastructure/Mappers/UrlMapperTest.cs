using UrlShortener.Infrastructure.Mappers;

namespace UrlShortener.Test.Infrastructure.Mappers;

public class UrlMapperTest
{
    [Fact]
    public void Factory_ShouldReturnEntity()
    {
        // Arrange
        var urlMapper = new UrlMapper()
        {
            Id = "a1b2",
            OriginalUrl = "google.com"
        };
        
        // Act
        var url = urlMapper.ToEntity();
        
        
        // Assert
        url.ShouldNotBeNull();
        url.Id.ShouldBe(urlMapper.Id);
        url.OriginalUrl.ShouldBe(urlMapper.OriginalUrl);
    }
}