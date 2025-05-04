using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Mappers;

internal class UrlMapper
{
    public string Id { get; init; } = default!;
    
    public string OriginalUrl { get; init; } = default!;

    public DateTime CreatedAt { get; init; }

    public Url ToEntity() =>
        Url.Factory(
            Id,
            OriginalUrl);

    public static UrlMapper FromEntity(Url url) =>
        new()
        {
            CreatedAt = url.CreatedAt,
            Id = url.Id,
            OriginalUrl = url.OriginalUrl
        };
}
