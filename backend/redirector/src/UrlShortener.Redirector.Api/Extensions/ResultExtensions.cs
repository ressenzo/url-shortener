using UrlShortener.Redirector.Application.Shared;

namespace UrlShortener.Redirector.Api.Extensions;

internal static class ResultExtensions
{
	public static IResult ToInternalServerError<T>(
		this Result<T> _
	) where T : class
	{
		var value = Result<T>.InternalError();
		return Results.InternalServerError(value);
	}
}
