namespace EventStore.Application.Features.Ticket.Register
{
    public class RegisterTicketMediatorCommandResponse
    {
        public string Id { get; set; }

        private RegisterTicketMediatorCommandResponse(string id)
        {
            Id = id;
        }

        public static RegisterTicketMediatorCommandResponse CreateResponse(string id)
        {
            return new RegisterTicketMediatorCommandResponse(id);
        }
    }
}