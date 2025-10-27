namespace EventStore.Application.Features.TimeEntry.Remove;

public class RemoveTimeEntryMediatorCommandResponse
{
    public string TimeEntryId { get; private set; }

    private RemoveTimeEntryMediatorCommandResponse(string timeEntryId)
    {
        TimeEntryId = timeEntryId;
    }

    public static RemoveTimeEntryMediatorCommandResponse CreateResponse(string timeEntryId)
    {
        return new RemoveTimeEntryMediatorCommandResponse(timeEntryId);
    }
}