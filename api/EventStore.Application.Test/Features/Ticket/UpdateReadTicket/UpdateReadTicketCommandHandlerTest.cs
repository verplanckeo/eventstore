using System.Threading.Tasks;
using EventStore.Application.Features.Ticket;
using EventStore.Application.Features.Ticket.UpdateReadTicket;
using EventStore.Application.Repositories.Ticket;
using EventStore.Core.Domains.Ticket.Option;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.Ticket.UpdateReadTicket
{
    [TestClass]
    public class UpdateReadTicketCommandHandlerTest
    {
        private IReadTicketRepository _repository;

        private UpdateReadTicketCommandHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _repository = A.Fake<IReadTicketRepository>();
            _sut = new UpdateReadTicketCommandHandler(_repository);
        }

        [TestMethod]
        public async Task UpdateReadTicket_Success()
        {
            // Arrange
            var request = UpdateReadTicketCommand.CreateCommand("root", "376564a6-64f0-4485-a98b-dbc092c01cf4", "username", "title", TicketState.New,
                TicketPriority.Low, TicketType.Bug, 1);

            // Act
            await _sut.Handle(request, default);

            // Assert
            A.CallTo(() =>
                    _repository.SaveOrUpdateTicketAsync(
                        A<ReadTicketModel>.That.Matches(u => 
                            u.AggregateRootId == request.AggregateRootId
                            && u.UserId.ToString() == request.UserId
                            && u.UserName == request.UserName
                            && u.Title == request.Title
                            && u.Version == request.Version 
                            && u.TicketPriority == request.TicketPriority
                            && u.TicketState == request.TicketState
                            && u.TicketType == request.TicketType
                            ), default))
                .MustHaveHappenedOnceExactly();
        }
    }
}