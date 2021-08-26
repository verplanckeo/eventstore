using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventStore.Api.Features.Ticket.Models
{
    /// <summary>
    /// Priority for a given ticket (i.e. Critical)
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
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