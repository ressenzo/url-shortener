using MongoDB.Driver;
using UrlShortener.Redirector.Application.Repositories;
using UrlShortener.Redirector.Application.Models;

namespace UrlShortener.Redirector.Infrastructure.Repositories;

internal sealed class UrlRepository(
	IMongoDatabase mongoDatabase
) : IUrlRepository
{
	private readonly IMongoCollection<UrlModel> _urlCollection =
		mongoDatabase.GetCollection<UrlModel>(name: "urls");

	public async Task<UrlModel?> GetUrl(
		string id,
		CancellationToken cancellationToken
	)
	{
		var urlModel = await _urlCollection
			.Find(x => x.Id.Equals(id))
			.Project(x => new UrlModel(
				x.Id,
				x.OriginalUrl
			))
			.FirstOrDefaultAsync(cancellationToken);
		return urlModel;
	}
}
