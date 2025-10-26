using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventStore.Api.Features.Ticket.Models
{
    /// <summary>
    /// Which type you wish to get.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketOptionType
    {
        /// <summary>
        /// Ask for all available ticket priorities (i.e. Critical, low ...).
        /// </summary>
        TicketPriority = 0,
        /// <summary>
        /// Ask for all available ticket states (i.e. New, Closed ...).
        /// </summary>
        TicketState = 1,
        /// <summary>
        /// Ask for all available ticket types (i.e. Bug, Defect ...).
        /// </summary>
        TicketType = 2
    }
}