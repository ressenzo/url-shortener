namespace UrlShortener.Generator.Application.Repositories;

public interface IHostRepository
{
	Task<string?> GetHost(CancellationToken cancellationToken);
}
