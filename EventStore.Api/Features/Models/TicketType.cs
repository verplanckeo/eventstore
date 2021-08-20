namespace EventStore.Api.Features.Models
{
    /// <summary>
    /// Type of ticket (i.e. Bug)
    /// </summary>
    public enum TicketType
    {
        /// <summary>
        /// Functionality that is not working according to the specs and has already been released into production.
        /// </summary>
        Bug = 0,
        /// <summary>
        /// Functionality that is not working according to the specs and has not yet been released.
        /// </summary>
        Defect = 1,
        /// <summary>
        /// New functionality that was not part of the original specs.
        /// </summary>
        ProductBacklogItem = 2
    }
}