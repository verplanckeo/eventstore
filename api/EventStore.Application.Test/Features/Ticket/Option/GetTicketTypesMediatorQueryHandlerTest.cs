using System.Linq;
using System.Threading.Tasks;
using EventStore.Application.Features.Ticket.Option;
using EventStore.Core.Domains.Ticket.Option;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.Ticket.Option
{
    [TestClass]
    public class GetTicketTypesMediatorQueryHandlerTest
    {
        private GetTicketTypesMediatorQueryHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new GetTicketTypesMediatorQueryHandler();
        }

        [TestMethod]
        public async Task GetTicketTypes_Success()
        {
            // Arrange


            // Act
            var result = await _sut.Handle(GetTicketTypesMediatorQuery.CreateQuery(), default);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Types.Contains(TicketType.Bug.ToString()));
            Assert.IsTrue(result.Types.Contains(TicketType.Defect.ToString()));
            Assert.IsTrue(result.Types.Contains(TicketType.ProductBacklogItem.ToString()));
        }
    }
}