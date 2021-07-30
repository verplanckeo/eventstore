namespace EventStore.Application.Features.User
{
    /// <summary>
    /// Basic representation of a user record
    /// </summary>
    public class ReadUserModel
    {
        public string AggregateRootId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public int Version { get; set; }

        private ReadUserModel(string aggregateRootId, string firstName, string lastName, string userName, int version)
        {
            AggregateRootId = aggregateRootId;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Version = version;
        }

        public static ReadUserModel CreateNewReadUser(string aggregateRootId, string firstName, string lastName, string userName, int version)
        {
            return new ReadUserModel(aggregateRootId, firstName, lastName, userName, version);
        }
    }
}