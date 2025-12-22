using Microsoft.AspNetCore.Mvc;
using UrlShortener.Generator.Application.Shared;

namespace UrlShortener.Generator.Api.Extensions;

internal static class ResultExtensions
{
	public static IActionResult ToInternalServerError<T>(
		this Result<T> _
	) where T : class
	{
		var value = Result<T>.InternalError();
		return new ObjectResult(value)
		{
			StatusCode = StatusCodes.Status500InternalServerError
		};
	}
}
