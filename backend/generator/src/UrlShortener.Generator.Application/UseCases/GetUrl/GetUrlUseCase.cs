using Microsoft.Extensions.Logging;
using UrlShortener.Generator.Application.Repositories;
using UrlShortener.Generator.Application.Shared;

namespace UrlShortener.Generator.Application.UseCases.GetUrl;

internal sealed class GetUrlUseCase(
	ILogger<GetUrlUseCase> logger,
	IUrlRepository urlRepository,
	IUrlStatRepository urlStatRepository) : IGetUrlUseCase
{
	public async Task<Result<GetUrlResponse>> GetUrl(
		string id,
		CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(id))
		{
			logger.LogInformation("Invalid id");
			return Result<GetUrlResponse>.ValidationError("Invalid id");
		}

		var url = await urlRepository.GetUrl(id, cancellationToken);
		if (url is null)
		{
			logger.LogInformation(
				message: "Url for {Id} id was not found",
				args: id);
			return Result<GetUrlResponse>.NotFound("Not found Url for the given id");
		}

		await urlStatRepository.NotifyAccess(
			url,
			lastAccessAt: DateTime.Now,
			cancellationToken
		);
		var response = new GetUrlResponse(url.OriginalUrl);
		return Result<GetUrlResponse>.Success(response);
	}
}
