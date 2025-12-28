namespace UrlShortener.Stats.Application.UseCases.SaveUrlStat;

public interface ISaveUrlStatUseCase
{
	Task<bool> SaveUrl(
		SaveUrlStatRequest request,
		CancellationToken cancellationToken
	);
}
