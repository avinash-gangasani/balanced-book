using BalancedBook.NET;
using BalancedBook.NET.Enums;
using BalancedBook.NET.Models;

internal class BackgroundWorkerService : BackgroundService
{
    private readonly ILogger<BackgroundService> logger;
    private readonly WorkerNotifierService notifier;
    public BackgroundWorkerService(ILogger<BackgroundService> _logger, WorkerNotifierService notifier)
    {
        logger = _logger;
        this.notifier = notifier;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            notifier.NotifyJobExecuted(GenerateOrder());
            await Task.Delay(1000);
        }
    }

    private static int _nextOrderId = 1;

    private Order GenerateOrder()
    {
        Random rand = new Random();

        int orderId = Interlocked.Increment(ref _nextOrderId);
        OrderType type = rand.Next(2) == 0 ? OrderType.Buy : OrderType.Sell;
        var price = Math.Round(rand.NextDouble() * 100, 2);
        var quantity = rand.Next(0, 100);

        return new Order
        {
            OrderId = orderId,
            Price = price,
            Quantity = quantity,
            Type = type
        };
    }
}