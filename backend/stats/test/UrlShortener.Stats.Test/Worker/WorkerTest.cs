using Moq.AutoMock;
using UrlShortener.Stats.Application.Repositories;

namespace UrlShortener.Stats.Test.Worker;

public class WorkerTest
{
	private readonly Stats.Worker.Worker _worker;
	private readonly Mock<IMessagingConsumerRepository> _messagingRepository;

	public WorkerTest()
	{
		var autoMock = new AutoMocker();
		_worker = autoMock.CreateInstance<Stats.Worker.Worker>();
		_messagingRepository = autoMock.GetMock<IMessagingConsumerRepository>();

		_messagingRepository
			.Setup(x => x.Start(It.IsAny<CancellationToken>()))
			.Returns(Task.CompletedTask);
	}

	[Fact]
	public async Task ExecuteAsync_ShouldStartMessagingRepository()
	{
		// Arrange
		using var cts = new CancellationTokenSource();

		// Act
		await _worker.StartAsync(cts.Token);
		await Task.Delay(50);
		await _worker.StopAsync(CancellationToken.None);

		// Assert
		_messagingRepository.Verify(
			x => x.Start(It.IsAny<CancellationToken>()),
			Times.Once
		);
	}

	[Fact]
	public async Task ExecuteAsync_ShouldStopWhenCancelled()
	{
		// Arrange
		using var cts = new CancellationTokenSource();
		await _worker.StartAsync(cts.Token);

		// Act
		cts.Cancel();
		await _worker.StopAsync(CancellationToken.None);

		// Assert
		cts.IsCancellationRequested.ShouldBeTrue();
	}
}
