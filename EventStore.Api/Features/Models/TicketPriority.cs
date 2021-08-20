namespace EventStore.Api.Features.Models
{
    /// <summary>
    /// Priority for a given ticket (i.e. Critical)
    /// </summary>
    public enum TicketPriority
    {
        /// <summary>
        /// Low priority
        /// </summary>
        Low = 0,
        /// <summary>
        /// Medium priority
        /// </summary>
        Medium = 1,
        /// <summary>
        /// High priority
        /// </summary>
        High = 2,
        /// <summary>
        /// Business critical
        /// </summary>
        Critical = 3
    }
}