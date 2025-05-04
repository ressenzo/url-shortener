using Microsoft.Extensions.Logging;
using UrlShortener.Application.Repositories;
using UrlShortener.Application.Shared;
using UrlShortener.Application.UseCases.ShortenUrl;
using UrlShortener.Domain.Services.UrlService;

namespace UrlShortener.Test.Application.UseCases.ShortenUrl;

public class ShortenUrlUseCaseTest
{
    private readonly ShortenUrlUseCase _useCase;
    private readonly Mock<ILogger<ShortenUrlUseCase>> _logger;
    private readonly Mock<IUrlRepository> _urlRepository;
    private readonly Mock<IUrlService> _urlService;

    public ShortenUrlUseCaseTest()
    {
        _logger = new();
        _urlRepository = new();
        _urlService = new();
        _useCase = new(
            _logger.Object,
            _urlRepository.Object,
            _urlService.Object);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null!)]
    public async Task ShortenUrl_ShouldReturnValidationError_WhenServerIsNotValid(
        string? server)
    {
        // Arrange - Act
        var result = await _useCase.ShortenUrl(
            server!,
            It.IsAny<string>(),
            It.IsAny<CancellationToken>());
        
        // Assert
        result.Status.ShouldBe(ResultStatus.ValidationError);
        result.Errors.First().ShouldBe("Server was not provided");
    }

    [Fact]
    public async Task ShortenUrl_ShouldReturnValidationError_WhenUrlIsNotValid()
    {
        // Arrange
        string? invalidUrl = null;
        
        // Act
        var result = await _useCase.ShortenUrl(
            server: "server",
            invalidUrl!,
            It.IsAny<CancellationToken>());
        
        // Assert
        result.Status.ShouldBe(ResultStatus.ValidationError);
    }

    [Fact]
    public async Task ShortenUrl_ShouldReturnSuccess_WhenUrlIsValid()
    {
        // Arrange
        string validServer = "http://localhost:5100";
        string validUrl = "http://google.com";
        _urlService.Setup(x => x.GetUrl(
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Returns("shortenedUrl");
        
        // Act
        var result = await _useCase.ShortenUrl(
            validServer,
            validUrl,
            It.IsAny<CancellationToken>());
        
        // Assert
        result.Status.ShouldBe(ResultStatus.Success);
    }
}