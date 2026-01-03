using Microsoft.Extensions.Logging;
using ResultPattern;
using UrlShortener.Generator.Application.Repositories;
using UrlShortener.Generator.Domain.Entities;
using UrlShortener.Generator.Domain.Services.UrlService;

namespace UrlShortener.Generator.Application.UseCases.ShortenUrl;

internal sealed class ShortenUrlUseCase(
	ILogger<ShortenUrlUseCase> logger,
	IUrlRepository urlRepository,
	IRedirectHostRepository redirectHostRepository,
	IUrlService urlService
) : IShortenUrlUseCase
{
	public async Task<Result<ShortenUrlResponse>> ShortenUrl(
		string originalUrl,
		CancellationToken cancellationToken
	)
	{
		var url = new Url(originalUrl);
		if (!url.IsValid())
		{
			logger.LogInformation("Url is not valid");
			return Result<ShortenUrlResponse>.ValidationError(url.Errors);
		}

		var host = await redirectHostRepository.GetHost(
			cancellationToken
		) ?? "http://localhost:5001";

		await urlRepository.CreateUrl(url, cancellationToken);
		var shortenedUrl = urlService.GetUrl(
			host,
			url.Id
		);
		var response = new ShortenUrlResponse()
		{
			ShortenedUrl = shortenedUrl
		};
		return Result<ShortenUrlResponse>.Success(response);
	}
}
