using Microsoft.Extensions.Logging;
using UrlShortener.Application.Repositories;
using UrlShortener.Application.Shared;
using UrlShortener.Application.UseCases.ShortenUrl;

namespace UrlShortener.Test.Application.UseCases.ShortenUrl;

public class ShortenUrlUseCaseTest
{
    private readonly ShortenUrlUseCase _useCase;
    private readonly Mock<ILogger<ShortenUrlUseCase>> _logger;
    private readonly Mock<IUrlRepository> _urlRepository;

    public ShortenUrlUseCaseTest()
    {
        _logger = new();
        _urlRepository = new();
        _useCase = new(
            _logger.Object,
            _urlRepository.Object);
    }

    [Fact]
    public async Task ShortenUrl_ShouldReturnValidationError_WhenUrlIsNotValid()
    {
        // Arrange
        string? invalidUrl = null;
        
        // Act
        var result = await _useCase.ShortenUrl(
            invalidUrl!,
            It.IsAny<CancellationToken>());
        
        // Assert
        result.Status.ShouldBe(ResultStatus.ValidationError);
    }

    [Fact]
    public async Task ShortenUrl_ShouldReturnSuccess_WhenUrlIsValid()
    {
        // Arrange
        string validUrl = "http://google.com";
        
        // Act
        var result = await _useCase.ShortenUrl(
            validUrl,
            It.IsAny<CancellationToken>());
        
        // Assert
        result.Status.ShouldBe(ResultStatus.Success);
    }
}