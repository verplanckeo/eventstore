namespace EventStore.Infrastructure.Infra
{
    /// <summary>
    /// All connection strings are represented using this class
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// Connection string to the eventstore db
        /// </summary>
        public string EventStoreDb { get; set; }    
    }
}