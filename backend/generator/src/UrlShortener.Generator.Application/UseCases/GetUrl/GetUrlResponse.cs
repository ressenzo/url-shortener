using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Generator.Application.UseCases.GetUrl;

[ExcludeFromCodeCoverage]
public record GetUrlResponse(string OriginalUrl);
