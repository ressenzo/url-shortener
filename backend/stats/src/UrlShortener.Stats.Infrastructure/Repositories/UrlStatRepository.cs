using MongoDB.Driver;
using UrlShortener.Stats.Application.Repositories;
using UrlShortener.Stats.Domain.Entities.Interfaces;
using UrlShortener.Stats.Infrastructure.Models;

namespace UrlShortener.Stats.Infrastructure.Repositories;

internal sealed class UrlRepository(
	IMongoDatabase mongoDatabase
) : IUrlRepository
{
	private readonly IMongoCollection<UrlStatModel> _urlCollection =
		mongoDatabase.GetCollection<UrlStatModel>(
			name: "urls"
		);

	public async Task<IUrlStat?> GetUrlStat(
		string id,
		CancellationToken cancellationToken
	)
	{
		var stat = await _urlCollection
			.Find(x => x.Id.Equals(id))
			.FirstOrDefaultAsync(cancellationToken);

		return stat?.ToEntity();
	}

	public async Task SaveUrlStat(
		IUrlStat urlStat,
		CancellationToken cancellationToken
	)
	{
		var filter = Builders<UrlStatModel>.Filter.Eq(
			x => x.Id,
			urlStat.Id
		);
		var update = Builders<UrlStatModel>.Update.Combine(
			Builders<UrlStatModel>.Update.Set(x => x.AccessesQuantity, urlStat.AccessesQuantity),
			Builders<UrlStatModel>.Update.Set(x => x.LastAccessAt, urlStat.LastAccessAt)
		);
		var options = new UpdateOptions() { IsUpsert = true };

		await _urlCollection.UpdateOneAsync(
			filter,
			update,
			options,
			cancellationToken
		);
	}
}
