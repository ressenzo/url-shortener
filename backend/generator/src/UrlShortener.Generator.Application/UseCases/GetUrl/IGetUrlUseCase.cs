using UrlShortener.Generator.Application.Shared;

namespace UrlShortener.Generator.Application.UseCases.GetUrl;

public interface IGetUrlUseCase
{
	Task<Result<GetUrlResponse>> GetUrl(
		string id,
		CancellationToken cancellationToken);
}
