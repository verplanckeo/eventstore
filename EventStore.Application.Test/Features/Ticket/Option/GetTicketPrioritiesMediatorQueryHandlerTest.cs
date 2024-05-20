using System.Linq;
using System.Threading.Tasks;
using EventStore.Application.Features.Ticket.Option;
using EventStore.Core.Domains.Ticket.Option;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.Ticket.Option
{
    [TestClass]
    public class GetTicketPrioritiesMediatorQueryHandlerTest
    {
        private GetTicketPrioritiesMediatorQueryHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new GetTicketPrioritiesMediatorQueryHandler();
        }

        [TestMethod]
        public async Task GetTicketPriorities_Success()
        {
            // Arrange


            // Act
            var result = await _sut.Handle(GetTicketPrioritiesMediatorQuery.CreateQuery(), default);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Priorities.Contains(TicketPriority.Low.ToString()));
            Assert.IsTrue(result.Priorities.Contains(TicketPriority.Medium.ToString()));
            Assert.IsTrue(result.Priorities.Contains(TicketPriority.High.ToString()));
            Assert.IsTrue(result.Priorities.Contains(TicketPriority.Critical.ToString()));
        }
    }
}