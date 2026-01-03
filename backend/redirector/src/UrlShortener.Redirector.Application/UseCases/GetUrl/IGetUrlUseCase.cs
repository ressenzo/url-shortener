using ResultPattern;

namespace UrlShortener.Redirector.Application.UseCases.GetUrl;

public interface IGetUrlUseCase
{
	Task<Result<GetUrlResponse>> GetUrl(
		string id,
		CancellationToken cancellationToken
	);
}
