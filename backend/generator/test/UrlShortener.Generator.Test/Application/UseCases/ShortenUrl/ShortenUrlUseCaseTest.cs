using Moq.AutoMock;
using UrlShortener.Generator.Application.Shared;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;
using UrlShortener.Generator.Domain.Services.UrlService;

namespace UrlShortener.Generator.Test.Application.UseCases.ShortenUrl;

public class ShortenUrlUseCaseTest
{
	private readonly ShortenUrlUseCase _useCase;
	private readonly Mock<IUrlService> _urlService;

	public ShortenUrlUseCaseTest()
	{
		var mocker = new AutoMocker();
		_useCase = mocker.CreateInstance<ShortenUrlUseCase>();
		_urlService = mocker.GetMock<IUrlService>();
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData(null!)]
	public async Task ShortenUrl_ShouldReturnValidationError_WhenHostIsNotValid(
		string? host)
	{
		// Arrange - Act
		var result = await _useCase.ShortenUrl(
			host!,
			It.IsAny<string>(),
			It.IsAny<CancellationToken>());

		// Assert
		result.Status.ShouldBe(ResultStatus.ValidationError);
		result.Errors.First().ShouldBe("Host was not provided");
	}

	[Fact]
	public async Task ShortenUrl_ShouldReturnValidationError_WhenUrlIsNotValid()
	{
		// Arrange
		string? invalidUrl = null;

		// Act
		var result = await _useCase.ShortenUrl(
			host: "host",
			invalidUrl!,
			It.IsAny<CancellationToken>());

		// Assert
		result.Status.ShouldBe(ResultStatus.ValidationError);
	}

	[Fact]
	public async Task ShortenUrl_ShouldReturnSuccess_WhenUrlIsValid()
	{
		// Arrange
		string validHost = "http://localhost:5100";
		string validUrl = "http://google.com";
		_urlService.Setup(x => x.GetUrl(
				It.IsAny<string>(),
				It.IsAny<string>()))
			.Returns("shortenedUrl");

		// Act
		var result = await _useCase.ShortenUrl(
			validHost,
			validUrl,
			It.IsAny<CancellationToken>());

		// Assert
		result.Status.ShouldBe(ResultStatus.Success);
	}
}
