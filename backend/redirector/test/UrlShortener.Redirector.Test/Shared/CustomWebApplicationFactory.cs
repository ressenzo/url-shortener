using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ResultPattern;
using UrlShortener.Redirector.Application.UseCases.GetUrl;


namespace UrlShortener.Redirector.Test.Shared;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			// Mock the use case to avoid database dependency
			var getUrlUseCaseMock = new Mock<IGetUrlUseCase>();

			getUrlUseCaseMock
				.Setup(x => x.GetUrl("12345678", It.IsAny<CancellationToken>()))
				.ReturnsAsync(Result<GetUrlResponse>.Success(
					new GetUrlResponse(OriginalUrl: "https://google.com")
				));

			getUrlUseCaseMock
				.Setup(x => x.GetUrl("not-found", It.IsAny<CancellationToken>()))
				.ReturnsAsync(Result<GetUrlResponse>.NotFound(
					["Not found URL"]
				));

			getUrlUseCaseMock
				.Setup(x => x.GetUrl("validation-error", It.IsAny<CancellationToken>()))
				.ReturnsAsync(Result<GetUrlResponse>.ValidationError(
					["Original Url was not provided"]
				));

			services.AddSingleton(getUrlUseCaseMock.Object);
		});
	}
}
