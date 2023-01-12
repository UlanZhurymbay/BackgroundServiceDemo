using BackgroundServiceDemo.Models;

namespace BackgroundServiceDemo.ServiceSecond.InterfacesAndServices;

public interface IBackgroundTaskQueueSecond
{
    ValueTask QueueBackgroundWorkItemAsync(Order order);
    ValueTask<Order> DequeueAsync(CancellationToken cancellationToken);
}