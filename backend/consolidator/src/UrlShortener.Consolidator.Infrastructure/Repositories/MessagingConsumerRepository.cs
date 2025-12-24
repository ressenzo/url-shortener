using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UrlShortener.Consolidator.Application.Repositories;
using UrlShortener.Consolidator.Application.UseCases.SaveUrl;
using UrlShortener.Consolidator.Infrastructure.Settings;

namespace UrlShortener.Consolidator.Infrastructure.Repositories;

internal sealed class MessagingConsumerRepository :
	IMessagingConsumerRepository,
	IAsyncDisposable
{
	private const string _QUEUE_NAME = "url-shortener.creation";

	private readonly ILogger<MessagingConsumerRepository> _logger;
	private readonly ConnectionFactory _factory;
	private IChannel? _channel;
	private IConnection? _connection;
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public MessagingConsumerRepository(
		ILogger<MessagingConsumerRepository> logger,
		IOptions<RabbitMqSettings> options,
		IServiceScopeFactory serviceScopeFactory
	)
	{
		_logger = logger;
		
		var settings = options.Value;
		_factory = new ConnectionFactory()
		{
			HostName = settings.HostName,
			VirtualHost = settings.VirtualHost,
			Port = settings.Port,
			UserName = settings.UserName,
			Password = settings.Password
		};
		
		_serviceScopeFactory = serviceScopeFactory;
	}

	public async Task Start(CancellationToken cancellationToken)
	{
		_connection = await _factory.CreateConnectionAsync(
			cancellationToken
		);
		_channel = await _connection.CreateChannelAsync(
			cancellationToken: cancellationToken
		);
		ArgumentNullException.ThrowIfNull(_connection);
		ArgumentNullException.ThrowIfNull(_channel);
		await CreateQueue(cancellationToken);
		await CreateConsumer(cancellationToken);
	}

	private async Task CreateConsumer(CancellationToken cancellationToken)
	{
		var consumer = new AsyncEventingBasicConsumer(_channel!);

		consumer.ReceivedAsync += async (_, ea) =>
		{
			try
			{
				var result = await SaveUrl(ea, cancellationToken);
				if (result)
					await _channel!.BasicAckAsync(
						ea.DeliveryTag,
						multiple: false,
						cancellationToken
					);
				else
					await _channel!.BasicRejectAsync(
						ea.DeliveryTag,
						requeue: false,
						cancellationToken
					);
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"{Message}",
					ex.Message
				);
				await _channel!.BasicRejectAsync(
					ea.DeliveryTag,
					requeue: false,
					cancellationToken
				);
			}
		};
		await SetConsumer(consumer, cancellationToken);
	}

	private async Task<bool> SaveUrl(
		BasicDeliverEventArgs ea,
		CancellationToken cancellationToken
	)
	{
		var message = Encoding.UTF8.GetString(
			ea.Body.ToArray()
		);
		var request = JsonSerializer
			.Deserialize<SaveUrlRequest>(message);
		if (request is null)
			return false;
		var scope = _serviceScopeFactory.CreateScope();
		var saveUrlUseCase = scope.ServiceProvider
			.GetRequiredService<ISaveUrlUseCase>();
		return await saveUrlUseCase.SaveUrl(
			request,
			cancellationToken
		);
	}

	private async Task CreateQueue(CancellationToken cancellationToken) =>
		await _channel!.QueueDeclareAsync(
			queue: _QUEUE_NAME,
			durable: true,
			exclusive: false,
			autoDelete: false,
			arguments: null,
			cancellationToken: cancellationToken
		);

	private async Task SetConsumer(
		AsyncEventingBasicConsumer consumer,
		CancellationToken cancellationToken
	) =>
		await _channel!.BasicConsumeAsync(
			queue: _QUEUE_NAME,
			autoAck: false,
			consumer: consumer,
			cancellationToken
		);

	public async ValueTask DisposeAsync()
	{
		if (_channel is not null)
		{
			await _channel.CloseAsync();
			await _channel.DisposeAsync();
		}

		if (_connection is not null)
		{
			await _connection.CloseAsync();
			await _connection.DisposeAsync();
		}
	}
}
