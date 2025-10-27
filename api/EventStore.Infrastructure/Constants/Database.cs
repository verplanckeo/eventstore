namespace EventStore.Infrastructure.Constants
{
    public class Database
    {
        public const string ConnectionStringName = "EventStoreDb";

        public const string SchemaWrite = "write";

        public const string SchemaRead = "read";

        #region Tables
        
        public const string TableEventStore = "EventStore";

        public const string TableReadUser = "User";

        public const string TableReadTicket = "Ticket";
        
        public const string TableReadProject = "Project";
        
        public const string TableReadTimeEntry = "TimeEntry";
        
        #endregion
    }
}