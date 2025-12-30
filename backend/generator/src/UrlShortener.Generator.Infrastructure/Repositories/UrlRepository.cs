using MongoDB.Driver;
using UrlShortener.Generator.Application.Repositories;
using UrlShortener.Generator.Domain.Entities;
using UrlShortener.Generator.Infrastructure.Models;

namespace UrlShortener.Generator.Infrastructure.Repositories;

internal sealed class UrlRepository(
	IMongoDatabase mongoDatabase
) : IUrlRepository
{
	private readonly IMongoCollection<UrlModel> _urlCollection =
		mongoDatabase.GetCollection<UrlModel>(name: "urls");

	public async Task<Url> CreateUrl(
		Url url,
		CancellationToken cancellationToken)
	{
		var urlModel = UrlModel.FromEntity(url);
		await _urlCollection.InsertOneAsync(
			urlModel,
			options: null,
			cancellationToken
		);
		return url;
	}

	public async Task<Url?> GetUrl(
		string id,
		CancellationToken cancellationToken)
	{
		var urlModel = await _urlCollection
			.Find(x => x.Id.Equals(id))
			.FirstOrDefaultAsync(cancellationToken);
		return urlModel?.ToEntity();
	}
}
