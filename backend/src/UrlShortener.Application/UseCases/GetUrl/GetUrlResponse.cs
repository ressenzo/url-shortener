using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Application.UseCases.GetUrl;

[ExcludeFromCodeCoverage]
public record GetUrlResponse(string OriginalUrl);
