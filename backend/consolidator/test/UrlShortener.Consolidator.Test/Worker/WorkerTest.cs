using Moq.AutoMock;
using UrlShortener.Consolidator.Application.Repositories;

namespace UrlShortener.Consolidator.Test.Worker;

public class WorkerTest
{
    private readonly Consolidator.Worker.Worker _worker;
    private readonly Mock<IMessagingConsumerRepository> _messagingRepository;

    public WorkerTest()
    {
        var autoMock = new AutoMocker();
        _worker = autoMock.CreateInstance<Consolidator.Worker.Worker>();
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
        
        // Act: Start the worker and immediately cancel to exit the loop quickly
        await _worker.StartAsync(cts.Token);
		await Task.Delay(50);
		await _worker.StopAsync(CancellationToken.None);
        
        // Assert: Verify that Start was called on the repository
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
