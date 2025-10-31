using System;
using EventStore.Core.Domains.TimeEntry;
using MediatR;

namespace EventStore.Application.Features.TimeEntry.Register;

public class RegisterTimeEntryMediatorCommand : IRequest<RegisterTimeEntryMediatorCommandResponse>
{
    public DateTimeOffset From { get; set; }
    public DateTimeOffset Until { get; set; }
    public string UserId { get; set; }
    public string ProjectId { get; set; }
    public ActivityTypes ActivityType { get; set; }
    public string Comment { get; set; }
}