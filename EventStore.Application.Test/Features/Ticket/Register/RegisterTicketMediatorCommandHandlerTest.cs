using EventStore.Application.Features.Ticket.Register;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.Ticket;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.Ticket.Register
{
    [TestClass]
    public class RegisterTicketMediatorCommandHandlerTest
    {
        private IMediatorFactory _mediatorFactory;
        private ITicketRepository _repository;

        private RegisterTicketMediatorCommandHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _mediatorFactory = A.Fake<IMediatorFactory>();
            _repository = A.Fake<ITicketRepository>();

            _sut = new RegisterTicketMediatorCommandHandler(_mediatorFactory, _repository);
        }
    }
}