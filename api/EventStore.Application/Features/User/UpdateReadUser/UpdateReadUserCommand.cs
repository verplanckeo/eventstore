using MediatR;

namespace EventStore.Application.Features.User.UpdateReadUser
{
    public class UpdateReadUserCommand : IRequest
    {
        public string AggregateRootId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public int Version { get; set; }

        private UpdateReadUserCommand(string aggregateRootId, string firstName, string lastName, string userName, int version)
        {
            AggregateRootId = aggregateRootId;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Version = version;
        }

        public static UpdateReadUserCommand CreateCommand(string aggregateRootId, string firstName, string lastName, string userName, int version)
        {
            return new UpdateReadUserCommand(aggregateRootId, firstName, lastName, userName, version);
        }
    }
}