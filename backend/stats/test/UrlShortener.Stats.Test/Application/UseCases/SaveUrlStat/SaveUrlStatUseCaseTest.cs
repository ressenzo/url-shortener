using Moq.AutoMock;
using UrlShortener.Stats.Application.Repositories;
using UrlShortener.Stats.Application.UseCases.SaveUrlStat;
using UrlShortener.Stats.Domain.Entities.Interfaces;

namespace UrlShortener.Stats.Test.Application.UseCases.SaveUrlStat;

public class SaveUrlUseCaseTest
{
	private readonly SaveUrlStatUseCase _useCase;
	private readonly Mock<IUrlRepository> _urlRepository;
	private SaveUrlStatRequest _request;
	private readonly AutoMocker _autoMock;

	public SaveUrlUseCaseTest()
	{
		_autoMock = new AutoMocker();
		_useCase = _autoMock.CreateInstance<SaveUrlStatUseCase>();
		_urlRepository = _autoMock.GetMock<IUrlRepository>();
		_request = new(
			Id: "Id",
			OriginalUrl: "Url",
			CreatedAt: DateTime.UtcNow.AddMinutes(-1)
		);
	}

	[Fact]
	public async Task SaveUrl_ShouldReturnFalse_WhenRequestIsNull()
	{
		// Arrange
		_request = null!;

		// Act
		var result = await _useCase.SaveUrl(
			_request,
			CancellationToken.None
		);

		// Assert
		result.ShouldBeFalse();
		_urlRepository
			.Verify(
				x => x.GetUrlStat(
					It.IsAny<string>(),
					CancellationToken.None
				),
				Times.Never
			);
		_urlRepository
			.Verify(
				x => x.SaveUrlStat(
					It.Is<IUrlStat>(x => x.Equals(_request)),
					CancellationToken.None
				),
				Times.Never
			);
	}

	[Fact]
	public async Task SaveUrl_ShouldReturnFalse_WhenExceptionIsThrow()
	{
		// Arrange
		_urlRepository
			.Setup(
				x => x.GetUrlStat(
					It.IsAny<string>(),
					CancellationToken.None
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
	}

	[Fact]
	public async Task SaveUrl_ShouldReturnTrue_WhenUrlStatIsNotFound()
	{
		// Arrange
		_urlRepository
			.Setup(
				x => x.GetUrlStat(
					It.IsAny<string>(),
					CancellationToken.None
				)
			)
			.ReturnsAsync((IUrlStat?)null);

		// Act
		var result = await _useCase.SaveUrl(
			_request,
			CancellationToken.None
		);

		// Assert
		result.ShouldBeTrue();
		_urlRepository
			.Verify(
				x => x.SaveUrlStat(
					It.Is<IUrlStat>(
						x => x.Id.Equals(_request.Id) &&
							x.OriginalUrl.Equals(_request.OriginalUrl) &&
							x.AccessesQuantity.Equals(1)
					),
					It.IsAny<CancellationToken>()
				),
				Times.Once
			);
	}

	[Fact]
	public async Task SaveUrl_ShouldReturnTrue_WhenUrlStatIsFound()
	{
		// Arrange
		var urlStat = _autoMock.GetMock<IUrlStat>();
		_urlRepository
			.Setup(
				x => x.GetUrlStat(
					It.IsAny<string>(),
					CancellationToken.None
				)
			)
			.ReturnsAsync(urlStat.Object);

		// Act
		var result = await _useCase.SaveUrl(
			_request,
			CancellationToken.None
		);

		// Assert
		result.ShouldBeTrue();
		urlStat.Verify(
			x => x.AddAccess(),
			Times.Once
		);
		_urlRepository
			.Verify(
				x => x.SaveUrlStat(
					It.Is<IUrlStat>(x => x.Equals(urlStat.Object)),
					It.IsAny<CancellationToken>()
				),
				Times.Once
			);
	}

	// [Fact]
	// public async Task SaveUrl_ShouldReturnTrue()
	// {
	// 	// Act
	// 	var result = await _useCase.SaveUrl(
	// 		_request,
	// 		CancellationToken.None
	// 	);

	// 	// Assert
	// 	result.ShouldBeTrue();
	// 	_urlRepository
	// 		.Verify(
	// 			x => x.SaveUrl(
	// 				It.Is<SaveUrlStatRequest>(x => x.Equals(_request)),
	// 				CancellationToken.None
	// 			),
	// 			Times.Once
	// 		);
	// }
}
