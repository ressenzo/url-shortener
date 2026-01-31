using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using UrlShortener.Redirector.Test.Shared;

namespace UrlShortener.Redirector.Test.Api.Endpoints;

public class RedirectEndpointIntegrationTest(
	CustomWebApplicationFactory factory
) : IClassFixture<CustomWebApplicationFactory>
{
	private readonly CustomWebApplicationFactory _factory = factory;

	[Fact]
	public async Task GetUrl_ShouldReturnPermanetRedirect_WhenUrlIsFound()
	{
		// Arrange
		var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
		{
			AllowAutoRedirect = false
		});

		// Act
		var response = await client.GetAsync("/12345678");

		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.PermanentRedirect);
	}

	[Fact]
	public async Task GetUrl_ShouldReturnNotFound_WhenUrlIsNotFound()
	{
		// Arrange
		var client = _factory.CreateClient();

		// Act
		var response = await client.GetAsync("/not-found");

		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
	}

	[Fact]
	public async Task GetUrl_ShouldReturnBadRequest_WhenThereIsValidationError()
	{
		// Arrange
		var client = _factory.CreateClient();

		// Act
		var response = await client.GetAsync("/validation-error");

		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
	}
}
