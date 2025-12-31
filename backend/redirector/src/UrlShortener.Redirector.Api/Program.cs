using UrlShortener.Redirector.Api.Endpoints;
using UrlShortener.Redirector.Application;
using UrlShortener.Redirector.Infrastructure;

const string _CORS_NAME = "All";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddCors(
	x =>
	{
		x.AddPolicy(
			_CORS_NAME,
			builder =>
			{
				builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			}
		);
	}
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();
app.AddEndpoints();
app.UseCors(_CORS_NAME)
	.UseHttpsRedirection();

app.Run();
