namespace UrlShortener.Domain.Entities;

public class Url : Entity
{
	private Url(
		string id,
		string originalUrl)
			: base(id) =>
		OriginalUrl = originalUrl;

	private Url(string originalUrl)
			: base(id: null) =>
		(OriginalUrl, CreatedAt) = (originalUrl, DateTime.UtcNow);

	public string OriginalUrl { get; }

	public DateTime CreatedAt { get; }

	public static Url Factory(
		string id,
		string originalUrl) =>
		new(
			id,
			originalUrl);

	public static Url Factory(
		string originalUrl) =>
		new(originalUrl);

	public override bool IsValid()
	{
		if (string.IsNullOrWhiteSpace(OriginalUrl))
		{
			AddErrors(["Original Url was not provided"]);
			return false;
		}

		if (!Uri.IsWellFormedUriString(OriginalUrl, UriKind.RelativeOrAbsolute))
		{
			AddErrors(["Provided Original Url is not valid"]);
			return false;
		}

		return true;
	}
}
