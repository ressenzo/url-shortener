using UrlShortener.Stats.Domain.Entities;

namespace UrlShortener.Stats.Test.Domain.Entities;

public class UrlStatTest
{
	[Fact]
	public void ShouldSetValues()
	{
		// Arrange
		string id = "12345678";
		string originalUrl = "http://test.com";
		int accessesQuantity = 3;
		DateTime lastAccess = DateTime.Now.AddDays(-1);

		// Act
		var urlStat = new UrlStat(
			id,
			originalUrl,
			accessesQuantity,
			lastAccess
		);

		// Assert
		urlStat.Id.ShouldBe(id);
		urlStat.OriginalUrl.ShouldBe(originalUrl);
		urlStat.AccessesQuantity.ShouldBe(accessesQuantity);
		urlStat.LastAccess.ShouldBe(lastAccess);
	}

	[Fact]
	public void ShouldAddAccessesQuantityAndSetLastAccess_WhenAddAccess()
	{
		// Arrange
		string id = "12345678";
		string originalUrl = "http://test.com";
		int accessesQuantity = 3;
		DateTime lastAccess = DateTime.Now.AddDays(-1);
		var urlStat = new UrlStat(
			id,
			originalUrl,
			accessesQuantity,
			lastAccess
		);

		// Act
		urlStat.AddAccess();

		// Assert
		urlStat.AccessesQuantity.ShouldBe(accessesQuantity + 1);
		urlStat.LastAccess.ShouldBeGreaterThan(lastAccess);
	}
}
