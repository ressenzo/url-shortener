using UrlShortener.Generator.Api.Filters;
using UrlShortener.Generator.Application;
using UrlShortener.Generator.Domain;
using UrlShortener.Generator.Infrastructure;

const string _CORS_NAME = "All";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(x =>
{
	x.Filters.Add(typeof(ExceptionFilter));
});
builder.Services.AddCors(x =>
{
	x.AddPolicy(_CORS_NAME,
		builder =>
		{
			builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer();
builder.Services.AddDomainLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(_CORS_NAME);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
