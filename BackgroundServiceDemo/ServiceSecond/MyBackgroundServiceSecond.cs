using System.Diagnostics.CodeAnalysis;
using BackgroundServiceDemo.Models;
using BackgroundServiceDemo.ServiceSecond.InterfacesAndServices;

namespace BackgroundServiceDemo.ServiceSecond;

public class MyBackgroundServiceSecond : BackgroundService
{
    private readonly IBackgroundTaskQueueSecond _backgroundQueue;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    public MyBackgroundServiceSecond(IBackgroundTaskQueueSecond backgroundQueue,
        IServiceScopeFactory serviceScopeFactory)
    {
        _backgroundQueue = backgroundQueue;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var order = await _backgroundQueue.DequeueAsync(stoppingToken);
            try
            {
                while (order.WaitingPeriodTime > DateTime.Now)
                {
                    await DoWork(order, stoppingToken);
                }
            }
            catch
            {
            }
            finally
            {
                Console.WriteLine("Id {0} Completed!", order.Id);
            }
        }
    }

    private async ValueTask DoWork(Order order, CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Id = {order.Id} {order.WaitingPeriodTime:T} {DateTime.Now:T}");
        Console.WriteLine("--------------------------------------------");
        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
    }
}