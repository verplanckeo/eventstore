using EventStore.Application.Features.Ticket.Register;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.Ticket;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventStore.Application.Test.Features.Ticket.Register
{
    [TestClass]
    public class RegisterTicketMediatorCommandValidatorTest
    {
        private RegisterTicketMediatorCommandValidator _sut;

        [TestInitialize]
        public void Initialize()
        {
            
            _sut = new RegisterTicketMediatorCommandValidator();
        }

        public void Given_A_Ticket__Validation_Ok()
        {

        }
    }
}