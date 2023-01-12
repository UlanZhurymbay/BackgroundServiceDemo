using BackgroundServiceDemo.Models;

namespace BackgroundServiceDemo.Services.InterfacesAndServices;

public interface IBackgroundTaskQueue
{
    public void EnqueueTaskConcurrentQueue(Order order);
    Task<Order> DequeueConcurrentQueue(CancellationToken token);
}