using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Redirector.Application.UseCases.GetUrl;

[ExcludeFromCodeCoverage]
public record GetUrlResponse(string OriginalUrl);
