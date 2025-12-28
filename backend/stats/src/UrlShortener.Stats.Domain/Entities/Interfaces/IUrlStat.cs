namespace UrlShortener.Stats.Domain.Entities.Interfaces;

internal interface IUrlStat
{
	public string Id { get; }

	public string OriginalUrl { get; }

	public int AccessesQuantity { get; }

	public DateTime LastAccess { get; }

	public void AddAccess();
}
