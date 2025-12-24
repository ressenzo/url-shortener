namespace UrlShortener.Generator.Api.Configurations;

internal static class AppConfigurations
{
	// TODO: create class to store this value
	// and dont repeat it
	private const string _CORS_NAME = "All";

	public static IApplicationBuilder AddConfigurations(
		this WebApplication app
	)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCors(_CORS_NAME)
			.UseHttpsRedirection()
			.UseAuthorization();
		app.MapHealthChecks("/healthz");
		app.MapControllers();

		return app;
	}
}
