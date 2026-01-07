using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ResultPattern;
using UrlShortener.Generator.Api.Requests;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;

namespace UrlShortener.Generator.Test.Api.Controllers;

public class UrlControllerIntegrationTest(
	CustomWebApplicationFactory factory
) : IClassFixture<CustomWebApplicationFactory>
{
	private readonly CustomWebApplicationFactory _factory = factory;

	[Fact]
	public async Task ShortenUrl_ShouldReturnCreated_WhenValidUrlProvided()
	{
		// Arrange
		var client = _factory.CreateClient();
		var request = new ShortenUrlRequest("https://www.example.com");

		// Act
		var response = await client.PostAsJsonAsync("/api/urls", request);

		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.Created);
		var result = await response.Content.ReadFromJsonAsync<ShortenUrlResponse>();
		result.ShouldNotBeNull();
		result.ShortenedUrl.ShouldNotBeNullOrEmpty();
		result.ShortenedUrl.ShouldStartWith("http");
	}

	[Fact]
	public async Task ShortenUrl_ShouldReturnBadRequest_WhenInvalidUrlProvided()
	{
		// Arrange
		var client = _factory.CreateClient();
		var request = new ShortenUrlRequest("invalid-url");

		// Act
		var response = await client.PostAsJsonAsync("/api/urls", request);

		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
	}

	[Fact]
	public async Task ShortenUrl_ShouldReturnBadRequest_WhenEmptyUrlProvided()
	{
		// Arrange
		var client = _factory.CreateClient();
		var request = new ShortenUrlRequest("");

		// Act
		var response = await client.PostAsJsonAsync("/api/urls", request);

		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
	}
}

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			// Mock the use case to avoid database dependency
			var shortenUrlUseCaseMock = new Mock<IShortenUrlUseCase>();

			// Setup for valid URL
			shortenUrlUseCaseMock.Setup(x => x.ShortenUrl("https://www.example.com", It.IsAny<CancellationToken>()))
				.ReturnsAsync(Result<ShortenUrlResponse>.Success(new ShortenUrlResponse { ShortenedUrl = "https://short.ly/abc123" }));

			// Setup for invalid URL
			shortenUrlUseCaseMock.Setup(x => x.ShortenUrl("invalid-url", It.IsAny<CancellationToken>()))
				.ReturnsAsync(Result<ShortenUrlResponse>.ValidationError(["Invalid URL"]));

			// Setup for empty URL
			shortenUrlUseCaseMock.Setup(x => x.ShortenUrl("", It.IsAny<CancellationToken>()))
				.ReturnsAsync(Result<ShortenUrlResponse>.ValidationError(["Original Url was not provided"]));

			services.AddSingleton(shortenUrlUseCaseMock.Object);
		});
	}
}
