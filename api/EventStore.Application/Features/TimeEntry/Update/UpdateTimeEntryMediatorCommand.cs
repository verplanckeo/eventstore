using System;
using EventStore.Core.Domains.TimeEntry;
using MediatR;

namespace EventStore.Application.Features.TimeEntry.Update;

public class UpdateTimeEntryMediatorCommand : IRequest<UpdateTimeEntryMediatorCommandResponse>
{
    public string TimeEntryId { get; set; }
    public DateTime From { get; set; }
    public DateTime Until { get; set; }
    public string UserId { get; set; }
    public string ProjectId { get; set; }
    public ActivityTypes ActivityType { get; set; }
    public string Comment { get; set; }
}