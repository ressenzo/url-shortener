using Microsoft.Extensions.Logging;
using ResultPattern;
using UrlShortener.Redirector.Application.Repositories;

namespace UrlShortener.Redirector.Application.UseCases.GetUrl;

internal sealed class GetUrlUseCase(
	ILogger<GetUrlUseCase> logger,
	IUrlRepository urlRepository,
	IUrlStatRepository urlStatRepository
) : IGetUrlUseCase
{
	public async Task<Result<GetUrlResponse>> GetUrl(
		string id,
		CancellationToken cancellationToken
	)
	{
		if (string.IsNullOrWhiteSpace(id))
		{
			logger.LogInformation("Invalid id");
			return Result<GetUrlResponse>
				.ValidationError(["Invalid id"]);
		}

		var url = await urlRepository.GetUrl(id, cancellationToken);
		if (url is null)
		{
			logger.LogInformation(
				message: "Url for id '{Id}' was not found",
				args: id
			);
			return Result<GetUrlResponse>
				.NotFound(["Not found Url for the given id"]);
		}

		await urlStatRepository.NotifyAccess(
			url,
			lastAccessAt: DateTime.Now,
			cancellationToken
		);
		logger.LogInformation(
			message: "Url for id '{Id}' was found",
			args: id
		);
		var response = new GetUrlResponse(url.OriginalUrl);
		return Result<GetUrlResponse>.Success(response);
	}
}
