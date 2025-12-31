namespace UrlShortener.Redirector.Infrastructure.Settings;

public record RabbitMqSettings
{
	public string HostName { get; set; } = default!;

	public int Port
	{
		get; set;
	}

	public string VirtualHost { get; set; } = default!;

	public string UserName { get; set; } = default!;

	public string Password { get; set; } = default!;
}
