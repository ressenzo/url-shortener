using MongoDB.Driver;
using UrlShortener.Application.Repositories;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Mappers;

namespace UrlShortener.Infrastructure.Repositories;

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