using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/urls")]
public class UrlController : ControllerBase
{
    [HttpGet("{url}")]
    public async Task<IActionResult> Get(
        [FromRoute] string url,
        CancellationToken cancellationToken)
    {
        var request = HttpContext.Request;
        return await Task.FromResult(Ok(new
            {
                url = $"{request.Scheme}://{request.Host}"
            }));
    }
}
