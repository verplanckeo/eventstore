using System.Linq;
using System.Threading.Tasks;
using EventStore.Application.Features.Ticket.Option;
using EventStore.Core.Domains.Ticket.Option;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.Ticket.Option
{
    [TestClass]
    public class GetTicketStatesMediatorQueryHandlerTest
    {
        private GetTicketStatesMediatorQueryHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new GetTicketStatesMediatorQueryHandler();
        }

        [TestMethod]
        public async Task GetTicketStates_Success()
        {
            // Arrange


            // Act
            var result = await _sut.Handle(GetTicketStatesMediatorQuery.CreateQuery(), default);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.States.Contains(TicketState.New.ToString()));
        }
    }
}