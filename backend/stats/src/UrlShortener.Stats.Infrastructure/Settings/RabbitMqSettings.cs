namespace UrlShortener.Stats.Infrastructure.Settings;

public record RabbitMqSettings
{
	public string HostName { get; init; } = default!;

	public int Port
	{
		get; init;
	}

	public string VirtualHost { get; init; } = default!;

	public string UserName { get; init; } = default!;

	public string Password { get; init; } = default!;
}
