namespace EventStore.Core.DddSeedwork
{
    /// <summary>
    /// Interface to identify a class as an EntityId
    /// </summary>
    public interface IEntityId
    {
        string ToString();
    }
}