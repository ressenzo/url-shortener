using MongoDB.Driver;
using UrlShortener.Generator.Application.Repositories;
using UrlShortener.Generator.Infrastructure.Models;

namespace UrlShortener.Generator.Infrastructure.Repositories;

internal sealed class RedirectHostRepository(
	IMongoDatabase mongoDatabase
) : IRedirectHostRepository
{
	private readonly IMongoCollection<HostModel> _urlCollection =
		mongoDatabase.GetCollection<HostModel>(name: "redirectHost");

	public async Task<string?> GetHost(CancellationToken cancellationToken)
	{
		var hostModel = await _urlCollection
			.Find(FilterDefinition<HostModel>.Empty)
			.Project(x => new HostModel(
				x.Value
			))
			.FirstOrDefaultAsync(cancellationToken);

		return hostModel?.Value;
	}
}
