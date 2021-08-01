namespace EventStore.Application.Features.User
{
    /// <summary>
    /// Basic representation of a user record
    /// </summary>
    public class ReadUserModel
    {
        public string AggregateRootId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string UserName { get; private set; }

        public int Version { get; private set; }


        //TODO: Rework default constructor - for now it's use only used for unit tests
        public ReadUserModel() { }

        private ReadUserModel(string aggregateRootId, string firstName, string lastName, string userName, int version)
        {
            AggregateRootId = aggregateRootId;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Version = version;
        }

        public void ChangeUserModel(ReadUserModel updated)
        {
            FirstName = updated.FirstName;
            LastName = updated.LastName;
            UserName = updated.UserName;
            Version++;
        }

        public static ReadUserModel CreateNewReadUser(string aggregateRootId, string firstName, string lastName, string userName, int version)
        {
            return new ReadUserModel(aggregateRootId, firstName, lastName, userName, version);
        }
    }
}