namespace UrlShortener.Stats.Application.UseCases.SaveUrl;

public interface ISaveUrlUseCase
{
	Task<bool> SaveUrl(
		SaveUrlRequest request,
		CancellationToken cancellationToken
	);
}
