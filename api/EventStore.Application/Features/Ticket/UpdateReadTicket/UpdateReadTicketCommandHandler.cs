using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Repositories.Ticket;
using MediatR;

namespace EventStore.Application.Features.Ticket.UpdateReadTicket
{
    public class UpdateReadTicketCommandHandler : IRequestHandler<UpdateReadTicketCommand>
    {
        private readonly IReadTicketRepository _repository;

        public UpdateReadTicketCommandHandler(IReadTicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateReadTicketCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.SaveOrUpdateTicketAsync(
                ReadTicketModel.CreateModel(
                    request.AggregateRootId, 
                    request.UserId, 
                    request.UserName,
                    request.Title, 
                    request.TicketState, 
                    request.TicketPriority, 
                    request.TicketType, 
                    request.Version), 
                cancellationToken);

            if (string.IsNullOrEmpty(result))
            {
                //we could not save or update the model - gracefully handle this.
            }
        }
    }
}