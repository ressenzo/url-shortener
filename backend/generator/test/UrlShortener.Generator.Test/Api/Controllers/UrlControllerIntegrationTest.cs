using System.Net;
using System.Net.Http.Json;
using UrlShortener.Generator.Api.Requests;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;
using UrlShortener.Generator.Test.Shared;

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
