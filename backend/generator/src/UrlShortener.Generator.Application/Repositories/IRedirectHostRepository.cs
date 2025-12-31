namespace UrlShortener.Generator.Application.Repositories;

public interface IRedirectHostRepository
{
	Task<string?> GetHost(CancellationToken cancellationToken);
}
