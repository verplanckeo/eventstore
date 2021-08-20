namespace EventStore.Core.Domains.Ticket.Option
{
    public enum TicketState
    {
        New = 0,
        InProgress = 1,
        Resolved = 2,
        Done = 3,
        Closed = 4,
        Removed = 5,
        Reopen = 6
    }
}