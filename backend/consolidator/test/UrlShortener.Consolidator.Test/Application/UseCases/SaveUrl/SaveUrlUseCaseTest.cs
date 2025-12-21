using Moq.AutoMock;
using UrlShortener.Consolidator.Application.Repositories;
using UrlShortener.Consolidator.Application.UseCases.SaveUrl;

namespace UrlShortener.Consolidator.Test.Application.UseCases.SaveUrl;

public class SaveUrlUseCaseTest
{
	private readonly SaveUrlUseCase _useCase;
	private readonly Mock<IUrlRepository> _urlRepository;
	private readonly SaveUrlRequest _request;

	public SaveUrlUseCaseTest()
	{
		var autoMock = new AutoMocker();
		_useCase = autoMock.CreateInstance<SaveUrlUseCase>();
		_urlRepository = autoMock.GetMock<IUrlRepository>();
		_request = new(
			Id: "Id",
			OriginalUrl: "Url",
			CreatedAt: DateTime.UtcNow.AddMinutes(-1)
		);
	}

	[Fact]
	public async Task SaveUrl_ShouldReturnFalse_WhenExceptionIsThrow()
	{
		// Arrange
		_urlRepository
			.Setup(
				x => x.SaveUrl(
					It.IsAny<SaveUrlRequest>()
				)
			)
			.ThrowsAsync(new Exception("Exception"));

		// Act
		var result = await _useCase.SaveUrl(
			_request,
			CancellationToken.None
		);

		// Assert
		result.ShouldBeFalse();
		_urlRepository
			.Verify(
				x => x.SaveUrl(
					It.Is<SaveUrlRequest>(x => x.Equals(_request))
				),
				Times.Once
			);
	}

	[Fact]
	public async Task SaveUrl_ShouldReturnTrue()
	{
		// Act
		var result = await _useCase.SaveUrl(
			_request,
			CancellationToken.None
		);

		// Assert
		result.ShouldBeTrue();
		_urlRepository
			.Verify(
				x => x.SaveUrl(
					It.Is<SaveUrlRequest>(x => x.Equals(_request))
				),
				Times.Once
			);
	}
}
