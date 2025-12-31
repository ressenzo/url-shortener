using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using UrlShortener.Generator.Api.Controllers;
using UrlShortener.Generator.Api.Requests;
using UrlShortener.Generator.Application.Shared;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;

namespace UrlShortener.Generator.Test.Api.Controllers;

public class UrlControllerTest
{
	private readonly UrlController _controller;
	private readonly Mock<IShortenUrlUseCase> _shortenUrlUseCase;

	public UrlControllerTest()
	{
		var mocker = new AutoMocker();
		_controller = mocker.CreateInstance<UrlController>();
		_shortenUrlUseCase = mocker.GetMock<IShortenUrlUseCase>();

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
}
