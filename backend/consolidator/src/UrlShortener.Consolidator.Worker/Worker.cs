using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace UrlShortener.Consolidator.Worker;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
	// 3 minutes
	private const int _DELAY_IN_MILISECONDS = 180000;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
		var factory = new ConnectionFactory()
		{
			HostName = "localhost",
			Port = 5672,
			VirtualHost = "/",
			UserName = "guest",
			Password = "guest"
		};
		using var connection = await factory.CreateConnectionAsync(
			stoppingToken
		);
		using var channel = await connection.CreateChannelAsync(
			cancellationToken: stoppingToken
		);
		await CreateQueue(channel);
		var consumer = new AsyncEventingBasicConsumer(
			channel
		);
		consumer.ReceivedAsync += async (model, ea) =>
		{
			var body = ea.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);
			Console.WriteLine(message);
		};

		await channel.BasicConsumeAsync(
			queue: "url-shortener.creation",
			autoAck: false,
			consumer,
			stoppingToken
		);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(_DELAY_IN_MILISECONDS, stoppingToken);
        }
    }

	private static async Task CreateQueue(IChannel channel)
	{
		await channel.QueueDeclareAsync(
			queue: "url-shortener.creation",
			durable: false,
			exclusive: false,
			autoDelete: false,
			arguments: null
		);
	}
}
