using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Mediator;
using EventStore.Application.Repositories.Ticket;
using MediatR;

namespace EventStore.Application.Features.Ticket.Register
{
    public class RegisterTicketMediatorCommandHandler : IRequestHandler<RegisterTicketMediatorCommand, RegisterTicketMediatorCommandResponse>
    {
        private readonly ITicketRepository _repository;
        private readonly IMediatorFactory _mediatorFactory;

        public RegisterTicketMediatorCommandHandler(IMediatorFactory mediatorFactory, ITicketRepository repository)
        {
            _mediatorFactory = mediatorFactory;
            _repository = repository;
        }

        public async Task<RegisterTicketMediatorCommandResponse> Handle(RegisterTicketMediatorCommand request, CancellationToken cancellationToken)
        {
            var domainTicketUser = new Core.Domains.Ticket.User(request.UserId, request.UserName);
            var domainTicket = Core.Domains.Ticket.Ticket.CreateNewTicket(request.Title, request.Description, request.TicketType, request.TicketPriority, domainTicketUser);

            var id = await _repository.SaveTicketAsync(domainTicket, cancellationToken);

            return RegisterTicketMediatorCommandResponse.CreateResponse(id.ToString());
        }
    }
}