namespace EventStore.Infrastructure.Http
{
    public interface IEventStoreHttpContext
    {
        string CorrelationId { get; }

        string SessionId { get; }
    }
    public class EventStoreHttpContext : IEventStoreHttpContext
    {
        public string CorrelationId { get; }
        public string SessionId { get; }

        public EventStoreHttpContext()
        {
            
        }
    }
}