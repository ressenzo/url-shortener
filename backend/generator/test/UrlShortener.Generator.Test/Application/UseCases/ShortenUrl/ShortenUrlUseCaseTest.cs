using Moq.AutoMock;
using ResultPattern;
using UrlShortener.Generator.Application.Repositories;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;
using UrlShortener.Generator.Domain.Services.UrlService;

namespace UrlShortener.Generator.Test.Application.UseCases.ShortenUrl;

public class ShortenUrlUseCaseTest
{
	private readonly ShortenUrlUseCase _useCase;
	private readonly Mock<IUrlService> _urlService;
	private readonly Mock<IRedirectHostRepository> _redirectHostRepository;

	public ShortenUrlUseCaseTest()
	{
		var mocker = new AutoMocker();
		_useCase = mocker.CreateInstance<ShortenUrlUseCase>();
		_urlService = mocker.GetMock<IUrlService>();
		_redirectHostRepository = mocker.GetMock<IRedirectHostRepository>();
	}

	[Fact]
	public async Task ShortenUrl_ShouldReturnValidationError_WhenUrlIsNotValid()
	{
		// Arrange
		string? invalidUrl = null;

		// Act
		var result = await _useCase.ShortenUrl(
			invalidUrl!,
			It.IsAny<CancellationToken>()
		);

		// Assert
		result.Status.ShouldBe(ResultStatus.ValidationError);
	}

	[Theory]
	[InlineData("http://redirect-host.com", "http://redirect-host.com")]
	[InlineData(null, "http://localhost:5001")]
	public async Task ShortenUrl_ShouldReturnSuccess_WhenUrlIsValid(
		string? redirectHost,
		string expectedHostToBeShortened
	)
	{
		// Arrange
		string originalUrl = "http://google.com";
		string shortenedUrl = "shortenedUrl";
		_urlService
			.Setup(
				x => x.GetUrl(
					It.IsAny<string>(),
					It.IsAny<string>()
				)
			)
			.Returns(shortenedUrl);
		_redirectHostRepository
			.Setup(
				x => x.GetHost(It.IsAny<CancellationToken>())
			)
			.ReturnsAsync(redirectHost);

		// Act
		var result = await _useCase.ShortenUrl(
			originalUrl,
			It.IsAny<CancellationToken>()
		);

		// Assert
		result.Status.ShouldBe(ResultStatus.Success);
		result.Content!.ShortenedUrl.ShouldBe(shortenedUrl);
		_urlService
			.Verify(
				x => x.GetUrl(
					expectedHostToBeShortened,
					It.IsAny<string>()
				),
				Times.Once
			);
	}
}
