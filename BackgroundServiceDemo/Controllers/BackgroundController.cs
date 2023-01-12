using BackgroundServiceDemo.Models;
using BackgroundServiceDemo.Services.InterfacesAndServices;
using BackgroundServiceDemo.ServiceSecond.InterfacesAndServices;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundServiceDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class BackgroundController : ControllerBase
{
    private const int WaitingPeriodMinutes = 1;
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly IBackgroundTaskQueueSecond _taskQueueSecond;



    public BackgroundController(IBackgroundTaskQueue taskQueue, IBackgroundTaskQueueSecond taskQueueSecond)
    {
        _taskQueue = taskQueue;
        _taskQueueSecond = taskQueueSecond;
    }

    [HttpGet("FromChannel")]
    public async Task<string> GetFromChannel()
    {
        var sendTime = DateTime.Now.AddMinutes(WaitingPeriodMinutes);
        var order = new Order(sendTime);
        await _taskQueueSecond.QueueBackgroundWorkItemAsync(order);
        return order.ToString();
    }

    [HttpGet("FromConcurrentQueue")]
    public string GetFromConcurrentQueue()
    {
        var sendTime = DateTime.Now.AddMinutes(WaitingPeriodMinutes);
        var order = new Order(sendTime);
        _taskQueue.EnqueueTaskConcurrentQueue(order);
        return order.ToString();
    }
}