namespace BackgroundServiceDemo.Models;

public class Order
{
    public int Id { get; private set; }
    public DateTime WaitingPeriodTime { get; private set; }
    public DateTime Now { get; } = DateTime.Now;

    public Order(DateTime waitingPeriodTime)
    {
        WaitingPeriodTime = waitingPeriodTime.AddSeconds(-50);
        Id = IdClass.IdCount;
        IdClass.IdCount++;
    }

    public Order()
    {
        
    }
    
    public override string ToString()
    {
        return $"Id = {Id} \nNow = {Now:dd/MM/yyyy HH:mm:ss} \nWaitingPeriodTime = {WaitingPeriodTime:dd/MM/yyyy HH:mm:ss}";
    }
}
public static class IdClass
{
    public static int IdCount { get; set; } = 1;
}