using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using UrlShortener.Generator.Api.Controllers;
using UrlShortener.Generator.Api.Requests;
using UrlShortener.Generator.Application.Shared;
using UrlShortener.Generator.Application.UseCases.GetUrl;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;

namespace UrlShortener.Generator.Test.Api.Controllers;

public class UrlControllerTest
{
	private readonly UrlController _controller;
	private readonly Mock<IShortenUrlUseCase> _shortenUrlUseCase;
	private readonly Mock<IGetUrlUseCase> _getUrlUseCase;

	public UrlControllerTest()
	{
		var mocker = new AutoMocker();
		_controller = mocker.CreateInstance<UrlController>();
		_shortenUrlUseCase = mocker.GetMock<IShortenUrlUseCase>();
		_getUrlUseCase = mocker.GetMock<IGetUrlUseCase>();

		var httpContext = new DefaultHttpContext();
		httpContext.Request.Scheme = "https";
		httpContext.Request.Host = new HostString("example.com");
		_controller.ControllerContext = new ControllerContext
		{
			HttpContext = httpContext
		};
	}

	[Fact]
	public async Task ShortenUrl_ShouldReturnCreated_WhenSuccess()
	{
		// Arrange
		var request = new ShortenUrlRequest(
			"https://original.url"
		);
		var response = new ShortenUrlResponse
		{
			ShortenedUrl = "https://example.com/abc123"
		};
		var result = Result<ShortenUrlResponse>
			.Success(response);
		_shortenUrlUseCase
			.Setup(
				x => x.ShortenUrl(
					"https://example.com",
					"https://original.url",
					It.IsAny<CancellationToken>()
				)
			)
			.ReturnsAsync(result);

		// Act
		var actionResult = await _controller.ShortenUrl(
			request, CancellationToken.None
		);

		// Assert
		var createdResult = actionResult
			.ShouldBeOfType<CreatedResult>();
		createdResult.Value.ShouldBe(response);
	}

	[Fact]
	public async Task ShortenUrl_ShouldReturnBadRequest_WhenValidationError()
	{
		// Arrange
		var request = new ShortenUrlRequest(
			"invalid-url"
		);
		var result = Result<ShortenUrlResponse>
			.ValidationError("Invalid URL");
		_shortenUrlUseCase
			.Setup(
				x => x.ShortenUrl(
					"https://example.com",
					"invalid-url",
					It.IsAny<CancellationToken>()
				)
			)
			.ReturnsAsync(result);

		// Act
		var actionResult = await _controller.ShortenUrl(
			request,
			CancellationToken.None
		);

		// Assert
		var badRequestResult = actionResult
			.ShouldBeOfType<BadRequestObjectResult>();
		badRequestResult.Value.ShouldBe(result);
	}

	[Fact]
	public async Task ShortenUrl_ShouldReturnInternalError_WhenNotExpected()
	{
		// Arrange
		var request = new ShortenUrlRequest(
			"invalid-url"
		);
		var result = Result<ShortenUrlResponse>
			.NotFound("Not found");
		_shortenUrlUseCase
			.Setup(
				x => x.ShortenUrl(
					"https://example.com",
					"invalid-url",
					It.IsAny<CancellationToken>()
				)
			)
			.ReturnsAsync(result);

		// Act
		var actionResult = await _controller.ShortenUrl(
			request,
			CancellationToken.None
		);

		// Assert
		var internalServerErrorResult = actionResult
			.ShouldBeOfType<ObjectResult>();
		internalServerErrorResult.StatusCode.ShouldBe(
			(int)HttpStatusCode.InternalServerError
		);
		internalServerErrorResult.Value.ShouldBeEquivalentTo(
			Result<ShortenUrlResponse>.InternalError()
		);
	}
	
	[Fact]
	public async Task GetUrl_ShouldReturnRedirect_WhenSuccess()
	{
		// Arrange
		var id = "abc123";
		var response = new GetUrlResponse(
			"https://original.url"
		);
		var result = Result<GetUrlResponse>
			.Success(response);
		_getUrlUseCase
			.Setup(
				x => x.GetUrl(
					id,
					It.IsAny<CancellationToken>()
				)
			)
			.ReturnsAsync(result);

		// Act
		var actionResult = await _controller.GetUrl(
			id,
			CancellationToken.None
		);

		// Assert
		var redirectResult = actionResult
			.ShouldBeOfType<RedirectResult>();
		redirectResult.Url.ShouldBe("https://original.url");
	}

	[Fact]
	public async Task GetUrl_ShouldReturnBadRequest_WhenValidationError()
	{
		// Arrange
		var id = "invalid";
		var result = Result<GetUrlResponse>
			.ValidationError("Invalid id");
		_getUrlUseCase
			.Setup(
				x => x.GetUrl(
					id,
					It.IsAny<CancellationToken>()
				)
			)
			.ReturnsAsync(result);

		// Act
		var actionResult = await _controller.GetUrl(
			id,
			CancellationToken.None
		);

		// Assert
		var badRequestResult = actionResult
			.ShouldBeOfType<BadRequestObjectResult>();
		badRequestResult.Value.ShouldBe(result);
	}

	[Fact]
	public async Task GetUrl_ShouldReturnNotFound_WhenNotFound()
	{
		// Arrange
		var id = "nonexistent";
		var result = Result<GetUrlResponse>
			.NotFound("Not found");
		_getUrlUseCase
			.Setup(
				x => x.GetUrl(
					id,
					It.IsAny<CancellationToken>()
				)
			)
			.ReturnsAsync(result);

		// Act
		var actionResult = await _controller.GetUrl(
			id,
			CancellationToken.None
		);

		// Assert
		var notFoundResult = actionResult
			.ShouldBeOfType<NotFoundObjectResult>();
		notFoundResult.Value.ShouldBe(result);
	}

	[Fact]
	public async Task GetUrl_ShouldReturnInternalError_WhenNotExpected()
	{
		// Arrange
		var id = "nonexistent";
		var result = Result<GetUrlResponse>
			.InternalError();
		_getUrlUseCase
			.Setup(
				x => x.GetUrl(
					id,
					It.IsAny<CancellationToken>()
				)
			)
			.ReturnsAsync(result);

		// Act
		var actionResult = await _controller.GetUrl(
			id,
			CancellationToken.None
		);

		// Assert
		var internalServerErrorResult = actionResult
			.ShouldBeOfType<ObjectResult>();
		internalServerErrorResult.StatusCode.ShouldBe(
			(int)HttpStatusCode.InternalServerError
		);
		internalServerErrorResult.Value.ShouldBeEquivalentTo(
			Result<GetUrlResponse>.InternalError()
		);
	}
}
