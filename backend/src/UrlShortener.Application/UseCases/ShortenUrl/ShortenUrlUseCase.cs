using Microsoft.Extensions.Logging;
using UrlShortener.Application.Repositories;
using UrlShortener.Application.Shared;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Services.UrlService;

namespace UrlShortener.Application.UseCases.ShortenUrl;

internal sealed class ShortenUrlUseCase(
	ILogger<ShortenUrlUseCase> logger,
	IUrlRepository urlRepository,
	IUrlService urlService) : IShortenUrlUseCase
{
	public async Task<Result<ShortenUrlResponse>> ShortenUrl(
		string host,
		string originalUrl,
		CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(host))
		{
			logger.LogInformation("Host was not provided");
			return Result<ShortenUrlResponse>.ValidationError(
				errors: ["Host was not provided"]);
		}
		var url = Url.Factory(
			originalUrl);
		if (!url.IsValid())
		{
			logger.LogInformation("Url is not valid");
			return Result<ShortenUrlResponse>.ValidationError(url.Errors);
		}

		await urlRepository.CreateUrl(url, cancellationToken);
		var shortenedUrl = urlService.GetUrl(
			host,
			url.Id);
		var response = new ShortenUrlResponse()
		{
			ShortenedUrl = shortenedUrl
		};
		return Result<ShortenUrlResponse>.Success(response);
	}
}
