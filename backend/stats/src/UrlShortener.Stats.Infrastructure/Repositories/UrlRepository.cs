using MongoDB.Driver;
using UrlShortener.Stats.Application.Repositories;
using UrlShortener.Stats.Application.UseCases.SaveUrl;

namespace UrlShortener.Stats.Infrastructure.Repositories;

internal sealed class UrlRepository(
	IMongoDatabase mongoDatabase
) : IUrlRepository
{
	private readonly IMongoCollection<SaveUrlRequest> _urlCollection =
		mongoDatabase.GetCollection<SaveUrlRequest>(
			name: "urls"
		);

	public async Task SaveUrl(
		SaveUrlRequest url,
		CancellationToken cancellationToken
	) =>
		await _urlCollection.InsertOneAsync(
			url,
			options: null,
			cancellationToken
		);
}
