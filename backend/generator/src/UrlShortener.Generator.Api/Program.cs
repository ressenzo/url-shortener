using UrlShortener.Generator.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConfigurations(
	builder.Configuration
);

var app = builder.Build();
app.AddConfigurations();

app.Run();
