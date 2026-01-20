using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ResultPattern;
using UrlShortener.Generator.Application.UseCases.ShortenUrl;


namespace UrlShortener.Generator.Test.Shared;

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
