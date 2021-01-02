namespace EventStore.Application.Mediator
{
    public interface IMediatorFactory
    {
        IMediatorScope CreateScope();
    }
}