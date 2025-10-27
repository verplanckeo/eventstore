namespace EventStore.Application.Features.TimeEntry.Update;

public class UpdateTimeEntryMediatorCommandResponse
{
    public string TimeEntryId { get; private set; }

    private UpdateTimeEntryMediatorCommandResponse(string timeEntryId)
    {
        TimeEntryId = timeEntryId;
    }

    public static UpdateTimeEntryMediatorCommandResponse CreateResponse(string timeEntryId)
    {
        return new UpdateTimeEntryMediatorCommandResponse(timeEntryId);
    }
}