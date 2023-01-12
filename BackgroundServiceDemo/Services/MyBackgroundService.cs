using BackgroundServiceDemo.Models;
using BackgroundServiceDemo.Services.InterfacesAndServices;

namespace BackgroundServiceDemo.Services;

public class MyBackgroundService : BackgroundService
{
    private readonly IBackgroundTaskQueue _backgroundQueue;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    public MyBackgroundService(IBackgroundTaskQueue backgroundQueue, IServiceScopeFactory serviceScopeFactory)
    {
        _backgroundQueue = backgroundQueue;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var order = await _backgroundQueue.DequeueConcurrentQueue(stoppingToken);

            while (order.WaitingPeriodTime > DateTime.Now)
            {
                await DoWork(order, stoppingToken);
            }

            Console.WriteLine("Id {0} Completed!", order.Id);
        }
    }

    private async Task DoWork(Order order, CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Id = {order.Id} {order.WaitingPeriodTime:T} {DateTime.Now:T}");
        Console.WriteLine("--------------------------------------------");
        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
    }
}