using UrlShortener.Consolidator.Application.Repositories;

namespace UrlShortener.Consolidator.Worker;

public class Worker(
	ILogger<Worker> logger,
	IMessagingConsumerRepository messagingRepository) : BackgroundService
{
	// 3 minutes
	private const int _DELAY_IN_MILISECONDS = 180000;

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await messagingRepository.Start(stoppingToken);

		while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(_DELAY_IN_MILISECONDS, stoppingToken);
        }
    }
}
