using UrlShortener.Application.Repositories;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Repositories;

internal sealed class UrlRepository : IUrlRepository
{
    public Task<Url> CreateUrl(Url url)
    {
        throw new NotImplementedException();
    }
}