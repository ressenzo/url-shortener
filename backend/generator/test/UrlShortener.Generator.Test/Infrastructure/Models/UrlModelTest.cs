using UrlShortener.Generator.Infrastructure.Models;
using UrlShortener.Generator.Test.Shared;

namespace UrlShortener.Generator.Test.Infrastructure.Models;

public class UrlModelTest
{
	[Fact]
	public void Factory_ShouldReturnEntity_WhenCallToEntity()
	{
		// Arrange
		var urlModel = new UrlModel(
			id: "a1b2",
			originalUrl: "google.com",
			createdAt: DateTime.Now
		);

		// Act
		var url = urlModel.ToEntity();


		// Assert
		url.ShouldNotBeNull();
		url.Id.ShouldBe(urlModel.Id);
		url.OriginalUrl.ShouldBe(urlModel.OriginalUrl);
	}

	[Fact]
	public void Factory_ShouldReturnModel_WhenCallFromEntity()
	{
		// Arrange
		var url = new UrlBuilder()
			.Build();

		// Act
		var urlModel = UrlModel.FromEntity(url);


		// Assert
		url.ShouldNotBeNull();
		url.Id.ShouldBe(urlModel.Id);
		url.OriginalUrl.ShouldBe(urlModel.OriginalUrl);
		url.CreatedAt.ShouldBe(urlModel.CreatedAt);
	}
}
