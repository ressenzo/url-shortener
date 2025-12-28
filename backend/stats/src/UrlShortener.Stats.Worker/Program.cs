using UrlShortener.Stats.Worker;
using UrlShortener.Stats.Infrastructure;
using UrlShortener.Stats.Application;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
