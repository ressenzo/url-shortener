using Microsoft.Extensions.Logging;
using UrlShortener.Generator.Application.Repositories;
using UrlShortener.Generator.Application.Shared;
using UrlShortener.Generator.Application.UseCases.GetUrl;
using UrlShortener.Generator.Test.Shared;

namespace UrlShortener.Generator.Test.Application.UseCases.GetUrl;

public class GetUrlUseCaseTest
{
	private readonly GetUrlUseCase _useCase;
	private readonly Mock<ILogger<GetUrlUseCase>> _logger;
	private readonly Mock<IUrlRepository> _urlRepository;

	public GetUrlUseCaseTest()
	{
		_logger = new();
		_urlRepository = new();
		_useCase = new(
			_logger.Object,
			_urlRepository.Object);
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData(null)]
	public async Task GetUrl_ShouldReturnValidationError_WhenIdIsNotValid(
		string? id)
	{
		// Act
		var result = await _useCase.GetUrl(
			id!,
			It.IsAny<CancellationToken>());

		// Assert
		result.Status.ShouldBe(ResultStatus.ValidationError);
		result.Errors.First().ShouldBe("Invalid id");
	}

	[Fact]
	public async Task GetUrl_ShouldReturnNotFoundError_WhenUrlIsNotFound()
	{
		// Act
		var result = await _useCase.GetUrl(
			id: "12345",
			It.IsAny<CancellationToken>());

		// Assert
		result.Status.ShouldBe(ResultStatus.NotFound);
		result.Errors.First().ShouldBe(
			"Not found Url for the given id");
	}

	[Fact]
	public async Task GetUrl_ShouldReturnSuccess_WhenUrlIsFound()
	{
		// Arrange
		var url = new UrlBuilder()
			.Build();
		_urlRepository.Setup(x => x.GetUrl(
				It.IsAny<string>(),
				It.IsAny<CancellationToken>()))
			.ReturnsAsync(url);

		// Act
		var result = await _useCase.GetUrl(
			id: "12345",
			It.IsAny<CancellationToken>());

		// Assert
		result.Status.ShouldBe(ResultStatus.Success);
		result.Content!.OriginalUrl.ShouldBe(url.OriginalUrl);
	}
}
