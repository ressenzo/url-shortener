using UrlShortener.Consolidator.Worker;
using UrlShortener.Consolidator.Infrastructure;
using UrlShortener.Consolidator.Application;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
