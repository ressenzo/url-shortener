namespace UrlShortener.Generator.Domain.Entities;

public class Url : Entity
{
	public Url(
		string id,
		string originalUrl
	) : base(id) =>
		OriginalUrl = originalUrl;

	public Url(string originalUrl)
		: base(id: null) =>
		(OriginalUrl, CreatedAt) = (originalUrl, DateTime.UtcNow);

	public string OriginalUrl
	{
		get;
	}

	public DateTime CreatedAt
	{
		get;
	}

	public override bool IsValid()
	{
		if (string.IsNullOrWhiteSpace(OriginalUrl))
		{
			AddError("Original Url was not provided");
			return false;
		}

		if (!Uri.IsWellFormedUriString(OriginalUrl, UriKind.RelativeOrAbsolute))
		{
			AddError("Provided Original Url is not valid");
			return false;
		}

		return true;
	}
}
