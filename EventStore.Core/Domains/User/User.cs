using System.Collections.Generic;
using EventStore.Core.DddSeedwork;
using EventStore.Core.Domains.User.DomainEvents;

namespace EventStore.Core.Domains.User
{
    public class User : EventSourcedAggregateRoot<UserId>
    {
        public override UserId Id { get; protected set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Password { get; private set; }
        public Address UserAddress { get; private set; }

        private User() { }

        /// <summary>
        /// When an aggregate has been fetched from db, we call this CTor which will apply all events and increase the Version by 1
        /// </summary>
        /// <param name="events"></param>
        public User(IEnumerable<IDomainEvent> events) : base(events) { }

        public static User CreateNewUser(string userName, string firstName, string lastName, string password)
        {
            var user = new User();

            //This method will first call the "On(event)" method of this particular aggregate followed by adding the event to the list of domain events
            user.Apply(new UserRegisteredDomainEvent(new UserId().ToString(), userName, firstName, lastName, password));

            return user;
        }

        public void ChangeAddress(string street, string city, string zipcode, string country)
        {
            Apply(new AddressChangedDomainEvent(street, city, zipcode, country));
        }

        //To know how this method is called, check ...\EventStore.Core\DddSeedwork\EventSourcedAggregateRoot.cs
        //Using dynamic we call the "On(event)" method
        public void On(UserRegisteredDomainEvent evt)
        {
            Id = new UserId(evt.UserId);
            UserName = evt.UserName;
            FirstName = evt.FirstName;
            LastName = evt.LastName;
            Password = evt.Password;
        }

        public void On(AddressChangedDomainEvent evt)
        {
            UserAddress = new Address
            {
                Street = evt.Street,
                City = evt.City,
                ZipCode = evt.ZipCode,
                Country = evt.Country
            };
        }
    }
}