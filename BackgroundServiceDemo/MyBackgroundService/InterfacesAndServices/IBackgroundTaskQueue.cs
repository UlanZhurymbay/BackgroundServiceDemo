using BackgroundServiceDemo.Models;

namespace BackgroundServiceDemo.MyBackgroundService.InterfacesAndServices;

public interface IBackgroundTaskQueue
{
    public void EnqueueTaskConcurrentQueue(Order order);
    Task<Order> DequeueConcurrentQueue(CancellationToken token);
}