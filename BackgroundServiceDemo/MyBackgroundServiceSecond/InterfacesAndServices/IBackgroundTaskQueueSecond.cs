using BackgroundServiceDemo.Models;

namespace BackgroundServiceDemo.MyBackgroundServiceSecond.InterfacesAndServices;

public interface IBackgroundTaskQueueSecond
{
    ValueTask QueueBackgroundWorkItemAsync(Order order);
    ValueTask<Order> DequeueAsync(CancellationToken cancellationToken);
}