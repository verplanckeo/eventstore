using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventStore.Api.Features.Ticket.Models
{
    /// <summary>
    /// Type of ticket (i.e. Bug)
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
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