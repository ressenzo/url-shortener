using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Repositories;

public interface IUrlRepository
{
    public Task<Url> CreateUrl(Url url);
}