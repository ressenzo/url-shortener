using UrlShortener.Domain.Entities;

namespace UrlShortener.Test.Domain.Entities;

public class UrlTest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void IsValid_ShouldReturnFalse_WhenOriginalUrlIsEmptyOrWhiteSpaceOrNull(
        string? originalUrl)
    {
        // Arrange
        var url = Url.Factory(originalUrl!);
        
        // Act
        var isValid = url.IsValid();
        
        // Assert
        isValid.ShouldBeFalse();
        url.Errors.ShouldHaveSingleItem();
        url.Errors.First().ShouldBe("Original Url was not provided");
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenOriginalUrlIsInvalid()
    {
        // Arrange
        var url = Url.Factory(
            originalUrl: "not a url");
        
        // Act
        var isValid = url.IsValid();
        
        // Assert
        isValid.ShouldBeFalse();
        url.Errors.ShouldHaveSingleItem();
        url.Errors.First().ShouldBe("Provided Original Url is not valid");
    }

    [Fact]
    public void IsValid_ShouldReturnTrue_WhenOriginalUrlIsValid()
    {
        // Arrange
        var url = Url.Factory(
            originalUrl: "google.com");
        
        // Act
        var isValid = url.IsValid();
        
        // Assert
        isValid.ShouldBeTrue();
        url.Errors.ShouldBeEmpty();
    }
}