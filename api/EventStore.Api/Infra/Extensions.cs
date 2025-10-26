using System;
using System.Linq;
using System.Security.Claims;
using EventStore.Core.Domains.Ticket.Option;

namespace EventStore.Api.Infra
{
    /// <summary>
    /// Extension methods for help.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert domain enum TicketState to Api layer TicketState.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Features.Ticket.Models.TicketState ToApiTicketState(this TicketState value)
        {
            return value switch
            {
                TicketState.New => Features.Ticket.Models.TicketState.New,
                TicketState.InProgress => Features.Ticket.Models.TicketState.InProgress,
                TicketState.Resolved => Features.Ticket.Models.TicketState.Resolved,
                TicketState.Done => Features.Ticket.Models.TicketState.Done,
                TicketState.Closed => Features.Ticket.Models.TicketState.Closed,
                TicketState.Removed => Features.Ticket.Models.TicketState.Removed,
                TicketState.Reopen => Features.Ticket.Models.TicketState.Reopened,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        /// <summary>
        /// Convert domain enum TicketState to Api layer TicketState.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TicketState ToDomainTicketState(this Features.Ticket.Models.TicketState value)
        {
            return value switch
            {
                Features.Ticket.Models.TicketState.New => TicketState.New,
                Features.Ticket.Models.TicketState.InProgress => TicketState.InProgress,
                Features.Ticket.Models.TicketState.Resolved => TicketState.Resolved,
                Features.Ticket.Models.TicketState.Done => TicketState.Done,
                Features.Ticket.Models.TicketState.Closed => TicketState.Closed,
                Features.Ticket.Models.TicketState.Removed => TicketState.Removed,
                Features.Ticket.Models.TicketState.Reopened => TicketState.Reopen,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        /// <summary>
        /// Convert domain enum TicketPriority to Api layer TicketPriority.
        /// </summary>
        /// <param name="value"><see cref="TicketPriority"/></param>
        /// <returns><see cref="Features.Ticket.Models.TicketPriority"/></returns>
        public static Features.Ticket.Models.TicketPriority ToApiTicketPriority(this TicketPriority value)
        {
            return value switch
            {
                TicketPriority.Low => Features.Ticket.Models.TicketPriority.Low,
                TicketPriority.Medium => Features.Ticket.Models.TicketPriority.Medium,
                TicketPriority.High => Features.Ticket.Models.TicketPriority.High,
                TicketPriority.Critical => Features.Ticket.Models.TicketPriority.Critical,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown Ticket priority type.")
            };
        }

        /// <summary>
        /// Convert domain enum TicketPriority to Api layer TicketPriority.
        /// </summary>
        /// <param name="value"><see cref="TicketPriority"/></param>
        /// <returns><see cref="Features.Ticket.Models.TicketPriority"/></returns>
        public static TicketPriority ToDomainTicketPriority(this Features.Ticket.Models.TicketPriority value)
        {
            return value switch
            {
                Features.Ticket.Models.TicketPriority.Low => TicketPriority.Low,
                Features.Ticket.Models.TicketPriority.Medium => TicketPriority.Medium,
                Features.Ticket.Models.TicketPriority.High => TicketPriority.High,
                Features.Ticket.Models.TicketPriority.Critical => TicketPriority.Critical,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown Ticket priority type.")
            };
        }

        /// <summary>
        /// Convert domain enum TicketBug to Api layer TicketBug
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Features.Ticket.Models.TicketType ToApiTicketType(this TicketType value)
        {
            return value switch
            {
                TicketType.Bug => Features.Ticket.Models.TicketType.Bug,
                TicketType.Defect => Features.Ticket.Models.TicketType.Defect,
                TicketType.ProductBacklogItem => Features.Ticket.Models.TicketType.ProductBacklogItem,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown Ticket bug type.")
            };
        }

        /// <summary>
        /// Convert domain enum TicketBug to Api layer TicketBug
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TicketType ToDomainTicketType(this Features.Ticket.Models.TicketType value)
        {
            return value switch
            {
                Features.Ticket.Models.TicketType.Bug => TicketType.Bug,
                Features.Ticket.Models.TicketType.Defect => TicketType.Defect,
                Features.Ticket.Models.TicketType.ProductBacklogItem => TicketType.ProductBacklogItem,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown Ticket bug type.")
            };
        }

        /// <summary>
        /// Get a value from the principal.
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string GetClaimsValue(this ClaimsPrincipal principal, string claimType)
        {
            return principal.Claims.First(c => c.Type == claimType).Value;
        }
    }
}