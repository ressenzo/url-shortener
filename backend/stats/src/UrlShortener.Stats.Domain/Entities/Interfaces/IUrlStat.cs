namespace UrlShortener.Stats.Domain.Entities.Interfaces;

public interface IUrlStat
{
	public string Id { get; }

	public string OriginalUrl { get; }

	public int AccessesQuantity { get; }

	public DateTime LastAccessAt { get; }

	public void AddAccess();
}
