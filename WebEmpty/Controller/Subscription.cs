using HotChocolate.Authorization;
using HotChocolate.Subscriptions;

namespace WebEmpty.Controller;

public interface IPeopleCountService
{
    Task UpdateDeviceAsync(int count);
}
public class PeopleCountService:IPeopleCountService
{
    private readonly ITopicEventSender _eventSender;

    public PeopleCountService(ITopicEventSender eventSender)
    {
        _eventSender = eventSender;
    }
    
    public async Task UpdateDeviceAsync(int count)
    {
        // 这里你可以发布事件
        await _eventSender.SendAsync(nameof(Subscription.PeopleCount), count);
        await Console.Out.WriteLineAsync($"数量:{count}");
    }
}

[Authorize]
public class Subscription
{
    [Subscribe]
    [Topic]
    public int PeopleCount([EventMessage]int count)
    {
        return count;
    }
}