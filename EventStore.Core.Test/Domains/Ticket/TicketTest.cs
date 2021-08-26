using System.Linq;
using EventStore.Core.Domains.Ticket.DomainEvents;
using EventStore.Core.Domains.Ticket.Option;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Core.Test.Domains.Ticket
{
    [TestClass]
    public class TicketTest
    {
        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void RegisterNewTicket_Ok()
        {
            // Arrange
            var user = new Core.Domains.Ticket.User("17A3A82B-2907-43F6-9D25-7888962D432D", "username");
            var title = "Unit test Ticket";
            var description = "A ticket to verify our business logic";
            var ticketType = TicketType.Bug;
            var ticketPrio = TicketPriority.Low;

            // Act
            var ticket = Core.Domains.Ticket.Ticket.CreateNewTicket(title, description, ticketType, ticketPrio, user);

            // Assert
            Assert.AreEqual(title, ticket.Title);
            Assert.AreEqual(description, ticket.Description);
            Assert.AreEqual(ticketType, ticket.TicketType);
            Assert.AreEqual(ticketPrio, ticket.TicketPriority);
            Assert.AreEqual(user, ticket.User);
            Assert.IsNotNull(ticket.TicketState);
            Assert.AreEqual(TicketState.New, ticket.TicketState.State);
            Assert.AreEqual(1, ticket.DomainEvents.Count);
            Assert.AreEqual(typeof(TicketRegisteredDomainEvent), ticket.DomainEvents.First().GetType());
        }

        [TestMethod]
        public void ChangeTicketState_Ok()
        {
            // Arrange
            var user = new Core.Domains.Ticket.User("17A3A82B-2907-43F6-9D25-7888962D432D", "username");
            var title = "Unit test Ticket";
            var description = "A ticket to verify our business logic";
            var ticketType = TicketType.Bug;
            var ticketPrio = TicketPriority.Low;

            // Act
            var ticket = Core.Domains.Ticket.Ticket.CreateNewTicket(title, description, ticketType, ticketPrio, user);
            ticket.ChangeTicketState(TicketState.InProgress);

            // Assert
            Assert.AreEqual(title, ticket.Title);
            Assert.AreEqual(description, ticket.Description);
            Assert.AreEqual(ticketType, ticket.TicketType);
            Assert.AreEqual(ticketPrio, ticket.TicketPriority);
            Assert.AreEqual(user, ticket.User);
            Assert.IsNotNull(ticket.TicketState);
            Assert.AreEqual(TicketState.InProgress, ticket.TicketState.State);
            Assert.AreEqual(2, ticket.DomainEvents.Count);
            Assert.AreEqual(typeof(TicketRegisteredDomainEvent), ticket.DomainEvents.First().GetType());
            Assert.IsTrue(ticket.DomainEvents.Any(evt => evt.GetType() == typeof(TicketStateChangedDomainEvent)));
        }
    }
}