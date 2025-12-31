using MongoDB.Driver;
using UrlShortener.Generator.Application.Repositories;
using UrlShortener.Generator.Infrastructure.Models;

namespace UrlShortener.Generator.Infrastructure.Repositories;

internal sealed class HostRepository(
	IMongoDatabase mongoDatabase
) : IHostRepository
{
	private readonly IMongoCollection<HostModel> _urlCollection =
		mongoDatabase.GetCollection<HostModel>(name: "host");

	public async Task<string?> GetHost(CancellationToken cancellationToken)
	{
		var hostModel = await _urlCollection
			.Find(FilterDefinition<HostModel>.Empty)
			.FirstOrDefaultAsync(cancellationToken);

		return hostModel?.Value;
	}
}
