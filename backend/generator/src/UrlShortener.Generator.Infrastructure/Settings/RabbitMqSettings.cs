namespace UrlShortener.Generator.Infrastructure.Settings;

public record RabbitMqSettings(
	string HostName,
	int Port,
	string VirtualHost,
	string UserName,
	string Password
);
