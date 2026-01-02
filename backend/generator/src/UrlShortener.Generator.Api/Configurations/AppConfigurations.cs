using UrlShortener.Generator.Api.Commons;

namespace UrlShortener.Generator.Api.Configurations;

internal static class AppConfigurations
{
	public static IApplicationBuilder AddConfigurations(
		this WebApplication app
	)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCors(Constants._CORS_NAME)
			.UseHttpsRedirection()
			.UseAuthorization();
		app.MapHealthChecks("/healthz");
		app.MapControllers();

		return app;
	}
}
