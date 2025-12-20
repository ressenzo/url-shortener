using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Generator.Api.Requests;

[ExcludeFromCodeCoverage]
public record ShortenUrlRequest(string OriginalUrl);
