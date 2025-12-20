using MongoDB.Driver;
using UrlShortener.Generator.Application.Repositories;
using UrlShortener.Generator.Domain.Entities;
using UrlShortener.Generator.Infrastructure.Mappers;

namespace UrlShortener.Generator.Infrastructure.Repositories;

internal sealed class UrlRepository(
	IMongoDatabase mongoDatabase) : IUrlRepository
{
	private readonly IMongoCollection<UrlMapper> _urlCollection =
		mongoDatabase.GetCollection<UrlMapper>(name: "urls");

	public async Task<Url> CreateUrl(
		Url url,
		CancellationToken cancellationToken)
	{
		var urlMapper = UrlMapper.FromEntity(url);
		await _urlCollection.InsertOneAsync(
			urlMapper,
			options: null,
			cancellationToken);
		return url;
	}

	public async Task<Url?> GetUrl(
		string id,
		CancellationToken cancellationToken)
	{
		var urlMapper = await _urlCollection
			.Find(x => x.Id.Equals(id))
			.FirstOrDefaultAsync(cancellationToken);
		return urlMapper?.ToEntity();
	}
}
