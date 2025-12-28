using Microsoft.Extensions.Logging;
using UrlShortener.Stats.Application.Repositories;
using UrlShortener.Stats.Domain.Entities;
using UrlShortener.Stats.Domain.Entities.Interfaces;

namespace UrlShortener.Stats.Application.UseCases.SaveUrlStat;

internal sealed class SaveUrlStatUseCase(
	ILogger<SaveUrlStatUseCase> logger,
	IUrlRepository urlRepository
) : ISaveUrlStatUseCase
{
	public async Task<bool> SaveUrl(
		SaveUrlStatRequest request,
		CancellationToken cancellationToken
	)
	{
		try
		{
			var urlStat = await urlRepository.GetUrlStat(
				request.Id,
				cancellationToken
			);
			if (urlStat is null)
			{
				await CreateAndSaveFirstAccess(
					request,
					cancellationToken
				);
			}
			else
			{
				urlStat.AddAccess();
				await SaveUrlStat(
					urlStat,
					cancellationToken
				);
			}
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

	private async Task CreateAndSaveFirstAccess(
		SaveUrlStatRequest request,
		CancellationToken cancellationToken
	)
	{
		var urlStat = new UrlStat(
			request.Id,
			request.OriginalUrl,
			1,
			DateTime.Now
		);
		await SaveUrlStat(
			urlStat,
			cancellationToken
		);
	}

	private async Task SaveUrlStat(
		IUrlStat urlStat,
		CancellationToken cancellationToken
	) =>
		await urlRepository.SaveUrlStat(
			urlStat,
			cancellationToken
		);
}
