using System.Threading.Channels;
using BackgroundServiceDemo.Models;

namespace BackgroundServiceDemo.MyBackgroundServiceSecond.InterfacesAndServices;

public class BackgroundTaskQueueSecond : IBackgroundTaskQueueSecond
{
    private readonly Channel<Order> _queue;
    private const int CapacityChannel = 100;

    public BackgroundTaskQueueSecond()
    {
        var options = new BoundedChannelOptions(CapacityChannel)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _queue = Channel.CreateBounded<Order>(options);
    }

    public async ValueTask QueueBackgroundWorkItemAsync(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        await _queue.Writer.WriteAsync(order);
    }

    public async ValueTask<Order> DequeueAsync(CancellationToken cancellationToken)
    {
        var order = await _queue.Reader.ReadAsync(cancellationToken);
        return order;
    }
}