using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Api.Requests;

[ExcludeFromCodeCoverage]
public record ShortenUrlRequest(string OriginalUrl);
