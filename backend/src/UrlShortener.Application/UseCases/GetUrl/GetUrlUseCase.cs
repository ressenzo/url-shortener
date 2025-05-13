using Microsoft.Extensions.Logging;
using UrlShortener.Application.Repositories;
using UrlShortener.Application.Shared;

namespace UrlShortener.Application.UseCases.GetUrl;

internal sealed class GetUrlUseCase(
    ILogger<GetUrlUseCase> logger,
    IUrlRepository urlRepository) : IGetUrlUseCase
{
    public async Task<Result<GetUrlResponse>> GetUrl(
        string id,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            logger.LogInformation("Invalid id");
            return Result<GetUrlResponse>.ValidationError("Invalid id");
        }

        var url = await urlRepository.GetUrl(id, cancellationToken);
        if (url is null)
        {
            logger.LogInformation(
                message: "Url for {Id} id was not found",
                args: id);
            return Result<GetUrlResponse>.NotFound("Not found Url for the given id");
        }
        
        var response = new GetUrlResponse(url.OriginalUrl);
        return Result<GetUrlResponse>.Success(response);
    }
}
