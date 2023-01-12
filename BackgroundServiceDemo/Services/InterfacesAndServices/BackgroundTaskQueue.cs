using System.Collections.Concurrent;
using BackgroundServiceDemo.Models;

namespace BackgroundServiceDemo.Services.InterfacesAndServices;

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly ConcurrentQueue<Order> _items2 = new();
    private readonly SemaphoreSlim _signal = new(0);

    public void EnqueueTaskConcurrentQueue(Order order)
    {
        if (order is null)
            throw new ArgumentNullException(nameof(order));
        _items2.Enqueue(order);
        _signal.Release();
    }
    public async Task<Order> DequeueConcurrentQueue(CancellationToken token)
    {
        await _signal.WaitAsync(token);
        _items2.TryDequeue(out var order);
        return order;
    }
    
}