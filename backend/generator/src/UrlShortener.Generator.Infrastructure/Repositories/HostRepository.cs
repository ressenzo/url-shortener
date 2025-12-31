using UrlShortener.Generator.Application.Repositories;

namespace UrlShortener.Generator.Infrastructure.Repositories;

internal sealed class HostRepository : IHostRepository
{
	public Task<string> GetHost()
	{
		throw new NotImplementedException();
	}
}
