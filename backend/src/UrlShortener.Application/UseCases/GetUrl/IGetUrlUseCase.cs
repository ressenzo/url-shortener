using UrlShortener.Application.Shared;

namespace UrlShortener.Application.UseCases.GetUrl;

public interface IGetUrlUseCase
{
	Task<Result<GetUrlResponse>> GetUrl(
		string id,
		CancellationToken cancellationToken);
}
