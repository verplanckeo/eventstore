using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.User.DomainEvents
{
    public class UserRegisteredDomainEvent : DomainEvent
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public UserRegisteredDomainEvent(string userId, string userName, string firstName, string lastName)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}