using Microsoft.Extensions.Logging;
using UrlShortener.Stats.Application.Repositories;

namespace UrlShortener.Stats.Application.UseCases.SaveUrl;

internal sealed class SaveUrlUseCase(
	ILogger<SaveUrlUseCase> logger,
	IUrlRepository urlRepository
) : ISaveUrlUseCase
{
	public async Task<bool> SaveUrl(
		SaveUrlRequest request,
		CancellationToken cancellationToken
	)
	{
		try
		{
			if (request is null)
				return false;
			logger.LogInformation(
				"Saving url of id {Id}",
				request.Id
			);
			await urlRepository.SaveUrl(request, cancellationToken);
			logger.LogInformation(
				"Url of id {Id} saved",
				request.Id
			);
			return true;
		}
		catch (Exception ex)
		{
			logger.LogError(
				ex,
				"{Message}",
				ex.Message
			);
			return false;
		}
	}
}
