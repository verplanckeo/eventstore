using MediatR;

namespace EventStore.Application.Features.User.UpdateReadUser
{
    public class UpdateReadUserCommand : IRequest
    {
        public string AggregateRootId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        private UpdateReadUserCommand(string aggregateRootId, string firstName, string lastName)
        {
            AggregateRootId = aggregateRootId;
            FirstName = firstName;
            LastName = lastName;
        }

        public static UpdateReadUserCommand CreateCommand(string aggregateRootId, string firstName, string lastName)
        {
            return new UpdateReadUserCommand(aggregateRootId, firstName, lastName);
        }
    }
}