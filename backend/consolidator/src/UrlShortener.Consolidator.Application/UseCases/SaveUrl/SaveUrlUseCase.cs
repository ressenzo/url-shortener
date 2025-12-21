using Microsoft.Extensions.Logging;
using UrlShortener.Consolidator.Application.Repositories;

namespace UrlShortener.Consolidator.Application.UseCases.SaveUrl;

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
			await urlRepository.SaveUrl(request);
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
